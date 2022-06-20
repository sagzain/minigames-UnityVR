using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [SerializeField] private List<AudioSource> speakers;
    [SerializeField] private AudioClip music;

    private void Start()
    {
        if(music)
            speakers?.ForEach(speaker =>
            {
                speaker.clip = music;
                speaker.loop = true;
                speaker.Play();
            });
        
    }
}
