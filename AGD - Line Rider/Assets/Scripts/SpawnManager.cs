using UnityEngine;
using System.Collections;

public class SpawnManager : MonoBehaviour
{

	//Object Poolers
	public ObjectPoolScript pickupPool;
	public ObjectPoolScript obstaclePool;
    public ObjectPoolScript enemyPool;
    public ObjectPoolScript enemywallPool;

	public GameObject Player;
	public GameObject enemyBasic;

    public GameObject enemyWall;

	// Spawn range
	public Transform minSpawnPos, maxSpawnPos;
	Vector3 spawnPosition;
    private int minSpawnPosY = -7;
    private int maxSpawnPosY = 7;

	//Timers
	float pickupTimer = 0;
	float obstacleTimer = 0;
    float enemyTimer = 0;
    float enemywallTimer = 0;

	float pickUpSpawnInterval = 11f;
	float obstacleSpawnInterval = 5f;
    float enemySpawnInterval = 5f;
    float enemywallSpawnInterval = 5f;
	float estimatedPlaytime = 300f;

	//Mathf.Lerp
	float obstacleStartTime = 5f;
	float obstacleEndTime = 1f;

	float pickupStartTime = 11f;
	float pickUpEndTime = 3f;

	float enemyStartTime = 5f;
	float enemyEndTime = 1f;

	float enemywallStartTime = 10f;
	float enemywallEndTime = 1f;


	// Update is called once per frame
	void Update()
	{
		if (obstacleTimer < obstacleSpawnInterval)
		{
			obstacleTimer += Time.deltaTime;
		}
		else
		{
			obstacleTimer = 0;
			SpawnObject(obstaclePool);
			//Increase level difficulty by decreasing the spawn interval time
			obstacleSpawnInterval = Mathf.Lerp(obstacleStartTime, obstacleEndTime, Time.timeSinceLevelLoad / estimatedPlaytime);
		}

		if (pickupTimer < pickUpSpawnInterval)
		{
			pickupTimer += Time.deltaTime;
		}
		else
		{
			pickupTimer = 0;
			SpawnObject(pickupPool);
			pickUpSpawnInterval = Mathf.Lerp(pickupStartTime, pickUpEndTime, Time.timeSinceLevelLoad / estimatedPlaytime);
		}

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

	void SpawnObject(ObjectPoolScript pool)
	{
		spawnPosition = new Vector3(Player.transform.position.x + 10, enemyBasic.transform.position.y, enemyBasic.transform.position.z);
		GameObject newItem = pool.GetPooledObject();
		newItem.transform.position = spawnPosition;
		newItem.transform.rotation = transform.rotation;
		newItem.SetActive(true);
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