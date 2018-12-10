using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraSmooth : MonoBehaviour {

    private GameObject[] players;
    private List<Transform> targets;
    public float smooth = .5f;
    public Vector3 offset;
    public Vector3 velocity;
    public Camera thisCamera;
    public float minZoom = 40f;
    public float maxZoom = 10f;
    public float zoomLimiter = 50f;

    private void Start() {
        thisCamera = GetComponent<Camera>();
        targets = new List<Transform>();
        players = GameObject.FindGameObjectsWithTag("Player");
        if (players != null)
            foreach (GameObject player in players)
                targets.Add(player.transform);
    }

    private void LateUpdate() {
        if (targets.Count == 0)
            return;

        Move();
        Zoom();
    }

    private void Zoom()
    {
        float newZoom = Mathf.Lerp(maxZoom, minZoom, GetGreatestDistance() / zoomLimiter);
        thisCamera.fieldOfView = Mathf.Lerp(thisCamera.fieldOfView, newZoom, Time.deltaTime);
    }

    private void Move()
    {
        Vector3 centerPoint = GetCenterPoint();
        Vector3 newPosition = centerPoint + offset;
        transform.position = Vector3.SmoothDamp(transform.position, newPosition, ref velocity, smooth);
    }

    private float GetGreatestDistance()
    {
        return GetBounds().size.x;
    }

    private Vector3 GetCenterPoint()
    {
        return targets.Count == 1 ? targets[0].position : GetBounds().center;
    }

    private Bounds GetBounds()
    {
        Bounds bounds = new Bounds(targets[0].position, Vector3.zero);
        for (int i = 0; i < targets.Count; i++)
            bounds.Encapsulate(targets[i].position);

        return bounds;
    }
}
