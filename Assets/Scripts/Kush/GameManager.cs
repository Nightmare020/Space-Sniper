using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Round")]
    [SerializeField] int totRound = 5;
    [SerializeField] int curRound = 1;

    [Header("Total Properties")]
    [SerializeField] int curProperty = 1;
    int totProperty = 5;

    [Header("Target Properties")]
    [SerializeField] NPCProperties.Properties targetProperties;

    [Header("Debug Txt")]
    [SerializeField] TextMeshProUGUI targetPropDebugTxt;

    public NPCProperties.Properties GetTargetProperties()
    {
        return targetProperties;
    }

    public int GetCurRound()
    {
        return curRound;
    }

    public int GetCurProperty()
    {
        return curProperty;
    }

    public void IncProperty()
    {
        if (curProperty < totProperty)
            curProperty++;
        else
        {
            Debug.Log("Round Win!");
            RoundWin();
        }
    }

    void RoundWin()
    {
        if(curRound < totRound)
        {
            curRound++;
            curProperty = 1;

        }
        else
        {
            Debug.Log("Game Win");
        }
    }

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        targetProperties = NPCProperties.SetNPC(0, targetPropDebugTxt);

        //Debug
        //StartCoroutine(Assign());
    }

    IEnumerator Assign()
    {
        yield return new WaitForSeconds(1f);
        //Debug
        targetProperties = FindObjectOfType<NPC>().properties;
        if (targetProperties.Equals(FindObjectOfType<NPC>().properties))
        {
            Debug.LogError("Yay");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
