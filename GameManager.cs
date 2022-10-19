using Godot;
using System;

public class GameManager : Node2D
{
	// Declare member variables here. Examples:
	Vector2 ScreenSize = new Vector2(OS.WindowSize.x, OS.WindowSize.y);
	KinematicBody2D player;
	[Export] KinematicBody2D enemyPrefab;
	System.Collections.Generic.List<KinematicBody2D> enemies = new System.Collections.Generic.List<KinematicBody2D>();
	Random random;
	

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		random = new Random();
		player = GetNode<KinematicBody2D>("Player");

	}

  	// Called every frame. 'delta' is the elapsed time since the previous frame.
  	public override void _Process(float delta)
  	{
		
  	}
	
	public void SpawnEnemy(){
		var nme = GD.Load<PackedScene>("res://Enemy.tscn");
		var inst = (KinematicBody2D)nme.Instance();
		inst.Position = new Vector2(0, random.Next(0, (int)ScreenSize.y));
		AddChild(inst);
		enemies.Add(inst);
	}

	public Vector2 PlayerPosition(){
		return player.Position;
	}

	private void _on_Timer_timeout()
	{
		System.Console.WriteLine("bla");
		SpawnEnemy();
	}
}
