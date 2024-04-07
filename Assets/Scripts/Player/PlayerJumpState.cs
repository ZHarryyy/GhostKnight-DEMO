using UnityEngine;

public class PlayerJumpState : PlayerState
{
    //public bool Jumping = false;//��Ծ������״̬
    public PlayerJumpState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        rb.velocity = new Vector2(rb.velocity.x, player.jumpForce);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        player.SetVelocity(xInput * player.moveSpeed, rb.velocity.y);

        if (Input.GetKeyDown(KeyCode.J)) stateMachine.ChangeState(player.primaryAttackState);

        if (rb.velocity.y < 0)//�½�״̬
        {
            stateMachine.ChangeState(player.airState);
        }
    }
}
