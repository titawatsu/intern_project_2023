using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Pistol_Test : MonoBehaviour
{
    public Transform bulletSpawnPoint;

    public Animator pistolAnim;

    public InputActionReference shootAction;

    private float bulletRange = 100f;

    private float impactForce = 150f;

    private float nextTimeToFire = 0f;

    public ParticleSystem muzzleFlash;

    public GameObject impactEffect;

    public bool canShoot = true;

    private void OnEnable()
    {
        shootAction.action.Enable();
        shootAction.action.performed += ShootHandle;

    }

    private void OnDisable()
    {
        shootAction.action.Disable();
        shootAction.action.performed -= ShootHandle;
    }

    private void ShootHandle(InputAction.CallbackContext context)
    {
        Shoot();
    }

    public void Shoot()
    {
        pistolAnim.SetTrigger("Shoot");
        RaycastHit hitInfo;

        muzzleFlash.Play();

        if(Physics.Raycast(bulletSpawnPoint.position, bulletSpawnPoint.forward, out hitInfo, bulletRange))
        {
            if(hitInfo.rigidbody != null)
            {
                hitInfo.rigidbody.AddForce(-hitInfo.normal * impactForce);
            }
            Quaternion impactRotation = Quaternion.LookRotation(hitInfo.normal);
            GameObject impact = Instantiate(impactEffect, hitInfo.point, impactRotation);
            Destroy(impact, 5);
        }
    }
}
