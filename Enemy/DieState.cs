using System;
using GenericPlatforformer.State;
using Godot;

namespace GenericPlatforformer.Enemy;

public class DieState: EnemyState
{
    public DieState(Enemy enemy, Player player, AnimationNodeStateMachinePlayback playback, AudioStreamPlayer2D sound, bool canMove) : base(enemy,player, playback,sound, canMove)
    {
    }
    
    public override void Enter()
    {
        var land = (AudioStreamWav)ResourceLoader.Load("res://assets/sounds/hurt.wav");
        _sound.Stream = land;
        _sound.Play();
        _playback.Travel("die");
    }

}