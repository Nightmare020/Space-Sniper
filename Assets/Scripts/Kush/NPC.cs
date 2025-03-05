using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : NPCProperties
{
    [Header("NPC Properties")]
    public Properties properties;

    // Start is called before the first frame update
    void Start()
    {
        properties = SetNPC();
        NPCVisualManager.instance.SetVisuals(properties);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
