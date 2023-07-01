using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    private int damage;

    // Start is called before the first frame update
    void Start()
    {
        damage = 3;
    }

    // Update is called once per frame
    void Update()
    {
  
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        //
        if (other.CompareTag("Player")&&other.GetType().ToString()=="UnityEngine.CapsuleCollider2D")//必须加上，因为我还有一个boxCollider,会判定两次
        {
            other.GetComponent<PlayerController>().HurtPlayer(damage);
        }
    }
}
