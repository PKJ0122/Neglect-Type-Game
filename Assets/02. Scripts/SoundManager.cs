using System;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    public AudioSource bgmAudioSource;
    public AudioSource sfxAudioSource;

    public AudioClip[] bgmClips;
    public AudioClip[] sfxClips;

    public AudioMixer audioMixer;

    public static Action soundSet;
    public static Action<int> sfxPlay;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);

        sfxPlay += SfxPlay;
    }

    void Start()
    {
        soundSet += SoundSet;
        soundSet.Invoke();
    }

    void SoundSet()
    {
        audioMixer.SetFloat("BGM", -80 + (GameManager.instance.playerData.bgmLevel * 20));
        audioMixer.SetFloat("SFX", -80 + (GameManager.instance.playerData.sfxLevel * 20));
    }

    public void SfxPlay(int index)
    {
        if(index == -1)
        {
            bgmAudioSource.Play();
            return;
        }

        sfxAudioSource.PlayOneShot(sfxClips[index]);
    }
}
