using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AIwolf: MonoBehaviour
{
    public Transform rayCast;
    public LayerMask raycastMask;
    public float rayCastLength;
    public float attackDistance;
    public float moveSpeed;
    public float timer;//��ȴʱ���ʱ��
    public Transform leftBoundary;
    public Transform rightBoundary;

    private RaycastHit2D hit;
    private Transform target;
    private Animator anim;
    private float distance; //����͵��˵ľ���
    private bool attackMode;
    private bool inRange;
    private bool cooling;
    private float intTimer;//��ȴʱ��


    private void Awake()
    {
        intTimer = timer;
        anim = GetComponent<Animator>();
        SelectNewTarget();          //���ʼ������һ��Ŀ�꣬��Ȼ����
        raycastMask = LayerMask.GetMask("Player");
    }
    private void Update()
    {
        Debug.Log("inRange:" + inRange);
        RaycastDebugger();
        if (!attackMode)            //���������ƶ�
        {
            Move();
        }
        if (!InsideLimits() && !inRange)  //�߳�Ѳ�߷�Χ����Ҳ��ڼ�鷶Χ��
        {
            SelectNewTarget();
            //Debug.Log("InsideLimits():"+ InsideLimits());
        }
        if (inRange)            //����ڼ�鷶Χ�ڲŻᷢ������
        {
            hit = Physics2D.Raycast(rayCast.transform.position, transform.right, rayCastLength, raycastMask);   //ֻ��Player����ײ����Ȼ����ײ��ʯͷҲ���⵽������    
        }
        //��������
        if (hit.collider != null)           //���߼�⵽���
        {
            EnemyLogic();
            //
        }
        else if (hit.collider == null)
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
        distance = Vector2.Distance(transform.position, target.position);
        if (distance < rayCastLength)
        {
            Debug.DrawRay(rayCast.position, transform.right * rayCastLength, Color.red);
        }
        else if (distance > rayCastLength)
        {
            Debug.DrawRay(rayCast.position, transform.right * rayCastLength, Color.green);
        }
    }

    void EnemyLogic()
    {
        distance = Vector2.Distance(transform.position, target.position);
        if (distance > attackDistance)
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
        Flip();
    }

    void Attack()
    {
        Debug.Log("1");
        timer = intTimer;
        attackMode = true;
        anim.SetBool("Walk", false);
        anim.SetBool("Attack", true);
    }

    void StopAttack()
    {
        cooling = false;
        attackMode = false;
        anim.SetBool("Attack", false);
    }
    void CoolDown()
    {
        timer -= Time.deltaTime;
        if (timer <= 0 && cooling && attackMode)
        {
            cooling = false;
            timer = intTimer;
        }
    }
    public void TriggerCooling()        //�������¼���
    {
        cooling = true;
    }

    private bool InsideLimits()
    {
        return transform.position.x > leftBoundary.position.x && transform.position.x < rightBoundary.position.x;
    }
    private void SelectNewTarget()
    {
        float distanceToLeft = Vector2.Distance(transform.position, leftBoundary.position);
        float distanceToRight = Vector2.Distance(transform.position, rightBoundary.position);

        if (distanceToLeft > distanceToRight)
        {
            target = leftBoundary;
        }
        else
            target = rightBoundary;
        Flip();
    }

    void Flip()         //����ת��
    {
        Vector2 rotation = transform.eulerAngles;
        if (transform.position.x < target.position.x)
            rotation.y = 180f;
        else
            rotation.y = 0f;
        transform.eulerAngles = rotation;
    }

}
