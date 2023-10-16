using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stardust : MonoBehaviour
{
    Rigidbody2D rigid;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    public void Get()
    {
        if (GameManager.instance.stardust == GameManager.instance.maxStardust)
            return;

        Invoke("GetStardust", 2f);
    }

    public void GetStardust()
    {
        Vector3 targetPos = GameManager.instance.player.transform.position;
        Vector3 dir = targetPos - transform.position;
        dir = dir.normalized;

        rigid.velocity = dir * 15f;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
            return;
        if (GameManager.instance.stardust == GameManager.instance.maxStardust)
            return;

        GameManager.instance.stardust++;
        gameObject.SetActive(false);
    }
}
