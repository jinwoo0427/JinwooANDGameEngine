using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using DG.Tweening;

public class UpgradeUI : MonoBehaviour
{
    public Button up1;
    public Button up2;
    public Button up3;
    public PlayerTest _player;

    public Text up1T;
    public Text up2T;
    public Text up3T;

    private void Awake()
    {

    }
    
    public UnityAction CriticalDamageUp()
    {

        
        return () => {
            _player.PlayerStatus.criticalMaxDmg += 3;
            _player.PlayerStatus.criticalMinDmg += 3;
        };
    }
    public UnityAction CriticalUp()
    {
        
        return () => _player.PlayerStatus.critical += 0.1f;
    }
    public UnityAction StaminaUp()
    {
        return () => _player.PlayerStatus.maxStamina += 15;
    }
    public UnityAction HpHeal()
    {
        return () => _player._playerHealthbar.Heal(20f);
    }
    public UnityAction MaxHpUp()
    {

        return ()=>_player._playerHealthbar.MaxHpUp(15f);
    }
    public void ShowUpgradePanel()
    {
        //print("패널 업");
        gameObject.SetActive(true);
        //Sequence seq = DOTween.Sequence();
        //seq.Append(transform.DOMoveX(0.5f, 0.5f));

        //seq.AppendCallback(() => Time.timeScale = 0);

        Time.timeScale = 0;
        int randomButton = Random.Range(1, 5);
        
        switch (randomButton)
        {
            case 0:
                up1.onClick.AddListener(MaxHpUp());
                up1T.text = string.Format("최대체력 강화");
                up2.onClick.AddListener(HpHeal());
                up2T.text = string.Format("체력 회복");
                up3.onClick.AddListener(StaminaUp());
                up3T.text = string.Format("스테미나 강화");
                break;
            case 1:
                up1.onClick.AddListener(HpHeal());
                up1T.text = string.Format("체력 회복");
                up2.onClick.AddListener(StaminaUp());
                up2T.text = string.Format("스테미나 강화");
                up3.onClick.AddListener(MaxHpUp());
                up3T.text = string.Format("최대체력 강화");
                break;
            case 2:
                up1.onClick.AddListener(StaminaUp());
                up1T.text = string.Format("스테미나 강화");
                up2.onClick.AddListener(HpHeal());
                up2T.text = string.Format("체력 회복");
                up3.onClick.AddListener(CriticalUp());
                up3T.text = string.Format("크리티컬 확률 강화");
                break;
            case 3:
                up1.onClick.AddListener(CriticalDamageUp());
                up1T.text = string.Format("크리티컬 공격력 강화");
                up2.onClick.AddListener(HpHeal());
                up2T.text = string.Format("체력 회복");
                up3.onClick.AddListener(CriticalUp());
                up3T.text = string.Format("크리티컬 확률 강화");
                break;
            case 4:
                up1.onClick.AddListener(CriticalUp());
                up1T.text = string.Format("크리티컬 확률 강화");
                up2.onClick.AddListener(StaminaUp());
                up2T.text = string.Format("스테미나 강화");
                up3.onClick.AddListener(HpHeal());
                up3T.text = string.Format("체력 회복");
                break;
            
        }

    }
    public void TimeCtrl()
    {
        Time.timeScale = 0;
    }
    public void HideUpgradePanel()
    {
        Time.timeScale = 1;
        gameObject.SetActive(false);
        up1.onClick.RemoveAllListeners();
        up2.onClick.RemoveAllListeners();
        up3.onClick.RemoveAllListeners();
    }

}
