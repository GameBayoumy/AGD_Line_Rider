using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EasyBossEatState : MonoBehaviour, IBossState
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

    private Boss _boss;
    [SerializeField]
    private GameObject _bumper;

    private Transform _playerPos;
    private bool _spawnedBumper;
    private Vector3 _bumperSpawnPos;
    private GameObject _bumperObject;


    private void Awake()
    {
       // _bumper = _boss.transform.GetChild(2).gameObject;
        _playerPos = GameObject.FindWithTag("Player").transform;
        _spawnedBumper = false;
    }

    public void SpawnBumper()
    {
        _bumperSpawnPos = new Vector3(_playerPos.position.x + 20, 0, 0);
        _bumperObject = Instantiate(_bumper, _bumperSpawnPos, Quaternion.identity);
        _spawnedBumper = true;

    }
    public IBossState NextState()
    {
        Boss.spawnState.enabled = true;
        return Boss.spawnState;
    }

    public void Reset()
    {
        _spawnedBumper = false;
    }

    public bool ShouldSwitch()
    {
        if  ( _spawnedBumper && _playerPos.position.x > _bumperObject.transform.position.x)
        {
            return true;
            
        }
        else
        {
            return false;
        }

    }

    public void Update()
    {
        if (!_spawnedBumper)
        {
            SpawnBumper();
        }
        ShouldSwitch();

        if (ShouldSwitch())
        {
            IBossState state = NextState();
            if (state != null)
            {
                _boss.SetState(state);
                Destroy(_bumperObject);
                enabled = false;
                
                
            }
        }
    }
}
