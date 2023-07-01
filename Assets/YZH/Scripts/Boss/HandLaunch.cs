using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HandLaunch : MonoBehaviour
{
    GameObject hand;
    GameObject tep;
    Transform target;
    Rigidbody2D rb;
    public float speed;
    public float timer; //发射手的时间间隔
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Shoot();
    }

    private Vector2 PlayerDirection()
    {
        Vector2 playerPosition = target.transform.position;
        Vector2 direction = (playerPosition - (Vector2)transform.position).normalized;
        return direction;
    }
    void Shoot()
    {

    }
}
