using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class map : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject dialogBox;//需要触发的文本框
    private bool isPlayerInsign;//判断玩家是否在区域内

    void Start()
    {

    }
    private void Awake()
    {
        //Time.timeScale = 1;
    }
    // Update is called once per frame
    void Update()
    {
        if (isPlayerInsign)
        {
            if (Input.GetKeyDown(KeyCode.F) && isPlayerInsign)//按下F且Player处于范围内才能触发dialogbox
            {
                dialogBox.SetActive(true);
            }
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
