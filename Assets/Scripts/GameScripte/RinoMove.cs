using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RinoMove : MonoBehaviour
{
    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    public int nextmove;
    // Start is called before the first frame update
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        Invoke("Think", 5);
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

        Vector2 PlayerDetect = new Vector2(rigid.position.x + nextmove * 5f, rigid.position.y);
        Debug.DrawRay(PlayerDetect, Vector3.down, new Color(0, 1, 0));
        RaycastHit2D rayHitDectedPlayer = Physics2D.Raycast(PlayerDetect, Vector3.down, 1, LayerMask.GetMask("Player"));
        if(rayHitDectedPlayer.collider !=null)
        {
            if (nextmove > 0)
                rigid.AddForce(Vector2.right * 10f, ForceMode2D.Impulse);
            else if (nextmove < 0)
                rigid.AddForce(Vector2.left * 10f, ForceMode2D.Impulse);
        }
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
