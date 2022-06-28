using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class ItemDropper : MonoBehaviour
{
    [SerializeField]
    private ItemDropTableSO _dropTable;
    private float[] _itemWeights;

    public List<Resource> _items = new List<Resource>();

    [SerializeField]
    private bool _dropEffect = false; //��������Ʈ

    [SerializeField]
    private float _dropPower = 2f;

    [SerializeField]
    [Range(0, 1f)]
    private float _dropChance; //�� ��? �� �������� ���Ȯ��

    //3�� 
    //1�� : �������� ���� ���ΰ�?
    //2�� : � �������� �������ΰ�? (�� ���⼭ ����ġ�� �����ؼ� ����ġ�� ���� ���

    private void Start()
    {
        _itemWeights = _dropTable.dropList.Select(item => item.rate).ToArray();
    }
    //public void AllDeleteItem()
    //{
    //    foreach (var item in _items)
    //    {
    //        _items.Remove(item);
    //        PoolManager.Instance.Push(item);
    //    }
    //}
    public void DropItem()
    {
        float dropVariable = Random.value; //0 ~1;
        float dropCnt = Random.Range(dropVariable, 6f);
        for (int i = 0; i < dropCnt; i++)
        {
            if (dropVariable < _dropChance) //������� �ɷȴٸ� ������ ���
            {
                int idx = GetRandomWeightedIndex();
                Resource resource = PoolManager.Instance.Pop(_dropTable.dropList[idx].itemPrefab.name) as Resource;
                

                resource.transform.position = transform.position;

                Action destroyAction = null;
                destroyAction = () =>
                {
                    resource.DestroyResource();
                    GameManager.Instance.OnClearAllDropItems -= destroyAction;

                };
                GameManager.Instance.OnClearAllDropItems += destroyAction;

                if (_dropEffect)
                {
                    Vector3 offset = Random.insideUnitSphere * 5f;
                    offset.y = 0;

                    resource.transform.DOJump(transform.position + offset, _dropPower, 1, 0.4f);
                }
                //ItemManager.instance._items.Add(resource);
            }
            // �ƴϸ� �ƹ��͵� �ȹ���.
        }

    }

    private int GetRandomWeightedIndex()
    {
        float sum = 0f;
        for(int i = 0; i < _itemWeights.Length; i++)
        {
            sum += _itemWeights[i]; //�̷��� ��� �������� ���Ȯ���� �ջ�ȴ�.
        }

        float randomValue = Random.Range(0, sum);
        float tempSum = 0;

        for(int i = 0; i < _itemWeights.Length; i++)
        {
            if(randomValue >= tempSum && randomValue < tempSum + _itemWeights[i])
            {
                return i;
            }else
            {
                tempSum += _itemWeights[i];
            }
        }

        return 0;
    }
}