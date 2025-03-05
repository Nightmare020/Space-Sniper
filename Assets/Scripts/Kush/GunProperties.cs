using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunProperties : MonoBehaviour
{
    public float fireRate = 3f;
    public float reloadTime = 4f;
    public int maxAmmo = 5;

    [Header("DO NOT CHANGE THESE")]
    public float nextTimeToFire;
    public bool isReloading = false;
    public int curAmmo;

    private void Awake()
    {
        curAmmo = maxAmmo;
    }
}
