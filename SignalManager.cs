using Godot;
using System;

public class SignalManager : Godot.Node
{
    // Declare member variables here. Examples:
    [Signal]
    public delegate void player_moved(float newx, float newy);
    [Signal]
    public delegate void player_hit(float damage);
    [Signal]
    public delegate void enemy_hit(float damage);
    [Signal]
    public delegate void player_enemy_hit();
    [Signal]
    public delegate void check(bool check);

}
