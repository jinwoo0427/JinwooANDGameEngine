using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPointMove : MonoBehaviour
{
    [SerializeField]
    private Transform playerTrm;
    private void Awake()
    {
        transform.position = new Vector3(0, 0, playerTrm.position.z + 150f);
    }
    void Update()
    {
        transform.position = new Vector3(0, 0, playerTrm.position.z + 150f);
    }
}
