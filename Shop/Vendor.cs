using Godot;
using System;
using GenericPlatforformer;
using GenericPlatforformer.State;

public partial class Vendor : Node2D
{
    private Boolean _vendorActive = false;
    private Boolean _hadIntro = false;
    private Boolean _hadWarning = false;

    private GlobalState _globalState = null;

    [Export] private bool flipped = false;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        _globalState = GetNode<GenericPlatforformer.GlobalState>("/root/GlobalState");
        _globalState.Connect("CloseDialog", new Callable(this, "closeDialog"));


        GetNode<Sprite2D>("Shop2").FlipH = flipped;
    }

    private void closeDialog()
    {
        // nothing to do here
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
    }

    private void _on_area_2d_area_entered(Node2D node2D)
    {
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
            GetParent().GetNode<CanvasLayer>("Shop").Visible = true;
            GetParent().GetNode<Shop>("Shop/Shop").checkBuyable();
        }

        if (@event.IsActionPressed("nextDialog"))
        {
            if (GetNode<Panel>("NoScale").Visible == true)
            {
                GetNode<Panel>("NoScale").Visible = false;
                _globalState.FireCloseDialog();
            }

            if (GetNode<Panel>("Text1").Visible == true)
            {
                GetNode<Panel>("Text1").Visible = false;
                GetNode<Panel>("Text2").Visible = true;
                return;
            }

            if (GetNode<Panel>("Text2").Visible == true)
            {
                GetNode<Panel>("Text2").Visible = false;
                GetNode<Panel>("Text3").Visible = true;
                return;
            }

            if (GetNode<Panel>("Text3").Visible == true)
            {
                GetNode<Panel>("Text3").Visible = false;
                _globalState.FireCloseDialog();
                return;
            }
        }
    }

    private void _on_greet_area_area_entered(Node2D node2D)
    {
        if (node2D.Name == "Player")
        {
            if (_globalState.Scales >= 1 && !_hadIntro)
            {
                _hadIntro = true;
                node2D.GetNode<StateManager>("StateManager").ChangeState(StateTypes.Dialog);
                _hadIntro = true;
                GetNode<Panel>("Text1").Visible = true;
            }
            else if (_globalState.Scales < 1 && !_hadWarning)
            {
                node2D.GetNode<StateManager>("StateManager").ChangeState(StateTypes.Dialog);
                GetNode<Panel>("NoScale").Visible = true;
                _hadWarning = true;
            }
            // GetNode<AnimationPlayer>("AnimationPlayer").Play("greet");
        }
    }
}