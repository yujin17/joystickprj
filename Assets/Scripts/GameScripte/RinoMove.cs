using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RinoMove : MonoBehaviour
{
    //  public GameObject player;
    Transform target = null;
    float enemyMoveSpeed = 3f;
    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    public int nextmove;
    // Start is called before the first frame update
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
       // Invoke("Think", 5);
    }

    void Update()
    {
        if (target != null)
        {
            Vector2 dir = target.position - transform.position;
            transform.Translate(dir.normalized * enemyMoveSpeed * Time.deltaTime);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //움직이기
        rigid.velocity = new Vector2(nextmove, rigid.velocity.y);

        //낭떠러지 있을시 돌기
        Vector2 frontVec = new Vector2(rigid.position.x +nextmove*1.5f, rigid.position.y);
        Debug.DrawRay(frontVec, Vector3.down, new Color(0, 1, 0));
        RaycastHit2D rayHit = Physics2D.Raycast(frontVec, Vector3.down, 1, LayerMask.GetMask("Platform"));
        if (rayHit.collider == null)
            Turn();

       


        //박치기 원리 잘모르겠음 
        //Vector2 dir = (player.transform.position - transform.position).normalized;

        //float acceleration = 2.5f;
        //float velocity =  acceleration * Time.deltaTime;
        //float distance = Vector3.Distance(player.transform.position, transform.position);

        //if(distance <= 10.0f)
        //{
        //    transform.position = new Vector2(transform.position.x + (dir.x * velocity), transform.position.y);
        //}

    }

     void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag =="Player")
        {
            target=col.gameObject.transform;  //태그가 플레이어면 콜라이션의 게임오브젝트를 타겟에저장 
            Debug.Log("target found");
        }
    }
    void OnTriggerExit2D(Collider2D col)
    {
        target = null;
        Debug.Log("target lost");
    }
   

    void Think()
    {
        //다음동작
        nextmove = Random.Range(-1, 2);

        //애니메이션

        //플립x
        if (nextmove != 0)
            spriteRenderer.flipX = nextmove == 1;

        //재귀
        float nextThinkTime = Random.Range(2f, 5f);
        Invoke("Think", nextThinkTime);
    }
    void Turn()
    {
        nextmove *= -1;
        spriteRenderer.flipX = nextmove == 1;
        CancelInvoke();
        Invoke("Think", 5);
    }
    
}
