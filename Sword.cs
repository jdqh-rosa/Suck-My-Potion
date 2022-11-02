using Godot;
using System;

public class Sword : KinematicBody2D
{
	// Declare member variables here. Examples:
	// private int a = 2;
	private float damage = 1;
	private bool attacking = false;
	private bool canAttack = true;
	private Timer coolDownTimer;
	private Timer attackTimer;
	private SignalManager signalManager;

	[Signal]
	public delegate void Hit(float damage);

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		signalManager = GetNode<SignalManager>("/root/SignalManager");

		this.Visible = false;
		attackTimer = GetNode<Timer>("AttackTimer");
		coolDownTimer = GetNode<Timer>("CooldownTimer");
		GetNode<CollisionShape2D>("SwordBody/SwordCollisionShape2D").Disabled = true;
	}

	public void GetInput()
	{
		LookAt(GetGlobalMousePosition());

		if (Input.IsActionPressed("player_click"))
		{
			if (canAttack) InitiateAttack();
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(float delta)
	{
		GetInput();
		if (attacking){
			//Attack();
		}
	}

	private void InitiateAttack()
	{
		attacking = true;
		canAttack = false;
		GetNode<CollisionShape2D>("SwordBody/SwordCollisionShape2D").Disabled = false;
		this.Visible = true;
		attackTimer.Start();

	}

	private void _on_AttackTimer_timeout()
	{
		attacking = false;
		this.Visible = false;
		GetNode<CollisionShape2D>("SwordBody/SwordCollisionShape2D").Disabled = true;
		attackTimer.Stop();
		coolDownTimer.Start();
	}


	private void _on_CooldownTimer_timeout()
	{
		GD.Print("CoolDown Over");
		canAttack = true;
		coolDownTimer.Stop();
	}

	private void _on_SwordBody_body_entered(Area2D body)
	{
		if(body.Name == "Enemy"){
			GD.Print("Sword hits Enemy");
			signalManager.Connect("enemy_hit", body, "_on_take_damage");
			signalManager.EmitSignal("enemy_hit", damage);
			//parent._on_take_damage(damage);
		}
	}
}
