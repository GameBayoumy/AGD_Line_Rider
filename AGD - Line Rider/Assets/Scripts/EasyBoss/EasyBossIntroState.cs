using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EasyBossIntroState : MonoBehaviour, IBossState
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
    private float _introTimer;
    private float _introTime;

    private void Awake()
    {
        _introTimer = 0;
        _introTime = 10f;
    }

    public void Reset()
    {
        //Animation of boss appearing starts
        _introTimer = 0;
    }

    // kijkt of er geswitched KAN wordne
    public bool ShouldSwitch()
    {

        if (_introTimer < _introTime)
        {
            _introTimer += Time.deltaTime;
            return false;
        }

        else
        {
            return true;
        }
        

    }

    public IBossState NextState()
    {
        // Bepaald wat de volgende state is wanneer er geswitched mag worden
        //random.value <x
        // return Boss.introstate
        Boss.eatState.enabled = true;
        return Boss.eatState;
        //return null;
    }

    public void Update()
    {
        ShouldSwitch();

        if(ShouldSwitch())
        {
            IBossState state = NextState();
            if (state != null)
            {
                _boss.SetState(state);
                enabled = false;
            }
        }
    }
}
