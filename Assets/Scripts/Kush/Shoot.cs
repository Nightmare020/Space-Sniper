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
            gun.ShootBullet();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            gun.ReloadGun();
        }

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            gun.ScopeIn();
        }
    }
}
