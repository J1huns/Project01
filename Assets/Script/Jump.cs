using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{
    public int jumpPower = 10;
    Rigidbody2D rigid;
    bool isGrounded = false;
    [Header("점프 중력")] public float jumpingGravity = 3;
    [Header("낙하 중력")] public float fallingGravity = 8;

    private void Start()
    {
        rigid = gameObject.GetComponent<Rigidbody2D>();
    }



    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && isGrounded)               //땅에 닿아있을 때, 스페이스를 눌렀다면, 
        {
            rigid.AddForce(Vector2.up * jumpPower , ForceMode2D.Impulse);
        }

        GroundCheck();
        GractiyContorller();

    }

    //
    public void JumpButton()
    {
        if(isGrounded)
        {
            rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
        }
    }
    //

    void GroundCheck()
    {
        Debug.DrawRay(new Vector2(transform.position.x - 0.4f, transform.position.y), Vector2.down * 0.6f, Color.red);
        Debug.DrawRay(new Vector2(transform.position.x + 0.4f, transform.position.y), Vector2.down * 0.6f, Color.red);

        bool ray1 = false;
        bool ray2 = false;

        if (Physics2D.Raycast(new Vector2(transform.position.x - 0.4f, transform.position.y),
                                Vector2.down,
                                0.6f,
                                LayerMask.GetMask("Wall", "Cannon"))) ray1 = true;
        if (Physics2D.Raycast(new Vector2(transform.position.x + 0.4f, transform.position.y),
                                Vector2.down,
                                0.6f,
                                LayerMask.GetMask("Wall", "Cannon"))) ray2 = true;

        if (!ray1 && !ray2)
        { isGrounded = false; }
        else
        { isGrounded = true; }
    }
    
    void GractiyContorller()
    {
        if (!isGrounded)
        {
            if (rigid.velocity.y > 0)
                rigid.gravityScale = jumpingGravity;
            else if (rigid.velocity.y <= 0)
                rigid.gravityScale = fallingGravity;
        }
    }
    
}
