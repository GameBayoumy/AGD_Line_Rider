using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitializeBall : MonoBehaviour {

    public GameObject player;
    public PhysicsMaterial2D playerPhysics;
    public Sprite[] possibleSprites;

    private Rigidbody2D _playerRB;
    private Player _playerStats;
    private UnlockShaders _sprites;
    private SpriteRenderer _sprRenderer;

    // Use this for initialization
    void Start()
    {

        player = GameObject.Find("Player");
        _playerRB = player.GetComponent<Rigidbody2D>();
        _playerStats = player.GetComponent<Player>();
        _sprRenderer = player.GetComponent<SpriteRenderer>();

        PlayerPrefs.SetInt("BallID", PlayerPrefs.GetInt("BallID"));
        _sprRenderer.sprite = possibleSprites[PlayerPrefs.GetInt("BallID")];

        if (PlayerPrefs.GetInt("BallID") == 0)
        {
            PlayerPrefs.SetFloat("BallGravity", 3);
            PlayerPrefs.SetFloat("BallSpeed", 20);
            PlayerPrefs.SetFloat("BallBounce", 0);
        }
    }

    // Update is called once per frame
    void Update () {

        _playerRB.gravityScale = PlayerPrefs.GetFloat("BallGravity");
        playerPhysics.bounciness = PlayerPrefs.GetFloat("BallBounce");
        _playerStats.speed = PlayerPrefs.GetFloat("BallSpeed");

    }
}
