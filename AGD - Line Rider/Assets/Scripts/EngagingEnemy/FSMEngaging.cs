using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSMEngaging : EnemyBasic {

	// Use this for initialization
    protected virtual void Initialize()
    {
    }
    protected virtual void FSMUpdate()
    {
    }
    protected virtual void FSMFixedUpdate()
    {
    }

    void Start()
    {
        Initialize();
    }

    void Update()
    {
        FSMUpdate();
    }

    void FixedUpdate()
    {
        FSMFixedUpdate();
    }
}
