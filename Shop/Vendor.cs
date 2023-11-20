using Godot;
using System;

public partial class Vendor : Node2D
{	private Boolean _vendorActive = false;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	
	private void _on_area_2d_area_entered(Node2D node2D)
	{
		
			GD.Print(node2D.Name);
		if (node2D.Name == "LightDetector")
		{
			_vendorActive = true;
		}
	}
	
	private void _on_area_2d_area_exited(Node2D node2D)
	{
		if (node2D.Name == "LightDetector")
		{
			_vendorActive = false;
		}	
	}
	public override void _Input(InputEvent @event)
	{
		if (@event.IsActionPressed("activate") && _vendorActive)
		{
			GetTree().Root.GetNode<CanvasLayer>("Game/Level/Shop").Visible = true;
		}
	}


}
