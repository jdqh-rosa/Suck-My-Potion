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

	private SignalManager signalManager;

	[Signal] 
	public delegate void Hit(float damage);

	KinematicBody2D player;
	
	public override void _Ready(){
		player = GetParent().GetNode<KinematicBody2D>("Player");
		SetPlayerDirection();
		signalManager = GetNode<SignalManager>("/root/SignalManager");
		signalManager.Connect("player_moved", this, "on_player_moved");
	}

	void on_check(bool check){
		GD.Print("check check 123:" + check);
	}
	public override void _PhysicsProcess(float delta)
	{
		velocity = new Vector2();
		SetPlayerDirection();
		velocity *= speed;
		velocity = MoveAndSlide(velocity);
	}
	
	public void SetPlayerDirection(){
		//var direction = (player.Position - Position).Normalized();
		var direction = (playerPos - Position).Normalized();
		velocity = direction;
	}

	void _on_Area2D_body_entered(Area2D body)
	{
		if(body.Name == "Player"){
			GD.Print("Collision with Player");
			signalManager.EmitSignal("player_hit", damage);
		}
	}

	private void on_player_moved(float newx, float newy){
		playerPos = new Vector2(newx, newy);
	}

	private void on_player_hit(float damage){
		signalManager.EmitSignal("player_hit", damage);
	}

}
