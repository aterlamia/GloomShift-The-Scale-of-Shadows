using Godot;

namespace GenericPlatforformer.State;

public class DialogState : State
{
    private readonly GlobalState _globalState;
    public DialogState(Player player, AnimationNodeStateMachinePlayback playback,AudioStreamPlayer2D sound, bool canMove, GlobalState globalState) : base(player, playback,sound,
        canMove)
    {
        _globalState = globalState;
    }

    public override void Enter()
    {
        _player.Halt();
    }
}