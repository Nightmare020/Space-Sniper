using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class NPC : NPCProperties
{
    [Header("NPC Properties")]
    public Properties properties;

    [Header("Visuals")]
    public GameObject visualsGO;

    public NPCSprite[] sprites;
    public NPCOutline[] outlines;

    //Internal Variables
    private Dictionary<string, SpriteRenderer> nameAndSprites = new();
    private Dictionary<string, GameObject> nameAndOutlines = new();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public IEnumerator NPCs()
    {
        yield return new WaitForSeconds(1f);
        Properties intermediate;
        do
        {
            intermediate = SetNPC();
        } while (intermediate.Equals(GameManager.instance.GetTargetProperties()));

        properties = intermediate;
        SetVisuals(properties);
    }

    public void SetTargetNPC()
    {
        properties = GameManager.instance.GetTargetProperties();
        //GetComponent<MeshRenderer>().enabled = true;
        SetVisuals(properties);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetVisuals(Properties properties)
    {
        sprites = visualsGO.GetComponentsInChildren<NPCSprite>(true);
        outlines = visualsGO.GetComponentsInChildren<NPCOutline>(true);
        foreach (var sprite in sprites)
        {
            nameAndSprites.Add(sprite.transform.name, sprite.GetComponent<SpriteRenderer>());
        }
        foreach (var outline in outlines)
        {
            nameAndOutlines.Add(outline.transform.name, outline.gameObject);
        }

        if (properties.sex == Sex.Male)
        {
            if(nameAndOutlines.ContainsKey("MaleOutline")) nameAndOutlines["MaleOutline"].gameObject.SetActive(true);
        }else if(properties.sex == Sex.Female)
        {
            if(nameAndOutlines.ContainsKey("FemaleOutline")) nameAndOutlines["FemaleOutline"].gameObject.SetActive(true);
        }

        string name = properties.sex.ToString() + properties.race.ToString() + properties.headHair.ToString() + properties.facialHair.ToString();
        if (nameAndSprites.ContainsKey(name))
            visualsGO.GetComponent<SpriteRenderer>().sprite = nameAndSprites[name].sprite;
        else
            Debug.LogError(name + " doesnt exist in the dictionary");
    }
}
