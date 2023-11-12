using Godot;
using System;

[Tool]
public partial class Light : Node2D
{
    private float _lightHeight = 52.0f;
    private float _lightWidth = 100.0f;

    [Export]
    public float LightHeight
    {
        get => _lightHeight;
        private set
        {
            _lightHeight = value;
            createLight();
        }
    }

    [Export]
    public float LightWidth
    {
        get => _lightWidth;
        private set
        {
            _lightWidth = value;
            createLight();
        }
    }

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        createLight();
    }

    private void createLight()
    {
        var lightDetector = GetNode<CollisionShape2D>("Area/Shape");
        GD.Print("hier");
        if (lightDetector == null)
        {
            return;
        }
        GD.Print("daar");
        // set the size of the lightdetector
        RectangleShape2D rectangle = new RectangleShape2D();  
        rectangle.Size = new Vector2(_lightWidth*2, _lightHeight*2);
        lightDetector.Shape = rectangle;  
        
        
        var lightEdge = GetNode<Polygon2D>("LightEdge");
        lightEdge.Polygon = new Vector2[]
        {
            new Vector2(0, -_lightHeight),
            new Vector2(_lightWidth, _lightHeight),
            new Vector2(-_lightWidth, _lightHeight)
        };
        
        
        var lightMiddle = GetNode<Polygon2D>("LightMiddle");
        lightMiddle.Polygon = new Vector2[]
        {
            new Vector2(0, -_lightHeight),
            new Vector2(_lightWidth - 20, _lightHeight),
            new Vector2(-_lightWidth + 20, _lightHeight)
        };
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