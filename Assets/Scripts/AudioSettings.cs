using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AudioSettings : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider sliderMusica;
    [SerializeField] private Slider sliderEfectos;

    private const string MUSIC_KEY = "MusicVolume";
    private const string SFX_KEY = "SFXVolume";

    private void Start()
    {
        float musicVolume = PlayerPrefs.GetFloat(MUSIC_KEY, 0.4f);
        float sfxVolume = PlayerPrefs.GetFloat(SFX_KEY, 0.4f);

        sliderMusica.value = musicVolume;
        sliderEfectos.value = sfxVolume;

        SetMusicVolume(musicVolume);
        SetSFXVolume(sfxVolume);
    }

    public void SetMusicVolume(float value)
    {
        audioMixer.SetFloat("MusicVolume",
            Mathf.Log10(Mathf.Max(value, 0.0001f)) * 20f);
    }

    public void SetSFXVolume(float value)
    {
        audioMixer.SetFloat("SFXVolume",
            Mathf.Log10(Mathf.Max(value, 0.0001f)) * 20f);
    }

    public void GuardarYVolver()
    {
        PlayerPrefs.SetFloat(MUSIC_KEY, sliderMusica.value);
        PlayerPrefs.SetFloat(SFX_KEY, sliderEfectos.value);

        PlayerPrefs.Save();

        SceneManager.LoadScene("MenuInicial");
    }
}