using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBossState  {

    Boss boss { get; set; }

    void Update();
    void Reset();
    IBossState NextState();
    bool ShouldSwitch();
    
}
