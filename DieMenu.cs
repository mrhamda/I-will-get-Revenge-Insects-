using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieMenu : MonoBehaviour
{
    [SerializeField] AudioClip lostAudio;
    [SerializeField] AudioClip highScoreAudio;
    private AudioSource audioSource;
    PlayerMovoment player;
    float nextTimeToPlayAudio;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        player = GameObject.FindObjectOfType<PlayerMovoment>();
    }

    // Update is called once per frame
    void Update()
    {
        if(player.score >= PlayerPrefs.GetInt("HighScore") && player == null && Time.time >=nextTimeToPlayAudio)
        {
            audioSource.PlayOneShot(highScoreAudio);
            nextTimeToPlayAudio = Time.time + 1000000000000000000;
        }else if(player.score < PlayerPrefs.GetInt("HighScore") && player == null && Time.time >= nextTimeToPlayAudio)
        {
            audioSource.PlayOneShot(lostAudio);
            nextTimeToPlayAudio = Time.time + 1000000000000000000;

        }
    }

     
}
