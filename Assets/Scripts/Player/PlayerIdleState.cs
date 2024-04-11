using UnityEngine;

public class PlayerIdleState : PlayerGroundedState
{
    public PlayerIdleState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();


        player.SetZeroVelocity();
        player.AirComboFinished = false;//���ÿ�������
        //player.jumpState.Jumping = false;//��غ����ÿ���Ծ
        player.gameObject.GetComponent<Rigidbody2D>().bodyType =  RigidbodyType2D.Kinematic;

    }

    public override void Exit()
    {
        base.Exit();
        player.gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;

    }

    public override void Update()
    {
        base.Update();

        if (xInput == player.facingDir && player.IsWallDetected()) return;

        if (xInput != 0 && !player.isBusy) stateMachine.ChangeState(player.moveState);
    }
}
