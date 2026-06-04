using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    [SerializeField] public AudioMixer audioMixer;

    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
        LoadVolumes();
    }

    public void SetVolume(string param, float sliderValue)
    {
        float dB = sliderValue > 0 ? Mathf.Log10(sliderValue / 100f) * 20f : -80f;
        audioMixer.SetFloat(param, dB);
    }

    private void LoadVolumes()
    {
        SetVolume("MusicVol", PlayerPrefs.GetFloat("MusicVol", 80f));
        SetVolume("SFXVol", PlayerPrefs.GetFloat("SFXVol", 100f));
        SetVolume("AmbientVol", PlayerPrefs.GetFloat("AmbientVol", 60f));
    }
}