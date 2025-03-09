using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Round")]
    [SerializeField] int curRound = 1;
    int totRound = 5;
    public bool roundWin = false;

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
    [SerializeField] private Image fadeImage;
    [SerializeField] private GameObject finalScreen;

    //Internal Variables
    List<int> targets = new();

    public void PlayAgain()
    {
        SceneManager.LoadScene(0);
    }

    public void PlayGame()
    {
        MouseLookAround.instance.SetMouseLock();
        MouseLookAround.instance.lookAllowed = true;
        ShootAndLogicHandling.instance.shootingAllowed = true;

        targetProperties = new List<NPCProperties.Properties>(totRound);
        NPCProperties.SetTargetProperties(targetProperties);
        GetNewTarget();
        SpawnNPC();
    }

    public IEnumerator Fade(float startAlpha, float endAlpha, float fadeDuration, bool clear = false, bool end = false)
    {
        var textMeshChild = fadeImage.gameObject.GetComponentInChildren<TextMeshProUGUI>();

        if (end)
        {
            textMeshChild.text = "";
        }

        Color textColor = textMeshChild.color;
        textColor.a = startAlpha;
        textMeshChild.color = textColor;
        float elapsedTime = 0f;
        Color color = fadeImage.color;

        while (elapsedTime < fadeDuration)
        {
            color.a = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / fadeDuration);
            textColor.a = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / fadeDuration);
            fadeImage.color = color;
            textMeshChild.color = textColor;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        color.a = endAlpha;
        textColor.a = endAlpha;
        fadeImage.color = color;
        textMeshChild.color = textColor;

        if (clear)
        {
            NPCManager.instance.ClearNPCs();
            SpawnNPC();
        }

        if (end)
        {
            finalScreen.SetActive(true);
            MouseLookAround.instance.SetMouseLock(false);
        }
    }

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
            ShootAndLogicHandling.instance.shootingAllowed = false;
            MouseLookAround.instance.lookAllowed = false;
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
        Debug.LogError("GAME WIN!!");
        targetText.text = "GAME WIN!";
        StartCoroutine(PlayWinSounds());
    }

    IEnumerator PlayWinSounds()
    {
        StartCoroutine(Fade(0, 1, AudioManager.instance.GetDialogueDuration((ClientName)clientNo, DialogueType.CorrectShot), false, true));
        ShootAndLogicHandling.instance.shootingAllowed = false;
        MouseLookAround.instance.lookAllowed = false;
        yield return new WaitForSeconds(AudioManager.instance.GetDialogueDuration((ClientName)clientNo, DialogueType.CorrectShot));
        NPCManager.instance.ClearNPCs();
        AudioManager.instance.Stop("Background 1");
        AudioManager.instance.Play("Outro");
    }

    void NextRound()
    {
        targetText.text = "ROUND WIN!";
        StartCoroutine(Fade(0, 1, 
            AudioManager.instance.GetDialogueDuration((ClientName)clientNo, DialogueType.CorrectShot), 
            true));
        curRound++;
        curProperty = 1;
        GetNewTarget();
        kills = 0;
        roundWin = false;
    }

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        

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
}
