using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private Transform player;
    [SerializeField]
    private WorldsController worldsController;

    private float dampTime = 0.15f;
    private Vector3 velocity = Vector3.zero;
    private Camera camera;
    private float Y = 0.3f;
    private bool zoom = false;

    private void Awake()
    {
        camera = GetComponent<Camera>();
    }
    private void LateUpdate()
    {
        if (!zoom)
        {
            Vector3 point = camera.WorldToViewportPoint(player.position);
            Vector3 delta = player.position - camera.ViewportToWorldPoint(new Vector3(0.5f, Y, point.z)); //(new Vector3(0.5, 0.5, point.z));
            Vector3 destination = transform.position + delta;
            transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);
        }
        else
        {
            if (camera.orthographicSize > 3)
            {
                camera.orthographicSize -= Time.deltaTime;
            }
            else
            {
                worldsController.SetResults();
            }
        }
    }

    public void SetZoom()
    {
        zoom = true;
    }
}
