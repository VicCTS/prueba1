using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioClip lvl1Music;

    private AudioSource source;

    // Start is called before the first frame update
    void Awake()
    {
        source = GetComponent<AudioSource>();

        source.clip = lvl1Music;
        source.Play();
    }

    public void StopBGM()
    {
        source.Stop();
    }
}
