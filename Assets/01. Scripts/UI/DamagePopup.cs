using UnityEngine;
using DG.Tweening;
using TMPro;

public class DamagePopup : PoolableMono
{
    private TextMeshPro _textMesh;

    private void Awake()
    {
        _textMesh = GetComponent<TextMeshPro>();
    }

    public void Setup(int damageAmount, Vector3 pos, bool isCritical, Color color)
    {
        transform.position = pos;
        _textMesh.SetText(damageAmount.ToString());

        if(isCritical)
        {
            _textMesh.color = Color.red;
            _textMesh.fontSize = 12f;
        }else
        {
            _textMesh.color = color;
        }

        ShowingSequence();
        
    }
    public void Setup(string text, Vector3 pos, Color color, float fontSize = 10f)
    {
        transform.position = pos;
        _textMesh.SetText(text);
        _textMesh.color = color;
        _textMesh.fontSize = fontSize;

        ShowingSequence();
    }

    private void ShowingSequence()
    {
        int randomDir = Random.Range(-1, 2);
        Vector3 endV = new Vector3(transform.position.x+randomDir, transform.position.y - 1.5f, transform.position.z);
        Sequence seq = DOTween.Sequence();
        //seq.Append(transform.DOMoveY(transform.position.y + 1.5f, 1f));
        transform.DOJump(endV, 2f, 1, 1f, false);


        //seq.Append(transform.DOMoveX(transform.position.x + randomDir, 1f));
        seq.Append(_textMesh.DOFade(0, 1f));
        seq.AppendCallback(() =>
        {
            PoolManager.Instance.Push(this);
        });
    }

    public override void ResetObject()
    {
        _textMesh.color = Color.white;
        _textMesh.fontSize = 7f;
    }

}
