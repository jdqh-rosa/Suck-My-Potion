extends Node2D

var target = "left"
var rotation_speed = 0.0

var timer = 0.0
var foot_target_offset = 60

var axe_side = 1 # {-1, 1} corresponds to left & right

func _process(delta):
	timer += delta
	
	## DEBUG: walk left n right with arrows
	if Input.is_action_pressed("ui_left"):
		position.x -= 300 * delta
		$FootLeft.default_foot_offset = (Vector2.LEFT * foot_target_offset) + Vector2(rand_range(-5,5), rand_range(-5,5) + 80)
		$FootRight.default_foot_offset = (Vector2.LEFT * foot_target_offset) + Vector2(rand_range(-5,5), rand_range(-5,5) + 80)
	if Input.is_action_pressed("ui_right"):
		position.x += 300 * delta
		$FootLeft.default_foot_offset = (Vector2.RIGHT * foot_target_offset) + Vector2(rand_range(-5,5), rand_range(-5,5) + 80)
		$FootRight.default_foot_offset = (Vector2.RIGHT * foot_target_offset) + Vector2(rand_range(-5,5), rand_range(-5,5) + 80)
	if Input.is_action_pressed("ui_up"):
		position.y -= 300 * delta
		$FootLeft.default_foot_offset = (Vector2.UP * foot_target_offset) + Vector2(rand_range(-5,5), rand_range(-5,5) + 80)
		$FootRight.default_foot_offset = (Vector2.UP * foot_target_offset) + Vector2(rand_range(-5,5), rand_range(-5,5) + 80)
	if Input.is_action_pressed("ui_down"):
		position.y += 300 * delta
		$FootLeft.default_foot_offset = (Vector2.DOWN * foot_target_offset) + Vector2(rand_range(-5,5), rand_range(-5,5) + 80)
		$FootRight.default_foot_offset = (Vector2.DOWN * foot_target_offset) + Vector2(rand_range(-5,5), rand_range(-5,5) + 80)
	
	## DEBUG: Swing Axe with mouse
	if Input.is_action_just_pressed("player_click"):
		axe_side *= -1
	
	look_at_mouse()
	animate_stem(delta)
	animate_axe(delta)
	
	# Bob body
	$Body.position.y = 5*sin(timer*4)
	$BodyShadow.scale.x = .8 + .1*sin(timer*4)
	$BodyShadow.scale.y = .5 + .1*sin(timer*4)


func look_at_mouse():
	$Body/Face.position = ( (get_global_mouse_position()-$Body.global_position).normalized() )*20;
	$Body/Face/Pupils.position = ( (get_global_mouse_position()-$Body.global_position)/300.0 )*20;
	
#	if $Body/Face.position.y < 0:
#		$Body/Face.z_index = -1
#	else:
#		$Body/Face.z_index = 1


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

var axe_position_speed = 0.0
var axe_rotation_speed = 0.0
func animate_axe(delta):
#	var axe_target_position = ((get_global_mouse_position()-$Body.global_position).normalized().rotated(-PI/3*axe_side) )*100
#	axe_position_speed = lerp(axe_position_speed, (axe_target_position - $Axe.position)*30, delta*20)
#	$Axe.position += axe_position_speed * delta 
	
	var target_rotation = (get_global_mouse_position()-$Axe.global_position).angle() - PI/2 * axe_side
	axe_rotation_speed = lerp(axe_rotation_speed, (target_rotation-$Axe.rotation)*30, delta*20)
	$Axe.rotation += axe_rotation_speed * delta
	
