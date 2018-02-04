using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public float healthPoints;
    public float movementSpeed;
    public int attacksSpawned;

    // Boss states.
    public static EasyBossIntroState introState;
    public static EasyBossEatState eatState;
    public static EasyBossSpawnState spawnState;

    private IBossState _currentState;
    private List<IBossState> _availableStates = new List<IBossState>();
    private List<KeyValuePair<float, IBossState>> _stateProgression = new List<KeyValuePair<float, IBossState>>();

    [SerializeField]
    private GameObject _setSpawner;

    private float _timer;

    private void Awake()
    {
        // Retrieve all the states.
        introState = gameObject.GetComponent<EasyBossIntroState>();
        eatState = gameObject.GetComponent<EasyBossEatState>();
        spawnState = gameObject.GetComponent<EasyBossSpawnState>();

        // Set Boss reference to this object.
        introState.boss = this;
        eatState.boss = this;
        spawnState.boss = this;

        // States which the boss can go to.
        _availableStates.Add(introState);
  
        // Contains the MINIMAL spawn time and state.
        _stateProgression.Add(new KeyValuePair<float, IBossState>(10f, spawnState));
        _stateProgression.Add(new KeyValuePair<float, IBossState>(20f, eatState));

        SetState(introState);
    }
    private void Update()
    {
        _currentState.Update();

        if (_currentState.ShouldSwitch())
        {
            _currentState.Disable();
            IBossState _nextState = NextState();
            if (_nextState != null)
            {
                SetState(_nextState);
            }
        }

        // Disables the set spawner if boss is active.
        if (enabled)
        {
            _setSpawner.SetActive(false);
        }
        _timer += Time.deltaTime;
    }

    public void SetState(IBossState state)
    {
        _currentState = state;
        _currentState.Reset();
    }

    private IBossState NextState()
    {
        if (_stateProgression.Count > 0 && _timer > _stateProgression[0].Key)
        {
            _availableStates.Add(_stateProgression[0].Value);
            _stateProgression.RemoveAt(0);
        }

        // Using the int override for Random.Range.
        int _randomState = Random.Range(0, _availableStates.Count);
        IBossState _nextState = _availableStates[_randomState];
        Debug.Log(_nextState);
        _nextState.Enable();
        return _nextState;
    }
}
