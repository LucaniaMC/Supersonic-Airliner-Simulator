using UnityEngine;

public class PlayerStateMachine : MonoBehaviour
{
    public GameObject overlay;      //overlay object on top of screen
    public WindParticleEffect windParticles;    //wind particle script

    //Instances
    public GameObject boom;
    public GameObject launch;
    public GameObject confetti;
    public GameObject boost;

    //References
    [HideInInspector] public PlayerMovement movement;
    [HideInInspector] public FuelBar fuelBar;
    [HideInInspector] public PlayerShadow shadow;


    public PlayerState currentState { get; private set; }   
    public PlayerState defaultState {get; private set;}


    #region Loop
    void Start() 
    {
        fuelBar = FindObjectOfType<FuelBar>();
        shadow = GetComponentInChildren<PlayerShadow>();
        movement = FindObjectOfType<PlayerMovement>();

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
