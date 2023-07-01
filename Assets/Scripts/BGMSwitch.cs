using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMSwitch : MonoBehaviour
{
    public AudioSource goalBgm;
    public float goalVolume;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag != "Player") return;

        GameObject BGM = GameObject.Find("SceneSound/BGM");
        foreach (Transform child in BGM.transform)
        {
            AudioSource source = child.GetComponent<AudioSource>();
            if (source != goalBgm && source.enabled == true) { Debug.Log(source); StartCoroutine(Weak(source)); }
            if (source == goalBgm) { goalBgm.enabled = true; }
        }
    }
    IEnumerator Weak(AudioSource bgm)
    {
        while (bgm.volume > 0.02f)
        {
            bgm.volume -= 0.01f;
            yield return new WaitForSeconds(0.1f);
        }
        bgm.enabled = false;
        StartCoroutine(Enhance(goalBgm, goalVolume)); //其他BGM声音消失后开始增强目标BGM
    }
    IEnumerator Enhance(AudioSource goalBgm, float goalVolume)
    {
        while (goalBgm.volume < goalVolume)
        {
            goalBgm.volume += 0.01f;
            yield return new WaitForSeconds(0.01f);
        }
    }
}