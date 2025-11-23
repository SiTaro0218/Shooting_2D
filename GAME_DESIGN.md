# 2D Shooting Game - Game Design Document

## Game Concept
A classic vertical scrolling space shooter where the player controls a spaceship, defeats waves of enemies, and tries to achieve the highest score possible while surviving as long as they can.

## Visual Layout

```
╔════════════════════════════════════════════════════════════╗
║  Score: 150              [GAME AREA]          Health: 3    ║
╠════════════════════════════════════════════════════════════╣
║                                                            ║
║                  [ENEMY SPAWN ZONE]                        ║
║              🔴 Enemy      🔴 Enemy                         ║
║                 ↓             ↓                            ║
║                 💥            💥                            ║
║                                                            ║
║                                                            ║
║              💥 Enemy Bullet (Orange)                      ║
║                 ↓                                          ║
║                                                            ║
║              🟡 Player Bullet (Yellow)                     ║
║                 ↑                                          ║
║                                                            ║
║                 🔵 PLAYER                                  ║
║             (WASD/Arrows)                                  ║
║           [PLAYER MOVEMENT ZONE]                           ║
╚════════════════════════════════════════════════════════════╝
```

## Game Flow

### 1. Game Start
```
┌─────────────┐
│ MainGame    │
│   Scene     │ → Player spawns at bottom center
│  Loaded     │   EnemySpawner activated
└─────────────┘   GameManager initialized
```

### 2. Gameplay Loop
```
┌──────────────┐     ┌──────────────┐     ┌──────────────┐
│ Enemy Spawns │ ──→ │ Enemy Moves  │ ──→ │ Enemy Shoots │
│  (Top Y=6)   │     │  Downward    │     │  (Downward)  │
└──────────────┘     └──────────────┘     └──────────────┘
                              │
                              ↓
                     ┌──────────────┐
                     │ Collision?   │
                     └──────────────┘
                        ↓           ↓
                    Player        Player
                    Bullet        Takes Hit
                      │              │
                      ↓              ↓
                 Enemy Dies    Health -= 1
                  Score += 10       │
                                    ↓
                              Health = 0?
                                    │
                                    ↓
                              [GAME OVER]
```

### 3. Player Actions
```
Input         Action              Result
─────────────────────────────────────────
WASD/Arrows   Move Player         Position Update
Space         Fire Bullet         Bullet Spawned
                                  (Moves Upward)
R (Game Over) Restart Game        Scene Reload
```

## Game Mechanics

### Player Mechanics
```
┌───────────────────────────────────────────────┐
│              PLAYER SYSTEM                     │
├───────────────────────────────────────────────┤
│ Movement:                                     │
│  • 8-directional (WASD/Arrows)                │
│  • Speed: 5 units/second                      │
│  • Bounded to screen edges                    │
│                                               │
│ Shooting:                                     │
│  • Press Space/Fire1                          │
│  • Fire Rate: 0.2s (5 shots/sec)              │
│  • Bullets move upward at 10 units/sec        │
│                                               │
│ Health:                                       │
│  • Start: 3 HP                                │
│  • Damage: -1 per enemy/bullet hit            │
│  • Death: Health = 0 → Game Over              │
└───────────────────────────────────────────────┘
```

### Enemy System
```
┌───────────────────────────────────────────────┐
│              ENEMY SYSTEM                      │
├───────────────────────────────────────────────┤
│ Spawning:                                     │
│  • Every 2 seconds                            │
│  • Random X position: -8 to +8                │
│  • Fixed Y position: 6 (top)                  │
│                                               │
│ Movement:                                     │
│  • Move downward at 2 units/second            │
│  • Destroyed if Y < -10 (off screen)          │
│                                               │
│ Combat:                                       │
│  • Shoots every 2 seconds                     │
│  • Bullets move downward at 10 units/sec      │
│  • Health: 3 HP                               │
│  • Dies when health = 0                       │
│  • Awards 10 points on death                  │
└───────────────────────────────────────────────┘
```

### Collision Rules
```
┌──────────────────┬─────────────────┬─────────────┐
│   Object A       │    Object B     │   Result    │
├──────────────────┼─────────────────┼─────────────┤
│ Player           │ Enemy           │ Player -1HP │
│                  │                 │ Enemy dies  │
├──────────────────┼─────────────────┼─────────────┤
│ Player           │ Enemy Bullet    │ Player -1HP │
│                  │                 │ Bullet gone │
├──────────────────┼─────────────────┼─────────────┤
│ Player Bullet    │ Enemy           │ Enemy -1HP  │
│                  │                 │ Bullet gone │
├──────────────────┼─────────────────┼─────────────┤
│ Bullet           │ Bullet          │ No effect   │
│                  │                 │ (pass thru) │
└──────────────────┴─────────────────┴─────────────┘
```

## Score System
```
Action                Points
─────────────────────────────
Destroy Enemy          +10
Hit Enemy (no kill)     +0
Get Hit                 +0
Game Over           (Final Score)
```

## Technical Architecture

### Component Hierarchy
```
MainGame Scene
├── Main Camera (Orthographic, Size: 5)
│
├── GameManager
│   ├── Tracks Score
│   ├── Monitors Health
│   ├── Handles Game Over
│   └── UI References
│
├── EnemySpawner
│   ├── Spawn Timer
│   ├── Enemy Prefab Array
│   └── Spawn Configuration
│
└── Player (Spawned from Prefab)
    ├── SpriteRenderer (Blue)
    ├── Rigidbody2D (Kinematic)
    ├── BoxCollider2D (Trigger)
    ├── PlayerController Script
    └── FirePoint (Child)
```

### Prefab Structure
```
Player Prefab
├── Player (Root)
│   ├── Tag: "Player"
│   ├── Layer: Player
│   ├── SpriteRenderer (Blue square)
│   ├── Rigidbody2D (Kinematic)
│   ├── BoxCollider2D (Trigger)
│   └── PlayerController
└── FirePoint (Child at Y: +0.5)

Enemy Prefab
├── Enemy (Root)
│   ├── Tag: "Enemy"
│   ├── Layer: Enemy
│   ├── SpriteRenderer (Red square)
│   ├── Rigidbody2D (Kinematic)
│   ├── BoxCollider2D (Trigger)
│   └── Enemy Script
└── FirePoint (Child at Y: -0.5)

PlayerBullet Prefab
├── Tag: "PlayerBullet"
├── Layer: Bullet
├── SpriteRenderer (Yellow)
├── Rigidbody2D (Kinematic)
├── BoxCollider2D (Trigger)
└── Bullet Script (isPlayerBullet: true)

EnemyBullet Prefab
├── Tag: "EnemyBullet"
├── Layer: Bullet
├── SpriteRenderer (Orange)
├── Rigidbody2D (Kinematic)
├── BoxCollider2D (Trigger)
└── Bullet Script (isPlayerBullet: false)
```

## Physics Configuration
```
┌─────────────────────────────────────────────────┐
│          Physics 2D Settings                     │
├─────────────────────────────────────────────────┤
│ Gravity: (0, 0)  [No gravity - space setting]   │
│                                                  │
│ Layer Collision Matrix:                         │
│  ✓ Player ←→ Enemy                               │
│  ✓ Player ←→ Bullet (for enemy bullets)         │
│  ✓ Enemy ←→ Bullet (for player bullets)         │
│  ✗ Player ←→ Player                              │
│  ✗ Enemy ←→ Enemy                                │
│  ✗ Bullet ←→ Bullet                              │
└─────────────────────────────────────────────────┘
```

## Game States

### State Diagram
```
    START
      │
      ↓
 ┌─────────┐
 │ PLAYING │ ←──────────┐
 └─────────┘            │
      │                 │
      │ (Health = 0)    │
      ↓                 │
 ┌──────────┐           │
 │ GAME OVER│           │
 └──────────┘           │
      │                 │
      │ (Press R)       │
      └─────────────────┘
```

## Progression & Difficulty

### Current Implementation (Static)
- Constant spawn rate: 2 seconds
- Constant enemy speed: 2 units/sec
- No difficulty increase

### Future Enhancement Ideas
- Increase spawn rate over time
- Increase enemy speed gradually
- Introduce new enemy types
- Wave-based difficulty
- Boss battles at intervals

## Performance Considerations

### Memory Management
```
Object             Lifespan         Cleanup
─────────────────────────────────────────────
Bullet             3 seconds        Auto-destroy
Enemy (off-screen) When Y < -10    Auto-destroy
Enemy (destroyed)  Immediate        Destroy()
```

### Optimization
- Kinematic rigidbodies (no physics calculations)
- Trigger colliders (no physics resolution)
- Object pooling potential (future enhancement)
- Minimal Update() calls

## UI Design (Optional)

```
┌───────────────────────────────────────────────┐
│ Score: 150                      Health: ❤❤❤   │ ← HUD
├───────────────────────────────────────────────┤
│                                               │
│                 [GAMEPLAY]                    │
│                                               │
└───────────────────────────────────────────────┘

Game Over Screen:
┌───────────────────────────────────────────────┐
│                                               │
│               GAME OVER                       │
│                                               │
│           Final Score: 350                    │
│                                               │
│         Press R to Restart                    │
│                                               │
└───────────────────────────────────────────────┘
```

## Color Scheme

```
Element            Color        Purpose
─────────────────────────────────────────────
Player             Blue         Easy to identify
Enemy              Red          Threatening
Player Bullet      Yellow       Bright, visible
Enemy Bullet       Orange       Distinguishable
Background         Dark Blue    Contrast
UI Text            White        Readable
```

## Input Mapping

```
Keyboard         Alternative    Gamepad         Action
────────────────────────────────────────────────────────
W                Up Arrow       D-Pad Up        Move Up
A                Left Arrow     D-Pad Left      Move Left
S                Down Arrow     D-Pad Down      Move Down
D                Right Arrow    D-Pad Right     Move Right
Space            Left Ctrl      Button A        Shoot
R                -              -               Restart
```

## Audio Design (Not Implemented)

```
Event                 Sound Effect Needed
────────────────────────────────────────────
Player Shoots         → Laser shot (high pitch)
Enemy Shoots          → Laser shot (low pitch)
Enemy Hit             → Impact sound
Enemy Destroyed       → Explosion
Player Hit            → Damage sound
Player Destroyed      → Big explosion
Background            → Space ambient music
Game Over             → Defeat sound
```

## Testing Scenarios

### 1. Basic Gameplay
- ✓ Player moves in all 8 directions
- ✓ Player stays within screen bounds
- ✓ Bullets fire upward from player
- ✓ Enemies spawn and move downward
- ✓ Enemy bullets fire downward

### 2. Collision Detection
- ✓ Player bullet hits enemy
- ✓ Enemy bullet hits player
- ✓ Enemy collision with player
- ✓ Bullets don't collide with each other

### 3. Game State
- ✓ Score increases on enemy death
- ✓ Health decreases on hit
- ✓ Game over at health = 0
- ✓ Restart works correctly

### 4. Edge Cases
- ✓ Rapid fire works correctly
- ✓ Multiple enemies on screen
- ✓ Bullets clean up properly
- ✓ Enemies clean up when off-screen

## Extensibility

### Easy to Add
- New enemy types (duplicate prefab, modify)
- Power-ups (new prefab + collision logic)
- Different bullet patterns (modify spawn angles)
- Sound effects (add AudioSource + clips)
- Particle effects (add ParticleSystem)

### Medium Difficulty
- Multiple levels/scenes
- Boss battles
- Weapon upgrades
- Shop/currency system

### Advanced
- Procedural enemy patterns
- Achievement system
- Online leaderboards
- Multiplayer support

## Summary

This 2D shooting game implements all essential features of a classic space shooter:
- Responsive player controls
- Challenging enemy AI
- Score-based progression
- Clear win/loss conditions
- Extensible architecture

The game is built using Unity best practices and can serve as:
- A complete playable game
- A learning project for Unity 2D
- A foundation for more complex shooters
- A portfolio piece

**Design Status**: ✅ Complete and documented
