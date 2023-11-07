using Godot;
using System;

public partial class PlayerContainer : Node2D
{
    public Boolean IsShadow = false;

    private Shadow _shadow;
    private PLayer _pLayer;
    private AnimationPlayer _animationPlayer;
    private Camera2D _cameraPLayer;
    private Camera2D _cameraShadow;

    [Export] private float _speed = 100;
    [Export] private float _gravity = 200;
    [Export] private float _jumpHeight = -110;


    private Vector2 _lastDirection = Vector2.Zero;
    private Vector2 _currentDirection = Vector2.Zero;


    public override void _Ready()
    {
        _shadow = GetNode<Shadow>("Shadow");
        _pLayer = GetNode<PLayer>("Player");
        _animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
        _cameraPLayer = GetNode<Camera2D>("Player/Camera");
        _cameraShadow = GetNode<Camera2D>("Shadow/Camera");
    }


    private Camera2D _activeCam()
    {
        if (IsShadow)
        {
            return _cameraShadow;
        }
        else
        {
            return _cameraPLayer;
        }
    }

private Vector2 getDirection()
    {
        if (IsShadow)
        {
            return _shadow.GetDirection();
        }
        else
        {
            return _pLayer.GetDirection();
        }
    }

    public override void _PhysicsProcess(double delta)
    {
        _pLayer.Calc(delta);

        if (IsShadow)
        {
            _shadow.IsAutonamous = true;
            _pLayer.ShadowControlled = true;
        }
        else
        {
            _shadow.IsAutonamous = false;
            _pLayer.ShadowControlled = false;

            if (_pLayer.IsOnFloor() && !_shadow.IsOnFloor())
            {
            }
            else
            {
                _shadow.ParentVelocity = _pLayer.Velocity;
            }

            _shadow.SetDirection(_pLayer.GetDirection());
        }

        _shadow.Calc(delta);
        _shadow.Animate();
        _shadow.MoveAndSlide();
        _pLayer.Animate();
        _pLayer.MoveAndSlide();

        HandleShadowMovement();
        HandleLightDetection();


        _currentDirection = getDirection();

        // if the current direction is to the right, lerp the camamera 50 px to the right
         
        HandleCameraMovement();
    }

    private void HandleCameraMovement()
    {
        var cam = _activeCam();
        if (_currentDirection.X > 0)
        {
            cam.Offset = new Vector2(Mathf.Lerp(cam.Offset.X, 200, 0.01f), Mathf.Lerp(cam.Offset.Y, -50, 0.01f));
        }
        else if (_currentDirection.X < 0)
        {
            cam.Offset = new Vector2(Mathf.Lerp(cam.Offset.X, -200, 0.01f), Mathf.Lerp(cam.Offset.Y, -50, 0.01f));
        }
        else
        {
            // if the offset is not 0 lerp back to 0
            if (Math.Abs(_cameraPLayer.Offset.X) > 0.1)
            {
                cam.Offset = new Vector2(Mathf.Lerp(cam.Offset.X, 0, 0.05f), Mathf.Lerp(cam.Offset.Y, 0, 0.01f));
            }
        }
    }

    private void HandleLightDetection()
    {
        // get the Lights node and determine if the player is within a certain distance of one of the lights
        var lights = GetTree().Root.GetNode<Node2D>("Game/Level/Lights");
        _shadow.GetNode<AnimatedSprite2D>("ShadowCol/ShadowSprite").Material.Set("shader_parameter/max_line_width", 6);
        _shadow.GetNode<AnimatedSprite2D>("ShadowCol/ShadowSprite").Material.Set("shader_parameter/outline_colour",
            new Vector4(0.4f, 0.4f, 0.4f, 0.3f));
        // loop over all lights and determine the distance. if at least one light is close enough get out of the loop
        foreach (Node2D l in lights.GetChildren())
        {
            if (l.Position.DistanceTo(_pLayer.Position) < 100)
            {
                _shadow.GetNode<AnimatedSprite2D>("ShadowCol/ShadowSprite").Material
                    .Set("shader_parameter/max_line_width", 9);
                _shadow.GetNode<AnimatedSprite2D>("ShadowCol/ShadowSprite").Material
                    .Set("shader_parameter/outline_colour", new Vector4(0.7f, 0, 0, 0.3f));
                break;
            }
        }
    }

    private void HandleShadowMovement()
    {
        if (IsShadow)
        {
            //The further the shadow is away from the player the smaller it gets
            var dist = _pLayer.Position.DistanceTo(_shadow.Position);

            var scale = dist / 1000.0f;
            if (scale > 0.5f)
            {
                scale = 0.5f;
            }

            _shadow.Scale = new Vector2(1.0f - scale, 1.0f - scale);
        }
    }

    //listen to the tab key
    public override void _Input(InputEvent @event)
    {
        if (@event.IsActionPressed("switch"))
        {
            IsShadow = !IsShadow;
            _cameraShadow.Enabled = IsShadow;
            _cameraPLayer.Enabled = !IsShadow;

            if (IsShadow)
            {
                if (_cameraShadow.Offset != Vector2.Zero)
                {
                    _cameraShadow.Offset = _cameraPLayer.Offset;
                }

                _cameraPLayer.Offset = Vector2.Zero;    
            }
            else
            {
                if (_cameraPLayer.Offset != Vector2.Zero)
                {
                    _cameraPLayer.Offset = _cameraShadow.Offset;
                }

                _cameraShadow.Offset = Vector2.Zero;
            }
        }
    }
}