using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadSpawn : PoolableMono
{

    private float offset = 192f;

    //public GameObject front, back;
    private void Awake()
    {
        //front = transform.Find("front").GetComponent<GameObject>();   
        //back = transform.Find("back").GetComponent<GameObject>();   
    }

    public override void ResetObject()
    {
        
    }
}
