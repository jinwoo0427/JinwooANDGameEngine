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
    private void Awake()
    {
        Instance = this;
        frontCnt = 0;
        backCnt = 0;
        totalCnt = 2;
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
        //if (totalCnt >= 2)
        //{
        //    //if (backroads.Count > 0)
        //    //{
        //    //    print("µÚ¿¡²¨ Á¦°Å");
        //    //    backroads.RemoveAt(0);
        //    //    //backCnt--;
        //    //}
        //    //else
        //    //{

        //    //    print("¾Õ¿¡²¨ Á¦°Å");
        //    //    //backCnt++;
        //    //}
        //    PoolManager.Instance.Push(frontroads[0]);
        //    frontroads.RemoveAt(0);
        //}
        //if (frontCnt % 3 == 1)
        //{
        //    DeleteRoad();
        //}

        RoadSpawn road = PoolManager.Instance.Pop("RoadSetFront") as RoadSpawn;
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
        //if(totalCnt >= 2)
        //{
        //    if (frontroads.Count > 0)
        //    {
        //        print("¾Õ¿¡²¨ Á¦°Å");
        //        PoolManager.Instance.Push(frontroads[0]);
        //        frontroads.RemoveAt(0);
        //        //frontCnt--;
        //    }
        //    else
        //    {
        //        print("µÚ¿¡²¨ Á¦°Å");
        //        PoolManager.Instance.Push(backroads[0]);
        //        backroads.RemoveAt(0);
        //        //frontCnt++;
        //    }
        //}
        //if (backCnt % 3 == 1)
        //{
        //    DeleteRoad();
        //}
        RoadSpawn road = PoolManager.Instance.Pop("RoadSetBack") as RoadSpawn;
        road.transform.SetParent(frontmap.transform);
        road.transform.position = new Vector3(0, 0, back.position.z);
        roads.Add(road);
        //backroads.Add(road);
        //frontCnt++;
        backCnt++;
        totalCnt++;
    }


}
