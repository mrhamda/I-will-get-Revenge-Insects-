using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
public class ButtonManager : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject settingsMenu;

    public Slider volumeMusicSlider;
    public Slider volumeSoundEffectSlider;

    public AudioSource soundEffectAudioSource;
    public AudioSource musicAudioSource;

    [SerializeField] TextMeshProUGUI highScore;

    [SerializeField] GameObject helpMenu;
    // Start is called before the first frame update
    void Start()
    {
        volumeMusicSlider.value = PlayerPrefs.GetFloat("MusicVolume");
        volumeSoundEffectSlider.value = PlayerPrefs.GetFloat("SoundEffectVolume");

        soundEffectAudioSource.volume = PlayerPrefs.GetFloat("SoundEffectVolume");
        musicAudioSource.volume = PlayerPrefs.GetFloat("MusicVolume");
        
    }

    // Update is called once per frame
    void Update()
    {
        float volumeSoundEffects = volumeSoundEffectSlider.value;
        float volumeMusic = volumeMusicSlider.value;

        PlayerPrefs.SetFloat("MusicVolume", volumeMusic);
        PlayerPrefs.SetFloat("SoundEffectVolume", volumeSoundEffects);

        soundEffectAudioSource.volume = PlayerPrefs.GetFloat("SoundEffectVolume");
        musicAudioSource.volume = PlayerPrefs.GetFloat("MusicVolume");

        highScore.text ="HighScore: " + "" +  PlayerPrefs.GetInt("HighScore").ToString();
    }

    public void Play()
    {
        SceneManager.LoadScene("GamePlay");
    }
    public void GoBackHome()
    {
        mainMenu.SetActive(true);
        settingsMenu.SetActive(false);
        helpMenu.SetActive(false);
    }

    public void SettingsMenu()
    {
        mainMenu.SetActive(true);
        settingsMenu.SetActive(true);
    }
    public void HelpMenu()
    {
        helpMenu.SetActive(true);
        mainMenu.SetActive(true);
        settingsMenu.SetActive(false);
    }
}
