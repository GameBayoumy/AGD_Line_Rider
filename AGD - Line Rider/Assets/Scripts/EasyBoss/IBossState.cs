using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBossState  {

    //Retrieves the boss game object
    Boss boss { get; set; }

    void Update();
    void Reset();

    //When ShouldSwitch returns true, checks which state should be the next state, based on certain transition conditions
    IBossState NextState();
    //Checks  whether the state is able to switch (e.g. behavior has finished its task)
    bool ShouldSwitch();
    
}
