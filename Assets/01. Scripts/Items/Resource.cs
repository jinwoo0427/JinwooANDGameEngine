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

    //�ش� ���ҽ��� �־��� ��
    public void PickUpResource()
    {
        OnPickUpEvent?.Invoke();
        StartCoroutine(DestroyCoroutine());
    }

    IEnumerator DestroyCoroutine()
    {
        _audioSource.Play();
        _collider.enabled = false;
        yield return new WaitForSeconds(_audioSource.clip.length + 3f); //������� �߰��� �Ծ������ �ʰ�
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
