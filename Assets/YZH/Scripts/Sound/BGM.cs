using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine;

public class BGM : MonoBehaviour
{
    public AudioClip initialBackgroundMusic;
    public AudioClip transitionBackgroundMusic;
    public float transitionDuration = 2f;
    private AudioSource audioSource;
    private bool weaking = false;
    private bool enhancing = false;
    private bool isPlayingSecondBGM = false;
    private float transitionTimer = 0f;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = initialBackgroundMusic;
        audioSource.Play();
    }

    private void Update()
    {
        if (weaking)
        {
            // øÿ÷∆“Ù¿÷Ω•±‰
            transitionTimer += Time.deltaTime;
            float normalizedTime = Mathf.Clamp01(transitionTimer / transitionDuration);
            audioSource.volume = Mathf.Lerp(0.5f, 0f, normalizedTime);

            if (transitionTimer >= transitionDuration)
            {
                audioSource.clip = transitionBackgroundMusic;
                weaking = false;
                enhancing = true;
                transitionTimer = 0;
            }

        }
        if (enhancing)
        {   
            
            if (!isPlayingSecondBGM)
            {
                audioSource.Play();
                isPlayingSecondBGM=true;
            }

            transitionTimer += Time.deltaTime;
            float normalizedTime = Mathf.Clamp01(transitionTimer / transitionDuration);
            audioSource.volume = Mathf.Lerp(0f, 1f, normalizedTime);
            if (transitionTimer >= transitionDuration)
            {
                  enhancing=false;
            }
               
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")&&!isPlayingSecondBGM)
        {
            // ¥•∑¢“Ù¿÷Ω•±‰
            weaking = true;
        }
    }
}