using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NPC : NPCProperties
{
    [Header("NPC Properties")]
    public Properties properties;

    [Header("Debug Txt")]
    public TextMeshProUGUI propertyDebugTxt;

    [Header("Visual Categories")]
    public GameObject facialHair;
    public GameObject race;
    public GameObject headHair;
    public GameObject sex;
    

    // Start is called before the first frame update
    void Start()
    {

    }

    public IEnumerator NPCs()
    {
        yield return new WaitForSeconds(.0f);
        properties = SetNPC();
        SetVisuals(properties);
    }

    public void SetTargetNPC()
    {
        properties = GameManager.instance.GetTargetProperties();
        GetComponent<MeshRenderer>().enabled = true;
        SetVisuals(properties);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetVisuals(Properties properties)
    {
        facialHair.transform.GetChild((int)properties.facialHair + 1).gameObject.SetActive(true);
        race.transform.GetChild((int)properties.race + 1).gameObject.SetActive(true);
        headHair.transform.GetChild((int)properties.headHair + 1).gameObject.SetActive(true);
        sex.transform.GetChild((int)properties.sex + 1).gameObject.SetActive(true);
    }
}
