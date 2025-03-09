using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class ShootAndLogicHandling : MonoBehaviour
{
    public static ShootAndLogicHandling instance;

    [Header("Gun and Shooting Related")]
    [SerializeField] Gun gun;
    public bool shootingAllowed = true;

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
        if (shootingAllowed)
        {
            if (Input.GetButton("Fire1"))
            {
                gun.ShootBullet();
            }

            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                gun.ScopeIn();
            }
        }
    }

    public void ProcessHit(RaycastHit hit)
    {
        onShoot.Invoke(hit.point);
        if(hit.transform.gameObject.TryGetComponent<NPC>(out NPC npc))
        {
            AudioManager.instance.Play("TargetHit");
            onHit.Invoke(hit.point);

            debugTxt.text = string.Empty;

            GameManager.instance.AddKill();

            NPCProperties.CompareReturn returnVal = npc.CompareProperties(debugTxt, GameManager.instance.GetTargetProperties(), npc.properties, GameManager.instance.GetCurProperty());

            if (returnVal.value == -1)
            {
                Debug.Log("Master Win!");
                //We need to play the master win from the client here, but "yay" is also good
                //AudioManager.instance.Play("RightTarget");
                AudioManager.instance.PlayDialogue((ClientName)GameManager.instance.GetClientNo(), DialogueType.MasterCorrect);
                GameManager.instance.RoundWin();
            }
            else if (returnVal.value == 1)
            {
                //We need to play correct shot from client here, but "yay" is also good
                //AudioManager.instance.Play("RightTarget");
                AudioManager.instance.PlayDialogue((ClientName)GameManager.instance.GetClientNo(), DialogueType.CorrectShot);
                if (returnVal.property == GameManager.instance.GetTotProperty())
                {
                    Debug.Log("Round Win!!");
                    GameManager.instance.RoundWin();
                }
            }
            else if(returnVal.value == 0)
            {
                //AudioManager.instance.Play("WrongTarget");
                switch (returnVal.property) {
                    case 1:
                        Debug.Log("Sex Fails, play master insult, followed by sex line");
                        AudioManager.instance.PlayDialogue((ClientName)GameManager.instance.GetClientNo(), (DialogueType)GameManager.instance.GetTargetProperties().sex);
                        GameManager.instance.AddPropertyToTargetText(returnVal.property - 1);
                        break;

                    case 2:
                        Debug.Log("Race Fails, play race description");
                        AudioManager.instance.PlayDialogue((ClientName)GameManager.instance.GetClientNo(), (DialogueType)DescriptionHelper.RaceClue);
                        GameManager.instance.AddPropertyToTargetText(returnVal.property - 1);
                        break;

                    case 3:
                        Debug.Log("head hair Fails, play head hair description");
                        AudioManager.instance.PlayDialogue((ClientName)GameManager.instance.GetClientNo(), (DialogueType)DescriptionHelper.HeadHairClue);
                        GameManager.instance.AddPropertyToTargetText(returnVal.property - 1);
                        break;

                    case 4:
                        Debug.Log("facial hair Fails, play facial hair description");
                        AudioManager.instance.PlayDialogue((ClientName)GameManager.instance.GetClientNo(), (DialogueType)DescriptionHelper.FacialHairClue);
                        GameManager.instance.AddPropertyToTargetText(returnVal.property - 1);
                        break;
                }
            }

            hit.transform.gameObject.SetActive(false);
        }
        else onMiss.Invoke(hit.point);
    }
}
