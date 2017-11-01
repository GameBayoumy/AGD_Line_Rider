using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public AudioClip mainBGMclip;
    public static AudioClip mainBGM;

    static GameManager instance;

    private void Start()
    {
        mainBGM = mainBGMclip;
        GetInstance();
    }

    public static GameManager GetInstance()
    {
        if (!instance)
        {
            GameObject gameManager = new GameObject("GameManager");
            instance = gameManager.AddComponent<GameManager>();
            instance.Initialize();
        }
        return instance;
    }

    void Initialize()
    {
        SoundManager.PlayBGM(mainBGM, false, 0f);
        instance.mainBGMclip = mainBGM;
        DontDestroyOnLoad(gameObject);
    }
}
