using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGround : MonoBehaviour
{
    public Transform BackGroundPrefab;
    public Transform player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        BackGroundPrefab.position =new Vector2(player.position.x,BackGroundPrefab.position.y);
    }
}
