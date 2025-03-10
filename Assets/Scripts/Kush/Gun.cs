using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [Header ("Gun Properties")]
    public float fireRate = 3f;
    public float reloadTime = 4f;
    public int maxAmmo = 5;

    [Header("UI")]
    public GameObject scope;

    [Header("DO NOT CHANGE THESE")]
    public float nextTimeToFire;
    public bool isReloading = false;
    public int curAmmo;
    public bool scoped = false;

    private Camera mainCamera;
    private ApplyPostprocess postProcess;

    private void Awake()
    {
        curAmmo = maxAmmo;
        mainCamera = Camera.main;
        postProcess = mainCamera.GetComponent<ApplyPostprocess>();

        if (postProcess != null)
        {
            postProcess.enabled = false;
        }
    }

    private void Update()
    {
        if (curAmmo <=0 && scoped)
        {
            UnScope();
        }

        if (curAmmo <= 0 && !isReloading)
        {
            ShootAndLogicHandling.instance.reloadDebugTxt.text = "Reload Gun";
        }
    }

    public void ScopeIn()
    {
        if (!isReloading && curAmmo > 0)
        {
            scoped = !scoped;
            scope.SetActive(scoped);
            gameObject.GetComponent<MeshRenderer>().enabled = !scoped;
            MouseLookAround.instance.ZoomIn(scoped);

            if (postProcess != null)
            {
                postProcess.enabled = scoped;
            }
        }

        if (scoped && curAmmo <= 0)
        {
            UnScope();
        }
    }

    public void UnScope()
    {
        scoped = false;
        scope.SetActive(false);
        gameObject.GetComponent<MeshRenderer>().enabled = true;
        MouseLookAround.instance.ZoomIn(false);

        if (postProcess != null)
        {
            postProcess.enabled = false;
        }
    }

    public void ReloadGun()
    {
        if ((curAmmo < maxAmmo && !isReloading))
        {
            ShootAndLogicHandling.instance.reloadDebugTxt.text = "Reloading...";
            isReloading = true;
            AudioManager.instance.Play("Reload");
            StartCoroutine(Reloading());
        }
    }

    IEnumerator Reloading()
    {
        yield return new WaitForSeconds(reloadTime);
        ShootAndLogicHandling.instance.reloadDebugTxt.text = string.Empty;
        curAmmo = maxAmmo;
        isReloading = false;
    }

    public void ShootBullet()
    {
        if (Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            curAmmo--;
            
            if (curAmmo > 0 && !isReloading)
            {
                AudioManager.instance.Play("SniperShot");

                //Shoot logic
                if (Physics.Raycast(MouseLookAround.instance.transform.position, MouseLookAround.instance.transform.forward, out RaycastHit hit))
                {
                    Debug.LogError(hit.transform.name);

                    ShootAndLogicHandling.instance.ProcessHit(hit);
                }
            }
            else
            {
                AudioManager.instance.Play("NoAmmo");
            }
        }
        else if(curAmmo <= 0)
        {
            GameManager.instance.RoundLost();
        }
    }
}
