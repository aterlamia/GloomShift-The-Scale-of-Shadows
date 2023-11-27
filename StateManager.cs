using Godot;
using System;
using System.Collections.Generic;
using GenericPlatforformer;
using GenericPlatforformer.State;

public partial class StateManager : Node
{
	GlobalState _globalState = null;
	[Export] string stateManagerName = "StateManager";
	[Signal]
	public delegate void StateChangedEventHandler(string state);
	
	public State CurrentState { get; private set; }

	
	private Player _player = null;
	private AnimationTree _animationTree = null;
	private AnimationNodeStateMachinePlayback _playback = null;
	public void ChangeState(StateTypes state)
	{
		var stateName = "ground"; // sane fallback
		switch (state)
		{
			case StateTypes.Air:
				stateName = "air";
				break;
			case StateTypes.Ground:
				stateName = "ground";
				break;
			case StateTypes.Attack:
				stateName = "attack";
				break;
			case StateTypes.Dialog:
				stateName = "dialog";
				break;
			default:
				GD.Print("Unknown state type");
				break;
		}
		if (CurrentState != null)
		{
			CurrentState.NextState = null;
			CurrentState.Exit();
		}

		CurrentState = AvailableStates[stateName];
		CurrentState.Enter();
	}
	
	public Dictionary<string, State> AvailableStates = new Dictionary<string, State>();
	public override void _Ready()
	{
		_globalState = GetNode<GenericPlatforformer.GlobalState>("/root/GlobalState");
		_player = GetParent<Player>();
		_animationTree = _player.GetNode<AnimationTree>("AnimationTree");
		_playback = (AnimationNodeStateMachinePlayback)_animationTree.Get("parameters/playback");
			
		AvailableStates.Add("ground", new GroundState(_player, _playback, true));
		AvailableStates.Add("air", new AirState(_player, _playback, true));
		AvailableStates.Add("attack", new AttackState(_player, _playback, false, 5.0f));
		AvailableStates.Add("dialog", new DialogState(_player, _playback, false, _globalState ));
		
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
