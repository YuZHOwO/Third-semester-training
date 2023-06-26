using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    [Header("���������ٶ�")]  //�趨�ϣ�����ʱ�������ƶ��������ṩ�����ٶ�����Եйֱ�����ʱ��׷��
    public float attackCompensateSpeed;
    [Space]
    private Rigidbody2D rb;
    private Rigidbody2D rbMap;          //�ĵص�ͼ���������
    private BoxCollider2D feet;
    private Animator animator;
    private PlayerCollision coll;

    private int damage;
    private float direction;        //�ܶ�����
    private float slideDirection;  //��������
    public float speed;
    public float jumpSpeed;
    public float slideSpeed;
    public float climbSpeed;
    private float slideTime = 0f;   //������ʱ��
    private int airAttack = 0;
    private float airTime = 0;
    public float attackSpeed;
    private float timeSpeed = 1f;
    private float climbSpeed_r;     //��ǽ�ٶȣ�������Input.GetButton("Vertical")ֻ������������˼���һ���жϷ��������
    private bool wallGrab;
    private bool wallClimb;
    private bool airAttack1;
    private bool airAttack2;
    private bool airAttack3;
    private bool isAttacking;
    private int isFacingRight=1;
    private int attackCombo;        //���������ʾ������������һ��
    private float timer;                //������������֮�����ļ�ʱ��
    private float attackInterval=2f;  //��������������ʱ����
    protected bool isGround;
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
        Direct();
        Slide();
        CheckGround();
        CheckFall();
        Slide();
        rbMap.position = rb.position;           // �������������ɫ�ƶ�
        ClimbWall();
        AirAttack();
        Attack();
        //Debug.Log(attackCombo);
    }

    private void FixedUpdate()
    {
 
    }


    //����Ϊ��������
    void run()
    {
        animator.SetBool("Idle", isGround);
        if (!isSliding&&!isAttacking)
        {
        direction = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(direction * speed, rb.velocity.y);
        bool Xspeed = Mathf.Abs(rb.velocity.x) > Mathf.Epsilon;
        animator.SetBool("Run", Xspeed);  
        }
    }
   void Direct()
    {
        if (Input.GetKeyDown(KeyCode.D))
            isFacingRight = 1;
        if (Input.GetKeyDown(KeyCode.A))
            isFacingRight = -1;
    }
    void Flip()  //��ת����
    {
        bool Xspeed = Mathf.Abs(rb.velocity.x) > Mathf.Epsilon;
        if (Xspeed&&!wallGrab)
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
        if (Input.GetButtonDown("Jump")&&!isSliding)
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
        if(!isSliding)      //������;��ֹת��
            slideDirection = Input.GetAxisRaw("Horizontal");
        if (isSliding)
        {
                  //���̻�ȡ�����ʾ�����֣���ֹ���ٻ���
            rb.velocity=new Vector2 (slideSpeed* slideDirection, rb.velocity.y);
            slideTime += Time.deltaTime;
            if (slideTime >= 1f)    //����ʱ�����
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
    //��ǽ

    void ClimbWall()
    {
        if (coll.onWall && Input.GetButton("Climb")&& airAttack==0)//���乥������ץǽ��
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
        if (Input.GetButtonDown("Attack") && !isGround)//�ڿ������¹���
            airAttack = 1;
        if (airAttack == 1)//��һ��Ϊ�ν����ν���һ��ʱ��Ϊ�ڶ��Σ���������
        {
            airTime += Time.deltaTime;
            if (airTime > 0.3f)
            {
                rb.gravityScale = 6f;
                airAttack = 2;
                airTime = 0;
            }
        }
        if (isGround && airAttack == 2)//��غ�������������,ն��
        {
            airAttack = 3;
        }
        if (airAttack == 3)                 //��ع�һ��ʱ����˳����乥��
        {
            airTime += Time.deltaTime;
            if (airTime > 0.2f)
            {
                airAttack = 0;
                airTime = 0;
                rb.gravityScale = 2f;
            }
        }
        animator.SetInteger("AirAttack", airAttack);
    }
    void Attack()
    {
        if(Input.GetButtonDown("Attack") && isGround&&!isAttacking)
        {
            isAttacking = true;
            attackCombo++;
            if (attackCombo > 3)
                attackCombo = 1;
            timer = attackInterval;
            rb.velocity = new Vector2(transform.localScale.x * attackCompensateSpeed* isFacingRight, rb.velocity.y);
            animator.SetTrigger("Attack");
            animator.SetInteger("AttackCombo", attackCombo);
        }

        if (timer > 0)
        {
            timer -= Time.deltaTime;
            if(timer<=0)
            {
                timer = 0;
                attackCombo = 0;        //���ι������ʱ��̫����������������
            }
        }
    }

    void AttackEnd()            //�ŵ��������ﵱ�����¼�
    {
        isAttacking = false;
    }
    private void OnTriggerEnter2D(Collider2D other)  //����ײ�����봥����ʱ���Զ�����
    {
        if (other.CompareTag("Enemy"))
        {
            if (isFacingRight==1)
                other.GetComponent<Enemy>().GetHurt(Vector2.right);
            else if(isFacingRight == -1)
                other.GetComponent<Enemy>().GetHurt(Vector2.left);

        }
    }
}
