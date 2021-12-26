using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RinoMove : MonoBehaviour
{
    //  public GameObject player;
    Transform target = null;
    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    CapsuleCollider2D capsulecollider;
  
    public int nextmove;
    float enemyMoveSpeed = 4f;

    // Start is called before the first frame update
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        capsulecollider = GetComponent<CapsuleCollider2D>();
       Invoke("Think", 5);
    }

    void Update()
    {
        //�ڽ� �ݶ��̴� �� �÷��̾� ���� �ȴٸ�
        if (target != null)
        {
            Vector2 dir = target.position - transform.position; 
            if (dir.x > 0) //�÷��̾ ���̳뺸�� ���̸�
            {
                spriteRenderer.flipX = true;
                //�̵���Ŵ 
                transform.Translate(dir.normalized * enemyMoveSpeed * Time.deltaTime);

            }
            else //������ 
            {
                spriteRenderer.flipX = false;
                transform.Translate(dir.normalized * enemyMoveSpeed * Time.deltaTime);

            }
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //�����̱�
        rigid.velocity = new Vector2(nextmove, rigid.velocity.y);

        //�������� ������ ����
        Vector2 frontVec = new Vector2(rigid.position.x +nextmove*1.5f, rigid.position.y);
        Debug.DrawRay(frontVec, Vector3.down, new Color(0, 1, 0));
        RaycastHit2D rayHit = Physics2D.Raycast(frontVec, Vector3.down, 1, LayerMask.GetMask("Platform"));
        if (rayHit.collider == null)
            Turn();

       

    }
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Fire")
        {
            OnDamagedforDie();
        }
    }
    //Ʈ���ſ� �÷��� ������ Ÿ�ٿ� �÷��̾� ���� 
    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag =="Player")
        {
            target=col.gameObject.transform;  //�±װ� �÷��̾�� �ݶ��̼��� ���ӿ�����Ʈ�� Ÿ�ٿ����� 
            Debug.Log("rino found target");
        }
    }

    //Ʈ���ſ� ������ Ÿ���� ���.
    void OnTriggerExit2D(Collider2D col)
    {
        target = null;
        CancelInvoke();
        Think();
        Debug.Log("rino lost target");
    }
   

    void Think()
    {
        //��������
        nextmove = Random.Range(-1, 2);

        //�ִϸ��̼�

        //�ø�x
        if (nextmove != 0)
            spriteRenderer.flipX = nextmove == 1;

        //���
        float nextThinkTime = Random.Range(2f, 5f);
        Invoke("Think", nextThinkTime);
    }
    void Turn()
    {
        nextmove *= -1;
        spriteRenderer.flipX = nextmove == 1;
        CancelInvoke();
        //Invoke("Think", 5);
    }

    public void OnDamagedforDie()
    {

        Debug.Log("���̳��ױ����");
            //Sprite Alpha
            spriteRenderer.color = new Color(1, 1, 1, 0.4f);
            //Sprite Flip Y
            spriteRenderer.flipY = true;
            //Colider Disable
            capsulecollider.enabled = false;
            //Die Effect Jump
            rigid.AddForce(Vector2.up * 5, ForceMode2D.Impulse);
            //Destroy 
            Invoke("DeActive", 5);
        
    }
    void DeActive()
    {
        gameObject.SetActive(false);
    }


    

        //��������2
       
    
}
