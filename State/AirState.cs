using GenericPlatforformer.Shop;
using Godot;

namespace GenericPlatforformer.State;

public class AirState : State
{
    private readonly float _jumpForce = -350.0f;
    private bool _hasDoubleJumped = false;
    private readonly GlobalState _globalState;

    public AirState(Player player, AnimationNodeStateMachinePlayback playback, AudioStreamPlayer2D sound, bool canMove, GlobalState globalState)
        : base(player, playback, sound,
            canMove)
    {
     _globalState = globalState;   
    }

    public override void ProcessState(double delta)
    {
        if (_player.IsOnFloor())
        {
            _sound.Play();
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
        if (!Input.IsActionJustPressed("jump") || _hasDoubleJumped || !_globalState.HasPower(Powers.DualJump)) return;

        _player.Velocity = new Vector2(_player.Velocity.X, _jumpForce);
        _hasDoubleJumped = true;
        _playback.Travel("double_jump");
    }

    public override void Enter()
    {
        var land = (AudioStreamOggVorbis)ResourceLoader.Load("res://assets/sounds/jump.ogg");
        _sound.Stream = land;
    }

    public override void Exit()
    {
        _hasDoubleJumped = false;
    }
}