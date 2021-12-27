using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    Animator anim;
    AudioSource audioSource;

    public GameManager gameManager;
    public GameObject AttackItem;
    public GameObject Fire;
    public AudioClip audioJump;
    public AudioClip audioAttack;
    public AudioClip audioDamaged;
    public AudioClip audioItem;
    public AudioClip audioFinish;
    public AudioClip audioDie;
    
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
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //공격
        if(Input.GetKeyDown(KeyCode.X)&&AttackMode)
        {
            FireShot();
        }
        //점프
        if (Input.GetButtonDown("Jump"))
        {
            PlayerJum();
            PlaySound("JUMP");
        }
        //이동
        if (Input.GetButtonUp("Horizontal"))
        {
            rigid.velocity = new Vector2(rigid.velocity.normalized.x * 0.5f, rigid.velocity.y);
        }
        //방향전환 
        if (Input.GetButton("Horizontal"))
        {
            PlayerFilpX();
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
        Playermove();

        PlayerJumFall();
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        //에너미 공격 
        if (col.gameObject.tag == "TurtleEnemy"||col.gameObject.tag=="Enemy")
        {
            if (rigid.velocity.y < 0 && transform.position.y > col.transform.position.y)
            {
                OnAttack(col.transform);
            }
            else //damaged
            {
                PlayerOnDamaged(col.transform.position);//충돌시 플레이어 포지션값넘김 
            }
        }
        if(col.gameObject.tag=="TutleSpike")
        {
            PlayerOnDamaged(col.transform.position);
        }

        if (col.gameObject.tag=="AttackItem")
        {
            AttackMode = true;
            Destroy(AttackItem);
        }

        //if(collision.gameObject.tag=="Enemy")
        //{
        //    PlayerOnDamaged(gameObject.transform.position);
        //}
    }

    void OnTriggerEnter2D(Collider2D collision)
    {

      if(collision.gameObject.tag=="ItemBox")
        {
            AttackItem.SetActive(true);
        }
      else if(collision.gameObject.tag=="Finish")
        {
            gameManager.NextStage();
        }
    }

  
    void Playermove()
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
    }
    void PlayerFilpX()
    {
        spriteRenderer.flipX = Input.GetAxisRaw("Horizontal") == -1;
        if (Input.GetAxisRaw("Horizontal") == -1)
            FireFlipX = -1;
        else if (Input.GetAxisRaw("Horizontal") == 1)
            FireFlipX = 1;
    }
    void PlayerJum()
    {
        if (JumpCnt < 1)
            rigid.AddForce(Vector2.up * JumPow, ForceMode2D.Impulse);
        JumpCnt++;
        anim.SetBool("isJump", true);

    }

    void PlayerJumFall()
    {
        if (rigid.velocity.y < 0)
        {
            Debug.DrawRay(rigid.position, Vector3.down, new Color(0, 1, 0));
            RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, Vector3.down, 1, LayerMask.GetMask("Platform", "ItemBox"));

            if (rayHit.collider != null)
            {
                if (rayHit.distance < 1.5f)
                    anim.SetBool("isJump", false);
                JumpCnt = 0;
            }
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

    void PlayerOnDamaged(Vector2 PlayerPos)
    {
        //공격모드풀림
        AttackMode = false;
        //목숨감소
        gameManager.health--;
        //change layer 
        gameObject.layer = 3;
        //view alpa
        spriteRenderer.color = new Color(1, 1, 1, 0.4f);
        //보는 방향에 따른 피격시 튕기는 힘 방향조절 
        if (FireFlipX == 1)
        {
            int dirc = transform.position.x - PlayerPos.x > 0 ? 1 : -1;
            rigid.AddForce(new Vector2(dirc, 1) * 7, ForceMode2D.Impulse);
        }
        else
        {
            int dirc = transform.position.x + PlayerPos.x > 0 ? 1 : -1;
            rigid.AddForce(new Vector2(dirc, 1) * 7, ForceMode2D.Impulse);
        }
        Invoke("PlayerOffDamaged", 1);

    }
    void PlayerOffDamaged()
    {
        gameObject.layer = 8;
        spriteRenderer.color = new Color(1, 1, 1, 1);

    }

    void OnAttack(Transform enemy)
    {
        RinoMove rinoMove = enemy.GetComponent<RinoMove>();
        TurtleMove turtleMove = enemy.GetComponent<TurtleMove>();//다른클래스


        //Reaction Force 
        rigid.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
        //Enemy Die
        if (enemy.tag == "TurtleEnemy")
        {
            turtleMove.OnDamagedforDie();
        }
        else if(enemy.tag=="Enemy")
        {
            rinoMove.OnDamagedforDie();
        }
      
        PlaySound("ATTACK");
        
       
    }

    void PlaySound(string action)
    {
        switch(action)
        {
            case "JUMP":
                audioSource.clip = audioJump;
                break;
            case "ATTACK":
                audioSource.clip = audioAttack;
                break;
            case "DAMAGED":
                audioSource.clip = audioDamaged;
                break;
            case "ITEM":
                audioSource.clip = audioItem;
                break;
            case "DIE":
                audioSource.clip = audioDie;
                break;
            case "FINISH":
                audioSource.clip = audioJump;
                break;
        }
        audioSource.Play();

    }

    public void VelocityZero()
    {
        rigid.velocity = Vector2.zero;
    }

}
