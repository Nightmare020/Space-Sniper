using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCAnimationController : MonoBehaviour
{
    private NPCAIController controller;
    [SerializeField]
    private Transform child;
    private Vector3 fallVec;
    private void Start()
    {
        controller = GetComponent<NPCAIController>();
        fallVec = new Vector3(-90, 0, 0f);
    }
    public void FallReaction()
    {
        StartCoroutine(Fall());
    }
    public void PanicLevelIncreaseReaction()
    {
        StartCoroutine(Reaction());
    }
    private IEnumerator Reaction()
    {
        StartCoroutine(controller.ScheduleDeactivation(0));
        Debug.Log("Ai Jão!");
        yield return new WaitForSeconds(2);
        controller.ChangeTarget(WaypointManager.instance.GetRandomWaypoint().transform);
    }

    public IEnumerator Fall()
    {
        StartCoroutine(controller.ScheduleDeactivation(0));
        child.localRotation = Quaternion.Euler(child.localRotation.eulerAngles + fallVec);
        child.localPosition -= new Vector3(0, 0.5f, 0);
        Debug.Log("POFT " + gameObject.name);
        yield return new WaitForSeconds(2);
        child.localRotation = Quaternion.Euler(child.localRotation.eulerAngles - fallVec);
        child.localPosition += new Vector3(0, 0.5f, 0);
        controller.ChangeTarget(WaypointManager.instance.GetRandomWaypoint().transform);
    }
}