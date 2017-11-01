using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour {
    
    // Static variables
    static SoundManager _instance;
    static float _currentVolumeNormalizedBGM = 1f;
    static float _currentVolumeNormalizedSFX = 1f;
    static bool _isMuted = false;

    // Const variables
    const float _maxVolumeBGM = 1f;
    const float _maxVolumeSFX = 1f;

    // Private variables
    List<AudioSource> _sfxSources;
    AudioSource _bgmSource;
    AudioMixer _audioMixer;

    public static SoundManager GetInstance()
    {
        if (!_instance)
        {
            GameObject soundManager = new GameObject("SoundManager");
            _instance = soundManager.AddComponent<SoundManager>();
            _instance.Initialize();
        }
        return _instance;
    }

    void Initialize()
    {
        // Add bgm sound source
        _bgmSource = gameObject.AddComponent<AudioSource>();
        GetInstance()._audioMixer = Resources.Load("AudioMixer") as AudioMixer;
        _bgmSource.outputAudioMixerGroup = _audioMixer.FindMatchingGroups("BGM")[0];
        _bgmSource.loop = true;
        _bgmSource.playOnAwake = false;
        _bgmSource.volume = GetBGMVolume();
        DontDestroyOnLoad(gameObject);
    }

    /// 
    /// Volume getters
    ///
    static float GetBGMVolume()
    {
        return _isMuted ? 0f : _maxVolumeBGM * _currentVolumeNormalizedBGM;
    }

    public static float GetSFXVolume()
    {
        return _isMuted ? 0f : _maxVolumeSFX * _currentVolumeNormalizedSFX;
    }

    /// 
    /// BGM functions
    ///
    public static void PlayBGM(AudioClip bgmClip, bool fade, float fadeDuration)
    {
        SoundManager soundMan = GetInstance();
        if (fade)
        {
            if (soundMan._bgmSource.isPlaying)
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
            soundMan._bgmSource.volume = GetBGMVolume();
            soundMan._bgmSource.clip = bgmClip;
            soundMan._bgmSource.Play();
        }
    }

    public static void StopBGM(bool fade, float fadeDuration)
    {
        SoundManager soundMan = GetInstance();
        if (soundMan._bgmSource.isPlaying)
        {
            // Fade out, then switch and fade in
            if (fade)
            {
                soundMan.FadeBGMOut(fadeDuration);
            }
            else
            {
                soundMan._bgmSource.Stop();
            }
        }
    }

    /// 
    /// BGM utils
    ///
    void FadeBGMOut(float fadeDuration)
    {
        float delay = 0f;
        float toVolume = 0f;
        StartCoroutine(FadeBGM(toVolume, delay, fadeDuration));
    }

    void FadeBGMIn(AudioClip bgmClip, float delay, float fadeDuration)
    {
        SoundManager soundMan = GetInstance();
        soundMan._bgmSource.clip = bgmClip;
        soundMan._bgmSource.Play();
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
            float volume = Mathf.Lerp(0f, fadeToVolume * _currentVolumeNormalizedBGM, t);
            soundMan._bgmSource.volume = volume;
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
        if (_sfxSources == null)
        {
            _sfxSources = new List<AudioSource>();
        }
        _sfxSources.Add(sfxSource);
        return sfxSource;
    }

    IEnumerator RemoveSFXSource(AudioSource sfxSource)
    {
        yield return new WaitForSeconds(sfxSource.clip.length);
        _sfxSources.Remove(sfxSource);
        Destroy(sfxSource);
    }

    IEnumerator RemoveSFXSourceFixedLength(AudioSource sfxSource, float length)
    {
        yield return new WaitForSeconds(length);
        _sfxSources.Remove(sfxSource);
        Destroy(sfxSource);
    }

    /// 
    /// SFX functions
    ///
    public static void PlaySFX(AudioClip sfxClip, string mixerOutputGroupName)
    {
        SoundManager soundMan = GetInstance();
        AudioSource source = soundMan.GetSFXSource();
        AudioMixer audioMixer = Resources.Load("AudioMixer") as AudioMixer;
        source.outputAudioMixerGroup = audioMixer.FindMatchingGroups(mixerOutputGroupName)[0];
        source.volume = GetSFXVolume();
        source.clip = sfxClip;
        source.Play();
        soundMan.StartCoroutine(soundMan.RemoveSFXSource(source));
    }

    public static void PlaySFXRandomized(AudioClip sfxClip, string mixerOutputGroupName)
    {
        SoundManager soundMan = GetInstance();
        AudioSource source = soundMan.GetSFXSource();
        AudioMixer audioMixer = Resources.Load("AudioMixer") as AudioMixer;
        source.outputAudioMixerGroup = audioMixer.FindMatchingGroups(mixerOutputGroupName)[0];
        source.volume = GetSFXVolume();
        source.clip = sfxClip;
        source.pitch = Random.Range(0.85f, 1.2f);
        source.Play();
        soundMan.StartCoroutine(soundMan.RemoveSFXSource(source));
    }

    public static void PlaySFXFixedDuration(AudioClip sfxClip, string mixerOutputGroupName, float duration, float volumeMultiplier = 1.0f)
    {
        SoundManager soundMan = GetInstance();
        AudioSource source = soundMan.GetSFXSource();
        AudioMixer audioMixer = Resources.Load("AudioMixer") as AudioMixer;
        source.outputAudioMixerGroup = audioMixer.FindMatchingGroups(mixerOutputGroupName)[0];
        source.volume = GetSFXVolume() * volumeMultiplier;
        source.clip = sfxClip;
        source.loop = true;
        source.Play();
        soundMan.StartCoroutine(soundMan.RemoveSFXSourceFixedLength(source, duration));
    }

    /// 
    /// Volume Control functions
    ///
    public static void DisableSoundImmediate()
    {
        SoundManager soundMan = GetInstance();
        soundMan.StopAllCoroutines();
        if (soundMan._sfxSources != null)
        {
            foreach (AudioSource source in soundMan._sfxSources)
            {
                source.volume = 0;
            }
        }
        soundMan._bgmSource.volume = 0f;
        _isMuted = true;
    }

    public static void EnableSoundImmediate()
    {
        SoundManager soundMan = GetInstance();
        if (soundMan._sfxSources != null)
        {
            foreach (AudioSource source in soundMan._sfxSources)
            {
                source.volume = GetSFXVolume();
            }
        }
        soundMan._bgmSource.volume = GetBGMVolume();
        _isMuted = false;
    }

    public static void SetGlobalVolume(float newVolume)
    {
        _currentVolumeNormalizedBGM = newVolume;
        _currentVolumeNormalizedSFX = newVolume;
        AdjustSoundImmediate();
    }

    public static void SetSFXVolume(float newVolume)
    {
        _currentVolumeNormalizedSFX = newVolume;
        AdjustSoundImmediate();
    }

    public static void SetBGMVolume(float newVolume)
    {
        _currentVolumeNormalizedBGM = newVolume;
        AdjustSoundImmediate();
    }

    public static void AdjustSoundImmediate()
    {
        SoundManager soundMan = GetInstance();
        if (soundMan._sfxSources != null)
        {
            foreach (AudioSource source in soundMan._sfxSources)
            {
                source.volume = GetSFXVolume();
            }
        }
        Debug.Log("BGM Volume: " + GetBGMVolume());
        soundMan._bgmSource.volume = GetBGMVolume();
        Debug.Log("BGM Volume is now: " + GetBGMVolume());
    }
}
