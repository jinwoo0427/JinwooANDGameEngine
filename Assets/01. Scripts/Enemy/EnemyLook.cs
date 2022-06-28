using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLook : MonoBehaviour
{

    private void Awake()
    {

    }

    public void FaceDirection(Vector3 pointerInput)
    {
        transform.LookAt(pointerInput);
        //Vector3 direction = pointerInput - transform.position;
        //Vector3 result = Vector3.Cross(Vector3.up, direction);

        //if (result.z > 0)
        //{
        //    _spriteRenderer.flipX = true;
        //}
        //else if (result.z < 0)
        //{
        //    _spriteRenderer.flipX = false;
        //}
    }
}
