using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class doorhome : MonoBehaviour
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
        Time.timeScale = 1.0f;
        if (isPlayerInsign)     //�������ڴ�������Χ�ھͷų���F���ı���
        {

            dialogBox.SetActive(true);
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
