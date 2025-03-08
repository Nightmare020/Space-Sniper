using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCAIController : MonoBehaviour
{
    // Start is called before the first frame update
    private NPCMovement movement;
    private PanicController panicController;
    [SerializeField]
    bool deactivated = false;
    void Start()
    {
        movement= GetComponent<NPCMovement>();
        panicController= GetComponent<PanicController>();
        movement.Init();
        panicController.Init();
    }
    public bool IsTarget(Transform target)
    {
        return movement.IsTarget(target);
    }
    public void ChangeTarget(Transform target)
    {
        deactivated = false;
        movement.ChageTarget(target);
    }
    public void ChangeSpeed(float speed)
    {
        movement.ChangeSpeed(speed);
    }
    public IEnumerator ScheduleDeactivation(float timer)
    {
        deactivated = true;
        yield return new WaitForSeconds(timer);
        if(deactivated)
        movement.StopAgent();
    }

    public bool IsActive() { return !deactivated; }
    public bool CanHide()
    {
        return panicController.CanHide();
    }
}
