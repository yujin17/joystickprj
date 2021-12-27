using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WPlant : MonoBehaviour
{
    CapsuleCollider2D capsulecol;
    Rigidbody2D rigid;
    SpriteRenderer spriterd;

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriterd = GetComponent<SpriteRenderer>();
        capsulecol = GetComponent<CapsuleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
