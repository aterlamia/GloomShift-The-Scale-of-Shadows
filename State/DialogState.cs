using Godot;

namespace GenericPlatforformer.State;

public class DialogState : State
{
    private readonly GlobalState _globalState;
    public DialogState(Player player, AnimationNodeStateMachinePlayback playback, bool canMove, GlobalState globalState) : base(player, playback,
        canMove)
    {
        _globalState = globalState;
    }

    public override void Enter()
    {
        _player.Halt();
    }
}