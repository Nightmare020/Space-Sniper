using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Round")]
    [SerializeField] int curRound = 1;
    int totRound = 5;

    [Header("Total Properties")]
    [SerializeField] int curProperty = 1;
    int totProperty = 4;

    [Header("Target Properties")]
    [SerializeField] NPCProperties.Properties curTargetProperties;
    [SerializeField] List<NPCProperties.Properties> targetProperties;

    [Header("Kill stat")]
    [SerializeField] private int kills = 0;

    [Header("Debug Txt")]
    [SerializeField] TextMeshProUGUI targetPropDebugTxt;

    //Internal Variables
    List<int> targets = new();

    public NPCProperties.Properties GetTargetProperties()
    {
        return curTargetProperties;
    }

    public int GetCurRound()
    {
        return curRound;
    }

    public int GetTotRound()
    {
        return totRound;
    }

    public int GetCurProperty()
    {
        return curProperty;
    }

    public int GetTotProperty()
    {
        return totProperty;
    }

    public void IncProperty()
    {
        if (curProperty < totProperty)
            curProperty++;
        else
        {
            Debug.Log("Round Win!");
            RoundWin();
            targetPropDebugTxt.text = "You won the Round!";
        }
    }

    public int GetCurKills()
    {
        return kills;
    }

    public void AddKill()
    {
        kills++;
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
        targetProperties = new List<NPCProperties.Properties>(totRound);
        NPCProperties.SetTargetProperties(targetProperties);
        GetNewTarget();


        //Debug
        //StartCoroutine(Assign());
    }

    void GetNewTarget()
    {
        if (curRound == 1)
        {
            curTargetProperties = targetProperties[0];
            targets.Add(0);
        }
        else
        {
            int i;
            do
            {
                i = Random.Range(1, targetProperties.Count);
            } while (targets.Contains(i));

            curTargetProperties = targetProperties[i];
            targets.Add(i);
        }
    }

    IEnumerator Assign()
    {
        yield return new WaitForSeconds(1f);
        //Debug
        curTargetProperties = FindObjectOfType<NPC>().properties;
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
