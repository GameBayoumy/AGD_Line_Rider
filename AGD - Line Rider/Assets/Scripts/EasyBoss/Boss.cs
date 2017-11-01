using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public float healthPoints;
    public float movementSpeed;
    public int attacksSpawned;

    // boss states
    public static EasyBossIntroState introState;
    private IBossState _currentState;

    private void Awake()
    {
        introState = new EasyBossIntroState();
        introState.boss = this;
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
    }

}
