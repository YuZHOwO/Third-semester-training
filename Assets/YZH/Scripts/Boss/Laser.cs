using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    Animator anim;
    private float time;
    public bool laserMode;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        //
        
    }

    // Update is called once per frame
    void Update()
    {   
       time =FindObjectOfType<Boss1>().laserTimeInterval;
       if (time <= 0.004f)
        {
            anim.SetTrigger("Laser");
            //Debug.Log("���伤�⣡");
        }
           
        //Debug.Log("laserMode :" + laserMode);
    }

    void EnterLaser()//�����������¼���
    {
        laserMode = true;
    }
    void QuitLaser()
    {
        laserMode = false;
    }

}
