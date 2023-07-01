using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class door : MonoBehaviour
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
        if (doordialog.isfirst) { }
        {
            if (isPlayerInsign)
            {
                if (Input.GetKeyDown(KeyCode.F) && isPlayerInsign)
                {
                    dialogBox.SetActive(true);
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

