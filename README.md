# Shooting_2D
A 2D bullet hell shooter game made in Unity.

## Description
A classic 2D vertical scrolling shooter where the player controls a spaceship, shoots enemies, and tries to survive as long as possible while achieving a high score.

## Features
- **Player Movement**: WASD or Arrow keys
- **Shooting System**: Automatic firing with Space/Fire1 button
- **Enemy Spawning**: Enemies spawn from the top of the screen
- **Combat**: Both player and enemies can shoot
- **Health System**: Player has 3 health points
- **Score Tracking**: Earn points by destroying enemies
- **Game Over & Restart**: Press 'R' to restart after game over

## Getting Started

### Quick Setup (Automated)
1. Open the project in Unity 2022.3.10f1 or later
2. In Unity Editor menu, go to **Tools > Create Shooting Game Prefabs**
3. This will automatically create all necessary prefabs
4. Open **Assets/Scenes/MainGame.unity**
5. Drag the **Player** prefab from Assets/Prefabs into the scene at position (0, -4, 0)
6. Select **EnemySpawner** in the Hierarchy and assign the **Enemy** prefab to its Enemy Prefabs array
7. Link bullet prefabs:
   - Open Player prefab and assign PlayerBullet to Bullet Prefab field
   - Open Enemy prefab and assign EnemyBullet to Bullet Prefab field
8. Configure collision layers (Edit > Project Settings > Physics 2D)
9. Press Play to test!

### Manual Setup
See [SETUP_GUIDE.md](SETUP_GUIDE.md) for detailed step-by-step instructions.

## Game Controls
- **Movement**: Arrow Keys or WASD
- **Shoot**: Space or Left Ctrl/Mouse Button
- **Restart**: R key (after game over)

## Project Structure
```
Shooting_2D/
├── Assets/
│   ├── Scenes/
│   │   └── MainGame.unity        # Main game scene
│   ├── Scripts/
│   │   ├── PlayerController.cs   # Player movement and shooting
│   │   ├── Enemy.cs              # Enemy behavior
│   │   ├── Bullet.cs             # Bullet logic
│   │   ├── EnemySpawner.cs       # Enemy spawn system
│   │   ├── GameManager.cs        # Game state management
│   │   └── PrefabCreator.cs      # Editor tool for prefab creation
│   ├── Prefabs/                  # Game object prefabs (created in Unity)
│   └── README.md                 # Detailed documentation
├── ProjectSettings/              # Unity project configuration
├── Packages/                     # Unity package dependencies
├── SETUP_GUIDE.md               # Step-by-step setup instructions
└── README.md                    # This file
```

## Documentation
- **[Assets/README.md](Assets/README.md)**: Detailed game mechanics and component documentation
- **[SETUP_GUIDE.md](SETUP_GUIDE.md)**: Complete setup guide with troubleshooting

## Requirements
- Unity 2022.3.10f1 or later
- Basic 2D sprites or primitive shapes (Quad)

## Credits
Created as a 2D shooting game implementation for Unity.
