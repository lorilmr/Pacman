using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public UIManager uiManager;

    private const float WAIT_RELOAD = 2f;
    private const float SUPER_DOT_TIME = 8f;

    public bool isStartGame {
        get;
        private set;
    }
    void Awake()
    {
        Screen.SetResolution(1136,640,false);
        Instance = this;
        isStartGame = false;
    }
    public void EatenDot(bool isSuperDot)
    {
        uiManager.EatenDot();
        if (uiManager.RemainNum <= 0) {
            GameOver(true);
            return;
        }
        if (isSuperDot) {
            StartCoroutine(BornSuperDot());
            AddBuff();
        }
    }
    private void AddBuff() {
        MonsterAI[] allMonster = GameObject.FindObjectsOfType<MonsterAI>();
        foreach (var item in allMonster)
        {
            item.DebuffAdded();
        }
        StartCoroutine(RemoveBuff());
    }
    IEnumerator RemoveBuff() {
        yield return new WaitForSeconds(3f);
        MonsterAI[] allMonster = GameObject.FindObjectsOfType<MonsterAI>();
        foreach (var item in allMonster)
        {
            item.DebuffRemove();
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Y)) {
            StartGame();
        }
    }

    public void StartGame() {
        isStartGame = true;
        StartCoroutine(BornSuperDot());
    }
    public void GameOver(bool isWin)
    {
        isStartGame = false;
        Debug.Log("游戏结束");
        uiManager.ShowGameOverPanel(isWin);
        Invoke("ReLoadScene", WAIT_RELOAD);

    }
    void ReLoadScene()
    {
        SceneManager.LoadScene(0);
    }
    private IEnumerator BornSuperDot() {
        yield return new WaitForSeconds(SUPER_DOT_TIME);
        Pacdot[] allDots = GameObject.FindObjectsOfType<Pacdot>();
        if (allDots.Length < 50) {
            yield break;
        }
        Pacdot superDot = allDots[Random.Range(0, allDots.Length)];
        superDot.MakeToSuper();    
    }
}
