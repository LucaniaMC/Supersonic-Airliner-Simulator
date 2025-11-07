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
        Debug.Log("Player: ground state entered");

        player.shadow.isActive = false;
    }

    public override void StateUpdate()
    {
        Transitions();
    }

    public override void StateFixedUpdate(){}

    public override void OnExit()
    {
        EffectManager.instance.InstantiateEffect("Launch", player.transform.position, Quaternion.identity);
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
        Debug.Log("Player: air state entered");
        player.shadow.isActive = true;
        LevelManager.instance.SetStartTime();
    }

    public override void StateUpdate()
    {
        player.movement.MoveTowardsCursor();

        //Switch between speeds when holding down left mouse button
        if (Input.GetMouseButton(0))
        {
            player.movement.SonicBoost();
            player.fuelBar.DecreaseFuel(1f);
        }
        else
        {
            player.movement.Move();
            player.fuelBar.DecreaseFuel(0.5f);
        }

        //play boost start sound on click
        if (Input.GetMouseButtonDown(0))
        {
            EffectManager.instance.InstantiateEffect("Boost", player.transform.position, player.transform.rotation);
            AudioManager.instance.PlaySFX("BoostStart", false);
        }

        //fail if out of fuel
        if (player.fuelBar.fuel == 0)
        {
            LevelManager.instance.Fail(DeathType.Fuel);
            Debug.Log("PlayerState: Failed by out of fuel");
        }
        
        Transitions();
    }

    public override void StateFixedUpdate(){}

    public override void OnExit(){}

    public override void Transitions()
    {
        if (LevelManager.instance.status == LevelManager.LevelStatus.Failed)
        {
            player.TransitionToState(new PlayerFailState(player, LevelManager.instance.causeOfDeath));
        }

        if (LevelManager.instance.status == LevelManager.LevelStatus.Finished)
        {
            player.TransitionToState(new PlayerWinState(player));
        }
    }
}
#endregion


#region Fail State
public class PlayerFailState : PlayerState
{
    DeathType deathType;

    public PlayerFailState(PlayerStateMachine player, DeathType deathType) : base(player)
    {
        this.deathType = deathType;
    }

    public override void OnEnter()
    {
        Debug.Log("Player: fail state entered");
        AudioManager.instance.ToggleLoopingSFX("BoostLoop", false);
        AudioManager.instance.PlaySFX("Fail", false); //Failsound

        switch (deathType)
        {
            case DeathType.Fuel:
                // do something
                break;

            case DeathType.DeathZone:
                // do something
                break;

            case DeathType.House:
                // do something
                break;

            case DeathType.Bird:
                // do something
                break;

            case DeathType.BlackHole:
                // do something
                break;

            case DeathType.Collision:
                // do something
                break;

            default:
                // nothing
                break;
        }
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
        Debug.Log("Player: win state entered");
        player.shadow.isActive = false;
        AudioManager.instance.ToggleLoopingSFX("BoostLoop", false);
        AudioManager.instance.PlaySFX("Finish", false);
        EffectManager.instance.InstantiateEffect("Confetti", LevelManager.instance.goal.transform.position, Quaternion.identity);
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