using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalCam : MonoBehaviour
{
    [SerializeField]
    private Transform playerTransform;

    private Vector3 targetPosition;

    // Update is called once per frame
    void Update()
    {
        targetPosition = transform.position;
        targetPosition.z = playerTransform.position.z;
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime);
    }
}
