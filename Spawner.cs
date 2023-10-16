using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform[] spawnPoint;
    public EnemyData[] enemyData;
    public BossData[] bossData;

    float timer;
    public bool isBossSpawn = false;

    private void Awake()
    {
        spawnPoint = GetComponentsInChildren<Transform>();
    }

    private void Update()
    {
        timer += Time.deltaTime;

        //타이머가 일정 시간 값에 도달하면 소환
        if (timer > enemyData[GameManager.instance.stage].spawnTime)
        {
            timer = 0;
            Spawn();
        }

        //boss가 spawn되었는지 확인 후 보스 소환
        if (isBossSpawn)
            return;
        int curKill = GameManager.instance.kill;
        int goalKill = GameManager.instance.stageGoal[GameManager.instance.stage];
        if(curKill >= goalKill)
        {
            SpawnBoss();
        }
    }

    void Spawn()
    {
        GameObject enemy = GameManager.instance.prefabManager.Get(0);
        enemy.transform.position = spawnPoint[Random.Range(1, spawnPoint.Length)].position;
        //생성되는 적의 프리펩은 stage 레벨에 따라 달라짐
        enemy.GetComponent<Enemy>().Init(enemyData[GameManager.instance.stage]);
    }

    void SpawnBoss()
    {
        Debug.Log("Spawn Boss");
        GameObject boss = GameManager.instance.prefabManager.Get(1);
        boss.transform.position = spawnPoint[Random.Range(1, spawnPoint.Length)].position;
        //생성되는 적의 프리펩은 stage 레벨에 따라 달라짐
        boss.GetComponent<Boss>().Init(bossData[GameManager.instance.stage]);
        isBossSpawn = true;
    }    
}
