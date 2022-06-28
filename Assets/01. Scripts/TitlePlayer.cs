using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitlePlayer : MonoBehaviour
{
    public Vector3 pos;
    void Start()
    {
        pos = transform.position;
    }

    void Update()
    {
        transform.position = new Vector3(transform.position.x, -5.7f, transform.position.z);
    }
    public void ResetPos()
    {
        transform.position = pos;
    }
}
