using Godot;
using System;
using System.Collections.Generic;
using GenericPlatforformer.State;

namespace GenericPlatforformer.Enemy;
public partial class StateManagerCrawler : Node
{

	[Export] string stateManagerName = "StateManagerCrawler";
	[Signal]
	public delegate void StateChangedEventHandler(string state);
	
	

	public EnemyState CurrentState { get; private set; }

	
	private crawler _enemy = null;
	private Player _player = null;
	private AnimationTree _animationTree = null;
	private AnimationNodeStateMachinePlayback _playback = null;
	private AudioStreamPlayer2D _sound;
	
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
		GD.Print("errr" + state);
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
			case StateTypes.Prepare:
				stateName = "prepare";
				break;
			default:
				break;
		}
		
		GD.Print("errr" + CurrentState);
		if (CurrentState != null)
		{
			CurrentState.NextState = null;
			CurrentState.Exit();
		}

		if (CurrentState != AvailableStates[stateName])
		{
			CurrentState = AvailableStates[stateName];
			CurrentState.Enter();
		}
	}
	
	public Dictionary<string, EnemyState> AvailableStates = new Dictionary<string, EnemyState>();
	public override void _Ready()
	{
		_player = GetTree().GetFirstNodeInGroup("Player") as Player;
		_enemy = GetParent<crawler>();
		_animationTree = _enemy.GetNode<AnimationTree>("AnimationTree");
		_playback = (AnimationNodeStateMachinePlayback)_animationTree.Get("parameters/playback");
		_sound = _enemy.GetNode<AudioStreamPlayer2D>("Sounds");

		var groundstate = new GroundState(_enemy, _player, _playback, _sound, true);
		groundstate.FollowRange = 500f;
		groundstate.AttackRange = 300f;
		groundstate.MoveSpeed = 120f;
		AvailableStates.Add("ground", groundstate);
		
		AvailableStates.Add("attack", new CrawlerAttackState(_enemy, _player,_playback,_sound, false, 2));
		AvailableStates.Add("prepare", new PrepareState(_enemy, _player,_playback,_sound,true));
		AvailableStates.Add("hit", new HitState(_enemy, _player,_playback,_sound, false));
		AvailableStates.Add("die", new DieState(_enemy, _player,_playback,_sound, false));
		
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
