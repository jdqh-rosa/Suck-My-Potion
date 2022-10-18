extends Node2D

export (Vector2) var target_offset
var target
var moving = true
onready var last_parent_position = get_parent().global_position
var rotation_speed = 0

func _process(delta):
	# Uninherit parent position.
	var parent_position_delta = last_parent_position - get_parent().global_position
	global_position += parent_position_delta
	last_parent_position = get_parent().global_position
	
	# Take step if too far from target.
	target = get_parent().global_position + target_offset
	
	if !moving and global_position.distance_to(target) > 100:
		moving = true
	
	var displacement = (target - global_position)
	if moving:
		global_position += 160 * displacement/20.0 * delta
		
		if global_position.distance_to(target) < 5:
			moving = false
	
	# Face direction of movement
	var direction = displacement.angle()
	rotation_speed = lerp(rotation_speed, (direction - rotation)*30, delta*20)
	rotation += rotation_speed * delta;
