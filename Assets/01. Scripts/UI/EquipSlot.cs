using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EquipSlot : MonoBehaviour
{
    [SerializeField] private Weapon _weapon;
    public Weapon Weapon { get => _weapon; }

    private Image _weaponImage;

    private RectTransform _rectTrm = null;
    private float _initFontSize;
    private Color _trasparentColor = new Color(0, 0, 0, 0);

    public RectTransform RectTrm
    {
        get
        {
            if (_rectTrm == null)
                _rectTrm = GetComponent<RectTransform>();
            return _rectTrm;
        }
    }

    private void Awake()
    {
        _weaponImage = transform.Find("GunImage").GetComponent<Image>();
        _rectTrm = GetComponent<RectTransform>();
    }

    public void Init(Weapon weapon)
    {
        _weapon = weapon;
        if (_weapon != null)
        {
            _weaponImage.sprite = weapon.WeaponData.sprite;
            _weaponImage.color = Color.white;
        }
        else
        {
            _weaponImage.sprite = null;
            _weaponImage.color = _trasparentColor;
        }
    }

   
}
