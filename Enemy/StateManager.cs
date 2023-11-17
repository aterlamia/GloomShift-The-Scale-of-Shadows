using Godot;
using System;
using System.Collections.Generic;
using GenericPlatforformer.State;

namespace GenericPlatforformer.Enemy;
public partial class StateManager : Node
{

	[Export] string stateManagerName = "StateManager";
	[Signal]
	public delegate void StateChangedEventHandler(string state);
	
	public EnemyState CurrentState { get; private set; }

	
	private Enemy _enemy = null;
	private Player _player = null;
	private AnimationTree _animationTree = null;
	private AnimationNodeStateMachinePlayback _playback = null;

	public void CheckHealthAndSwitch()
	{
		var damageHandler = _enemy.GetNode<DamageHandler>("DamageHandler");
		if (damageHandler.Health <= 0)
		{
			damageHandler.Die();
		}
		else
		{
			ChangeState(StateTypes.Ground);
		}

	}
	
	// Make this listeb to signals
	public void ChangeState(StateTypes state)
	{
		var stateName = "ground"; // sane fallback
		switch (state)
		{
			case StateTypes.Ground:
				stateName = "ground";
				break;
			case StateTypes.Attack:
				stateName = "attack";
				break;
			case StateTypes.Hit:
				stateName = "hit";
				break;
			case StateTypes.Die:
				stateName = "die";
				break;
			default:
				GD.Print("Unknown state type");
				break;
		}
		GD.Print("Changing state" + state.ToString() + " to " + stateName );
		if (CurrentState != null)
		{
			CurrentState.NextState = null;
			CurrentState.Exit();
		}

		CurrentState = AvailableStates[stateName];
		CurrentState.Enter();
	}
	
	public Dictionary<string, EnemyState> AvailableStates = new Dictionary<string, EnemyState>();
	public override void _Ready()
	{
		_player = GetTree().GetFirstNodeInGroup("Player") as Player;
		_enemy = GetParent<Enemy>();
		_animationTree = _enemy.GetNode<AnimationTree>("AnimationTree");
		_playback = (AnimationNodeStateMachinePlayback)_animationTree.Get("parameters/playback");
			
		AvailableStates.Add("ground", new GroundState(_enemy,_player, _playback, true));
		AvailableStates.Add("attack", new AttackState(_enemy, _player,_playback, false, 5.0f));
		AvailableStates.Add("hit", new HitState(_enemy, _player,_playback, false));
		AvailableStates.Add("die", new DieState(_enemy, _player,_playback, false));
		
		ChangeState(StateTypes.Ground);
	}

	public override void _PhysicsProcess(double delta)
	{
		if (CurrentState.NextState != null)
		{
			ChangeState((StateTypes)CurrentState.NextState);
		}

		CurrentState.ProcessState(delta);
	}

	public override void _Input(InputEvent @event)
	{
		CurrentState.InputState(@event);
	}
	
	public bool CanMove()
	{
		if(CurrentState == null)
			return false;
		
		return CurrentState.CanMove;
	}
}
