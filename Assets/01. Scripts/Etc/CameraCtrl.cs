using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCtrl : MonoBehaviour
{
    [SerializeField]
    private float zoomSpeed = 0f;
    [SerializeField]
    private float zoomMax = 0f;
    [SerializeField]
    private float zoomMin = 0f;

    public Transform target;
    private Vector3 offset;
    public float distance;
    public float angle;

    private void Start()
    {
        transform.rotation = Quaternion.Euler(angle, 0f, 0f);
        offset = transform.position - target.position;
    }
    
    public void FollowCam()
    {
        offset.z = distance;
        Vector3 newCamPos = target.position + offset;
        transform.rotation = Quaternion.Euler(angle, 0f, 0f);
        transform.position = newCamPos;
        
    }

    public void CameraZoom()
    {
        float zoomDirection = Input.GetAxis("Mouse ScrollWheel");

        if (transform.position.y <= zoomMax && zoomDirection > 0)
            return;
        if (transform.position.y >= zoomMin && zoomDirection < 0)
            return;

        transform.position += transform.forward * zoomDirection * zoomSpeed;

         
    }

    void Update()
    {
        CameraZoom();
        FollowCam();
    }
}
