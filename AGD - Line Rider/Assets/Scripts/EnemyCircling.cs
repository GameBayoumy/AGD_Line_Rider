using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//!  The CirclingEnemy class. 
/*!
  This class contains all the code that makes the CirclingEnemy spin around.
*/


public class EnemyCircling : EnemyBasic {

    [SerializeField]
    public float RotateSpeed = 4f; /*!< Determines the speed of the Rotation */
    [SerializeField]
    public float Radius = 2f; /*!< Determines the distance from the Center */
    public Vector2 Offset; /*!< Offset stores the Radius value into an Vector2 */
    public Vector2 Center; /*!< Center stores the coordinate into an Vector2 which the enemy rotates around */
    public float Angle; /*!< Handles the correct angle of the rotation */


    public int CollisionCount = 0; /*!< Stores how many frame there has been collision with an object */


    public void Start()
    {
        //! Start.
        /*!
          Stores the local position of the EnemyPrefab at the start.
        */
        Center = transform.position;
    }

    public void Update()
    {
        //! Update.
        /*!
          Lets the CirclingEnemy rotate using the offset and the position.
        */

        if(CollisionCount == 0){
            Angle += RotateSpeed * Time.deltaTime;
            Offset = new Vector2(Mathf.Sin(Angle), Mathf.Cos(Angle)) * Radius;
            transform.position = Center + Offset;
        }
    }


    public void OnCollisionEnter2D(Collision2D collision)
    {
        //! CollisionEnter.
        /*!
          Handles the collision with the Trail and Player. Also makes sure that the Enemy can be blocked by the Trail.
        */
        if (collision.gameObject.name == "Trail")
        {
            CollisionCount = 1;
        }

        if (collision.gameObject.tag == "Player")
        {
            GameOverMenu.SetGameOverState(true);
        }
    }

    public void OnCollisionExit2D(Collision2D collision)
    {
        //! CollisionExit.
        /*!
          Sets CollisionCount back to 0 when the collision stops so the rotation can continue.
        */
        CollisionCount = 0;
    }
}
