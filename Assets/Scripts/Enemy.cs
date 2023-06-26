using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int healthPoint;
    public int damage;

    // Start is called before the first frame update
    public void Start()
    {
     
    }

    // Update is called once per frame
    public void Update()
    {
        Debug.Log(healthPoint);
        if (healthPoint <= 0)
        {
            Destroy(gameObject);
        }
    }
    public void TakeDamage(int damage)
    {
        healthPoint -= damage;
    }
}
