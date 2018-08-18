using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSmooth : MonoBehaviour {

    public Transform target;
    public float smooth = .1f;
    public Vector3 offset;

    private void Start() {
        if (target == null)
            target = GameObject.FindWithTag("Player").transform;
    }

    private void FixedUpdate() {
        Vector3 cameraPosition = target.position + offset;
        Vector3 smoothingPosition = Vector3.Lerp(transform.position, cameraPosition, smooth);

        transform.position = smoothingPosition;
        transform.LookAt(target);
    }
}
