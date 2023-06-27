using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class AI : MonoBehaviour
{
    public Transform rayCast;
    public LayerMask raycastMask;
    public float rayCastLength;
    public float attackDistance;
    public float moveSpeed;
    public float timer;//攻击冷却时间
    public Transform leftBoundary;
    public Transform rightBoundary;

    private RaycastHit2D hit;
    private Transform target;
    private Animator anim;
    private float distance; //人物和敌人的距离
    private bool attackMode;
    private bool inRange;
    private bool cooling;
    private float intTimer;

    private void Awake()
    {
        intTimer = timer;
        anim =GetComponent<Animator>();
        SelectNewTarget();
    }
    private void Update()
    {
        if (!attackMode) 
        { 
            Move(); 
        }
        if(!InsideLimits()&&!inRange)
        {
            SelectNewTarget();
            //Debug.Log("InsideLimits():"+ InsideLimits());
        }
        if (inRange)
        {
            hit = Physics2D.Raycast(rayCast.transform.position, transform.right, rayCastLength, raycastMask);
        }
        RaycastDebugger();
        if (hit.collider != null)
        {
            EnemyLogic();
        }
        else if(hit.collider == null)
        {
            inRange = false;
        }

        if (inRange == false)
        {
            StopAttack();
        }
        if (cooling)
        {
            CoolDown();
            anim.SetBool("Attack", false);
        }
        //Debug.Log(inRange);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            target = other.transform;
            inRange = true;
            Flip();
        }
        
    }
    void RaycastDebugger()
    {
        if (distance > attackDistance)
        {
            Debug.DrawRay(rayCast.position, transform.right * rayCastLength, Color.red);
        }
        else if (distance < attackDistance)
        {
            Debug.DrawRay(rayCast.position, transform.right * rayCastLength, Color.green);
        }
    }

    void EnemyLogic()
    {
        distance =Vector2.Distance(transform.position,target.position);
        if(distance>attackDistance)
        {
  
            StopAttack();
        }
        else if (attackDistance >= distance && cooling == false)
        {
            Attack();
        }
        if (cooling)
        {
            anim.SetBool("Attack", false);
        }
    }

    void Move()
    {
        anim.SetBool("Walk", true);
        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            Vector2 targetPosition = new Vector2(target.position.x, transform.position.y);
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        }
    }

    void Attack()
    {
        timer = intTimer;
        attackMode = true;
        anim.SetBool("Walk", false);
        anim.SetBool("Attack", true);
    }

    void StopAttack()
    {
        cooling = false;
        attackMode =false;
        anim.SetBool("Attack", false);
    }
    void CoolDown()
    {
        timer -= Time.deltaTime;
        if(timer<=0&&cooling&&attackMode)
        {
            cooling = false;
            timer = intTimer;
        }
    }
    public void TriggerCooling()
    {
        cooling = true;
    }

    private bool InsideLimits()
    {
        return transform.position.x< leftBoundary.position.x&& transform.position.x>rightBoundary.position.x;
    }
    private void SelectNewTarget()
    {
        float distanceToLeft = Vector2.Distance(transform.position, leftBoundary.position);
        float distanceToRight= Vector2.Distance(transform.position, rightBoundary.position);

        if(distanceToLeft> distanceToRight)
        {
            target = leftBoundary;
        }
        else
            target = rightBoundary;
        Flip();
    }

    void Flip()
    {
        Vector3 rotation = transform.eulerAngles;
        if(transform.position.x>target.position.x)
            rotation.y = 180f;
        else
            rotation.y = 0f;
        transform.eulerAngles = rotation;
    }

}
