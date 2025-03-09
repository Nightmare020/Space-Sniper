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

    [Header("Client Info")]
    [SerializeField] int clientNo = 0;

    [Header("Total Properties")]
    [SerializeField] int curProperty = 1;
    int totProperty = 4;

    [Header("Target Properties")]
    [SerializeField] NPCProperties.Properties curTargetProperties;
    [SerializeField] List<NPCProperties.Properties> targetProperties;

    [Header("Kill stat")]
    [SerializeField] private int kills = 0;

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI targetText;

    //Internal Variables
    List<int> targets = new();

    public NPCProperties.Properties GetTargetProperties()
    {
        return curTargetProperties;
    }

    public int GetClientNo() { return clientNo; }

    public int GetCurRound() { return curRound; }

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

    public int GetCurKills()
    {
        return kills;
    }

    public void AddKill()
    {
        kills++;
    }

    public void RoundWin()
    {
        if(curRound < totRound)
        {
            NextRound();

        }
        else
        {
            GameWin();
            Debug.Log("Game Win");
        }
    }

    public void RoundLost()
    {
        ShootAndLogicHandling.instance.shootingAllowed = false;
        MouseLookAround.instance.lookAllowed = false;
        Debug.LogError("ROUND LOST!!");
    }

    void GameWin()
    {
        NPCManager.instance.ClearNPCs();
        Debug.LogError("GAME WIN!!");
        targetText.text = "GAME WIN!";
    }

    void NextRound()
    {
        targetText.text = "ROUND WIN!";

        curRound++;
        curProperty = 1;
        NPCManager.instance.ClearNPCs();
        GetNewTarget();
        SpawnNPC();
        kills = 0;
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
        SpawnNPC();

        //Debug
        //StartCoroutine(Assign());
    }

    void GetNewTarget()
    {
        if (curRound == 1)
        {
            curTargetProperties = targetProperties[0];
            targets.Add(0);
            clientNo = 0;
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
            clientNo = i;
        }
    }

    public void AddPropertyToTargetText(int property)
    {
        switch (property)
        {
            case (int)NPCProperties.Order.Sex:
                if (!targetText.text.Contains(curTargetProperties.sex.ToString()))
                {
                    targetText.text = "Target is:\n" + curTargetProperties.sex.ToString();
                }
                break;

            case (int)NPCProperties.Order.Race:
                if (!targetText.text.Contains(curTargetProperties.race.ToString()))
                {
                    targetText.text += "\n" + curTargetProperties.race.ToString();
                }
                break;

            case (int)NPCProperties.Order.HeadHair:
                if (!targetText.text.Contains(curTargetProperties.headHair.ToString()))
                {
                    targetText.text += "\n" + curTargetProperties.headHair.ToString();
                }
                break;

            case (int)NPCProperties.Order.FacialHair:
                if (!targetText.text.Contains(curTargetProperties.facialHair.ToString()) && curTargetProperties.sex != NPCProperties.Sex.Female)
                {
                    targetText.text += "\n" + curTargetProperties.facialHair.ToString();
                }
                break;
        }
    }

    void SpawnNPC()
    {
        StartCoroutine(NPCManager.instance.SpawnNPCs());
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
