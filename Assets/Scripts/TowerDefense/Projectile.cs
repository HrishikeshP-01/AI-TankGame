using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField]
    private GameObject explosionPrefab;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag=="Player" && explosionPrefab!=null)
        {
            GameObject.Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
        else if (other.tag == "Environment") { Destroy(this.gameObject); }
    }
}
