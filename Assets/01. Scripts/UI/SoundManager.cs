using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    public AudioSource _audio;
    public AudioClip _uiClip;

    private void Awake()
    {
        instance = this;
        _audio = GetComponent<AudioSource>();
    }
    public void ClickUI()
    {
        _audio.clip = _uiClip;
        _audio.Play();
    }


}
