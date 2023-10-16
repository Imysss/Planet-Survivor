using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BulletData
{
    public float damage;
    public int per;
    public float summonSpeed;
    public int count;
}

public class Bullet : MonoBehaviour
{
    public float damage;
    public int per;
    public float summonSpeed;
    public int count;
    public Sprite[] bulletSprtie;

    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Init(int bulletId, BulletData data, Vector3 dir)
    {
        //bullet id�� ��ȭ�� ���� bullet sprite�� �޶���
        spriteRenderer.sprite = bulletSprtie[GameManager.instance.bulletManager[bulletId].bulletLevel];

        this.damage = data.damage;
        this.per = data.per;
        this.summonSpeed = data.summonSpeed;
        this.count = data.count;

        //�ӷ��� �����־� �Ѿ��� ���ư��� �ӵ� ����
        rigid.velocity = dir * 15f;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Enemy"))
        {
            return;
        }
        per--;
        if (per <= 0)
        {
            //��Ȱ��ȭ ���� �̸� ���� �ӵ� �ʱ�ȭ
            rigid.velocity = Vector2.zero;
            gameObject.SetActive(false);
        }
    }
}
