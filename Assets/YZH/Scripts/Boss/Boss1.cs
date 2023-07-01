using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Boss1 : MonoBehaviour
{
    public Transform target;
    private Animator anim;
    public int HP;
    public int damage;
    public float meleeAttackDistance;  //近战攻击距离
    public float moveSpeed;
    private bool lasering;
    private bool move;
    private int hurtTime;  
    public float intTimer;//近战攻击间隔
    public float laserTimeInterval;  //激光发射间隔
    public float laserTime;   //一次激光发射的持续时间
    public int laserDamage;
    private float timer; //计时器
    private float distance; //人物和Boss的距离
    public GameObject hand;         //技能之一
    public Transform handPos;
    private bool dead;
    public float cleanTime=1f;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!dead)
        {
        distance =Vector2.Distance(transform.position, target.position);
        Move();
        Flip();
        Laser();
        Debug.Log(HP);
        lasering = FindObjectOfType<Laser>().laserMode;
        MeleeAttack(); 
        }
        Die();
    }
    void Move()
    {
        if(distance> meleeAttackDistance&&!lasering&&target.position.x>203)
            transform.position = Vector2.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
        //Debug.Log(target.position.x);
    }
    void Flip()         //Boss转向
    {
        if(!lasering) { 
        Vector2 rotation = transform.eulerAngles;
        if (transform.position.x > target.position.x)
            rotation.y = 180f;
        else
            rotation.y = 0f;
        transform.eulerAngles = rotation;
        }
    }

    void HandShoot()
    {
        Instantiate(hand, handPos.transform.position, transform.rotation);
    }

    void MeleeAttack()
    {
        if(distance<= meleeAttackDistance)
        {
            intTimer-=Time.deltaTime;
            if(intTimer < 0.1f)
            {
                anim.SetTrigger("MeleeAttack");
                intTimer = 4f;
            }
        }
    }

    void Laser()
    {
        if (HP < 200)
        {
            laserTimeInterval-=Time.deltaTime;
            if(laserTimeInterval<0)
            {
                laserTimeInterval=8f;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        //
        if (other.CompareTag("PlayerWeapon"))
        {
            HP -= FindObjectOfType<PlayerController>().damage;
        }
    }

    void Die()
    {
        if (HP <= 0&&!dead)
        {
            anim.SetTrigger("Die");
            dead = true;
        }
        if(dead)
        {
            cleanTime-=Time.deltaTime;
            if (cleanTime < 0)
                anim.SetBool("Death", true);
        }
            
    }
}
