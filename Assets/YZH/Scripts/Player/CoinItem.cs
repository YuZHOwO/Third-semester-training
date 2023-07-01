using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinItem : MonoBehaviour
{
    AudioSource Aud;
    public AudioClip getCoin;

    // Start is called before the first frame update
    void Start()
    {
        Aud = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        CoinUI.CurrentCoinQuantity = FindObjectOfType<PlayerController>().CoinNum();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && other.GetType().ToString() == "UnityEngine.CapsuleCollider2D")
        {
            AudioSource.PlayClipAtPoint(getCoin, transform.position);
            CoinUI.CurrentCoinQuantity += 1;
            FindObjectOfType<PlayerController>().GetCoin();
            Destroy(gameObject);
        }
        
    }
}