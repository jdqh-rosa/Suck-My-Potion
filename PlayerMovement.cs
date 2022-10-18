using Godot;
using System;

public class PlayerMovement : KinematicBody2D
{
   	[Export] public int speed = 200;

	public Vector2 velocity = new Vector2();

	public void GetInput()
	{
		velocity = new Vector2();

		if (Input.IsActionPressed("player_right"))
			velocity.x += 1;

		if (Input.IsActionPressed("player_left"))
			velocity.x -= 1;

		if (Input.IsActionPressed("player_down"))
			velocity.y += 1;

		if (Input.IsActionPressed("player_up"))
			velocity.y -= 1;

		velocity = velocity.Normalized() * speed;
	}

	public override void _PhysicsProcess(float delta)
	{
		GetInput();
		velocity = MoveAndSlide(velocity);
	}
}
