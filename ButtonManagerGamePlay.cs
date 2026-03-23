using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class ButtonManagerGamePlay : MonoBehaviour
{
    PlayerMovoment player;
    public GameObject settingsMenu;
    public GameObject mainMenu;
    public Slider volumeMusicSlider;
    public Slider volumeSoundEffectSlider;

    public AudioSource soundEffectAudioSource;
    public AudioSource musicAudioSource;

    [SerializeField] GameObject helpMenu;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindObjectOfType<PlayerMovoment>();

        volumeMusicSlider.value = PlayerPrefs.GetFloat("MusicVolume");
        volumeSoundEffectSlider.value = PlayerPrefs.GetFloat("SoundEffectVolume");

        soundEffectAudioSource.volume = PlayerPrefs.GetFloat("SoundEffectVolume");
        musicAudioSource.volume = PlayerPrefs.GetFloat("MusicVolume");
    }

    private void Update()
    {
        if(player !=null)
        {
            float volumeSoundEffects = volumeSoundEffectSlider.value;
            float volumeMusic = volumeMusicSlider.value;

            PlayerPrefs.SetFloat("MusicVolume", volumeMusic);
            PlayerPrefs.SetFloat("SoundEffectVolume", volumeSoundEffects);

            soundEffectAudioSource.volume = PlayerPrefs.GetFloat("SoundEffectVolume");
            musicAudioSource.volume = PlayerPrefs.GetFloat("MusicVolume");
        }
       
    }
    public void Resume()
    {
        player.canMove = true;
        player.pauseMenu.SetActive(false);
    }

    public void Restart()
    {
        SceneManager.LoadScene("GamePlay");
    }

    public void Home()
    {
        SceneManager.LoadScene("Home");

    }
    public void SettingsMenu()
    {
        settingsMenu.SetActive(true);
    }
    public void Back()
    {
        mainMenu.SetActive(true);
        settingsMenu.SetActive(false);
        helpMenu.SetActive(false);
    }
    public void HelpMenu()
    {
        helpMenu.SetActive(true);
    }
}
