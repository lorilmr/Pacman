using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject ContrlPanel;
    public GameObject TimerPanel;
    public GameObject ResultPanel;
    public GameObject WinPanel;
    public GameObject FailPanel;

    public Image TimerRender;
    public Sprite[] TimerSpriteArr;

    public Text RemainText;
    public Text EatenText;
    public Text ScoreText;

    public int RemainNum {
        get;
        private set;
    }

    private int EatenNum;
    private int ScoreNum;

    public void EatenDot() {
        RemainNum--;
        EatenNum++;
        ScoreNum++;
        UpResultUI();
    }
    private void UpResultUI() {
        RemainText.text = RemainNum.ToString();
        EatenText.text = EatenNum.ToString();
        ScoreText.text = ScoreNum.ToString();
    }
    public void ContrlPanelVisible(int idx) {
        ContrlPanel.SetActive(idx==1);
        TimerPanel.SetActive(idx == 2);
        ResultPanel.SetActive(idx == 3);
    }
    // Start is called before the first frame update
    void Start()
    {
        ContrlPanelVisible(1);
        RemainNum = GameObject.FindObjectsOfType<Pacdot>().Length;
        EatenNum=0;
        ScoreNum=0;
        UpResultUI();
    }
    public void OnQuitClick() {
        Application.Quit();
    }
    public void OnStartClick()
    {
        ContrlPanelVisible(2);
        StartCoroutine(PlayTimerAnimation());
    }
    IEnumerator PlayTimerAnimation() {
        for (int i=0;i<TimerSpriteArr.Length;i++) {
            TimerRender.sprite = TimerSpriteArr[i];
            yield return new WaitForSeconds(1f);
        }
        yield return new WaitForSeconds(1f);
        ContrlPanelVisible(3);
        GameManager.Instance.StartGame();
    }
    // Update is called once per frame
    public void ShowGameOverPanel(bool isWin) {
        WinPanel.SetActive(isWin);
        FailPanel.SetActive(!isWin);
    }
}
