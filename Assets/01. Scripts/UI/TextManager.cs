using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextManager
{
    public static TextManager Instance;

    private string _currentLocale = "kr";
    private string _fallbackLocale = "eng";
    private Dictionary<string, Dictionary<string, string>> _localeDictionary = new Dictionary<string, Dictionary<string, string>>();

    //���߿� ���⿡�� ���Ϸκ��� �о �����ϰų�, ������ �о �����ϴ� ������� �����Ѵ�.
    public void Init()
    {
        //invincible
        Dictionary<string, string> krDictionary = new Dictionary<string, string>();
        krDictionary.Add("invincible", "����");
        _localeDictionary.Add("kr", krDictionary);

        Dictionary<string, string> engDictionary = new Dictionary<string, string>();
        engDictionary.Add("invincible", "Invincible");
        _localeDictionary.Add("eng", engDictionary);
    }  

    public string LocaleString(string key)
    {
        if(_localeDictionary[_currentLocale].ContainsKey(key)) {
            return _localeDictionary[_currentLocale][key];
        }
        //������ ����� ����
        return _localeDictionary[_fallbackLocale][key]; 
    }
}
