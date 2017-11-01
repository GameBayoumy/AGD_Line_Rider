using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EasyBossLaserState : MonoBehaviour,IBossState {

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

    public IBossState NextState()
    {
        throw new NotImplementedException();
    }

    public void Reset()
    {
        throw new NotImplementedException();
    }

    public bool ShouldSwitch()
    {
        throw new NotImplementedException();
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void IBossState.Update()
    {
        throw new NotImplementedException();
    }
}
