using Godot;
using System;

public partial class PlayerContainer : Node2D
{
    public Boolean IsShadow = false;

    private Shadow _shadow;
    private Sprite2D _shadowSprite;
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
        _shadowSprite = _shadow.GetNode<Sprite2D>("Sprite2D");
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

    private Vector2 GetDirection()
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
        if (!IsShadow)
        {
            _pLayer.Calc(delta);
            _pLayer.MoveAndSlide();

            _shadow.Position = _pLayer.Position + new Vector2(-10 * _pLayer.GetDirection().X, 0);

            if (_pLayer.GetDirection().X == 0)
            {
                _shadow.Scale = new Vector2(1.1f, 1.1f);
            }
            else
            {
                _shadow.Scale = new Vector2(1, 1);
            }

            _shadow.Velocity = _pLayer.Velocity;
            _shadow.ParentControl(delta, _pLayer.GetDirection());
            _shadow.MoveAndSlide();
        }
        else
        {
            _shadow.Calc(delta);
            _shadow.MoveAndSlide();
        }

        HandleShadowMovement();
        HandleLightDetection();
        _currentDirection = GetDirection();
        HandleCameraMovement();
    }

    private void HandleCameraMovement()
    {
        var cam = _activeCam();
        if (_currentDirection.X > 0)
        {
            cam.Offset = new Vector2(Mathf.Lerp(cam.Offset.X, 80, 0.01f), Mathf.Lerp(cam.Offset.Y, -5, 0.01f));
        }
        else if (_currentDirection.X < 0)
        {
            cam.Offset = new Vector2(Mathf.Lerp(cam.Offset.X, -80, 0.01f), Mathf.Lerp(cam.Offset.Y, -5, 0.01f));
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
        _shadowSprite.Material.Set("shader_parameter/max_line_width", 6);
        _shadowSprite.Material.Set("shader_parameter/outline_colour",
            new Vector4(0.4f, 0.4f, 0.4f, 0.3f));
        // loop over all lights and determine the distance. if at least one light is close enough get out of the loop
        foreach (Node2D l in lights.GetChildren())
        {
            if (l.GlobalPosition.DistanceTo(_pLayer.GlobalPosition) < 100)
            {
                _shadowSprite.Material
                    .Set("shader_parameter/max_line_width", 9);
                _shadowSprite.Material
                    .Set("shader_parameter/outline_colour", new Vector4(0.7f, 0, 0, 0.3f));
                break;
            }
        }
    }

    private void HandleShadowMovement()
    {
        if (IsShadow)
        {
            if (_shadow.LightDetector != null)
            {
                var distanceToPole = _shadow.LightDetector.GetParent<Node2D>().GlobalPosition
                    .DistanceTo(_shadow.GlobalPosition);
                
                // Get the height and width of the area2d take the middle of it and calculate the triangle from the top of the middle to the bottom rigth
                // and bottom left
                var shape = _shadow.LightDetector.GetNode<CollisionShape2D>("CollisionShape2D");
                var shadowHeight = _shadow.GetNode<CollisionShape2D>("LightDetector/CollisionShape2D").Shape.GetRect().Size.Y;
                var height = shape.Shape.GetRect().Size.Y;
                var width = shape.Shape.GetRect().Size.X;
                var bottom = width / 2;
                var hypothenuse = Math.Sqrt(Math.Pow(height, 2) + Math.Pow(bottom, 2));
                var angle = Math.Asin(height / hypothenuse);
                var adjecent = bottom - distanceToPole;
                
                //calulate the opposide based on the angle and the adjecent
                var opposite = Math.Tan(angle) * adjecent;
                var scale = opposite / shadowHeight;
                _shadow.Scale = new Vector2(Mathf.Min((float)scale,1), Mathf.Min((float)scale,1));


            }

            //The further the shadow is away from the player the smaller it gets
            // var dist = _pLayer.Position.DistanceTo(_shadow.Position);
            //
            // var scale = dist / 1000.0f;
            // if (scale > 0.5f)
            // {
            //     scale = 0.5f;
            // }
            //
            // _shadow.Scale = new Vector2(1.0f - scale, 1.0f - scale);
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