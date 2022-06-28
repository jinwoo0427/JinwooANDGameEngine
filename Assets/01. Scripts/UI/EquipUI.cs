using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class EquipUI : MonoBehaviour
{
    [SerializeField] private EquipSlot _gunPanelPrefab;
    private List<EquipSlot> _panelList;

    [SerializeField] private AudioClip _changeClip;
    [SerializeField] private float _transitionTime = 0.2f;

    private AudioSource _audioSource;

    [Header("초기 위치값")]
    [SerializeField] private Vector2 _initAnchorPos;
    [SerializeField]
    private float _xDelta = 7f;

    public int _currentWeaponIdx = 0;
    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _panelList = new List<EquipSlot>();
    }

    public void InitUIPanel(List<Weapon> weaponList, int nowIndex)
    {
        List<Weapon> cloneList = weaponList.ToList();
        //리스트를 복제하는거. 다만 안에 있는 원소는 참조로 복제된다.

        for (int i = 0; i < nowIndex; i++)
        {
            Weapon first = cloneList.First();
            cloneList.Remove(first);
            cloneList.Add(first);
        }

        cloneList.Reverse();
        _panelList.Clear();

        for (int i = 0; i < cloneList.Count; i++)
        {
            EquipSlot panel = null;
            if (i < transform.childCount)  //재활용
            {
                panel = transform.GetChild(i).GetComponent<EquipSlot>();
            }
            else  //새로 생성
            {
                panel = Instantiate(_gunPanelPrefab, transform);
            }

            RectTransform rectTrm = panel.GetComponent<RectTransform>();
            rectTrm.anchoredPosition = _initAnchorPos + new Vector2((cloneList.Count - i - 1) * _xDelta, 0);

            //if (i != cloneList.Count - 1)
            //{
            //    rectTrm.localScale = Vector3.one * 0.9f;
            //}

            panel.Init(cloneList[i]);
            
            _panelList.Add(panel);
        }
        _panelList.Reverse();

        //ConnectAmmoTextEvent(); //텍스트 연결
    }



    //private void ConnectAmmoTextEvent()
    //{
    //    EquipSlot first = _panelList.First(); //지금 활성화되어있는 녀석
    //    first.Weapon?.OnAmmoChange.AddListener((amount) =>
    //    {
    //        first.UpdateBullet(amount);
    //    });
    //}

    #region 무기 변경 UI 닷트윈
    public void ChangeWeaponUI(bool isNext, Action CallBack = null)
    {
        EquipSlot first = _panelList.First();
        EquipSlot last = _panelList.Last();
        EquipSlot next = _panelList[1];

        first.Weapon?.OnAmmoChange.RemoveAllListeners(); //첫번째 무기의 리스너 제거


        Sequence seq = DOTween.Sequence();
        //맨 뒤에게 제일 앞으로옴 q
        if (!isNext)
        {

            Debug.Log("이전거로 교체");
            seq.Append(first.RectTrm.DOScale(Vector3.one * 0.9f, _transitionTime));
            seq.Join(first.RectTrm.DOAnchorPos(_initAnchorPos + new Vector2(_xDelta, 0), _transitionTime));
            for (int i = 1; i < _panelList.Count - 1; i++)
            {
                seq.Join(_panelList[i].RectTrm.DOAnchorPos(
                    _initAnchorPos + new Vector2(_xDelta * (i + 1), 0),
                    _transitionTime));
            }
            seq.Join(last.RectTrm.DOScale(Vector3.one, _transitionTime));
            seq.Join(last.RectTrm.DOAnchorPos(_initAnchorPos + new Vector2(0, 82), _transitionTime));

            seq.AppendCallback(() =>
            {
                last.RectTrm.SetAsLastSibling();
                _panelList.RemoveAt(_panelList.Count - 1);
                _panelList.Insert(0, last); //맨 앞으로
            });

            seq.Append(last.RectTrm.DOAnchorPos(_initAnchorPos, _transitionTime));

        }
        //바로 뒤에 것이 앞으로옴 (현재 것은 무조건 뒤로 감) e  
        else
        {
            Debug.Log("다음거로 교체");
            seq.Append(first.RectTrm.DOScale(Vector3.one * 0.9f, _transitionTime));
            seq.Join(first.RectTrm.DOAnchorPos(_initAnchorPos + new Vector2(0, 82), _transitionTime));

            seq.Join(next.RectTrm.DOScale(Vector3.one, _transitionTime));
            seq.Join(next.RectTrm.DOAnchorPos(_initAnchorPos, _transitionTime));

            for (int i = 2; i < _panelList.Count; i++)
            {
                seq.Join(_panelList[i].RectTrm.DOAnchorPos(
                    _initAnchorPos + new Vector2(_xDelta * (i - 1), 0),
                    _transitionTime));
            }

            seq.AppendCallback(() =>
            {
                first.RectTrm.SetAsFirstSibling(); //첫번째 자식으로 설정한다.
                _panelList.RemoveAt(0);
                _panelList.Add(first);
            });

            seq.Append(first.RectTrm.DOAnchorPos(
                _initAnchorPos + new Vector2(_xDelta * (_panelList.Count - 1), 0),
                _transitionTime));



        }

        seq.AppendCallback(() =>
        {
            //ConnectAmmoTextEvent(); //변경된 무기로 이벤트 연결
            CallBack?.Invoke();
        });


    }
    #endregion
}
