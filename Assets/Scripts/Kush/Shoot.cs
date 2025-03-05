using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    [Header("Gun Reference")]
    [SerializeField] GunProperties gun;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            ShootBullet();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            ReloadGun();
        }
    }

    void ReloadGun()
    {
        if ((gun.curAmmo < gun.maxAmmo && !gun.isReloading))
        {
            gun.isReloading = true;
            StartCoroutine(Reloading());
        }
    }

    IEnumerator Reloading()
    {
        yield return new WaitForSeconds(gun.reloadTime);
        gun.curAmmo = gun.maxAmmo;
        gun.isReloading = false;
    }

    void ShootBullet()
    {
        if(Time.time >= gun.nextTimeToFire && gun.curAmmo > 0 && !gun.isReloading)
        {
            gun.nextTimeToFire = Time.time + 1f / gun.fireRate;
            gun.curAmmo--;

            //Shoot logic
            RaycastHit hit;
            if (Physics.Raycast(MouseLookAround.instance.transform.position, MouseLookAround.instance.transform.forward, out hit))
            {
                Debug.LogError(hit.transform.name);
            }
        }
    }
}
