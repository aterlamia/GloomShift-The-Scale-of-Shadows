using Godot;
using System;

[Tool]
public partial class lampwall_ : Node2D
{
	private float _lightHeight = 52.0f;
	private float _lightWidth = 100.0f;

	private float _shadowHeight = 52.0f;
	private float _shadowWidth = 100.0f;
	private float _shadowWidthTop = 100.0f;
	private float _shadowStart = 100.0f;

	
	[Export]
	public float ShadowWidthTop
	{
		get => _shadowWidthTop;
		private set
		{
			_shadowWidthTop = value;

			if (Engine.IsEditorHint())
			{
				CreateShadow();
			}
		}
	}
	
	[Export]
	public float ShadowHeight
	{
		get => _shadowHeight;
		private set
		{
			_shadowHeight = value;
			if (Engine.IsEditorHint())
			{
				CreateShadow();
			}
		}
	}


	[Export]
	public float ShadowWidth
	{
		get => _shadowWidth;
		private set
		{
			_shadowWidth = value;
			if (Engine.IsEditorHint())
			{
				CreateShadow();
			}
		}
	}

	[Export]
	public float ShadowStart
	{
		get => _shadowStart;
		private set
		{
			_shadowStart = value;
			if (Engine.IsEditorHint())
			{
				CreateShadow();
			}
		}
	}


	[Export]
	public float LightHeight
	{
		get => _lightHeight;
		private set
		{
			_lightHeight = value;
			if (Engine.IsEditorHint())
			{
				createLight();
			}
		}
	}

	[Export]
	public float LightWidth
	{
		get => _lightWidth;
		private set
		{
			_lightWidth = value;
			if (Engine.IsEditorHint())
			{
				createLight();
			}
		}
	}

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		createLight();
		CreateShadow();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	private void CreateShadow()
	{
		if (!HasNode("Shadow"))
		{
			return;
		}
		var shadow = GetNode<Polygon2D>("Shadow");

		if (shadow == null)
		{
			return;
		}
		shadow.Polygon = new Vector2[]
		{
			new Vector2((_shadowWidthTop / 2) * -1 , _shadowStart),
			new Vector2(_shadowWidthTop / 2 , _shadowStart),
			new Vector2(_shadowWidth / 2, _shadowStart + _shadowHeight),
			new Vector2((_shadowWidth / 2) * -1, _shadowStart + _shadowHeight),
		};

		var shadowCol = GetNode<CollisionPolygon2D>("ShadowCol/ShadowShape");
		shadowCol.Polygon = new Vector2[]
		{
			new Vector2((_shadowWidth / 2) * -1 - 10, _shadowStart),
			new Vector2(_shadowWidth / 2 + 10, _shadowStart),
			new Vector2(_shadowWidth / 2, _shadowStart + _shadowHeight),
			new Vector2((_shadowWidth / 2) * -1, _shadowStart + _shadowHeight),
		};
	}

	private void createLight()
	{
		if(!HasNode("LightShape"))
		{
			return;
		}
		var lightEdge = GetNode<Polygon2D>("LightShape");
		lightEdge.Polygon = new Vector2[]
		{
			new Vector2(0, 0),
			new Vector2(_lightWidth, _lightHeight),
			new Vector2(-_lightWidth, _lightHeight)
		};
	}
}
