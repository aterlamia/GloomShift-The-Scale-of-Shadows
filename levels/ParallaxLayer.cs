using Godot;
using System;

public partial class ParallaxLayer : Godot.ParallaxLayer
{
    // Speed multiplier for the parallax effect
    // [Export] public float parallaxStrength = 0.5f;
    //
    // private Camera2D activeCamera;
    // private Player _player;
    //
    // public override void _Ready()
    // {
    //     _player = GetTree().GetFirstNodeInGroup("Player") as Player;
    //     // Get the active camera in the scene
    //     activeCamera = GetViewport().GetCamera2D();
    // }
    //
    // public override void _Process(double delta)
    // {
    //     // Check if the active camera exists
    //     if (activeCamera != null)
    //     {
    //         // Calculate the offset based on the camera position and parallax strength
    //         Vector2 cameraOffset = _player.Position * parallaxStrength * (float) delta;
    //
    //         // Update the parallax layer position
    //         Position = -cameraOffset;
    //     }
    // }
}