using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class pointernormal : MonoBehaviour
{
    //����ʵ�ֹؿ�����󳡾����л�
    private bool isPlayerInsign;
    private GameObject dialogBox;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {


        if (doordialog.isfirst) { }
        {
            if (isPlayerInsign)
            {
                if (Input.GetKeyDown(KeyCode.F) && isPlayerInsign)
                {
                    SceneManager.LoadScene(5);//�л�������5
                }
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

