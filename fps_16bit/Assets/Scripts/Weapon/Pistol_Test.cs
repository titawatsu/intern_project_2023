using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Pistol_Test : MonoBehaviour
{
    [SerializeField] private bool addBulletSpread = true;

    [SerializeField] private Vector3 bulletSpreadVariance = new Vector3(0.1f, 0.1f, 0.1f);

    [SerializeField] private ParticleSystem shootParticle;

    [SerializeField] private Transform bulletSpawnPoint;

    [SerializeField] private ParticleSystem impactParticle;

    [SerializeField] TrailRenderer bulletTrail;

    [SerializeField] private float fireDelay = 0.3f;

    [SerializeField] private Animator anim;

    [SerializeField] float lastShootTime;

    [SerializeField] private LayerMask gunMask;

    public Animator pistolAnim;

    public InputActionReference shootAction;

    private RaycastHit hitInfo;

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

    }

    private void Shoot()
    {
        if (lastShootTime + fireDelay < Time.deltaTime)
        {
            pistolAnim.SetTrigger("Shoot");
            shootParticle.Play();
            Vector3 shootDirection = GetDirection();

            if (Physics.Raycast(bulletSpawnPoint.position, shootDirection, out hitInfo, float.MaxValue, gunMask))
            {
                TrailRenderer trail = Instantiate(bulletTrail, bulletSpawnPoint.position, Quaternion.identity);

                StartCoroutine(SpawnTrail(trail, hitInfo));

                lastShootTime = Time.deltaTime;
            }
        }
    }

    private Vector3 GetDirection()
    {
        Vector3 shootDirection = transform.forward;

        if (addBulletSpread)
        {
            shootDirection += new Vector3(
                Random.Range(-bulletSpreadVariance.x, bulletSpreadVariance.x),
                Random.Range(-bulletSpreadVariance.y, bulletSpreadVariance.y),
                Random.Range(-bulletSpreadVariance.z, bulletSpreadVariance.z)

                );
            shootDirection.Normalize();
        }
        return shootDirection;
    }

    private IEnumerator SpawnTrail(TrailRenderer trail, RaycastHit hit)
    {
        float time = 0;
        Vector3 startPos = trail.transform.position;

        while (time < 1)
        {
            trail.transform.position = Vector3.Lerp(startPos, hit.point, time);
            time += Time.deltaTime / trail.time;

            yield return null;
        }

        trail.transform.position = hit.point;
        Instantiate(impactParticle, hit.point, Quaternion.LookRotation(hit.normal));

        Destroy(trail.gameObject, trail.time);
    }
}
