## References:
- **Class Exercises:**
  - [Exercise 1 (2024)](https://www.youtube.com/watch?v=l9-KTxvcRCM) Directed by Exercise 1.
  - [Unity 2D Platformer for Complete Beginners - #1 PLAYER MOVEMENT (2020)](https://www.youtube.com/watch?v=TcranVQUQ5U) Directed by Unity 2D Platformer for Complete Beginners - #1 PLAYER MOVEMENT.
  - [Knight Sprite Sheet (Free) | 2D Characters | Unity Asset Store](https://assetstore.unity.com/packages/2d/characters/knight-sprite-sheet-free-93897)
  - [Pixel Adventure 1](https://pixelfrog-assets.itch.io/pixel-adventure-1)
  - [Dragon Warrior (Free) | 2D Characters | Unity Asset Store](https://assetstore.unity.com/packages/2d/characters/dragon-warrior-free-93896)
  - [Heart Asset](https://drive.google.com/drive/folders/1audm9sjm-JiGRu4PtckMoI8a_nudnzHg)

## Overview of the Game Elements Covered in the Video

### Player
- Small jump
- Big jump
- Double jump
- Wall jump
- Crouch
- Shoot fireballs - held in an array of fireballs. Animated explosion and idle
- Firepoint depicts where the fireballs will shoot from
- Speed, jump controls
- Animations: idle, jump, attack, hurt, move, crouch
- Immunity on death

### Level
- Built on a tile map, ground, wall, and background
- Doors at the end of each room to move the camera to the next or previous room
- Killzones in pits

### Platforms
- Have box colliders and platform effectors to let the player move through from the bottom and to allow for some movement beyond the edge of the platform

### Collectibles
- Health collectible - animated by me
- Swords - for points, animated by me - lots of points to encourage collection

### Traps
- **Saw**
  - Control its movement distance, speed, and damage
  - Collider for collision detections
  - Animated with the animator
  - Can make saw platforms by controlling that
- **Firetraps**
  - Activation delay
  - Active time
  - Goes red
  - Damage control
  - Animator for the fire activated
- **Spikes**
  - Damage control
- **Arrow traps**
  - Array of arrows
  - Attack cooldown to determine how fast the arrows come out
  - Arrowholder array of arrows
  - Speed of arrows and reset time
- **Spikehead**
  - Control the speed, range, and check delay
  - Raycast to detect the player
  - Moves down at a speed that is fair for the player to get past

### Enemies
- **Melee Enemies**
  - Patrols to waypoints
  - Collider for the player
  - Animated headbutt, death, and hurt
- **Ranged Enemies**
  - Shoot fireballs same as player
  - Patrol to waypoints
  - Adjust the range, cooldown, and damage

### Boss
- Big version of ranged enemy
- Player can duck to avoid the fireballs

### UI
- Canvas for the UI, Screen, Pause Screen, End Screen, and Win Screen
