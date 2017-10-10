using UnityEngine;
using System.Collections;

public class SpawnManager : MonoBehaviour
{

	//Object Poolers
    public ObjectPoolScript enemyPool;
    public ObjectPoolScript enemywallPool;
    public ObjectPoolScript enemyLaserPool;

	public GameObject Player;
	public GameObject enemyBasic;
    public GameObject enemyLaser;
    public GameObject enemyWall;

	// Spawn range
	Vector3 spawnPosition;
    Vector3 spawnRotation;
    float spawnRangeRotation = 14;
    private int minSpawnPosY = -7;
    private int maxSpawnPosY = 7;

	//Timers

    float enemyTimer = 0;
    float enemywallTimer = 0;
    float enemylaserTimer = 0;


    float enemySpawnInterval = 15f;
    float enemywallSpawnInterval = 20f;
    float enemylaserSpawnInterval = 30f;
	float estimatedPlaytime = 1500f;

	//Mathf.Lerp


	float enemyStartTime = 5f;
	float enemyEndTime = 1f;

	float enemywallStartTime = 10f;
	float enemywallEndTime = 1f;

    float enemylaserStartTime = 15f;
    float enemylaserEndTime = 1f;



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
            Debug.Log(enemySpawnInterval);
            //Increase level difficulty by decreasing the spawn interval time
            //enemySpawnInterval = Mathf.Lerp(enemyStartTime, enemyEndTime, Time.timeSinceLevelLoad / estimatedPlaytime);
            enemySpawnInterval--;
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
            //enemywallSpawnInterval = Mathf.Lerp(enemywallStartTime, enemywallEndTime, Time.timeSinceLevelLoad / estimatedPlaytime);
            enemywallSpawnInterval--;
		}

		if (enemylaserTimer < enemylaserSpawnInterval)
		{
			enemylaserTimer += Time.deltaTime;
		}
		else
		{
			enemylaserTimer = 0;
			SpawnLaserObstacle(enemyLaserPool);
            //Increase level difficulty by decreasing the spawn interval time
            //enemylaserSpawnInterval = Mathf.Lerp(enemylaserStartTime, enemylaserEndTime, Time.timeSinceLevelLoad / estimatedPlaytime);
            enemylaserSpawnInterval--;
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

	void SpawnLaserObstacle(ObjectPoolScript pool)
	{

		spawnPosition = new Vector3(Player.transform.position.x + 40, 7.8f, 0);
        spawnRotation = new Vector3(0, 0, Random.Range(-spawnRangeRotation, spawnRangeRotation));
		GameObject newItem = pool.GetPooledObject();
		newItem.transform.position = spawnPosition;
        newItem.transform.eulerAngles = spawnRotation;
		newItem.SetActive(true);
	}
}