using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public AudioClip mainBGMclip;
    public static AudioClip mainBGM;
    public AudioClip menuClickSFX;
    public static AudioClip menuSFX;

    static GameManager _instance;

    private void Start()
    {
        mainBGM = mainBGMclip;
        menuSFX = menuClickSFX;
        GetInstance();
    }

    public static GameManager GetInstance()
    {
        if (!_instance)
        {
            GameObject gameManager = new GameObject("GameManager");
            _instance = gameManager.AddComponent<GameManager>();
            _instance.Initialize();
        }
        return _instance;
    }

    void Initialize()
    {
        SoundManager.PlayBGM(mainBGM, false, 0f);
        _instance.mainBGMclip = mainBGM;
        DontDestroyOnLoad(gameObject);
    }
}
