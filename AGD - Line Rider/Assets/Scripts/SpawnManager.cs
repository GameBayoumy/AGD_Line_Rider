using UnityEngine;
using System.Collections;

public class SpawnManager : MonoBehaviour
{

	//Object Poolers
    public ObjectPoolScript enemyPool;
    public ObjectPoolScript enemywallPool;

	public GameObject Player;
	public GameObject enemyBasic;

    public GameObject enemyWall;

	// Spawn range
	Vector3 spawnPosition;
    private int minSpawnPosY = -7;
    private int maxSpawnPosY = 7;

	//Timers

    float enemyTimer = 0;
    float enemywallTimer = 0;


    float enemySpawnInterval = 5f;
    float enemywallSpawnInterval = 5f;
	float estimatedPlaytime = 300f;

	//Mathf.Lerp


	float enemyStartTime = 5f;
	float enemyEndTime = 1f;

	float enemywallStartTime = 10f;
	float enemywallEndTime = 1f;



    // Update is called once per frame
    void Update()
	{

		if (enemyTimer < enemySpawnInterval)
		{
			enemyTimer += Time.deltaTime;
		}
		else
		{
			enemyTimer = 0;
			SpawnObject(enemyPool);
			//Increase level difficulty by decreasing the spawn interval time
			enemySpawnInterval = Mathf.Lerp(enemyStartTime, enemyEndTime, Time.timeSinceLevelLoad / estimatedPlaytime);
		}

		if (enemywallTimer < enemywallSpawnInterval)
		{
			enemywallTimer += Time.deltaTime;
		}
		else
		{
			enemywallTimer = 0;
			SpawnWallObstacle(enemywallPool);
			//Increase level difficulty by decreasing the spawn interval time
			enemywallSpawnInterval = Mathf.Lerp(enemywallStartTime, enemywallEndTime, Time.timeSinceLevelLoad / estimatedPlaytime);
		}
	}

    // Spawns the enemy object
	void SpawnObject(ObjectPoolScript pool)
	{
        if (pool != null)
        {
            spawnPosition = new Vector3(Player.transform.position.x + 10, enemyBasic.transform.position.y, enemyBasic.transform.position.z);
            GameObject newItem = pool.GetPooledObject();
            newItem.transform.position = spawnPosition;
            newItem.transform.rotation = transform.rotation;
            newItem.SetActive(true);
        }
	}
	void SpawnWallObstacle(ObjectPoolScript pool)
	{

		spawnPosition = new Vector3(Player.transform.position.x + 30, Random.Range(minSpawnPosY, maxSpawnPosY), enemyWall.transform.position.z);
		GameObject newItem = pool.GetPooledObject();
		newItem.transform.position = spawnPosition;
		newItem.transform.rotation = transform.rotation;
		newItem.SetActive(true);
	}
}