using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public static ItemManager instance;
    public List<Resource> _items = new List<Resource>();

    private void Awake()
    {
        instance = this;
    }
    public void AllDeleteItem()
    {
        print("아이템 삭제");
        for (int i = 0; i < _items.Count; i++)
        {
            PoolManager.Instance.Push(_items[i]);
            _items.RemoveAt(i);
        }
    }
}
