using System.Collections;
using System.Collections.Generic;
using UnityEditor.EditorTools;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("#Game Info")]
    public int gold;
    public int diamond;

    [Header("#Player Info")]
    public int level;
    public int exp;
    public int[] nextExp = { 3, 5, 10, 30, 60, 100, 150, 210, 280, 360, 450, 600 };
    public int kill;

    [Header("#Game Info")]
    public int stage;

    [Header("#Stage Clear Info")]
    public int[] stageGoal = { 3, 5, 10, 30, 60, 100, 150, 210, 280, 360, 450, 600 };
    public bool isBossKilled;
    public bool isClear;

    [Header("#Stage Info")]
    public int maxStardust;
    public int stardust;
    public float gameTime;
    public bool isLive;

    [Header("#Game Object")]
    public Player player;
    public PrefabManager prefabManager;
    public Spawner spawner;
    public BulletManager[] bulletManager;
    public Bag bag;

    [Header("#UI")]
    public LevelUp uiLevelUp;
    public GameResult uiGameResult;


    private void Awake()
    {
        instance = this;
        Application.targetFrameRate = 60;
        //ó�� bag cnt=2�� ����
        bag.bagCnt = 2;
        //���� ������ �� bullet A Ȱ��ȭ
        uiLevelUp.choices[0].OnClick();
    }

    private void Update()
    {
        gameTime += Time.deltaTime;
    }

    private void LateUpdate()
    {
        //stage clear ���� Ȯ��
        if (kill >= stageGoal[Mathf.Min(stage, stageGoal.Length - 1)] && isBossKilled)
        {
            isClear = true;
        }
    }

    public void GetExp(int exp)
    {
        this.exp += exp;

        //exp Ȯ�� �� �߿� ���� ���� ��� >> �ְ� ����ġ�� �״�� ����ϵ��� ����
        if (this.exp >= nextExp[Mathf.Min(level, nextExp.Length - 1)])
        {
            this.exp -= nextExp[Mathf.Min(level, nextExp.Length - 1)];
            level++;
            uiLevelUp.Show();
        }
    }

    public void StageClear()
    {
        StartCoroutine(StageClearRoutine());
    }

    IEnumerator StageClearRoutine()
    {
        isLive = false;
        //�ʿ� �ִ� ���� ���� ���̱�
        //enemyCleaner.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        //�������� Ŭ���� ui �����ֱ� (�߰��� ȹ���� ��ȭ�� ���� �͵� ������� ��)
        //uiResult.gameObejct.SetActive(true);
        Stop();
    }

    //�ð� ����
    public void Stop()
    {
        isLive = false;
        //timeScale: ����Ƽ�� �ð� �ӵ�(����)
        Time.timeScale = 0;
        //uiJoy.localScale = Vector3.zero; //���̽�ƽ local scale �ʱ�ȭ
    }

    //�ð� ����
    public void Resume()
    {
        isLive = true;
        Time.timeScale = 1;
    }
}
