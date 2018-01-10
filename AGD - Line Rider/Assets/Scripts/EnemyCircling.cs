using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCircling : EnemyBasic {

    [SerializeField]
    private float RotateSpeed = 4f;
    [SerializeField]
    private float Radius = 2f;
    private Vector2 Offset;

    private Vector2 Center;
    private float Angle;


    private int CollisionCount = 0;


    private void Start()
    {
        Center = transform.position;
    }

    private void Update()
    {

        if(CollisionCount == 0){
            Angle += RotateSpeed * Time.deltaTime;
            Offset = new Vector2(Mathf.Sin(Angle), Mathf.Cos(Angle)) * Radius;
            transform.position = Center + Offset;
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Trail")
        {
            CollisionCount = 1;
        }

        if (collision.gameObject.tag == "Player")
        {
            GameOverMenu.SetGameOverState(true);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        CollisionCount = 0;
    }
}
