using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyLaser : MonoBehaviour {

    public float laserSpeed = 1.0f;
    public float factor = 1.0f;
    public bool isHorizontal = false;
    public Transform laserStart;
    public Transform laserEnd;

    private Vector3 currentLength;
    private bool laserHit = false;

	// Use this for initialization
	void Start () {
        currentLength = new Vector3(0, -19f, 0);
        SetPos(laserStart.position, laserEnd.position);
    }

    private void Update()
    {
        SetPos(laserStart.position, laserEnd.position);

        if (laserHit)
        {
            if (!isHorizontal)
                laserEnd.position = new Vector3(laserEnd.position.x, currentLength.y, 0);
            else if (isHorizontal)
                laserEnd.position = new Vector3(currentLength.x, laserEnd.position.y, 0);
            laserEnd.localPosition = new Vector3(0, laserEnd.localPosition.y, 0);
        }
        else
        {
            if(!isHorizontal)
                laserEnd.position = new Vector3(laserEnd.position.x, laserEnd.position.y - ((5 * invertDirNumber) * Time.deltaTime), 0);
            else if (isHorizontal)
                laserEnd.position = new Vector3(laserEnd.position.x - (5 * invertDirNumber * Time.deltaTime) * invertDirNumber, laserEnd.position.y, 0);
            laserEnd.localPosition = new Vector3(0, laserEnd.localPosition.y, 0);
        }
    }

    void SetPos(Vector3 start, Vector3 end)
    {
        var dir = end - start;
        var mid = (dir) / 2.0f + start;
        transform.position = mid;
        transform.rotation = Quaternion.FromToRotation(Vector3.up, dir);
        float newY = dir.magnitude * factor;

        transform.localScale = new Vector3(transform.localScale.x, newY, transform.localScale.z);
    }

	private void OnCollisionEnter2D(Collision2D collision)
	{
        if (collision.gameObject.tag == "Player")
        {
            SceneManager.LoadScene("LineMeshTest");
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Trail" || collision.gameObject.tag == "Wall")
        {
            Vector3 contactPoint = new Vector3(collision.contacts[0].point.x, collision.contacts[0].point.y, 0);
            
            laserHit = true;

            if (contactPoint.y > currentLength.y)
            {
                currentLength = contactPoint;
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.name == "Trail" || collision.gameObject.tag == "Wall")
        {
            laserHit = false;
            currentLength = new Vector3(0, -19f, 0);
        }
    }

}
