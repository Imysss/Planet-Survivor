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
        //처음 bag cnt=2로 설정
        bag.bagCnt = 2;
        //게임 시작할 때 bullet A 활성화
        uiLevelUp.choices[0].OnClick();
    }

    private void Update()
    {
        gameTime += Time.deltaTime;
    }

    private void LateUpdate()
    {
        //stage clear 조건 확인
        if (kill >= stageGoal[Mathf.Min(stage, stageGoal.Length - 1)] && isBossKilled)
        {
            isClear = true;
        }
    }

    public void GetExp(int exp)
    {
        this.exp += exp;

        //exp 확인 둘 중에 작은 값을 사용 >> 최고 경험치를 그대로 사용하도록 변경
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
        //맵에 있는 몬스터 전부 죽이기
        //enemyCleaner.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        //스테이지 클리어 ui 보여주기 (추가로 획득한 재화들 같은 것도 보여줘야 됨)
        //uiResult.gameObejct.SetActive(true);
        Stop();
    }

    //시간 정지
    public void Stop()
    {
        isLive = false;
        //timeScale: 유니티의 시간 속도(배율)
        Time.timeScale = 0;
        //uiJoy.localScale = Vector3.zero; //조이스틱 local scale 초기화
    }

    //시간 가동
    public void Resume()
    {
        isLive = true;
        Time.timeScale = 1;
    }
}
