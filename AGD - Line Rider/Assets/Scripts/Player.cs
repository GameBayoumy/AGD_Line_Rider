using UnityEngine;

public class Player : MonoBehaviour
{
	public float speed;
    public float maxResource;
    public float drawResource;
    public Touch touch;

    private Rigidbody2D rb2d;

	// Use this for initialization
	void Start()
	{
		rb2d = GetComponent<Rigidbody2D>();
        drawResource = maxResource;             // Set start draw resource to the max resource
	}

	// Update is called once per frame
	void Update()
	{
        // Stop drawing when resource is empty
        if (drawResource <= 0)
        {
            touch.canDraw = false;
            PlayGamesScript.UnlockAchievement(GPGSIds.achievement_bubbles);
            PlayerPrefs.SetInt("UnlockedBubbles", 1);
        }
        else
            touch.canDraw = true;
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

    public void CanDraw(bool value)
    {
        if(touch != null)
            touch.canDraw = value;
    }
}
