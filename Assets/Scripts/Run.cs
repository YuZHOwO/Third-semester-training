using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Run : MonoBehaviour
{
    private Rigidbody2D rb;
    private Rigidbody2D rbMap;
    private BoxCollider2D feet;
    private Animator animator;
    private PlayerCollision coll;
    private float direction;
    public float speed;
    public float jumpSpeed;
    public float slideSpeed;
    public float climbSpeed;
    private float slideTime = 0f;

    private float climbSpeed_r;
    private bool wallGrab;
    private bool wallClimb;
    public bool airAttack1;
    public bool airAttack2;
    public bool airAttack3;
    
    private bool isGround;
    private bool isFalling;
    private bool isSliding;
    private bool canDoubleJump;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        feet = GetComponent<BoxCollider2D>();
        rbMap= GameObject.FindGameObjectWithTag("MoonBackGround").GetComponent<Rigidbody2D>();
        coll = GetComponent<PlayerCollision>();
    }

    // Update is called once per frame
    void Update()
    {
        run();
        Jump();
        Flip();
        // Attack();
        Slide();
        CheckGround();
        CheckFall();
        Slide();
        animator.SetBool("Idle", isGround);
        rbMap.position = rb.position;           // 月亮背景跟随角色移动
        ClimbWall();
        Debug.Log(airAttack1);
    }

    private void FixedUpdate()
    {
 
    }


    //以下为函数部分
    void run()
    {  
        if(!isSliding)
        {
        direction = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(direction * speed, rb.velocity.y);
        bool Xspeed = Mathf.Abs(rb.velocity.x) > Mathf.Epsilon;
        animator.SetBool("Run", Xspeed);  
        }
    }
    public Vector2 GetPosition()
    {
        return rb.position;
    }

    void Flip()  //翻转动画
    {
        bool Xspeed = Mathf.Abs(rb.velocity.x) > Mathf.Epsilon;
        if (Xspeed)
        {
            if (rb.velocity.x > 0.01f)
            {
                transform.localRotation = Quaternion.Euler(0, 0, 0);
            }
            if (rb.velocity.x < -0.01f)
            {
                transform.localRotation = Quaternion.Euler(0, 180, 0);
            }
        }
    }
    void Jump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            if (isGround)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
                canDoubleJump = true;
                animator.SetBool("Jump", true);
            }
            else if(!isGround&&canDoubleJump)
            {
                Vector2 jump = new Vector2(rb.velocity.x, jumpSpeed);
                rb.velocity = jump;
                canDoubleJump = false;
                animator.SetBool("Jump", false);
                //animator.SetBool("Fall", false);
                animator.SetBool("DoubleJump", true);
            }
        }
       
    }

    void Slide()
    {
        startSlide();
        animator.SetBool("Slide",isSliding);
        if (isSliding)
        {
            
            rb.velocity=new Vector2 (slideSpeed* direction, rb.velocity.y);
            slideTime += Time.deltaTime;
            if (slideTime >= 1f)
                StopSlide();
        }
    }
    void startSlide()
    {
        if (Input.GetButtonDown("Slide") && !isSliding && isGround)
        {
            isSliding = true;
            slideTime = 0f;
        }
    }
    void StopSlide()
    {
        isSliding=false;
    }
    void CheckGround()
    {
        isGround = feet.IsTouchingLayers(LayerMask.GetMask("Ground"));
        wallGrab = false;
    }

    void CheckFall ()
    {
        if (!isGround && rb.velocity.y < -3f&&!wallClimb)
        {
            isFalling = true;
            animator.SetBool("Jump", false);
            animator.SetBool("DoubleJump", false);
        }
            
        else
            isFalling = false;
        animator.SetBool("Fall", isFalling);
    }  
    //爬墙

    void ClimbWall()
    {
        if (coll.onWall && Input.GetButton("Climb"))
            wallGrab = true;
        else
            wallGrab = false;
        if (wallGrab && Input.GetButton("Vertical"))
            wallClimb = true;
        else
            wallClimb = false;
        if (Input.GetKey(KeyCode.W))
            climbSpeed_r = climbSpeed;
        else
            climbSpeed_r = -climbSpeed;
        if (wallGrab && !wallClimb)
            rb.velocity = new Vector2(0, 0);
        else if (wallClimb)
            rb.velocity = new Vector2(0, climbSpeed_r);        
        animator.SetBool("GrabWall", wallGrab);
        animator.SetBool("ClimbWall", wallClimb);
    }

    void AirAttack()
    {
        rb.gravityScale = 2;
        animator.SetBool("AirAttack2", airAttack2);
        animator.SetBool("AirAttack3", airAttack3);
        if (!isGround && !wallGrab && !wallClimb) 
        {
            if (Input.GetButton("Attack"))
            {
                rb.gravityScale = 3;
                animator.SetBool("AirAttack1", true);
            }
        }
    }
}
