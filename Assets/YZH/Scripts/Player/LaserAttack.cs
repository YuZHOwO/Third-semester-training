using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserAttack : MonoBehaviour
{
    private int damage;
    // Start is called before the first frame update
    void Start()
    {
        damage = FindObjectOfType<Boss1>().laserDamage;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.CompareTag("Player") && other.GetType().ToString() == "UnityEngine.CapsuleCollider2D")//������ϣ���Ϊ�һ���һ��boxCollider,���ж�����
        {
            other.GetComponent<PlayerController>().HurtPlayer(damage);
        }
        
    }
}
