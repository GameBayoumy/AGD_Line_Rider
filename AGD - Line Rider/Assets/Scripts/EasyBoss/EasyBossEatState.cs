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
    private GameObject _bumper;
    private float _eatTimer;
    private float _eatTime;
    private Transform _playerPos;
    private bool _spawnedBumper;


    private void Awake()
    {
        _bumper = _boss.transform.GetChild(2).gameObject;
        _playerPos = GameObject.FindWithTag("Player").transform;
        _eatTimer = 0f;
        _eatTime = 30;
        _spawnedBumper = false;


    }

    private void SpawnBumper()
    {
        _bumper.transform.position = new Vector3(_playerPos.position.x + 20, 0, 0);
        _spawnedBumper = true;


    }
    public IBossState NextState()
    {
        // return Boss.laserState;
        return null;
    }

    public void Reset()
    {
        _eatTimer = 0;
    }

    public bool ShouldSwitch()
    {

        if (_eatTimer < _eatTime)
        {
            _eatTimer += Time.deltaTime;
            return false;
        }

        else
        {
            return true;
            
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
            }
        }
    }
}
