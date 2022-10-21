using Godot;
using System;

public class PlayerMovement : KinematicBody2D
{
	[Export] public float health = 5;
   	[Export] public int speed = 200;
	
	

	public Vector2 velocity = new Vector2();
	SignalManager signalManager;
	
	public override void _Ready()
	{
		signalManager = GetNode<SignalManager>("/root/SignalManager");
		signalManager.Connect("player_hit", this, "_on_take_damage");

		//GetTree().Connect("node_added", this, "_on_node_added");
	}

	public override void _Process(float delta)
  	{
		if(health<=0){
			this.Visible = false;
		}
  	}
	public void GetInput()
	{	
		LookAt(GetGlobalMousePosition());

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
		for (int i = 0; i < GetSlideCount(); i++)
		{
			var collision = GetSlideCollision(i);
			//GD.Print("I collided with ", ((Node)collision.Collider).Name);
		}
		signalManager.EmitSignal("player_moved", Position.x, Position.y);
	}
	private void _on_PlayerBody_body_entered(KinematicBody2D body)
	{
		if(body.Name == "Enemy"){
			GD.Print("Collision with Enemy");
			//body.EmitSignal("on_player_hit");
		}
	}

	private void _on_take_damage(float damage){
		health -= damage;
		GD.Print("Player took "+damage+" damage");
	}
}



