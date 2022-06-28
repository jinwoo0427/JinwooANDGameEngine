    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(AudioSource))]
public class Resource : PoolableMono
{
    [field: SerializeField]
    public ResourceDataSO ResourceData { get; set; }

    private AudioSource _audioSource;
    private Collider _collider;
    public UnityEvent OnPickUpEvent;


    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.clip = ResourceData.useSound;
        _collider = GetComponent<Collider>();
    }

    //해당 리소스를 주었을 때
    public void PickUpResource()
    {
        OnPickUpEvent?.Invoke();
        StartCoroutine(DestroyCoroutine());
    }

    IEnumerator DestroyCoroutine()
    {
        _audioSource.Play();
        _collider.enabled = false;
        yield return new WaitForSeconds(_audioSource.clip.length + 3f); //오디오가 중간에 먹어버리지 않게
        gameObject.SetActive(false);
        PoolManager.Instance.Push(this);
    }

    public void DestroyResource()
    {
        gameObject.SetActive(false);
        PoolManager.Instance.Push(this);

    }

    public override void ResetObject()
    {
        gameObject.SetActive(true);
        _collider.enabled = true;
    }

}
