using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanicController : MonoBehaviour
{
    [SerializeField] private float[] speedLevels;

    [SerializeField] private int currentLevel;

    private NPCAIController controller;
    [SerializeField]private bool canHide = false;
    private bool canFall = false;

    public void Init()
    {
        controller = GetComponent<NPCAIController>();
        ChangePanicBehaviour();
    }
    public void IncreasePanicLevel()
    {
        currentLevel = Mathf.Clamp(currentLevel+1, 0, 4);
        ChangePanicBehaviour();
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
        canFall= currentLevel >= 3;
        canHide= currentLevel >= 2;
        controller.ChangeSpeed(speedLevels[currentLevel]);
    }
}
