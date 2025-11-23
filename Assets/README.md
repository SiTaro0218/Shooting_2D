# 2D Shooting Game - Implementation Guide

## Overview
This is a 2D bullet hell shooter game built in Unity. The player controls a spaceship that can move and shoot, while enemies spawn from the top of the screen and attack.

## Game Features

### Player Mechanics
- **Movement**: WASD or Arrow keys to move in all directions
- **Shooting**: Space bar or Fire1 button to shoot bullets upward
- **Health System**: Player has 3 health points
- **Boundary Constraints**: Player is restricted to screen bounds

### Enemy System
- **Auto-spawn**: Enemies spawn automatically from the top of the screen
- **Movement**: Enemies move downward toward the player
- **Shooting**: Enemies can shoot bullets at the player
- **Health**: Enemies have configurable health points
- **Score**: Destroying enemies awards points

### Game Management
- **Score Tracking**: Points are awarded for destroying enemies
- **Game Over**: Triggered when player health reaches 0
- **Restart**: Press 'R' key to restart the game after game over

## Script Components

### PlayerController.cs
Handles player movement, shooting, and health management.
- Movement speed: 5 units/second
- Fire rate: 0.2 seconds between shots
- Max health: 3 points

### Enemy.cs
Controls enemy behavior, movement, and combat.
- Move speed: 2 units/second
- Fire rate: 2 seconds between shots
- Score value: 10 points per enemy

### Bullet.cs
Manages bullet behavior for both player and enemy projectiles.
- Speed: 10 units/second
- Lifetime: 3 seconds
- Damage: 1 point

### EnemySpawner.cs
Spawns enemies at random positions across the top of the screen.
- Spawn rate: 2 seconds between spawns
- Spawn range: -8 to +8 on X axis
- Spawn height: Y = 6

### GameManager.cs
Manages game state, score, UI, and game over conditions.
- Score tracking
- Health display
- Game over screen
- Restart functionality

## Setup Instructions

### Required Setup
1. **Create Player Prefab**:
   - Create a 2D Sprite GameObject
   - Tag it as "Player"
   - Add PlayerController script
   - Add Rigidbody2D (set to Kinematic)
   - Add BoxCollider2D or CircleCollider2D
   - Set layer to "Player"
   - Create a child GameObject as "FirePoint" for bullet spawn position

2. **Create Player Bullet Prefab**:
   - Create a 2D Sprite GameObject
   - Tag it as "PlayerBullet"
   - Add Bullet script (set isPlayerBullet = true)
   - Add Rigidbody2D (set to Kinematic)
   - Add BoxCollider2D (set as Trigger)
   - Set layer to "Bullet"

3. **Create Enemy Prefab**:
   - Create a 2D Sprite GameObject
   - Tag it as "Enemy"
   - Add Enemy script
   - Add Rigidbody2D (set to Kinematic)
   - Add BoxCollider2D or CircleCollider2D (set as Trigger)
   - Set layer to "Enemy"
   - Create a child GameObject as "FirePoint" for bullet spawn position

4. **Create Enemy Bullet Prefab**:
   - Create a 2D Sprite GameObject
   - Tag it as "EnemyBullet"
   - Add Bullet script (set isPlayerBullet = false, speed = -10 for downward)
   - Add Rigidbody2D (set to Kinematic)
   - Add BoxCollider2D (set as Trigger)
   - Set layer to "Bullet"

5. **Scene Setup**:
   - Camera should be orthographic with size 5
   - Add GameManager GameObject with GameManager script
   - Add EnemySpawner GameObject with EnemySpawner script
   - Assign enemy prefabs to EnemySpawner
   - Place Player in scene at bottom center

### Physics Layer Configuration
Configure collision matrix (Edit > Project Settings > Physics 2D):
- Player layer should collide with Enemy and EnemyBullet
- Enemy layer should collide with Player and PlayerBullet
- Bullet layer should collide with Player and Enemy

### UI Setup (Optional)
Create Canvas with:
- Score Text (Top left)
- Health Text (Top right)
- Game Over Panel (Center, initially disabled)
- Assign UI elements to GameManager script

## Controls
- **Movement**: Arrow Keys or WASD
- **Shoot**: Space or Fire1 button
- **Restart**: R key (after game over)

## Customization
You can adjust these values in the inspector:
- Player movement speed
- Player fire rate
- Player max health
- Enemy spawn rate
- Enemy movement speed
- Enemy fire rate
- Bullet speed and lifetime
- Score values

## Testing the Game
1. Open the MainGame scene
2. Press Play in Unity Editor
3. Use arrow keys/WASD to move
4. Press Space to shoot
5. Avoid enemy bullets and destroy enemies
6. Try to achieve a high score!

## Future Enhancements
Potential additions:
- Power-ups (health, rapid fire, shields)
- Multiple enemy types with different patterns
- Boss battles
- Sound effects and music
- Particle effects for explosions
- Background scrolling
- Weapon upgrades
- Different bullet patterns
