using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapSpawner : MonoBehaviour
{
    public static MapSpawner Instance;
    public List<RoadSpawn> roads; 
    public GameObject frontmap;
    //public GameObject frontmap;
    public GameObject backmap;
    private float offset = 192f;
    public int frontCnt = 1;
    public int backCnt = 2;
    public int totalCnt = 0;

    public Transform front;
    public Transform back;

    public GameObject backCol;
    private void Awake()
    {
        Instance = this;
        frontCnt = 0;
        backCnt = 0;
        //totalCnt = 2;
    }
    private void Start()
    {
        SpawnBack();
        SpawnFront();
    }
    public void DeleteRoad()
    {
        for (int i = 0; i < totalCnt - 2; i++)
        {
            PoolManager.Instance.Push(roads[i]);
            roads.RemoveAt(i);
        }
        totalCnt = 2;
    }
    public void SpawnFront()
    {
        RoadSpawn road = PoolManager.Instance.Pop("RoadSetFront") as RoadSpawn;
        road.isbackTrigger = true;
        road.transform.SetParent(frontmap.transform);
        road.transform.position = new Vector3(0, 0, front.position.z);
        roads.Add(road);
        //frontroads.Add(road);
        frontCnt++;
        //backCnt--;
        totalCnt++;
    }
    public void SpawnBack()
    {

        RoadSpawn road = PoolManager.Instance.Pop("RoadSetBack") as RoadSpawn;
        road.isfrontTrigger = true;
        road.transform.SetParent(frontmap.transform);
        road.transform.position = new Vector3(0, 0, back.position.z);
        roads.Add(road);
        //backroads.Add(road);
        //frontCnt++;
        backCnt++;
        totalCnt++;
    }


}
