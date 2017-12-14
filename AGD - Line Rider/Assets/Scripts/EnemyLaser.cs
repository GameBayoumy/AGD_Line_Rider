using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyLaser : MonoBehaviour {

    public float laserSpeed = 1.0f;
    public float factor = 1.0f;
    public bool isHorizontal = false;
    public bool invertDirection = false;
    public Transform laserStart;
    public Transform laserEnd;

    private Vector3 _currentLength;
    private bool _laserHit = false;
    private float _invertDirNumber = 1f;

	// Use this for initialization
	void Start () {
        _currentLength = new Vector3(0, -19f, 0);
        SetPos(laserStart.position, laserEnd.position);
    }

    private void Update()
    {
        if (invertDirection == true)
            _invertDirNumber = -1f;
        else
            _invertDirNumber = 1f;

        SetPos(laserStart.position, laserEnd.position);

        if (_laserHit)
        {
            if (!isHorizontal)
                laserEnd.position = new Vector3(laserEnd.position.x, _currentLength.y, 0);
            else if (isHorizontal)
                laserEnd.position = new Vector3(_currentLength.x, laserEnd.position.y, 0);
            laserEnd.localPosition = new Vector3(0, laserEnd.localPosition.y, 0);
        }
        else
        {
            if(!isHorizontal)
                laserEnd.position = new Vector3(laserEnd.position.x, laserEnd.position.y - ((5 * _invertDirNumber) * Time.deltaTime), 0);
            else if (isHorizontal)
                laserEnd.position = new Vector3(laserEnd.position.x - ((5 * _invertDirNumber) * Time.deltaTime), laserEnd.position.y, 0);
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
            GameOverMenu.SetGameOverState(true);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Trail" || collision.gameObject.tag == "Wall")
        {
            Vector3 contactPoint = new Vector3(collision.contacts[0].point.x, collision.contacts[0].point.y, 0);
            
            _laserHit = true;

            if (contactPoint.y > _currentLength.y)
            {
                _currentLength = contactPoint;
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.name == "Trail" || collision.gameObject.tag == "Wall")
        {
            _laserHit = false;
            _currentLength = new Vector3(0, -19f, 0);
        }
    }

}
