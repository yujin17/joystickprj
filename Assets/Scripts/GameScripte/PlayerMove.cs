using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    Animator anim;

    public GameObject AttackItem;
    public GameObject Fire;
    public bool AttackMode=false;
    public int AttackCnt=0;
    public int FireFlipX = 1;
    public int Speed;
    public int JumPow;
    public int JumpCnt = 0;
    // Start is called before the first frame update
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.X)&&AttackMode)
        {
            FireShot();
        }
        if (Input.GetButtonDown("Jump"))
        {
            if (JumpCnt < 1)
                rigid.AddForce(Vector2.up * JumPow, ForceMode2D.Impulse);
            JumpCnt++;
            anim.SetBool("isJump", true);
        }
        if (Input.GetButtonUp("Horizontal"))
        {
            rigid.velocity = new Vector2(rigid.velocity.normalized.x * 0.5f, rigid.velocity.y);
        }
        //방향전환 
        if (Input.GetButton("Horizontal"))
        {
            spriteRenderer.flipX = Input.GetAxisRaw("Horizontal") == -1;
            if (Input.GetAxisRaw("Horizontal") == -1)
                FireFlipX = -1;
            else if (Input.GetAxisRaw("Horizontal") == 1)
                FireFlipX = 1;
        }
        //애나메이션
        if (rigid.velocity.normalized.x == 0)
        {
            anim.SetBool("isWalk", false);
        }
        else
        {
            anim.SetBool("isWalk", true);
        }
    }
    void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");

        rigid.AddForce(Vector2.right * h, ForceMode2D.Impulse);

        if (rigid.velocity.x > Speed)
        {
            rigid.velocity = new Vector2(Speed, rigid.velocity.y);
        }
        else if (rigid.velocity.x < Speed * (-1))
        {
            rigid.velocity = new Vector2(Speed * (-1), rigid.velocity.y);
        }

        if (rigid.velocity.y < 0)
        {
            Debug.DrawRay(rigid.position, Vector3.down, new Color(0, 1, 0));
            RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, Vector3.down, 1, LayerMask.GetMask("Platform","ItemBox"));

            if (rayHit.collider != null)
            {
                if (rayHit.distance < 1.5f)
                    anim.SetBool("isJump", false);
                JumpCnt = 0;
            }
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag=="AttackItem")
        {
            AttackMode = true;
            Destroy(AttackItem);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {

      if(collision.gameObject.tag=="ItemBox")
        {
            AttackItem.SetActive(true);
        }
    }

    void FireShot()
    {
        GameObject fire = Instantiate(Fire, transform.position, Quaternion.Euler(0, 180, 0));
        Rigidbody2D rigid = fire.GetComponent<Rigidbody2D>();
        if (FireFlipX > 0)
            rigid.AddForce(Vector2.right * 15f, ForceMode2D.Impulse);
        else if (FireFlipX < 0)
            rigid.AddForce(Vector2.left * 15f, ForceMode2D.Impulse);

        Destroy(fire, 5f);
    }
}
