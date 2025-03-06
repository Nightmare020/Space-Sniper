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
    }

    IEnumerator SpawnNPCs()
    {
        int randomIndex = Random.Range(0, spawnPoints.Length);
        NPCs.Count;
        for(int i=0; i < spawnPoints.Length; i++)
        {
            yield return new WaitForSeconds(.2f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
