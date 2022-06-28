using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerWeapon : AgentWeapon
{
    //���߿� �÷��̾�� ���� ���ⱳü, ������, ������ �ڵ尡 ���� ���ɴϴ�.
    [SerializeField]
    private Weapon _currentWeapon = null; //���� ����
    public Weapon CurrentWeapon
    {
        get => _currentWeapon;
        set
        {
            _currentWeapon = value;
        }
    }
    #region ������ �� ��ü ����

    [SerializeField]
    private List<Weapon> _weaponList = new List<Weapon>();
    private PlayerTest _player;
    private int _currentWeaponIndex = 0;

    public UnityEvent<List<Weapon>, int> UpdateWeaponUI;
    public UnityEvent<bool, Action> ChangeWeaponUI;
    private bool _isChangeWeapon = false;
    #endregion


    public List<Weapon> _buyWeapon = new List<Weapon>();

    [field: SerializeField]
    public UnityEvent<int, int> OnChangeTotalAmmo { get; set; }  //���簪, �ִ밪

    [SerializeField] private ReloadGaugeUI _reloadUI = null;
    [SerializeField] private AudioClip _cannotSound = null; //�������� �ȵɶ� 


    [SerializeField] private int _maxTotalAmmo = 2000; //�ִ� 2000�߱��� ���� �� �־�
    [SerializeField] private int _totalAmmo = 200; //ó�� ���۽ÿ� 2000�� ������ ����

    public bool AmmoFull { get => _totalAmmo == _maxTotalAmmo; }
    public int TotalAmmo
    {
        get => _totalAmmo;
        set
        {
            _totalAmmo = Mathf.Clamp(value, 0, _maxTotalAmmo);
            OnChangeTotalAmmo?.Invoke(_totalAmmo, _maxTotalAmmo);
        }
    }

    private AudioSource _audioSource;

    

    private bool _isReloading = false;
    public bool IsReloading { get => _isReloading; }


    public override void AssignWeapon()
    {
        _weapon = _currentWeapon;
    }

    protected override void AwakeChild()
    {
        _audioSource = GetComponent<AudioSource>();
        _player = transform.parent.GetComponent<PlayerTest>();

    }

    protected void Start()
    {
        Weapon[] weapons = GetComponentsInChildren<Weapon>(); //�ڽ����� ���� ��� ������ �����´�.

        for (int idx = 0; idx < _player.PlayerStatus.maxWeapon; idx++)
        {
            if (weapons.Length <= idx)
            {
                _weaponList.Add(null);
            }
            else
            {
                _weaponList.Add(weapons[idx]);
                weapons[idx].gameObject.SetActive(false);
            }
        }

        if (_weaponList.Count > 0)
        {
            _currentWeapon = _weaponList[0];
            _currentWeapon.gameObject.SetActive(true);
            AssignWeapon();
            OnChangeTotalAmmo?.Invoke(_totalAmmo, _maxTotalAmmo);
        }
        _player.ChangeIk(CurrentWeapon.rightHandObj, CurrentWeapon.leftHandObj);
        UpdateWeaponUI?.Invoke(_weaponList, _currentWeaponIndex);
    }

    public void ReloadGun()
    {
        if (_weapon != null && !_isReloading && !_weapon.AmmoFull && !_player.Playermovement.IsRolling)
        {
            _isReloading = true;
            _weapon.StopShooting();
            _player.ChangeIk(CurrentWeapon.rightHandObj, null);
            _player.PlayerAnim.PlayReloadAnimation();
            //�ڷ�ƾ
            StartCoroutine(ReloadCoroutine());
        }
        else
        {
            PlayClip(_cannotSound);
        }
        
    }

    private void PlayClip(AudioClip clip)
    {
        _audioSource.Stop();
        _audioSource.clip = clip;
        _audioSource.Play();
    }

    IEnumerator ReloadCoroutine()
    {
        _reloadUI.gameObject.SetActive(true);
        float time = 0;
        while (time <= _weapon.WeaponData.reloadTime)
        {
            _reloadUI.ReloadGaugeNormal(time / _weapon.WeaponData.reloadTime);
            time += Time.deltaTime;
            yield return null;
        }
        _reloadUI.gameObject.SetActive(false);
        PlayClip(_weapon.WeaponData.reloadClip);

        //int reloadedAmmo = Mathf.Min(TotalAmmo, _weapon.EmptyBulletCnt);
        ////���� �ѿ� ������ �з��� ���� ��ź�߿� �����з����� �����ؼ�
        //TotalAmmo -= reloadedAmmo;

        _weapon.Ammo += CurrentWeapon.WeaponData.ammoCapacity;

        _isReloading = false;
        _player.ChangeIk(CurrentWeapon.rightHandObj, CurrentWeapon.leftHandObj);
    }

    public override void Shoot()
    {
        if (_player.Playermovement.IsRolling)
        {
            return;
        }
        if (_weapon == null)
        {
            PlayClip(_cannotSound);
            return;
        }
        if (_isReloading)
        {
            PlayClip(_weapon.WeaponData.outOfAmmoClip);
            return;
        }
        base.Shoot();
    }

    //�� �Լ��� xŰ�� ������ �� ����˴ϴ�. 
    public void AddWeapon(int num)
    {

        //if(_currentWeapon != _weaponList[0])
        //{
        //    _weaponList[0].gameObject.SetActive(true);
        //}
        //_currentWeapon.gameObject.SetActive(true);

        //_buyWeapon[num - 1].gameObject.SetActive(true);
        //_weaponList.Clear();

        //_weaponList.Add(_buyWeapon[num - 1]);
        _currentWeapon.gameObject.SetActive(false);
        if (_weaponList[1] == null)
        {
            _weaponList[1] = _buyWeapon[num - 1];
        }
        else
        {
            _weaponList[2] = _buyWeapon[num - 1];

        }


        //Weapon[] weapons = GetComponentsInChildren<Weapon>(); //�ڽ����� ���� ��� ������ �����´�.


        

        if (_weaponList.Count > 0)
        {
            _currentWeapon = _weaponList[0];
            _currentWeaponIndex = 0;
            _currentWeapon.gameObject.SetActive(true);
            AssignWeapon();
            OnChangeTotalAmmo?.Invoke(_totalAmmo, _maxTotalAmmo);
        }
        _player.ChangeIk(CurrentWeapon.rightHandObj, CurrentWeapon.leftHandObj);
        UpdateWeaponUI?.Invoke(_weaponList, _currentWeaponIndex);
        
    }

    //private void DropWeapon(Weapon weapon)
    //{
    //    _weaponList[_currentWeaponIndex] = null; //��������
    //    _weapon = null;
    //    _currentWeapon = null;
    //    weapon.StopShooting();
    //    weapon.transform.parent = null; //���忡�ٰ� ����������.

    //    //���� �������� �ѱ��������� �������� �ڵ带 �ۼ��Ҳ�
    //    Vector3 targetPosition = weapon.GetRightDirection() * 0.3f
    //                                        + weapon.transform.position;
    //    weapon.transform.rotation = Quaternion.identity;
    //    weapon.transform.localScale = Vector3.one;

    //    weapon.transform.DOMove(targetPosition, 0.5f).OnComplete(() =>
    //    {
    //        weapon.droppedWeapon.IsActive = true;
    //    });
    //}

    public void ChangeToNextWeapon(int isNum)
    {
        if (_isReloading || _weaponList.Count <= 1 || _isChangeWeapon == true || _player.Playermovement.IsRolling || isNum-1 == _currentWeaponIndex)
        {
            PlayClip(_cannotSound);
            Debug.Log("��ü �Ұ� ");
            return;
        }
        _isChangeWeapon = true;
        _currentWeapon?.gameObject.SetActive(false); //���� ��� �ִ� ���� ��Ȱ��ȭ ���ְ�
        int nextIdx = isNum - 1;
        bool isNext = true;
        //if (isNum - 1 < _currentWeaponIndex || _currentWeaponIndex == 1)
        //{
        //    nextIdx = _currentWeaponIndex - 1 < 0 ? _weaponList.Count - 1 : _currentWeaponIndex - 1;
        //}
        //else
        //{
        //    nextIdx = (_currentWeaponIndex + 1) % _weaponList.Count;
        //}
        if(isNum - 1 < _currentWeaponIndex)
        {
            isNext = false;
        }
        else if(isNum -1 > _currentWeaponIndex)
        {
            isNext = true;
        }

        if(isNum -1 == 0 && _currentWeaponIndex == 2)
        {
            isNext = true;
        }
        if(isNum -1 == 2 && _currentWeaponIndex == 0)
        {
            isNext = false;

        }

        ChangeWeaponUI?.Invoke(isNext, () =>
        {
            

            ChangeWeapon(_weaponList[nextIdx]);
            _currentWeaponIndex = nextIdx;

            _isChangeWeapon = false;
        });

    }

    private void ChangeWeapon(Weapon weapon)
    {
        _currentWeapon = weapon;
        if (weapon != null)
        {
            weapon.gameObject.SetActive(true);
            weapon.ResetWeapon();
            _player.ChangeIk(CurrentWeapon.rightHandObj, CurrentWeapon.leftHandObj);
        }
        AssignWeapon();
    }
}
