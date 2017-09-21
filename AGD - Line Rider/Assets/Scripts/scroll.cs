using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scroll : MonoBehaviour
{

    public float scrollSpeed = 0.5F;
    public float scrollScale;
    public Renderer rend;
    public GameObject Player;


	private void Awake()
	{
		Player = GameObject.FindGameObjectWithTag("Player");
	}

    void Start()
    {
        rend = GetComponent<Renderer>();
    }

    void Update()
    {
        float offset = Time.time * scrollSpeed;
        rend.material.SetTextureOffset("_MainTex", new Vector2(offset, 0));


        transform.position = new Vector3(Player.transform.position.x, transform.position.y, transform.position.z);



        //scrollSpeed = scrollScale * Player.GetComponent<Rigidbody2D>().velocity.x * Time.time;


        //if (Player.GetComponent<Rigidbody2D>().velocity.x == 0){
        //    scrollSpeed = 0.01f;
        //}
    }
}