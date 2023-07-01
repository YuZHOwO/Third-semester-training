using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    [Header("攻击补偿速度")]  //设定上，攻击时不允许移动，所以提供补偿速度满足对敌怪被击退时的追击
    public float attackCompensateSpeed;
    [Space]
    protected Rigidbody2D rb;
    private BoxCollider2D feet;
    private Animator animator;
    private PlayerCollision coll;
    AudioSource Aud;
    public AudioClip attack;
    public AudioClip hitTarget;
    public float HP;
    public float EP;
    private bool isHurt;
    private bool dead;
    public float cleanTime;
    internal int damage;        //攻击力
    internal int hurtDamage;     //受击
    private float direction;        //跑动朝向
    private float slideDirection;  //滑铲朝向
    public float speed;
    public float jumpSpeed;
    public float slideSpeed;
    public float climbSpeed;
    private float slideTime = 0f;   //滑铲计时器
    private int airAttack = 0;
    private float airTime = 0;
    private float climbSpeed_r;     //爬墙速度，但是用Input.GetButton("Vertical")只能往上爬，因此加了一个判断方向的数字
    private bool wallGrab;
    private bool wallClimb;
    private bool isAttacking;
    private int isFacingRight = 1;
    private int attackCombo;                //这个用来表示三连击打到了哪一下
    private float timer;                          //两次连续攻击之间间隔的计时器
    private float attackInterval = 2f;      //两次连续攻击的最大时间间隔
    protected bool isGround;
    private bool isFalling;
    private bool isSliding;
    private bool canDoubleJump;

    //-----------------------
    public static int coinNum=1000;
    public static int attackNum=2;
    public static int defendNum;
    public static float maxHP =50;
    public static float maxEP=50 ;
    public static int taskNum;
    public static int deathNum;
    public static int redBottleNum=2;
    public static int blueBottleNum=2;
    public static void turn0()
    {
        coinNum = 0;
        attackNum = 2;
        defendNum = 0;
        maxHP = 100;
        maxEP = 50;
        taskNum = 0;
        deathNum = 0;
        redBottleNum = 0;
        blueBottleNum = 0;

    }
    // Start is called before the first frame update
    void Start()
    {
        HP = maxHP;
        EP = maxEP;
        damage = attackNum;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        feet = GetComponent<BoxCollider2D>();
        coll = GetComponent<PlayerCollision>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!dead)
        {
            run();
            Jump();
            Flip();
            Direct();
            Slide();
            CheckGround();
            CheckFall();
            Slide();
            ClimbWall();
            AirAttack();
            Attack();
            Debug.Log(HP);
            if(Input.GetKeyDown(KeyCode.E)) DrinkBlood();
            if(Input.GetKeyDown(KeyCode.R)) DrinkEnergy();
        }
        cleanPlayer();
        //Debug.Log("isAttacking:"+isAttacking);
    }

    private void FixedUpdate()
    {

    }

    //以下为函数部分
    void run()
    {
        animator.SetBool("Idle", isGround);
        if (!isSliding && !isAttacking)
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
    void Flip()  //翻转动画
    {
        bool Xspeed = Mathf.Abs(rb.velocity.x) > Mathf.Epsilon;
        if (Xspeed && !wallGrab)
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
        if (Input.GetButtonDown("Jump") && !isSliding)
        {
            if (isGround)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
                canDoubleJump = true;
                animator.SetBool("Jump", true);
            }
            else if (!isGround && canDoubleJump)
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
        
        animator.SetBool("Slide", isSliding);
        if (!isSliding)      //滑铲中途禁止转向
            slideDirection = Input.GetAxisRaw("Horizontal");
        if (isSliding)
        {
            //立刻获取方向表示的数字，防止低速滑铲
            rb.velocity = new Vector2(slideSpeed * slideDirection, rb.velocity.y);
            slideTime += Time.deltaTime;
            if (slideTime >= 1f)    //滑铲时间结束
                StopSlide();
        }
    }
    void startSlide()
    {
        if (Input.GetButtonDown("Slide") && !isSliding && isGround && EP >= 10&&rb.velocity.x!=0)
        {
            EP -= 10;
            isSliding = true;
            slideTime = 0f;
        }
    }
    void StopSlide()
    {
        isSliding = false;
    }
    void CheckGround()
    {
        isGround = feet.IsTouchingLayers(LayerMask.GetMask("Ground"));
        wallGrab = false;
    }

    void CheckFall()
    {
        if (!isGround && rb.velocity.y < -3f && !wallClimb)
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
        if (coll.onWall && Input.GetButton("Climb") && airAttack == 0)//下落攻击不许抓墙！
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
        if (Input.GetButtonDown("Attack") && !isGround)//在空中摁下攻击
            airAttack = 1;
        if (airAttack == 1)//第一段为拔剑，拔剑过一段时间为第二段，下落蓄力
        {
            airTime += Time.deltaTime;
            if (airTime > 0.3f)
            {
                rb.gravityScale = 6f;
                airAttack = 2;
                airTime = 0;
            }
        }
        if (isGround && airAttack == 2)//落地后进入第三个动画,斩击
        {
            airAttack = 3;
        }
        if (airAttack == 3)                 //落地过一段时间后退出下落攻击
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
        if (Input.GetButtonDown("Attack") && isGround && !isAttacking)
        {
            AudioSource.PlayClipAtPoint(attack, transform.position);
            isAttacking = true;
            attackCombo++;
            if (attackCombo > 3)
                attackCombo = 1;
            timer = attackInterval;
            rb.velocity = new Vector2(transform.localScale.x * attackCompensateSpeed * isFacingRight, rb.velocity.y);
            animator.SetTrigger("Attack");
            animator.SetInteger("AttackCombo", attackCombo);
        }

        if (timer > 0)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                timer = 0;
                attackCombo = 0;        //两次攻击间隔时间太长，攻击次序清零
            }
        }
    }

    void AttackEnd()            //放到动画机里当动画事件
    {
        isAttacking = false;
    }
    private void OnTriggerEnter2D(Collider2D other)  //有碰撞器进入触发器时会自动调用
    {
        if (other.CompareTag("Enemy"))
        {
            AudioSource.PlayClipAtPoint(hitTarget, transform.position);
            if (isFacingRight == 1)
                other.GetComponent<Enemy>().GetHurt(Vector2.right);
            else if (isFacingRight == -1)
                other.GetComponent<Enemy>().GetHurt(Vector2.left);
        }
    }
    public void HurtPlayer(int damage)
    {
        if (!dead && !isSliding)
        {
            HP -= damage;
            animator.SetTrigger("Hurt");
        }
    }

    void cleanPlayer()
    {
        //
        if (HP <= 0 && !dead)
        {
            animator.SetTrigger("Die");
            dead = true;
        }
        if (dead)
        {
            cleanTime -= Time.deltaTime;
            SceneManager.LoadScene(14);
        }

        if (cleanTime < 0)
        {
            animator.SetBool("Death", true);
        }
    }
    public float GetMaxHP()
    {
        return maxHP;
    }
    public float GetMaxEP()
    {
        return maxEP;
    }
    public void GetCoin()
    {
        coinNum++;
    }
    public int CoinNum()
    {
        return coinNum;
    }
    public void DrinkBlood()
    {
        if (redBottleNum > 0)
        {
            redBottleNum--;
            if (HP + 30 > maxHP) HP = maxHP;
            else HP += 30;

        }
    }
    public void DrinkEnergy()
    {
        if (blueBottleNum > 0)
        {
            blueBottleNum--;
            if (EP + 30 > maxEP) EP = maxEP;
            else EP += 30;

        }
    }
}
