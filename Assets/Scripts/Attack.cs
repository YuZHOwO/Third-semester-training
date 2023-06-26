using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public int damage;
    public float time;
    public float startTime;
    private Animator anim;
    private PolygonCollider2D coll;
    // Start is called before the first frame update
    void Start()
    {
        anim = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
        coll = GetComponent<PolygonCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        _Attack();
    }

    void _Attack()
    {
        if (Input.GetButtonDown("Attack"))
        {
            //coll.enabled = true;
            anim.SetTrigger("Attack");
            StartCoroutine(StartAttack());
        }
    }

    IEnumerator StartAttack()
    {
        yield return new WaitForSeconds(startTime);
        coll.enabled = true;
        StartCoroutine(disableHitBox());
        //Debug.Log("attack");
    }
    IEnumerator disableHitBox()
    {
        yield return new WaitForSeconds(time);
        coll.enabled = false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.GetComponent<Enemy>().TakeDamage(damage);
        }
    }
}
