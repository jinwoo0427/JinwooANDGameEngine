using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class ItemCollector : MonoBehaviour
{
    private int _resourceLayer;
    private PlayerTest _player;
    public UpgradeUI upgrade;
    
    private void Awake()
    {
        _resourceLayer = LayerMask.NameToLayer("Resource");
        _player = GetComponent<PlayerTest>();
    }

    private void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.layer == _resourceLayer)
        {
            Resource res = collision.gameObject.GetComponent<Resource>();
            if(res != null)
            {
                int amount = res.ResourceData.GetAmount();
                PopupText(amount, res.ResourceData.popupTextColor);
                switch (res.ResourceData.ResourceType)
                {
                    case ResourceTypeEnum.Coin:            
                        GameManager.Instance.Coin += amount;
                        //Debug.Log("ÄÚÀÎ "+GameManager.Instance.Coin);
                        res.PickUpResource();
                        break;
                    case ResourceTypeEnum.Health:
                        _player.Health += res.ResourceData.GetAmount();
                        res.PickUpResource();
                        break;
                    case ResourceTypeEnum.Ammo:
                        _player.playerWeapon.TotalAmmo += res.ResourceData.GetAmount();
                        res.PickUpResource();
                        break;
                    case ResourceTypeEnum.ItemBag:
                        upgrade.ShowUpgradePanel();
                        res.PickUpResource();
                        break;
                    default:
                        break;
                }
            }
        }
    }
    
    private void PopupText(int amount, Color color)
    {
        DamagePopup dPopup = PoolManager.Instance.Pop("DamagePopup") as DamagePopup;
        dPopup?.Setup(amount, transform.position + new Vector3(0, 0.5f, 0), false, color);
    }
}
