using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    protected int maxPH;
    private int healthPoint;
    //protected int damage;
    protected int coinNum;     //死亡后爆金币的数量
    private int hurtDamage;          //玩家的攻击伤害
    protected float hurtSpeed;     //被击退的退后速度
    private Vector2 hurtDiretion;   //角色朝向
    private bool isHurt;
    private AnimatorStateInfo info;
    private Animator anim;
    private Rigidbody2D rb;
    protected float cleanTime;
    private bool dead=false;
    public GameObject Coin;
    // Start is called before the first frame update
    public void Start()
    {
        healthPoint = maxPH;
        anim = GetComponent<Animator>();
        rb= GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    public void Update()
    {
        //Debug.Log(healthPoint);
        info = anim.GetCurrentAnimatorStateInfo(0);
        if(isHurt)
        {
            rb.velocity = hurtDiretion * hurtSpeed;
            if(info.normalizedTime>= 0.6f)
                isHurt = false;
        }
        CleanEnemy();
    }
    public void GetHurt(Vector2 direction)
    {
        if(!dead) {  
            hurtDamage = FindObjectOfType<PlayerController>().damage;
            transform.localScale = new Vector3(-direction.x, 1, 1);
            isHurt = true;
            hurtDiretion = direction;
            anim.SetTrigger("Hurt");
            healthPoint -= hurtDamage;
        }
    }
    void CleanEnemy()
    {
        
        if (healthPoint <= 0 && !dead)
        {
            anim.SetTrigger("Die");
            dead = true;
            for(int i = 0; i < coinNum; i++)
            {
                Instantiate(Coin,transform.position, Quaternion.identity);
            }
        }
        if (dead)
            cleanTime -= Time.deltaTime;
        if (cleanTime < 0)
        {
            Destroy(gameObject);
            dead = false;
        }
    }

}
