using Godot;
using System;

public partial class Light : Node2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	
	private void _on_lightray_area_entered(Area2D body)
	{
		GD.Print("Hoeeeee");
		// if (body is Shadow)
		// {
		// 	GetNode<LightRay>("LightRay").LightOn();
		// }
	}
}
