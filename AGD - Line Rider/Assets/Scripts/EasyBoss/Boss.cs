using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public float healthPoints;
    public float movementSpeed;
    public int attacksSpawned;

    //Boss states.
    public static EasyBossIntroState introState;
    public static EasyBossEatState eatState;
    public static EasyBossChaseState chaseState;
    public static EasyBossClawState clawState;
    public static EasyBossDeathState deathState;
    public static EasyBossHurtState hurtState;
    public static EasyBossLaserState laserState;
    public static EasyBossSpawnState spawnState;

    private IBossState _currentState;

    private void Awake()
    {
        // Retrieve all the states
        introState = gameObject.GetComponent<EasyBossIntroState>();
        eatState = gameObject.GetComponent<EasyBossEatState>();
        chaseState = gameObject.GetComponent<EasyBossChaseState>();
        clawState = gameObject.GetComponent<EasyBossClawState>();
        deathState = gameObject.GetComponent<EasyBossDeathState>();
        hurtState = gameObject.GetComponent<EasyBossHurtState>();
        laserState = gameObject.GetComponent<EasyBossLaserState>();
        spawnState = gameObject.GetComponent<EasyBossSpawnState>();

        // Set Boss reference to this object.
        introState.boss = this;
        eatState.boss = this;
        spawnState.boss = this;

        SetState(introState);
   

    }
    private void Update()
    {
        _currentState.Update();
    }

    

    public void SetState(IBossState state)
    {
        _currentState = state;
        _currentState.Reset();

        Debug.Log("current state set to : " + _currentState);

    }

}
