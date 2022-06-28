using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;
public class LightShow : MonoBehaviour
{

    public Light li;

    void Start()
    {
        li = GetComponent<Light>();
        li.enabled = false;
        //gameObject.SetActive(false);
    }

    void Update()
    {
        Vector3 viewPos = MainCam.WorldToViewportPoint(transform.position);

        if (viewPos.x >= 0 && viewPos.x <= 1 &&
            viewPos.y >= 0 && viewPos.y <= 1 &&
            viewPos.z > 0)
        {
            li.enabled = true;
        }
        else
        {
            li.enabled = false;
        }

    }

}
