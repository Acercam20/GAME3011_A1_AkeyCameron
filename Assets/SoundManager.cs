using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioClip SelectTile;
    public AudioClip MoveTile;
    public AudioClip ClearTile;

    public void PlaySound(AudioClip a)
    {
        gameObject.GetComponent<AudioSource>().PlayOneShot(a);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
