using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpactScript : PoolableMono
{
    private AudioSource _audioSoruce;

    

    private void Awake()
    {
        _audioSoruce = GetComponent<AudioSource>();
        ChildAwake();
    }

    protected virtual void ChildAwake()
    {
        //do nothing here!
    }

    public virtual void SetPositionAndRotation(Vector3 pos, Quaternion rot)
    {
        transform.SetPositionAndRotation(pos, rot);
        if(_audioSoruce != null && _audioSoruce.clip != null)
        {
            _audioSoruce.Play();
        }

        StartCoroutine(DestroyAfterAnimation());
    }


    IEnumerator DestroyAfterAnimation()
    {
        yield return new WaitForSeconds(0.5f);
        PoolManager.Instance.Push(this);
    }
    public virtual void SetLocalScale(Vector3 scale)
    {
        transform.localScale = scale;
    }
    public override void ResetObject()
    {
        transform.localRotation = Quaternion.identity;
    }
}
