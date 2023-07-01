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
    protected int coinNum;     //�����󱬽�ҵ�����
    private int hurtDamage;          //��ҵĹ����˺�
    protected float hurtSpeed;     //�����˵��˺��ٶ�
    private Vector2 hurtDiretion;   //��ɫ����
    private bool isHurt;
    private AnimatorStateInfo info;
    private Animator anim;
    private Rigidbody2D rb;
    protected float cleanTime;
    private bool dead=false;
    public GameObject Coin;
    private int anger=0;
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
            rb.velocity = hurtDiretion * hurtSpeed;         //����
            if(info.normalizedTime>= 0.6f)
                isHurt = false;
        }
        CleanEnemy();
    }
    public void GetHurt(Vector2 direction)
    {
        if(!dead) {
            hurtDamage = FindObjectOfType<PlayerController>().damage;
            //transform.localScale = new Vector2(-direction.x, 1);
            //Debug.Log("ת��");
            isHurt = true;
            hurtDiretion = direction;
            anim.SetTrigger("Hurt");
            healthPoint -= hurtDamage;
            anger++;
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
