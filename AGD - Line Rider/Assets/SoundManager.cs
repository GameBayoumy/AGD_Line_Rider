using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

    // Static variables
    static SoundManager instance;
    static float CurrentVolumeNormalized_BGM = 1f;
    static float CurrentVolumeNormalized_SFX = 1f;
    static bool isMuted = false;

    // Const variables
    const float MaxVolume_BGM = 1f;
    const float MaxVolume_SFX = 1f;

    // Private variables
    List<AudioSource> sfxSources;
    AudioSource bgmSource;

    public static SoundManager GetInstance()
    {
        if (!instance)
        {
            GameObject soundManager = new GameObject("SoundManager");
            instance = soundManager.AddComponent<SoundManager>();
            instance.Initialize();
        }
        return instance;
    }

    void Initialize()
    {
        // Add bgm sound source
        bgmSource = gameObject.AddComponent<AudioSource>();
        bgmSource.loop = true;
        bgmSource.playOnAwake = false;
        bgmSource.volume = GetBGMVolume();
        DontDestroyOnLoad(gameObject);
    }

    /// 
    /// Volume getters
    ///
    static float GetBGMVolume()
    {
        return isMuted ? 0f : MaxVolume_BGM * CurrentVolumeNormalized_BGM;
    }

    public static float GetSFXVolume()
    {
        return isMuted ? 0f : MaxVolume_SFX * CurrentVolumeNormalized_SFX;
    }

    /// 
    /// BGM functions
    ///
    public static void PlayBGM(AudioClip bgmClip, bool fade, float fadeDuration)
    {
        SoundManager soundMan = GetInstance();
        if (fade)
        {
            if (soundMan.bgmSource.isPlaying)
            {
                // Fade out, then switch and fade in
                soundMan.FadeBGMOut(fadeDuration / 2);
                soundMan.FadeBGMIn(bgmClip, fadeDuration / 2, fadeDuration / 2);
            }
            else
            {
                // Fade in
                float delay = 0f;
                soundMan.FadeBGMIn(bgmClip, delay, fadeDuration);
            }
        }
        else
        {
            // Play music
            soundMan.bgmSource.volume = GetBGMVolume();
            soundMan.bgmSource.clip = bgmClip;
            soundMan.bgmSource.Play();
        }
    }

    public static void StopBGM(bool fade, float fadeDuration)
    {
        SoundManager soundMan = GetInstance();
        if (soundMan.bgmSource.isPlaying)
        {
            // Fade out, then switch and fade in
            if (fade)
            {
                soundMan.FadeBGMOut(fadeDuration);
            }
            else
            {
                soundMan.bgmSource.Stop();
            }
        }
    }

    /// 
    /// BGM utils
    ///
    void FadeBGMOut(float fadeDuration)
    {
        SoundManager soundMan = GetInstance();
        float delay = 0f;
        float toVolume = 0f;
        StartCoroutine(FadeBGM(toVolume, delay, fadeDuration));
    }

    void FadeBGMIn(AudioClip bgmClip, float delay, float fadeDuration)
    {
        SoundManager soundMan = GetInstance();
        soundMan.bgmSource.clip = bgmClip;
        soundMan.bgmSource.Play();
        float toVolume = GetBGMVolume();
        StartCoroutine(FadeBGM(toVolume, delay, fadeDuration));
    }

    IEnumerator FadeBGM(float fadeToVolume, float delay, float duration)
    {
        yield return new WaitForSeconds(delay);
        SoundManager soundMan = GetInstance();
        float elapsed = 0f;
        while (duration > 0)
        {
            float t = (elapsed / duration);
            float volume = Mathf.Lerp(0f, fadeToVolume * CurrentVolumeNormalized_BGM, t);
            soundMan.bgmSource.volume = volume;
            elapsed += Time.deltaTime;
            yield return 0;
        }
    }

    /// 
    /// SFX utils
    ///
    AudioSource GetSFXSource()
    {
        // set up a new sfx sound source for each new sfx clip
        AudioSource sfxSource = gameObject.AddComponent<AudioSource>();
        sfxSource.loop = false;
        sfxSource.playOnAwake = false;
        sfxSource.volume = GetSFXVolume();
        if (sfxSources == null)
        {
            sfxSources = new List<AudioSource>();
        }
        sfxSources.Add(sfxSource);
        return sfxSource;
    }

    IEnumerator RemoveSFXSource(AudioSource sfxSource)
    {
        yield return new WaitForSeconds(sfxSource.clip.length);
        sfxSources.Remove(sfxSource);
        Destroy(sfxSource);
    }

    IEnumerator RemoveSFXSourceFixedLength(AudioSource sfxSource, float length)
    {
        yield return new WaitForSeconds(length);
        sfxSources.Remove(sfxSource);
        Destroy(sfxSource);
    }
}
