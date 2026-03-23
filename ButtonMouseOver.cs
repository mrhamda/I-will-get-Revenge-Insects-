using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(SetGameObjectVolumeToSoundEffects))]

public class ButtonMouseOver : MonoBehaviour
{
    [SerializeField] AudioClip hoverAudio;
    private AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>(); 
    }

    public void PlayHoverAudio()
    {
        audioSource.PlayOneShot(hoverAudio);
    }
}
