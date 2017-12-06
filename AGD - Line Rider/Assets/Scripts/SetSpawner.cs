using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetSpawner : MonoBehaviour {

    // Spawn chance values.
    private float _easyChance;
    private float _normalChance;
    private float _hardChance;
    private float _totalChance;

    // Percentage spawn chance.
    private float _easySpawnChance;
    private float _normalSpawnChance;
    private float _hardSpawnChance;

    private float _randomValue;
    private Vector3 _endOfSet;
    private GameObject _spawnObject;
    private int _previousDifficulty;
    private HighScore _difficultySetter;
    private SetObjectPools _objectPool;
    private bool _canSpawn;
    private bool _customGame;


    void Awake()
    {
        _difficultySetter = GameObject.Find("GameController").GetComponent<HighScore>();
        _objectPool = GameObject.Find("SetObjectPooler").GetComponent<SetObjectPools>();
        _spawnObject = GameObject.Find("spawnPoint");
        _canSpawn = true;

        UpdateSpawnChances();
        _previousDifficulty = _difficultySetter.currentDifficulty;

        if (GameObject.Find("CustomCheck") != null)
            _customGame = true;
    }

    void Update()
    {
        if (_spawnObject.transform.position.x > _endOfSet.x)
        {
            _canSpawn = true;
        }
       
        if (_previousDifficulty != _difficultySetter.currentDifficulty)
        {
            _previousDifficulty = _difficultySetter.currentDifficulty;
            UpdateSpawnChances();
            
        }
        if (_canSpawn)
        {
            if (!_customGame)
            {
                // Spawns the set based on a random value and the spawn percentages. 
                _randomValue = Random.value;
                if (_randomValue <= _easySpawnChance)
                {
                    _objectPool.chosenSets = _objectPool.easySets;
                    SpawnSet();
                    return;
                }

                _randomValue -= _easySpawnChance;
                if (_randomValue <= _normalSpawnChance)
                {
                    _objectPool.chosenSets = _objectPool.normalSets;
                    SpawnSet();
                    return;
                }

                _randomValue -= _normalSpawnChance;
                if (_randomValue <= _hardSpawnChance)
                {
                    _objectPool.chosenSets = _objectPool.hardSets;
                    SpawnSet();
                    return;
                }
            }
            else
            {
                _objectPool.chosenSets = _objectPool.customSets;
                SpawnSet();
                return;
            }

        }
    }

    void UpdateSpawnChances()
    {
        // Set spawn value chances based on the difficulty.
        switch (_difficultySetter.currentDifficulty)
        {
            // Easy.
            case 1: 
                _easyChance = 100;
                _normalChance = 0;
                _hardChance = 0;
                break;
            // Normal.
            case 2: 
                _easyChance = 100;
                _normalChance = 500;
                _hardChance = 0;
                break;
            // Hard.
            case 3: 
                _easyChance = 100;
                _normalChance = 500;
                _hardChance = 1500;
                break;
        }

        // Calculate spawn percentages. 
        _totalChance = _easyChance + _normalChance + _hardChance;
        _easySpawnChance = _easyChance / _totalChance;
        _normalSpawnChance = _normalChance / _totalChance;
        _hardSpawnChance = _hardChance / _totalChance;

    }

    void SpawnSet()
    {
        _canSpawn = false;
        GameObject set = _objectPool.GetPooledSet();
        // Remove this code when the object pool does NOT return null.
        if (set == null)
        {
            return;
        }
        set.transform.position = new Vector3( _spawnObject.transform.position.x, 0, 0);
        set.SetActive(true);
        _endOfSet = set.gameObject.transform.GetChild(0).position;
        
    }
}
