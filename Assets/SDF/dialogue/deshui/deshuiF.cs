using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class deshuiF : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject dialogBox;
    private bool isPlayerInsign;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isPlayerInsign)
        {
            dialogBox.SetActive(true);//触发提示文本框
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player")
            && other.GetType().ToString() == "UnityEngine.CapsuleCollider2D")
        {
            isPlayerInsign = true;
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player")
            && other.GetType().ToString() == "UnityEngine.CapsuleCollider2D")
        {
            isPlayerInsign = false;
            dialogBox.SetActive(false);

        }
    }
}
