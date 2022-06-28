using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrontCheck : MonoBehaviour
{
    public GameObject back;
    private void Awake()
    {
        //back = transform.Find("back").gameObject;
    }
    private void Start()
    {
        gameObject.SetActive(true);
        back.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            print("¾Õ¿¡²¨ ÃâÇö");
            MapSpawner.Instance.SpawnFront();
            gameObject.SetActive(false);
            back.SetActive(true);
        }
    }
}
