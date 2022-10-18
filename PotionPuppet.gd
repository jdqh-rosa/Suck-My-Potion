extends Node2D

var target = "left"
var rotation_speed = 0.0

func _process(delta):
	
	look_at_mouse()
	animate_stem(delta)


func look_at_mouse():
	$Body/Face.position = ( (get_global_mouse_position()-global_position)/300.0 )*20;
	$Body/Face/Pupils.position = ( (get_global_mouse_position()-global_position)/300.0 )*20;


func animate_stem(delta):
	if get_local_mouse_position().x > 0: 
		target = "left"
	else:
		target = "right"
	
	if target == "left":
		rotation_speed = lerp(rotation_speed, (- $Body/stem.rotation_degrees)*30, delta*20)
		$Body/stem.rotation_degrees += rotation_speed * delta;
	if target == "right":
		rotation_speed = lerp(rotation_speed, (90 - $Body/stem.rotation_degrees)*30, delta*20)
		$Body/stem.rotation_degrees += rotation_speed * delta;


func feet():
	pass

