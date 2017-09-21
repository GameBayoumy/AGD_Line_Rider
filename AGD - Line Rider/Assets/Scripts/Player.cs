using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	public float speed;
    public float maxResource;
	public Rigidbody2D rb2d;
    public Touch touch;

    [SerializeField]
    private float drawResource = 1000;

	// Use this for initialization
	void Start()
	{
		rb2d = GetComponent<Rigidbody2D>();
	}

	// Update is called once per frame
	void Update()
	{
        if (touch.isDrawing)
        {
            drawResource--;
            touch.canDraw = true;
        }
        else
        {
            if(drawResource <= maxResource)
                drawResource++;
            touch.canDraw = false;
        }
	}

	private void FixedUpdate()
	{
		float moveHorizontal = 1;
		float moveVertical = 0;

		Vector2 movement = new Vector2(moveHorizontal, moveVertical);
		rb2d.AddForce(movement * speed);

		if (rb2d.velocity.x > 10)
		{
			rb2d.velocity = rb2d.velocity.normalized * 10;
		}
	}
}
