using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public Transform targetTransform;
    public float targetDistanceTolerance = 3.0f;

    private float movementSpeed;
    private float rotationSpeed;

    // Start is called before the first frame update
    void Start()
    {
        movementSpeed = 10.0f;
        rotationSpeed = 2.0f;
    }

    // Update is called once per frame
    void Update()
    {
        // Player should move to the target position i.e, the mouse click location on the platform

        if (Vector3.Distance(targetTransform.position, transform.position) < targetDistanceTolerance) { return; }

        Vector3 direction = targetTransform.position - transform.position;
        Quaternion tarRot = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, tarRot, rotationSpeed * Time.deltaTime);

        transform.Translate(new Vector3(0, 0, movementSpeed * Time.deltaTime));
    }
}
