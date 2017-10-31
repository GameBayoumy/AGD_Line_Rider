using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public AudioClip mainBGMclip;
    public static AudioClip mainBGM;

    public AudioClip randomBGMclip;

    static GameManager instance;

    private void Start()
    {
        mainBGM = mainBGMclip;
        GetInstance();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
            SoundManager.PlaySFX(randomBGMclip);
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
        DontDestroyOnLoad(gameObject);
    }
}
