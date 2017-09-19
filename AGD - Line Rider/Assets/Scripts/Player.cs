using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

	public float speed;
	private Rigidbody2D rb2d;

	// Use this for initialization
	void Start()
	{
		rb2d = GetComponent<Rigidbody2D>();
	}

	// Update is called once per frame
	void Update()
	{
        
	}

	private void FixedUpdate()
	{
		float moveHorizontal = 1;
		float moveVertical = 0;

		Vector2 movement = new Vector2(moveHorizontal, moveVertical);
        Debug.Log(speed);
		rb2d.AddForce(movement * speed);

		if (rb2d.velocity.x > 10)
		{
			rb2d.velocity = rb2d.velocity.normalized * 10;
		}

	}
}
