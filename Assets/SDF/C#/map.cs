using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class map : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject dialogBox;//��Ҫ�������ı���
    private bool isPlayerInsign;//�ж�����Ƿ���������

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
            if (Input.GetKeyDown(KeyCode.F) && isPlayerInsign)//����F��Player���ڷ�Χ�ڲ��ܴ���dialogbox
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
