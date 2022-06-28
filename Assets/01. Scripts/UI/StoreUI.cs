using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreUI : MonoBehaviour
{
    public Image up1;
    public Image up2;
    public Image up3;
    public Button buy;
    public PlayerTest _player;

    public Text rifle;
    public Text shotgun;
    public Text sniper;

    public Text t1;
    //[SerializeField] private Button storebutton;

    public int riflePrice;
    public int shotgunPrice;
    public int sniperPrice;

    [SerializeField]
    private int buyCount = 0;
    private void Awake()
    {
        //anim = GetComponent<Animator>();
        //gameObject.SetActive(false);

    }
    private void Start()
    {
        rifle.text = string.Format($"{riflePrice}");
        shotgun.text = string.Format($"{shotgunPrice}");
        sniper.text = string.Format($"{sniperPrice}");
    }

    public void BuyRifle()
    {
        if (MoneyUI.instance.MoneyValue >= riflePrice && buyCount < 2)
        {
            //buy.gameObject.SetActive(false);
            //HideUpgradePanel();
            _player.playerWeapon.AddWeapon(1);
            MoneyUI.instance.Addmoney(GameManager.Instance.Coin - riflePrice);
            buyCount++;
            up1.gameObject.SetActive(false);
            HideStorePanel();
            return;
            //up1.enabled = false;
        }
        t1.gameObject.SetActive(true);
            
    }
    public void BuyShotgun()
    {
        
        if (MoneyUI.instance.MoneyValue >= shotgunPrice && buyCount < 2)
        {
            _player.playerWeapon.AddWeapon(2);
            MoneyUI.instance.Addmoney(GameManager.Instance.Coin - shotgunPrice);
            //buy.gameObject.SetActive(false);
            buyCount++;
            up2.gameObject.SetActive(false);
            HideStorePanel();   
            return;
        }
        t1.gameObject.SetActive(true);

    }
           
    public void BuySniper()
    {
        
        if (MoneyUI.instance.MoneyValue >= sniperPrice && buyCount < 2)
        {
            _player.playerWeapon.AddWeapon(3);
            MoneyUI.instance.Addmoney(GameManager.Instance.Coin-sniperPrice);
            buyCount++;
            
            up3.gameObject.SetActive(false);
            HideStorePanel();
            return;
        }
        t1.gameObject.SetActive(true);
    }
    
    public void ShowStorePanel()
    {
        gameObject.SetActive(true);
        
        if (buyCount >= 2)
        {
            t1.gameObject.SetActive(true);
        }
        else
        {
            t1.gameObject.SetActive(false);
        }
    }
    
    public void HideStorePanel()
    {
        gameObject.SetActive(false);
        t1.gameObject.SetActive(false);
        
    }
}
