using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBossState
{
    // Retrieves the boss game object.
    Boss boss { get; set; }

    // Disables the component.
    void Disable();

    // Enables the component.
    void Enable();

    // Called every frame.
    void Update();

    // Resets variables where needed, so that the state will start with the default values.
    void Reset();

    // Checks whether the state is able to switch (e.g. behavior has finished its task).
    bool ShouldSwitch();

    
    
}
