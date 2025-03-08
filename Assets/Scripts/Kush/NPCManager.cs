using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCManager : MonoBehaviour
{
    public static NPCManager instance;

    [Header("Prefabs")]
    [SerializeField] GameObject NPCPrefab;
    [SerializeField] GameObject spawnPointParentPrefab;

    //Internal Variables
    private Transform[] spawnPoints;
    private List<Transform> NPCs = new();
    public List<NPCProperties.Properties> NPCProp = new();

    void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        var pointGO = Instantiate(spawnPointParentPrefab);
        var points = pointGO.GetComponentsInChildren<SpawnPoints>();
        spawnPoints = new Transform[points.Length];
        for(int i=0; i<points.Length; i++)
        {
            spawnPoints[i] = points[i].transform;
        }

        StartCoroutine(SpawnNPCs());
    }

    IEnumerator SpawnNPCs()
    {
        yield return new WaitForSeconds(2f);

        int randomIndex = Random.Range(0, spawnPoints.Length);
        for(int i=0; i < spawnPoints.Length; i++)
        {
            if (i == randomIndex)
            {
                var npc = Instantiate(NPCPrefab, spawnPoints[i].position, NPCPrefab.transform.rotation);
                npc.GetComponent<NPC>().SetTargetNPC();
                NPCs.Add(npc.transform);
                NPCProp.Add(npc.GetComponent<NPC>().properties);

                //Debug lines
                Debug.Log(npc.GetComponent<NPC>().properties.sex);
                Debug.Log(npc.GetComponent<NPC>().properties.race);
                Debug.Log(npc.GetComponent<NPC>().properties.headHair);
                Debug.Log(npc.GetComponent<NPC>().properties.facialHair);
                //Debug.Log(NPCProp.Contains(GameManager.instance.GetTargetProperties()));
            }
            else
            {
                var npc = Instantiate(NPCPrefab, spawnPoints[i].position, NPCPrefab.transform.rotation);
                StartCoroutine(npc.GetComponent<NPC>().NPCs());
                NPCs.Add(npc.transform);
                NPCProp.Add(npc.GetComponent<NPC>().properties);
            }

            yield return new WaitForSeconds(.2f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
