using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;
using DG.Tweening;

public class GameUIManager : MonoBehaviour
{
    private static GameUIManager instance;

    public static GameUIManager Instance
    {
        get
        {
            if (instance == null) instance = FindObjectOfType<GameUIManager>();

            return instance;
        }
    }

    [SerializeField] private GameObject gameoverUI;
    [SerializeField] private GameObject gameClearUI;
    [SerializeField] private GameObject pausePanel;
    private int pauseCnt = 0;

    
    [SerializeField] private Text waveText;

    private RectTransform _uiCanvasTrm = null;
    private RectTransform _bossHealthBarTrm = null;
    private BossHealthBar _bossHealthBar = null;
    public float _bossHealthAnchorY = -150f;
    private void Awake()
    {

        _uiCanvasTrm = GameObject.Find("GameCanvas").GetComponent<RectTransform>();
        _bossHealthBarTrm = _uiCanvasTrm.Find("bottomPanel/BossHPBar").GetComponent<RectTransform>();
        _bossHealthBar = _bossHealthBarTrm.GetComponent<BossHealthBar>();

        _bossHealthBarTrm.anchoredPosition = new Vector2(0, _bossHealthAnchorY);
        pausePanel.SetActive(false);
        pauseCnt = 0;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(pauseCnt == 0)
            {
                pausePanel.SetActive(true);
                pauseCnt = 1;
                Time.timeScale = 0;
            }
            else
            {
                pausePanel.SetActive(false);
                pauseCnt = 0;
                Time.timeScale = 1;
            }
        }
    }
    public void TimeCtrl()
    {
        Time.timeScale = 0;
    }
    public void ResumeButton()
    {
        pausePanel.SetActive(false);
        pauseCnt = 0;
        Time.timeScale = 1;
    }
    public void UpdateWaveText(int waves)
    {
        waveText.text = "Wave : " + waves;
    }


    public void SetActiveGameoverUI(bool active)
    {
        gameoverUI.SetActive(active);
    }

    public void GameRestart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void EnteringBoss(Boss boss)
    {
        //카메라 위치 조절
        //보스 체력바 등장
        Sequence seq = DOTween.Sequence();
        _bossHealthBar.SetHealthBar(0);
        seq.Append(_bossHealthBarTrm.DOAnchorPos(new Vector3(0,-30,0), 0.5f));
        seq.AppendCallback(() =>
        {
            _bossHealthBar.InitHealthBar(boss.OnDamaged);
        });

    }
}
