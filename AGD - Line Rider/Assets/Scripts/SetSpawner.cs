using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetSpawner : MonoBehaviour {

    //Object pools
    //Easy objectpool
    //Normal objectpool
    //Hard objectpool

    public bool canSpawn;
    public Transform spawnPosition;

    private float _easyChance;
    private float _normalChance;
    private float _hardChance;
    private float _totalChance;

    // Percentage spawn chance 
    private float _easySpawnChance;
    private float _normalSpawnChance;
    private float _hardSpawnChance;

    private float _randomValue;
    private float _endOfSet;
    // Use this for initialization


    // Update is called once per frame
    void Update()
    {
        if (_endOfSet < spawnPosition.position.x)
        {
            canSpawn = true;
        }

        if (canSpawn)
        {
            switch (/*SetDifficulty.difficulty*/ )
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

            _totalChance = _easyChance + _normalChance + _hardChance;
            _easySpawnChance = _easyChance / _totalChance;
            _normalSpawnChance = _normalChance / _totalChance;
            _hardSpawnChance = _hardChance / _totalChance;

            _randomValue = Random.value;
            if (_randomValue <= _easySpawnChance)
            {
                SpawnSet(); // easy pool
            }
            _randomValue -= _easySpawnChance;

            if (_randomValue <= _normalSpawnChance)
            {
                SpawnSet(); // normal pool
            }
            _randomValue -= _normalSpawnChance;

            if (_randomValue<= _hardSpawnChance)
            {
                SpawnSet(); // hard pool
            }

        }
    }

    void SpawnSet()// object pool inside method 
    {
        //GameObject newItem = pool.GetPooledObject();
        //newItem.transform.position = spawnPosition;
        //newItem.transform.rotation = transform.rotation;
        //newItem.SetActive(true);
        //_endOfSet = newItem.transform.GetChild(0) // assuming its the first child...? plox? i dont even know if this works.

    }
}
