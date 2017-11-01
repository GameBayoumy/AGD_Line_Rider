using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EasyBossIntroState : IBossState
{
    private Boss _boss;
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

    public void Reset()
    {
        //Animation of boss appearing starts
    }

    // kijkt of er geswitched KAN wordne
    public bool ShouldSwitch()
    {
        // timer
        return true;

    }

    public IBossState NextState()
    {
        // Bepaald wat de volgende state is wanneer er geswitched mag worden
        //random.value <x
        // return Boss.introstate
        return null;
    }

    public void Update()
    {
        if(ShouldSwitch())
        {
            IBossState state = NextState();
            if (state != null)
            {
                _boss.SetState(state);
            }
        }
    }
}
