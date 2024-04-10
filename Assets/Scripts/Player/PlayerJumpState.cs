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

        if (rb.velocity.y < 0)//�½�״̬
        {
            stateMachine.ChangeState(player.airState);
        }

        //��������
        if ((Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S)) && Input.GetKeyDown(KeyCode.J))
        {
            stateMachine.ChangeState(player.downAttackState);
        }
        else if (Input.GetKeyDown(KeyCode.J))
        {
            if(player.isRed)
            {
                //���й���
                stateMachine.ChangeState(player.primaryAttackState);
            }
            else
            {
                if(!player.AirComboFinished)
                {
                    stateMachine.ChangeState(player.jumpAttackState);
                }
            }

        }

    }
}
