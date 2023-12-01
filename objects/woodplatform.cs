using Godot;
using System;

public partial class woodplatform : AnimatableBody2D
{  
	[Export] private bool _moving = false;
	[Export] private Vector2 _endPos = new Vector2(0, 0);
	private Vector2 _startPos = new Vector2(0, 0);
	private Tween tween;
	private Node2D objectToMove;
	[Export] private float duration = 5.0f;
	public override void _Ready()
	{
		_startPos = Position; // Set i
		if (_moving)
		{
			StartMovement();
		}

	}

	private void _switchOn()
	{
		_moving = true;
		StartMovement();
	}
	private void StartMovement()
	{
		GD.Print(_endPos);
		tween = GetTree().CreateTween().SetProcessMode(Tween.TweenProcessMode.Physics);
		tween.SetLoops().SetParallel(false);
		tween.TweenProperty(this, "position", _startPos + _endPos, duration / 2);
		tween.TweenProperty(this, "position", _startPos, duration / 2);
	}

	private void OnTweenCompleted()
	{
		// The tween has completed one cycle
		StartMovement(); // Start the movement again
	}
	

	private void _onSwitch()
	{
		Position = _endPos;
	}
}
