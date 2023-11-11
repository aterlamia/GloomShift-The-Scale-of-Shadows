using Godot;

namespace GenericPlatforformer.State;

public class AirState : State
{
    private readonly float _jumpForce = -400.0f;
    private bool _hasDoubleJumped = false;

    public AirState(PLayer player, AnimationNodeStateMachinePlayback playback, bool canMove) : base(player, playback,
        canMove)
    {
    }

    public override void ProcessState(double delta)
    {
        if (_player.IsOnFloor())
        {
            _playback.Travel("land");
            NextState = StateTypes.Ground;
        }
        else
        {
            if (_player.Velocity.Y > 0.0f)
            {
                _playback.Travel("falling");
            }
        }
    }

    public override void InputState(InputEvent @event)
    {
        if (!Input.IsActionJustPressed("jump") || _hasDoubleJumped) return;
        
        _player.Velocity = new Vector2(_player.Velocity.X, _jumpForce);
        _hasDoubleJumped = true;
        _playback.Travel("double_jump");
    }


    public override void Exit()
    {
        _hasDoubleJumped = false;
    }
}