using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    public int bulletId;
    public int bulletLevel;
    public BulletData[] bulletData;

    float timer;

    private void Update()
    {
        if (bulletId < 0)
            return;


        timer += Time.deltaTime;
        if (timer > bulletData[bulletLevel].summonSpeed)
        {
            timer = 0;
            for (int i = 0; i < bulletData[bulletLevel].count; i++)
            {
                Shot();
            }
        }
    }
    
    void Shot()
    {
        if (GameManager.instance.player.scanner.targets.Length == 0)
            return;

        //총알이 나가는 방향 계산
        int rand = Random.Range(0, GameManager.instance.player.scanner.targets.Length);

        Vector3 targetPos = GameManager.instance.player.scanner.targets[rand].transform.position;
        Vector3 dir = targetPos - transform.position;
        dir = dir.normalized;

        Transform bullet = GameManager.instance.prefabManager.Get(3 + bulletId).transform;
        bullet.parent = transform;
        bullet.position = transform.position;
        bullet.rotation = Quaternion.FromToRotation(Vector3.up, dir);
        //생성되는 bullet은 bullet Id에 따라 달라짐
        bullet.GetComponent<Bullet>().Init(bulletId, bulletData[bulletLevel], dir);
    }
}
