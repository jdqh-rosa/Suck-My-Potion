using Godot;
using System;

public class Sword : KinematicBody2D
{
	// Declare member variables here. Examples:
	// private int a = 2;
	

	[Signal] 
	public delegate void Hit(float damage);
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{

	}

	public void GetInput(){
		LookAt(GetGlobalMousePosition());
		
		if(Input.IsActionPressed("player_click")){
			Attack();
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(float delta)
	{
		GetInput();
	}

	private void Attack(){

	}
}
