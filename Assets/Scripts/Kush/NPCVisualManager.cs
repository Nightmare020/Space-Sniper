using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCVisualManager : MonoBehaviour
{
    public static NPCVisualManager instance;

    [Header("Visual Categories")]
    public GameObject bodyTypes;
    public GameObject facialHair;
    public GameObject race;
    public GameObject headHair;
    public GameObject sex;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
    }

    public void SetVisuals(NPCProperties.Properties properties)
    {
        bodyTypes.transform.GetChild((int)properties.body + 1).gameObject.SetActive(true);
        facialHair.transform.GetChild((int)properties.facialHair + 1).gameObject.SetActive(true);
        race.transform.GetChild((int)properties.race + 1).gameObject.SetActive(true);
        headHair.transform.GetChild((int)properties.headHair + 1).gameObject.SetActive(true);
        sex.transform.GetChild((int)properties.sex + 1).gameObject.SetActive(true);
    }
}
