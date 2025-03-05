using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShootAndLogicHandling : MonoBehaviour
{
    public static ShootAndLogicHandling instance;

    [Header("Gun Reference")]
    [SerializeField] GunProperties gun;

    [Header("UI")]
    public TextMeshProUGUI debugTxt;
    public TextMeshProUGUI reloadDebugTxt;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            gun.ShootBullet();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            gun.ReloadGun();
        }

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            gun.ScopeIn();
        }
    }

    public void ProcessHit(RaycastHit hit)
    {
        if(hit.transform.gameObject.TryGetComponent<NPC>(out NPC npc))
        {
            if (npc.CompareProperties(debugTxt,
                    npc.properties,
                    GameManager.instance.GetTargetProperties(),
                    GameManager.instance.GetCurProperty()) == 1)
            {
                GameManager.instance.IncProperty();
            }
            else if (npc.CompareProperties(debugTxt,
                        npc.properties,
                        GameManager.instance.GetTargetProperties(),
                        GameManager.instance.GetCurProperty()) == -1)
            {
                Debug.Log("Master Win!");
            }
            else
            {
                if(GameManager.instance.GetCurProperty() != 1)
                {
                    debugTxt.text += ", but Not correct Target";
                }
                else
                {
                    debugTxt.text = "Completely Wrong Target";
                }
            }
        }
    }
}
