using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackCheck : MonoBehaviour
{
    public GameObject front;
    private void Awake()
    {
        //front = transform.Find("front").gameObject;
    }
    private void Start()
    {
        gameObject.SetActive(true);
        front.SetActive(false);
        //front = transform.Find("front").GetComponent<GameObject>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            print("µÚ¿¡²¨ ÃâÇö");
            MapSpawner.Instance.SpawnBack();
            gameObject.SetActive(false);
            front.SetActive(true);
        }
    }
}
