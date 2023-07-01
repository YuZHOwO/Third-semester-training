using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public float speed;
    public int damage;
    public float destoryDistance;

    private Rigidbody2D rb;
    private Vector2 startPos;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * speed ;
        startPos =transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float distance =Vector2.Distance(startPos,transform.position);
        if(distance >destoryDistance )
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)  //����ײ�����봥����ʱ���Զ�����
    {
        //
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("�˺���");
            if (startPos.x > other.transform.position.x)
            {
                other.GetComponent<Enemy>().GetHurt(Vector2.right);
            }
            else 
                other.GetComponent<Enemy>().GetHurt(Vector2.left);
            
        }
    }
}
