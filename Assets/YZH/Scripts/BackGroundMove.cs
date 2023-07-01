using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundMove : MonoBehaviour
{
    Rigidbody2D rbMap;
    // Start is called before the first frame update
    void Start()
    {
        rbMap = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        GameObject temp = GameObject.Find("Player");
        rbMap.position = temp.transform.position;
    }
}
