using Cinemachine;
using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Transform target;
    public Vector3 offset;
    public float smoothing = 5f;

    private void LateUpdate()
    {
        if (target != null)
        {
            Vector3 targetPosition = target.position + offset;
            transform.position = Vector3.Lerp(transform.position, targetPosition, smoothing * Time.deltaTime);
        }
    }

    public void SetTarget(Transform newTarget)
    {
        if (newTarget.GetComponent<NetworkIdentity>().isLocalPlayer)
        {
            target = newTarget;
        }
    }
}
