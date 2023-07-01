using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtEffect : MonoBehaviour
{
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other)  //����ײ�����봥����ʱ���Զ�����
    {
        if (other.CompareTag("PlayerWeapon"))
        {
            anim.SetTrigger("Hurt");
        }
    }
}
