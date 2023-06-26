using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    const int maxPH = 10;
    private int healthPoint;
    public int damage;
    public float hurtSpeed;
    private Vector2 hurtDiretion;
    private bool isHurt;
    private AnimatorStateInfo info;
    private Animator anim;
    private Rigidbody2D rb;
    public float cleanTime;
    private bool dead=false;
    public Image healthBar;
    private float healthBarWidth;
    // Start is called before the first frame update
    public void Start()
    {
        healthPoint = maxPH;
        anim = GetComponent<Animator>();
        rb= GetComponent<Rigidbody2D>();
        healthBarWidth = healthBar.rectTransform.rect.width;
    }

    // Update is called once per frame
    public void Update()
    {
        Debug.Log(healthPoint);
        info = anim.GetCurrentAnimatorStateInfo(0);
        if(isHurt )
        {
            rb.velocity = hurtDiretion * hurtSpeed;
            if(info.normalizedTime>= .6f)
                isHurt = false;
        }
        CleanEnemy();


    }
    public void GetHurt(Vector2 direction)
    {
        if(!dead) { 
            transform.localScale = new Vector3(-direction.x, 1, 1);
            isHurt = true;
            hurtDiretion = direction;
            anim.SetTrigger("Hurt");
            healthPoint -= 3;
        }
    }
    void CleanEnemy()
    {
        
        if (healthPoint <= 0 && !dead)
        {
            anim.SetTrigger("Die");
            dead = true;
        }
        if (dead)
            cleanTime -= Time.deltaTime;
        if (cleanTime < 0)
        {
            Destroy(gameObject);
            dead = false;
        }
        float a = healthPoint / maxPH * healthBarWidth;
        healthBar.rectTransform.sizeDelta = new Vector2(a, healthBar.rectTransform.rect.height);
    }
}
