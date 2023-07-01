using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static AudioSource audioSrc;
    public static AudioClip pickCoin;
    void Start()
    {
        audioSrc = GetComponent<AudioSource>();
        pickCoin = Resources.Load<AudioClip>("PickCoin");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void PlayPickCoinClip()
    {
        audioSrc.PlayOneShot(pickCoin);
    }
}
