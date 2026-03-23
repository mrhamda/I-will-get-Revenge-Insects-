using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetGameObjectVolumeToSoundEffects : MonoBehaviour
{
    private AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        SetVolume();
    }

    void SetVolume()
    {
        audioSource.volume = PlayerPrefs.GetFloat("SoundEffectVolume");

    }
}
