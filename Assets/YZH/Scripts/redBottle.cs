using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class redBottle : MonoBehaviour
{
    public TMP_Text textLabel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        textLabel.text=PlayerController.redBottleNum.ToString();
    }
}
