using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCAIController : MonoBehaviour
{
    // Start is called before the first frame update
    private NPCMovement movement;
    private PanicController panicController;
    void Start()
    {
        movement= GetComponent<NPCMovement>();
        panicController= GetComponent<PanicController>();
    }
    
    public void ChangeTarget(Transform target)
    {
        movement.ChageTarget(target);
    }
    public IEnumerator ScheduleDeactivation(float timer)
    {
        yield return new WaitForSeconds(timer);
        movement.StopAgent();
    }
}
