using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EasyBossSpawnState : MonoBehaviour, IBossState
{
    public Boss boss
    {
        get
        {
            return _boss;
        }

        set
        {
            _boss = value;
        }
    }
    [SerializeField]
    private GameObject _basicEnemy;

    [SerializeField]
    private GameObject _portal;

    private Boss _boss;
    private Transform _endPortal;
    private Transform _playerPos;

    private float _spawnTimer;
    private float _spawnTime;
    private bool _canSpawn;
    private bool _portalSpawned;
    private Vector3 _portalSpawnPos;
    private GameObject _portalObj;
    

    private void Awake()
    {
        _playerPos = GameObject.FindWithTag("Player").transform;
        _spawnTimer = 0f;
        _spawnTime = 1f;
        _canSpawn = false;
        _portalSpawned = false;

    }
    private void SpawnPortal()
    {
        _portalSpawnPos = new Vector3(_playerPos.position.x + 50, 7.7f, 0);
        _portalObj = Instantiate(_portal, _portalSpawnPos, Quaternion.identity);
        _endPortal = _portalObj.transform.GetChild(0);
        _portalSpawned = true;
    }

    public void Reset()
    {
        _spawnTimer = 0;
        _canSpawn = false;
        _portalSpawned = false;
    }

    public void Enable()
    {
        enabled = true;
    }

    public void Disable()
    {
        enabled = false;
    }
    private void SpawnEnemy()
    {
        if ((_playerPos.position.x < _endPortal.position.x - 10f) && _canSpawn)
        {
            _canSpawn = false;
            Instantiate(_basicEnemy, new Vector3(_playerPos.position.x + 10f, 7.7f, 0), Quaternion.identity);
        }
    }

    public bool ShouldSwitch()
    {
        if (_portalSpawned)
        {
            if (_playerPos.position.x > _endPortal.position.x)
            {
                return true;
            }
            else
            {
                return false;
            }
            
        }
        return false;
    }

    public void Update()
    {
        ShouldSwitch();
        if (!_portalSpawned)
        {
            SpawnPortal();
        }

        // Timer for spawning enemies
        if (_spawnTimer < _spawnTime)
        {
            _spawnTimer += Time.deltaTime;
            _canSpawn = false;
        }
        else
        {
            _canSpawn = true;
            _spawnTimer = 0;
            SpawnEnemy();
        }
    }
    
}
