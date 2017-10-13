using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyLaser : MonoBehaviour {

    public bool laserHit = false;
    public bool laserBoundary = false;
    public float laserSpeed = 1.0f;
    public float factor = 1.0f;
    public Transform laserStart;
    public Transform laserEnd;

    private Transform currentHeight;

	// Use this for initialization
	void Start () {
        SetPos(laserStart.position, laserEnd.position);
	}

    private void Update()
    {
        SetPos(laserStart.position, laserEnd.position);
        laserEnd.TransformDirection(laserEnd.localPosition.x, laserEnd.localPosition.y * ( 5 * Time.deltaTime), laserEnd.localPosition.z);
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
            Transform otherTransform;
            if (collision.gameObject.name == "Trail")
                otherTransform = collision.gameObject.transform.GetChild(0).transform;
            else
                otherTransform = collision.gameObject.transform;

            laserHit = true;
            if (currentHeight == null)
                currentHeight = otherTransform;

            if (otherTransform.position.y > currentHeight.transform.position.y)
                currentHeight = otherTransform;

            if (laserEnd.transform.position.y < currentHeight.position.y)
            {
                Vector3 newPosition = new Vector3(laserEnd.localPosition.x, laserEnd.InverseTransformPoint(currentHeight.position).y, laserEnd.localPosition.z);
                laserEnd.localPosition = newPosition;
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.name == "Trail" || collision.gameObject.tag == "Wall")
        {
            laserHit = false;
            currentHeight = null;
        }
    }

}
