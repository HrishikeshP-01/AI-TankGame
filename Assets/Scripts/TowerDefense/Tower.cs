using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField]
    private Animator animator;

    [SerializeField]
    private float fireSpeed = 3.0f;
    private float fireCounter = 0.0f;
    private bool canFire = true;

    [SerializeField]
    private Transform muzzle;
    [SerializeField]
    private GameObject projectile;

    private bool isLockedOn = false;

    public bool LockedOn { get { return isLockedOn; } }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") { animator.SetBool("TankInRange", true); }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player") { animator.SetBool("TankInRange", false); }
    }

    private void FireProjectile()
    {
        GameObject bullet = Instantiate(projectile, muzzle);
        bullet.GetComponent<Rigidbody>().AddForce(muzzle.forward * 300f);
    }

    private IEnumerator Fire()
    {
        canFire = false;
        FireProjectile();
        while(fireCounter<fireSpeed)
        {
            fireCounter += Time.deltaTime;
            yield return null; 
            /*
             * Pauses execution of while loop in the current frame & begins execution in the next.
             * This way, the while loop doesn't get executed in a single frame & this takes place over several frames & performs the actual fn - wait for a few seconds
             */
        }
        canFire = true;
        fireCounter = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (isLockedOn && canFire) { StartCoroutine(Fire()); }
    }
}
