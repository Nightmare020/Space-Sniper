using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class ShootAndLogicHandling : MonoBehaviour
{
    public static ShootAndLogicHandling instance;

    [Header("Gun Reference")]
    [SerializeField] GunProperties gun;

    [Header("UI")]
    public TextMeshProUGUI debugTxt;
    public TextMeshProUGUI reloadDebugTxt;

    [Header("Events")]
    [SerializeField] private UnityEvent<Vector3> onShoot;
    [SerializeField] private UnityEvent<Vector3> onHit;
    [SerializeField] private UnityEvent<Vector3> onMiss;

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
        onShoot.Invoke(hit.point);
        if(hit.transform.gameObject.TryGetComponent<NPC>(out NPC npc))
        {
            onHit.Invoke(hit.point);

            debugTxt.text = string.Empty;

            GameManager.instance.AddKill();

            NPCProperties.CompareReturn returnVal = npc.CompareProperties(debugTxt,                GameManager.instance.GetTargetProperties(), npc.properties,              GameManager.instance.GetCurProperty());

            if (returnVal.value == -1)
            {
                Debug.Log("Master Win!");
            }
            else if (returnVal.value == 1)
            {
                if (returnVal.property == GameManager.instance.GetTotProperty())
                {
                    Debug.Log("Round Win!!");
                }
            }
            else if(returnVal.value == 0)
            {
                switch (returnVal.property) {
                    case 1:
                        Debug.Log("Sex Fails, play master insult, followed by sex line");
                        break;

                    case 2:
                        Debug.Log("Race Fails, play race description");
                        break;

                    case 3:
                        Debug.Log("head hair Fails, play head hair description");
                        break;

                    case 4:
                        Debug.Log("facial hair Fails, play facial hair description");
                        break;
                }
            }

            hit.transform.gameObject.SetActive(false);
        }
        else onMiss.Invoke(hit.point);
    }
}
