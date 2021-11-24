using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockHead : MonoBehaviour
{
    Rigidbody2D rigid;
    Color color;
    // Start is called before the first frame update
    void Awake()
    {
       rigid  = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //�Ʒ��������� ���̽�� �������� �߷½����� ���� 
        RockHeadMove();

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        //�ٴڿ� ������ �������� ����
        if(collision.gameObject.tag=="Platform")
        {
            gameObject.layer = 6;
            gameObject.tag = "Platform";
        }
    }

    void RockHeadMove()
    {
        Vector2 downVec = new Vector2(rigid.position.x, rigid.position.y - 2f);
        Vector3 DownRayLength = new Vector3(0, -5, 0);
        Debug.DrawRay(downVec, DownRayLength, new Color(0, 1, 0));
        RaycastHit2D rayHit = Physics2D.Raycast(downVec, Vector3.down, 5f, LayerMask.GetMask("Player"));
        if (rayHit.collider != null)
        {
            rigid.gravityScale = 3;
            Debug.Log("�÷��̾� ����");
        }
    }
}
