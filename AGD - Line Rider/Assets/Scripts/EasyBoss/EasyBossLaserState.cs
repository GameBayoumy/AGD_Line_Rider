using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EasyBossLaserState : MonoBehaviour, IBossState {

   
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
    private float _chargeTimer = 0;
    private float _chargeTime = 0;
    private float _laserAttackTimer = 0;
    private float _laserAttackTime = 0;
    private bool _laserDone = false;


    public IBossState NextState()
    {
        // Bepaald wat de volgende state is wanneer er geswitched mag worden (ShouldSwitch() is true)
        //random.value <x
        // return Boss.introstate
        return null;
    }

    public void Reset()
    {
        _chargeTimer = 0;
        _laserAttackTime = 0;
        _laserAttackTimer = 0;
        _laserDone = false;
    }

    public bool ShouldSwitch()
    {
        //timer + laser gone
        return true;
    }


    void IBossState.Update()
    {
        if (_laserDone == false)
        {
            if (_chargeTimer < _chargeTime)
            {
                _chargeTime += Time.deltaTime;
            }
            else
            {
                //spawn laser
                // timer for laser active
                // if laser active time is over, laser done returns true.

            }
        }

    }
}
