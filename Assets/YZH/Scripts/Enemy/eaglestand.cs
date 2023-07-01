using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class eaglestand : Enemy
{
    // Start is called before the first frame update
    void Start()
    {
        maxPH = 10;
        //damage = 3;
        hurtSpeed = 5;
        cleanTime = 0.85f;
        coinNum = 4;
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
    }
}

