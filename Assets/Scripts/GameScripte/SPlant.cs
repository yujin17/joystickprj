using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SPlant : MonoBehaviour
{
    Rigidbody2D rigid;

    public GameObject bullet;
    // Start is called before the first frame update
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        Invoke("Fire", 2);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
    }

    void Fire()
    {
        GameObject Bullet = Instantiate(bullet, transform.position, transform.rotation);
        Rigidbody2D rigid = Bullet.GetComponent<Rigidbody2D>();
        rigid.AddForce(Vector2.left * 10,ForceMode2D.Impulse);
        Destroy(Bullet, 2);

        Invoke("Fire", 2);


    }
}
