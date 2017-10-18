using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetSpawner : MonoBehaviour {

    public bool canSpawn;
    //public Transform spawnTransform;
    private HighScore difficultySetter;
    private SetObjectPools objectPool;

    private float _easyChance;
    private float _normalChance;
    private float _hardChance;
    private float _totalChance;

    // Percentage spawn chance 
    private float _easySpawnChance;
    private float _normalSpawnChance;
    private float _hardSpawnChance;

    private float _randomValue;
    private Vector3 _endOfSet;
    private GameObject _spawnObject;
    private int _previousDifficulty;
    

    void Awake()
    {
        difficultySetter = GameObject.Find("GameController").GetComponent<HighScore>();
        objectPool = GameObject.Find("SetObjectPooler").GetComponent<SetObjectPools>();
        _spawnObject = GameObject.Find("spawnPoint");
        canSpawn = true;

        UpdateSpawnChances();
        _previousDifficulty = difficultySetter.currentDifficulty;
    }


    // Update is called once per frame
    void Update()
    {
        if (_spawnObject.transform.position.x > _endOfSet.x)
        {
            canSpawn = true;
            //Debug.Log("_endofSet.x :" + _endOfSet.x + ", spawnPosition.x:" + _spawnObject.transform.position.x);
        }
        else {
            //Debug.Log(" in_spawnPosition, should NOT BE 86 FFS" + _spawnObject.transform.position);
        }

        if (_previousDifficulty != difficultySetter.currentDifficulty)
        {
            _previousDifficulty = difficultySetter.currentDifficulty;
            UpdateSpawnChances();
        }

        if (canSpawn)
        {
            // Spawns the set based on a random value and the spawn percentages. 
            _randomValue = Random.value;
            if (_randomValue <= _easySpawnChance)
            {
                objectPool.chosenSets = objectPool.easySets;
                SpawnSet();
                return;
            }

            _randomValue -= _easySpawnChance;
            if (_randomValue <= _normalSpawnChance)
            {
                objectPool.chosenSets = objectPool.normalSets;
                SpawnSet();
                return;
            }

            _randomValue -= _normalSpawnChance;
            if (_randomValue <= _hardSpawnChance)
            {
                objectPool.chosenSets = objectPool.hardSets;
                SpawnSet();
                return;
            }

        }
    }

    void UpdateSpawnChances()
    {
        switch (difficultySetter.currentDifficulty)
        {
            case 0: //easy
                _easyChance = 100;
                _normalChance = 0;
                _hardChance = 0;
                break;
            case 1: // normal
                _easyChance = 100;
                _normalChance = 500;
                _hardChance = 0;
                break;
            case 2: // hard
                _easyChance = 100;
                _normalChance = 500;
                _hardChance = 1500;
                break;
        }

        // Calculate percentages 
        _totalChance = _easyChance + _normalChance + _hardChance;
        _easySpawnChance = _easyChance / _totalChance;
        _normalSpawnChance = _normalChance / _totalChance;
        _hardSpawnChance = _hardChance / _totalChance;

    }
    void SpawnSet()
    {
        canSpawn = false;

        GameObject set = objectPool.GetPooledSet();
        if (set == null)
        {
            return;
        }
        set.transform.position = new Vector3( _spawnObject.transform.position.x, 0, 0);
        Debug.Log(set.transform.position + "position set");
        //newItem.transform.rotation = transform.rotation;
        set.SetActive(true);
        _endOfSet = set.gameObject.transform.GetChild(0).position;
        
    }
}
