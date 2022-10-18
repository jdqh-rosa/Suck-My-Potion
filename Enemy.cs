using Godot;
using System;

public class Enemy : KinematicBody2D
{
	// Declare member variables here. Examples:
	public int health =1;
	public int speed =100;
	public int damage =1;
	public Vector2 velocity = new Vector2();

	public void SetVelocity()
	{
		velocity = new Vector2();
		
		velocity = GetPlayerDirection();
		
		velocity = velocity.Normalized() * speed;
	}

	public override void _PhysicsProcess(float delta)
	{
		SetVelocity();
		velocity = MoveAndSlide(velocity);
	}
	
	Vector2 GetPlayerDirection(){
		return new Vector2(1,0);
	}
}
