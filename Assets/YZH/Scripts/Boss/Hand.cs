using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Hand : MonoBehaviour
{
    public float speed;
    public int damage;
    public float destoryDistance;
    public Transform target;

    private Vector2 startPos;
    private bool haveTarget=false;
    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector2.Distance(startPos, transform.position);
        if (distance > destoryDistance)
        {
            Destroy(gameObject);
        }
        Move();
    }
    private void OnTriggerEnter2D(Collider2D other)  //����ײ�����봥����ʱ���Զ�����
    {
        //
        if (other.CompareTag("Player"))
        {
            Debug.Log("�˺���");
            other.GetComponent<PlayerController>().HurtPlayer(damage);
        }
    }
    void Move()
    {
        if (!haveTarget)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
            haveTarget = true;
        }
        
    }
}
