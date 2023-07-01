using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wolf : Enemy
{
    // Start is called before the first frame update
    void Start()
    {
        maxPH = 120;
        hurtSpeed = 2;
        cleanTime = 0.85f;
        coinNum = 50;
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
    }
}
