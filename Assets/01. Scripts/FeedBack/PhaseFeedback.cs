using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;
using System.Collections;

public class PhaseFeedback : FeedBack
{
    private SkinnedMeshRenderer _meshRenderer = null;
    [SerializeField]
    private float _duration = 0.05f;

    [SerializeField]
    private float _waitTime = 0.1f;

    [SerializeField]
    private Material _flashMat = null;

    private Material _originalMatShader;

    public UnityEvent DeathCallback;

    private void Awake()
    {
        _meshRenderer = transform.parent.Find("VisualMesh").GetComponent<SkinnedMeshRenderer>();
        _originalMatShader = _meshRenderer.material; //���� ���̴��� �����صΰ�
    }

    public override void CompletePrevFeedBack()
    {
        _meshRenderer.DOComplete();
        _meshRenderer.material.DOComplete();
        _meshRenderer.material.SetFloat("_SplitValue", 3);
        _meshRenderer.material = _originalMatShader;
    }

    public override void CreateFeedBack()
    {

        _meshRenderer.material = _flashMat; //���� ��Ƽ����� ��ü
        
        StartCoroutine(WaitBeforeChangingBack());
    }

    IEnumerator WaitBeforeChangingBack()
    {
        yield return new WaitForSeconds(_waitTime);
        //������ �־��µ� �����׿�..
        Sequence seq = DOTween.Sequence();
        seq.Append(_meshRenderer.material.DOFloat(0, "_SplitValue", _duration));
        if (DeathCallback != null)
        {
            seq.AppendCallback(() => DeathCallback.Invoke());
        }
    }


}
