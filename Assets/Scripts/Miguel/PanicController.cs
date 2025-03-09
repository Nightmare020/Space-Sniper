using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanicController : MonoBehaviour
{
    [SerializeField] private float[] speedLevels;

    [SerializeField] private int currentLevel;

    private NPCAIController controller;
    private NPCAnimationController anim_Controller;
    [Range(0f, 1f)]
    [SerializeField] private float fallChance;
    [SerializeField]private bool canHide = false;

    private float timer = 1;
   

    public void Init()
    {
        controller = GetComponent<NPCAIController>();
        anim_Controller = GetComponent<NPCAnimationController>();
        ChangePanicBehaviour();
        if (PanicManager.instance != null)
        {
            PanicManager.instance.AddController(this);
        }
        else Debug.LogWarning("No Panic manager in scene");
    }

    private void Update()
    {
       
    }
    public void IncreasePanicLevel()
    {
        if (currentLevel >=4)return;
        currentLevel = Mathf.Clamp(currentLevel+1, 0, 4);
        ChangePanicBehaviour();
        anim_Controller.PanicLevelIncreaseReaction();
    }

    public void SetPanicLevel(int level)
    {
        currentLevel = level;
        ChangePanicBehaviour();
    }
    public int GetLevel() 
    {
        return currentLevel;
    }
    public bool CanHide()
    {
        return canHide;
    }
    public void ChangePanicBehaviour()
    {
        canHide= currentLevel >= 2;
        controller.ChangeSpeed(speedLevels[currentLevel]);
    }
}
