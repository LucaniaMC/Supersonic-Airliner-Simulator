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
        AudioManager.instance.PlaySFX("BoostStart", false);
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
        player.movement.MoveTowardsCursor();

        //Switch between speeds when holding down left mouse button
        if (Input.GetMouseButton(0))
        {
            player.movement.SonicBoost();
        }
        else
        {
            player.movement.Move();
        }

        //play boost start sound on click
        if (Input.GetMouseButtonDown(0))
        {
            AudioManager.instance.PlaySFX("BoostStart", false);
        }


        if (player.fuelBar.fuel == 0)
        {
            player.levelManager.status = LevelManager.LevelStatus.Failed;
        }
        
        Transitions();
    }

    public override void StateFixedUpdate(){}

    public override void OnExit(){}

    public override void Transitions()
    {
        if (player.levelManager.status == LevelManager.LevelStatus.Failed)
        {
            player.TransitionToState(new PlayerFailState(player));
        }

        if (player.levelManager.status == LevelManager.LevelStatus.Finished)
        {
            player.TransitionToState(new PlayerWinState(player));
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
        Debug.Log("fail state entered");
        AudioManager.instance.ToggleLoopingSFX("BoostLoop", false);
        AudioManager.instance.PlaySFX("Fail", false); //Failsound
        player.levelManager.Fail();
    }

    public override void StateUpdate()
    {
        player.movement.MoveTowardsCursor();
        player.movement.Move();
        Transitions();
    }

    public override void StateFixedUpdate(){}

    public override void OnExit(){}

    public override void Transitions(){}
}
#endregion


#region Win State
public class PlayerWinState : PlayerState
{
    public PlayerWinState(PlayerStateMachine player) : base(player) { }

    public override void OnEnter()
    {
        Debug.Log("win state entered");
        player.shadow.isActive = false;
        AudioManager.instance.ToggleLoopingSFX("BoostLoop", false);
        AudioManager.instance.PlaySFX("Finish", false);
        player.levelManager.Finish();
        GameObject.Instantiate(player.confetti, player.levelManager.goal.transform.position, Quaternion.identity);
    }

    public override void StateUpdate()
    {
        Transitions();
    }

    public override void StateFixedUpdate(){}

    public override void OnExit(){}

    public override void Transitions(){}
}
#endregion