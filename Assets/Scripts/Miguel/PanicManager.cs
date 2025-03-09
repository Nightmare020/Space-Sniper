using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanicManager : MonoBehaviour
{
    public static PanicManager instance;
    private List<PanicController> panicControllers = new List<PanicController>();
    private void Awake()
    {
        instance = this;
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

    public void AddController(PanicController controller)
    {
        panicControllers.Add(controller);
    }
}
