using Godot;
using System;

public partial class lever : Node2D
{
	private bool _switchActive = false;
	
	[Signal]
	public delegate void SwitchedOnEventHandler();
	
	[Signal]
	public delegate void SwitchedOffEventHandler();
	
	public override void _Input(InputEvent @event)
	{
		if (@event.IsActionPressed("activate") && _switchActive)
		{
			EmitSignal(SignalName.SwitchedOn);
			GetNode<Sprite2D>("Leveroff").Visible = false;
			GetNode<Sprite2D>("Leveron").Visible = true;
			GetNode<PointLight2D>("PointLight2D").Color = new Color(0.0f, 0.6f, 0.0f, 1.0f);
		}
	}
	
	public override void _Process(double delta)
	{
	}

	private void _on_area_2d_area_entered(Node2D node2D)
	{
		if (node2D.Name == "LightDetector")
		{
			_switchActive = true;
		}
	}
	
	private void _on_area_2d_area_exited(Node2D node2D)
	{
		if (node2D.Name == "LightDetector")
		{
			_switchActive = false;
		}	
	}
}
