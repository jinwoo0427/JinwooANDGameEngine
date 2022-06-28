using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Define
{
    private static Camera _mainCam = null;
    private static CinemachineFreeLook _cmVCam = null;
    
    public static Camera MainCam
    {
        get
        {
            if(_mainCam == null)
            {
                _mainCam = Camera.main;
            }
            return _mainCam;
        }
    }

    public static CinemachineFreeLook VCam
    {
        get
        {
            if(_cmVCam == null)
            {
                _cmVCam = GameObject.FindObjectOfType<CinemachineFreeLook>();
            }
            return _cmVCam;
        }
    }

    public enum ResourceTypeEnum
    {
        None,
        Health,
        Ammo,
        Coin,
        Money,
        ItemBag
    }

    
}
