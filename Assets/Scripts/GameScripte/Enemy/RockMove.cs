using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockMove : MonoBehaviour
{
    Rigidbody2D rigid;
    CapsuleCollider2D capsulecol;
    Animator anim;
    SpriteRenderer spriterd;

    public int nextmove;
    // Start is called before the first frame update
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        capsulecol = GetComponent<CapsuleCollider2D>();
        anim = GetComponent<Animator>();
        spriterd = GetComponent<SpriteRenderer>();

        Think();
    }

    // Update is called once per frame
    
    private void Update()
    {
        if (rigid.velocity.normalized.x == 0)
        {
            anim.SetBool("isWalk", false);
        }
        else
        {
            anim.SetBool("isWalk", true);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //�����̱�
        rigid.velocity = new Vector2(nextmove, rigid.velocity.y);

        //�������� ������ ����
        Vector2 frontVec = new Vector2(rigid.position.x + nextmove * 1.5f, rigid.position.y);
        Debug.DrawRay(frontVec, Vector3.down, new Color(0, 1, 0));
        RaycastHit2D rayHit = Physics2D.Raycast(frontVec, Vector3.down, 1, LayerMask.GetMask("Platform"));
        if (rayHit.collider == null)
            Turn();
    }

    void Think()
    {
        //��������
        nextmove = Random.Range(-1, 2);

        //�ִϸ��̼�

        //�ø�x
        if (nextmove != 0)
            spriterd.flipX = nextmove == 1;

        //���
        float nextThinkTime = Random.Range(2f, 5f);
        Invoke("Think", nextThinkTime);
    }

    //��
    void Turn()
    {
        nextmove *= -1;
        spriterd.flipX = nextmove == 1;
        CancelInvoke();
        Invoke("Think", 5);
    }
    //������� 
    public void OnDamagedforDie()
    {

        //Sprite Alpha
        spriterd.color = new Color(1, 1, 1, 0.4f);
        //Sprite Flip Y
        spriterd.flipY = true;
        //Colider Disable
        capsulecol.enabled = false;
        //Die Effect Jump
        rigid.AddForce(Vector2.up * 5, ForceMode2D.Impulse);
        //Destroy 
        Invoke("DeActive", 5);

    }
    void DeActive()
    {
        gameObject.SetActive(false);
    }
}
