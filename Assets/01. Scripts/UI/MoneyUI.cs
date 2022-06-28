using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyUI : MonoBehaviour
{
    public static MoneyUI instance;
    //public Text currentmoney;
    public Text moneyComma;
    private int moneyValue;
    public int MoneyValue
    {
        get => moneyValue;
        set
        {
            moneyValue = value; 
        }
    }
    private void Awake()
    {
        instance = this;
        moneyValue = 0;
        
        //currentmoney = transform.Find("Money").GetComponent<Text>();
    }
    public void Addmoney(int money)
    {
        moneyValue = money;

        //currentmoney.text = moneyValue.ToString();
        moneyComma.text = GetThousandCommaText(moneyValue).ToString();
    }
    public string GetThousandCommaText(int data)
    {
        
        return string.Format("{0:#,###}", data);
    }
}
