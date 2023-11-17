using Godot;

namespace GenericPlatforformer.State;

public class GroundState: State
{
    private float _jumpForce = -400.0f;
    public override void ProcessState(double delta)
    {
        if (!_player.IsOnFloor())
        {
            NextState = StateTypes.Air;
        }
    }

    public override void InputState(InputEvent @event)
    {
        if (Input.IsActionJustPressed("jump"))
        {
            _player.Velocity = new Vector2(_player.Velocity.X,_jumpForce);
            _playback.Travel("jump_start");
        } else if (Input.IsActionJustPressed("attack"))
        {
            NextState = StateTypes.Attack;
        }
    }

    public override void Enter()
    {
        _playback.Travel("move");
    }

    public GroundState(Player player, AnimationNodeStateMachinePlayback playback, bool canMove) : base(player, playback, canMove)
    {
    }
}