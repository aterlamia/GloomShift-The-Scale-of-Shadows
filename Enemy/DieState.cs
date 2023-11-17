using System;
using GenericPlatforformer.State;
using Godot;

namespace GenericPlatforformer.Enemy;

public class DieState: EnemyState
{
    public DieState(Enemy enemy, Player player, AnimationNodeStateMachinePlayback playback, bool canMove) : base(enemy,player, playback, canMove)
    {
    }
    
    public override void Enter()
    {
        _playback.Travel("die");
    }

}