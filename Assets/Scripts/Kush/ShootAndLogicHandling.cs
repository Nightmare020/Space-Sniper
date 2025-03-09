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
    public bool shootingAllowed = false;

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
        if(hit.transform.gameObject.TryGetComponent<NPC>(out NPC npc) && !GameManager.instance.roundWin)
        {
            AudioManager.instance.Play("TargetHit");
            onHit.Invoke(hit.point);

            debugTxt.text = string.Empty;

            GameManager.instance.AddKill();

            NPCProperties.CompareReturn returnVal = npc.CompareProperties(debugTxt, GameManager.instance.GetTargetProperties(), npc.properties, GameManager.instance.GetCurProperty());

            if (returnVal.value == -1)
            {
                GameManager.instance.roundWin = true;
                Debug.Log("Master Win!");
                //We need to play the master win from the client here, but "yay" is also good
                StartCoroutine(PlaySequence("RightTarget", DialogueType.MasterCorrect));
                // GameManager.instance.RoundWin();
            }
            else if (returnVal.value == 1)
            {
                //We need to play correct shot from client here, but "yay" is also good
                if (returnVal.property == GameManager.instance.GetTotProperty())
                {
                    GameManager.instance.roundWin = true;
                    Debug.Log("Round Win!!");
                    StartCoroutine(PlaySequence("RightTarget", DialogueType.CorrectShot));
                    // GameManager.instance.RoundWin();
                }
            }
            else if(returnVal.value == 0)
            {
                AudioManager.instance.Play("WrongTarget");
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

    private IEnumerator PlaySequence(string s, DialogueType d = DialogueType.None)
    {
        AudioManager.instance.Play(s);
        yield return new WaitForSeconds(AudioManager.instance.GetSoundDuration(s));
        if (d != DialogueType.None)
        {
            AudioManager.instance.PlayDialogue((ClientName)GameManager.instance.GetClientNo(), d);
        }
        GameManager.instance.RoundWin();
        //yield return new WaitForSeconds(AudioManager.instance.GetDialogueDuration((ClientName)GameManager.instance.GetClientNo(), d));
        
    }
}
