using UnityEngine;

#region Default State
public abstract class PlayerState
{
    //References
    protected PlayerStateMachine player;

    public PlayerState(PlayerStateMachine player) 
    {
        this.player = player;
    }

    public abstract void OnEnter();              //Called once when the state is entered
    public abstract void StateUpdate();          //Called every frame in Update
    public abstract void StateFixedUpdate();     //Called in FixexUpdate
    public abstract void OnExit();               //Called once when the state is exited

    public virtual void Transitions() {}          //Called in State Update, organize all transitions
}
#endregion


#region Ground State
public class PlayerGroundedState : PlayerState
{
    public PlayerGroundedState(PlayerStateMachine player) : base(player) { }

    public override void OnEnter()
    {
        Debug.Log("ground state entered");

        player.fuelBar.enabled = false;
        player.shadow.isActive = false;
    }

    public override void StateUpdate()
    {
        Transitions();
    }

    public override void StateFixedUpdate(){}

    public override void OnExit()
    {
        GameObject.Instantiate(player.launch, player.transform.position, Quaternion.identity);
        player.fuelBar.enabled = true;
        player.shadow.isActive = true;
        player.audioManager.Play("BoostStart");
    }

    public override void Transitions()
    {
        if (Input.GetMouseButtonDown(0))
        {
            player.TransitionToState(new PlayerAirState(player));
        }
    }
}
#endregion


#region Air State
public class PlayerAirState : PlayerState
{
    public PlayerAirState(PlayerStateMachine player) : base(player) { }

    public override void OnEnter()
    {
        Debug.Log("air state entered");
        player.shadow.isActive = true;
    }

    public override void StateUpdate()
    {
        Move();
        MoveTowardsCursor();
        if (player.fuelBar.fuel == 0)
        {
            player.levelManager.failed = true;
        }
        Transitions();
    }

    public override void StateFixedUpdate()
    {

    }

    public override void OnExit()
    {

    }

    public override void Transitions()
    {
        if (player.levelManager.failed == true)
        {
            player.TransitionToState(new PlayerFailState(player));
        }

        if (player.levelManager.finished == true)
        {
            player.TransitionToState(new PlayerWinState(player));
        }
    }

    public void MoveTowardsCursor()
    {
        //Move towards mouse position
        player.target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        player.transform.position = Vector3.MoveTowards(player.transform.position, player.target, player.moveSpeed * Time.deltaTime);

        //Rotate towards mouse position
        Vector3 direction = player.target - player.transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        player.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);


        //Camera follow
        Camera.main.transform.position = player.transform.position;
    }

    public void Move()
    {
        //Switch between speeds when holding down left mouse button
        if (Input.GetMouseButton(0))    //Supersonic speed
        {
            player.moveSpeed = 3f;
            player.audioManager.ToggleLoopingSFX("BoostLoop", true);

            SpawnSonicBoom();
        }
        else   //Default speed
        {
            player.moveSpeed = 1.5f;
            player.audioManager.ToggleLoopingSFX("BoostLoop", false);
        }


        //Play initial boost
        if (Input.GetMouseButtonDown(0))
        {
            player.audioManager.Play("BoostStart");
        }
    }

    //for sonic boom timer
    float time = 0f;
    float delay = 0.1f;

    void SpawnSonicBoom()
    {
        time = time + 1f * Time.deltaTime;

        if (time >= delay)
        {
            time = 0f;
            GameObject.Instantiate(player.boom, player.transform.position, Quaternion.identity);
        }
    }
}
#endregion


#region Fail State
public class PlayerFailState : PlayerState
{
    public PlayerFailState(PlayerStateMachine player) : base(player) { }

    public override void OnEnter()
    {
        player.audioManager.ToggleLoopingSFX("BoostLoop", false);
        player.levelManager.Fail();
    }

    public override void StateUpdate()
    {
        Transitions();
    }

    public override void StateFixedUpdate()
    {

    }

    public override void OnExit()
    {

    }

    public override void Transitions()
    {

    }
}
#endregion


#region Win State
public class PlayerWinState : PlayerState
{
    public PlayerWinState(PlayerStateMachine player) : base(player) { }

    public override void OnEnter()
    {
        player.shadow.isActive = false;
        player.audioManager.ToggleLoopingSFX("BoostLoop", false);
        player.levelManager.Finish();
        GameObject.Instantiate(player.confetti, player.levelManager.goal.transform.position, Quaternion.identity);
    }

    public override void StateUpdate()
    {
        Transitions();
    }

    public override void StateFixedUpdate()
    {

    }

    public override void OnExit()
    {

    }

    public override void Transitions()
    {

    }
}
#endregion