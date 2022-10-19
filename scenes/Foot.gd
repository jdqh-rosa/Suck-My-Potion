extends AnimatedSprite

const SPEED = 11

export (Vector2) var default_foot_offset
export (float) var distance_threshold
export (Array, NodePath) var sibling_paths

var siblings = []
var is_animating := false

onready var last_parent_position:Vector2 = get_parent().global_position
onready var parent:Node2D = get_parent()

func _ready():
	# Add references to siblings
	for path in sibling_paths:
		siblings.append(get_node(path))


func _process(delta):
	# Uninherit parent position:
	var parent_position_delta = last_parent_position - parent.global_position
	global_position += parent_position_delta
	last_parent_position = parent.global_position

	# Check if foot should start animating:
	if !is_animating \
	and global_position.distance_to(last_parent_position+default_foot_offset) > distance_threshold:
		# Check if siblings are already animating
		var can_animate = true
		for sibling in siblings:
			if sibling.is_animating:
				can_animate = false
		
		# If no other sibling is animating, then animate.
		if can_animate:
			frame = 1
			is_animating = true
			
	
	# Process animation (if applicable):
	if is_animating:
		# Moov:
		global_position = lerp(global_position, last_parent_position + default_foot_offset, delta * SPEED)

		# End animation if reached default position:
		if global_position.distance_to(last_parent_position+default_foot_offset) < 20:
			frame = 0
			is_animating = false

#export (Vector2) var target_offset
#var target
#var moving = true
#onready var last_parent_position = get_parent().global_position
#var rotation_speed = 0
#
#func _process(delta):
#	# Uninherit parent position.
#	var parent_position_delta = last_parent_position - get_parent().global_position
#	global_position += parent_position_delta
#	last_parent_position = get_parent().global_position
#
#	# Take step if too far from target.
#	target = get_parent().global_position + target_offset
#
#	if !moving and global_position.distance_to(target) > 100:
#		moving = true
#
#	var displacement = (target - global_position)
#	if moving:
#		global_position += 160 * displacement/20.0 * delta
#
#		if global_position.distance_to(target) < 5:
#			moving = false
#
#	# Face direction of movement
#	var direction = displacement.angle()
#	rotation_speed = lerp(rotation_speed, (direction - rotation)*30, delta*20)
#	rotation += rotation_speed * delta;
