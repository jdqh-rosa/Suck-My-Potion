using Godot;
using System;

public class Enemy : KinematicBody2D
{
	// Declare member variables here. Examples:
	public int health =1;
	public int speed =100;
	public int damage =1;
	public Vector2 velocity = new Vector2();
	private Vector2 playerPos = new Vector2();

	KinematicBody2D player;
	
	public override void _Ready(){
		player = GetParent().GetNode<KinematicBody2D>("Player");
		SetPlayerDirection();
	}
	public override void _PhysicsProcess(float delta)
	{
		velocity = new Vector2();
		SetPlayerDirection();
		velocity *= speed;
		velocity = MoveAndSlide(velocity);
	}
	
	public void SetPlayerDirection(){
		var direction = (player.Position - Position).Normalized();
		velocity = direction;
	}
}
