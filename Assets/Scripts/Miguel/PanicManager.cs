using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanicManager : MonoBehaviour
{
    private List<PanicController> panicControllers;
    void Start()
    {
        panicControllers = new List<PanicController>(FindObjectsByType<PanicController>(FindObjectsSortMode.None));
    }

    public void IncreaseGeneralPanicLevel()
    {
        for(int i = 0; i < panicControllers.Count;i++)
        {
            if (panicControllers[i] != null) 
                panicControllers[i].IncreasePanicLevel();
            else
                panicControllers.RemoveAt(i);
        }
        
    }
}
