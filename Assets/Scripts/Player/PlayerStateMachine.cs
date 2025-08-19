using UnityEngine;

public class PlayerStateMachine : MonoBehaviour
{
    //For player movements
    public float moveSpeed = 0f;
    public Vector3 target;

    [HideInInspector] public bool input = true; //For disabling input

    //Instances
    public GameObject boom;
    public GameObject launch;

    [HideInInspector] public AudioManager audioManager;


    public PlayerState currentState { get; private set; }   
    public PlayerState defaultState {get; private set;}


    #region Loop
    void Start() 
    {
        audioManager = FindObjectOfType<AudioManager>();
        InitializeStateMachine();
    }


    void Update() 
    {
        currentState.StateUpdate();
    }


    void FixedUpdate() 
    {
        currentState.StateFixedUpdate();
    }
    #endregion


    #region State Machine Functions
    // Called in Awake/Start on the player script
    public void InitializeStateMachine() 
    {
        defaultState = new PlayerGroundedState(this);
        TransitionToState(defaultState);
    }


	//Exit current state, and enter new state
    public void TransitionToState(PlayerState newState)
    {
        if (currentState != null)
            currentState.OnExit();

        currentState = newState;

        if (currentState != null)
            currentState.OnEnter();
    }
    #endregion
}
