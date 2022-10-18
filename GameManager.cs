using Godot;
using System;

public class GameManager : Node2D
{
	// Declare member variables here. Examples:
	Vector2 ScreenSize = new Vector2(OS.WindowSize.x, OS.WindowSize.y);
	[Export] KinematicBody2D playerS;
	[Export] KinematicBody2D enemy;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		var player = GD.Load<PackedScene>("res://Player.tscn");
		var inst = player.Instance();
		AddChild(inst);
	}

  	// Called every frame. 'delta' is the elapsed time since the previous frame.
  	public override void _Process(float delta)
  	{
		
  	}

	public Vector2 PlayerPosition(){
		return playerS.Position;
	}
}
