using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushroom : Enemy
{
    // Start is called before the first frame update
    void Start()
    {
        maxPH = 20;
        hurtSpeed = 2;
        cleanTime = 0.85f;
        coinNum = 7;
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
    }
}
