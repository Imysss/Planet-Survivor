using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[System.Serializable]
public class EnemyData
{
    public int spriteId;

    public float spawnTime;

    public float speed;
    public int health;
    public float damage;
    public int exp;
}

public class Enemy : MonoBehaviour
{
    //Enemy Basic Status
    bool isLive;
    public float speed;
    public float health;
    public float maxHealth;
    public float damage;
    public int exp;

    //Enemy information according to stage
    public RuntimeAnimatorController[] animCon;

    //Enemy Target (Player)
    public Rigidbody2D target;

    //Basic
    Rigidbody2D rigid;
    Collider2D coll;
    SpriteRenderer spriteRenderer;
    Animator anim;
    WaitForFixedUpdate wait;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        wait = new WaitForFixedUpdate();
    }

    private void FixedUpdate()
    {
        if (!isLive || anim.GetCurrentAnimatorStateInfo(0).IsName("Hit"))
            return;

        //Specify the direction of movement of the enemy
        Vector2 dirVec = target.position - rigid.position;
        Vector2 nextVec = dirVec.normalized * speed * Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + nextVec);
        //Remove speed so that physical speed does not even affect movement
        rigid.velocity = Vector2.zero;  
    }

    private void LateUpdate()
    {
        if (!isLive)
            return;
        spriteRenderer.flipX = target.position.x < rigid.position.x;
    }

    private void OnEnable()
    {
        //Initialize generated Enemy
        target = GameManager.instance.player.GetComponent<Rigidbody2D>();
        isLive = true;
        coll.enabled = true;
        rigid.simulated = true;
        spriteRenderer.sortingOrder = 2;
        //anim.SetBool("Dead", false);
        health = maxHealth;
    }

    public void Init(EnemyData data)
    {
        anim.runtimeAnimatorController = animCon[data.spriteId];
        speed = data.speed;
        damage = data.damage;
        exp = data.exp;
        maxHealth = data.health;
        health = data.health;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //조건
        if (!collision.CompareTag("Bullet"))
            return;

        health -= collision.GetComponent<Bullet>().damage;

        //넉백 실행
        StartCoroutine(KnockBack());

        //죽었는지 맞은 건지 확인
        if (health > 0)
        {
            Debug.Log("Hit");
            //anim.SetTrigger("Hit");
        }
        else
        {
            isLive = false;
            coll.enabled = false;
            rigid.simulated = false;
            spriteRenderer.sortingOrder = 1;
            //anim.SetBool("Dead", true);
            //몬스터 죽으면 경험치 및 킬 수 증가
            GameManager.instance.kill++;
            GameManager.instance.GetExp(exp);
            //임시
            DropStardust();
            Invoke("Dead", 0.001f);
        }
    }

    IEnumerator KnockBack()
    {
        yield return wait;
        Vector3 playerPos = GameManager.instance.player.transform.position;
        Vector3 dirVec = transform.position - playerPos;
        rigid.AddForce(dirVec.normalized * 3, ForceMode2D.Impulse);
    }

    void DropStardust()
    {
        Transform stardust = GameManager.instance.prefabManager.Get(2).transform;
        stardust.position = transform.position;
    }

    void Dead()
    {
        gameObject.SetActive(false);
    }
}
