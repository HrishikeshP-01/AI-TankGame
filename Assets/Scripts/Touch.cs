using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Touch : Sense
{
    private void OnTriggerEnter(Collider other)
    {
        Aspect aspect = other.gameObject.GetComponent<Aspect>();
        if(aspect!=null)
        {
            if (aspect.aspectType != aspectName) { Debug.Log("Enemy Touch detected"); }
        }
    }
}
