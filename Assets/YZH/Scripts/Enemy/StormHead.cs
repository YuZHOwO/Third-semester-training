using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StormHead : Enemy
{
    void Start()
    {
        maxPH = 30;
        //damage = 3;
        hurtSpeed = 1;
        cleanTime = 0.8f;
        coinNum = 5;
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
    }
}
