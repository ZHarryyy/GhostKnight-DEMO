using UnityEngine;

public class PlayerDownAttackState : PlayerState
{

    public PlayerDownAttackState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
        
    }

    public override void Enter()
    {
        base.Enter();
        attackCharged = false;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if(attackCharged)
        {
            player.SetVelocity(xInput * player.moveSpeed, -10);//������һ֡���ٶȽ����˶� 
        }
        else
        {
            player.SetZeroVelocity();
        }

        if (triggerCalled)
        {
            if (player.IsGroundDetected())
            {
                stateMachine.ChangeState(player.idleState);
            }
            else
            {
                stateMachine.ChangeState(player.airState);
            }
        }
    }
}

