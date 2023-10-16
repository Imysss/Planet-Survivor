using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    //Player Basic Status
    public float speed;
    public float damage;
    public float health;
    public float maxHealth;

    //Player Move
    public Vector2 inputVec;

    //Game Object
    public Scanner scanner;
    public ItemScanner itemScanner;

    //Basic Object
    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    Animator anim;

    private void Start()
    {
        health = maxHealth;    
    }

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        scanner = GetComponent<Scanner>();
        itemScanner = GetComponent<ItemScanner>();
    }

    private void OnEnable()
    {
        //anim.runtimeAnimatorController = animCon[GameManager.instance.playerId];
    }
    
    void OnMove(InputValue value)
    {
        inputVec = value.Get<Vector2>();
    }
    
    private void FixedUpdate()
    {
        //Character Move
        Vector2 nextVec = inputVec * speed * Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + nextVec);
    }

    private void LateUpdate()
    {
        //Character Move Flip
        if (inputVec.x != 0)
        {
            spriteRenderer.flipX = inputVec.x < 0;
        }

        //Character Animation
        //anim.SetFloat("Speed", inputVec.magnitude);
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!collision.CompareTag("Enemy"))
            return;

        health -= (collision.gameObject.GetComponent<Enemy>().damage * Time.deltaTime);

        if (health <= 0)
        {
            //게임 종료
        }
    }


}
