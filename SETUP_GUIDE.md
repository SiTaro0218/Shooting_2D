# 2D Shooting Game - Setup Guide

This guide will walk you through setting up the 2D shooting game in Unity Editor.

## Prerequisites
- Unity 2022.3.10f1 or later
- Basic understanding of Unity Editor

## Quick Start

### Step 1: Open the Project
1. Open Unity Hub
2. Click "Add" and navigate to the Shooting_2D folder
3. Open the project (Unity will import all necessary packages)

### Step 2: Create Player Prefab

#### Create Player Sprite
1. In Unity Editor, go to **GameObject > 2D Object > Sprite > Square**
2. Rename it to "Player"
3. In Inspector:
   - Set Tag to "Player" (Create new tag if needed)
   - Set Layer to "Player" (Create new layer if needed)
   - Set Position: (0, -4, 0)
   - Set Scale: (0.5, 0.5, 1)
   - Change Sprite Color to Blue or any player color

#### Add Player Components
1. With Player selected, click **Add Component**:
   - Add **Rigidbody2D**
     - Set Body Type to **Kinematic**
     - Set Gravity Scale to 0
   - Add **Box Collider 2D**
     - Check "Is Trigger"
   - Add **PlayerController** script (already in Assets/Scripts)

#### Create Fire Point
1. Right-click on Player in Hierarchy > **Create Empty**
2. Rename to "FirePoint"
3. Set Position: (0, 0.5, 0) - This is relative to Player
4. This is where bullets will spawn from

#### Complete Player Setup
1. Select Player in Hierarchy
2. In PlayerController component:
   - Set Move Speed: 5
   - Set Fire Rate: 0.2
   - Drag the FirePoint from Hierarchy to "Fire Point" field
   - Leave "Bullet Prefab" empty for now (we'll set it after creating bullet)

#### Save as Prefab
1. Create folder: **Assets/Prefabs** (if not exists)
2. Drag Player from Hierarchy to Assets/Prefabs folder
3. Delete Player from Hierarchy (we'll place it in scene later)

### Step 3: Create Player Bullet Prefab

1. **GameObject > 2D Object > Sprite > Square**
2. Rename to "PlayerBullet"
3. In Inspector:
   - Set Tag to "PlayerBullet" (Create tag if needed)
   - Set Layer to "Bullet" (Create layer if needed)
   - Set Scale: (0.2, 0.4, 1)
   - Change Sprite Color to Yellow/White

4. Add Components:
   - **Rigidbody2D**
     - Body Type: **Kinematic**
     - Gravity Scale: 0
   - **Box Collider 2D**
     - Check "Is Trigger"
   - **Bullet** script
     - Speed: 10
     - Lifetime: 3
     - Damage: 1
     - Check "Is Player Bullet"

5. Drag to Assets/Prefabs folder to save as prefab
6. Delete from Hierarchy

### Step 4: Create Enemy Prefab

1. **GameObject > 2D Object > Sprite > Square**
2. Rename to "Enemy"
3. In Inspector:
   - Set Tag to "Enemy" (Create tag if needed)
   - Set Layer to "Enemy" (Create layer if needed)
   - Set Scale: (0.6, 0.6, 1)
   - Change Sprite Color to Red

4. Add Components:
   - **Rigidbody2D**
     - Body Type: **Kinematic**
     - Gravity Scale: 0
   - **Box Collider 2D**
     - Check "Is Trigger"
   - **Enemy** script
     - Move Speed: 2
     - Move Direction: -1
     - Max Health: 3
     - Fire Rate: 2
     - Check "Can Shoot"
     - Score Value: 10

5. Create FirePoint (child of Enemy):
   - Right-click Enemy > Create Empty
   - Rename to "FirePoint"
   - Set Position: (0, -0.5, 0)

6. Select Enemy again
7. In Enemy component:
   - Drag FirePoint to "Fire Point" field
   - Leave "Bullet Prefab" empty for now

8. Drag to Assets/Prefabs folder
9. Delete from Hierarchy

### Step 5: Create Enemy Bullet Prefab

1. **GameObject > 2D Object > Sprite > Square**
2. Rename to "EnemyBullet"
3. In Inspector:
   - Set Tag to "EnemyBullet" (Create tag if needed)
   - Set Layer to "Bullet"
   - Set Scale: (0.2, 0.4, 1)
   - Change Sprite Color to Orange/Red
   - Set Rotation: (0, 0, 180) to face downward

4. Add Components:
   - **Rigidbody2D**
     - Body Type: **Kinematic**
     - Gravity Scale: 0
   - **Box Collider 2D**
     - Check "Is Trigger"
   - **Bullet** script
     - Speed: 10
     - Lifetime: 3
     - Damage: 1
     - **UNCHECK** "Is Player Bullet"

5. Drag to Assets/Prefabs folder
6. Delete from Hierarchy

### Step 6: Link Prefabs

#### Link Bullets to Player
1. Open Assets/Prefabs folder
2. Select "Player" prefab
3. In PlayerController component:
   - Drag **PlayerBullet** prefab to "Bullet Prefab" field
4. Click "Apply" or save changes

#### Link Bullets to Enemy
1. Select "Enemy" prefab
2. In Enemy component:
   - Drag **EnemyBullet** prefab to "Bullet Prefab" field
3. Click "Apply" or save changes

### Step 7: Configure Scene

1. Open **Assets/Scenes/MainGame.unity**

2. **Place Player in Scene:**
   - Drag Player prefab from Assets/Prefabs to Hierarchy
   - Set Position: (0, -4, 0)

3. **Configure Enemy Spawner:**
   - Select "EnemySpawner" in Hierarchy
   - In EnemySpawner component:
     - Set Enemy Prefabs array size to 1
     - Drag **Enemy** prefab to Element 0
     - Spawn Rate: 2
     - Spawn Range X: 8
     - Spawn Y: 6

4. **Verify Camera:**
   - Select Main Camera
   - Ensure it's set to Orthographic
   - Size: 5
   - Position: (0, 0, -10)

### Step 8: Configure Physics Layers

1. Go to **Edit > Project Settings > Physics 2D**
2. Scroll down to **Layer Collision Matrix**
3. Configure collisions:
   - ✅ Player ↔ Enemy
   - ✅ Player ↔ Bullet (for enemy bullets)
   - ✅ Enemy ↔ Bullet (for player bullets)
   - ❌ Bullet ↔ Bullet (bullets shouldn't collide with each other)
   - ❌ Player ↔ Player layer itself
   - ❌ Enemy ↔ Enemy layer itself

### Step 9: Test the Game

1. Press **Play** button in Unity Editor
2. Test controls:
   - **WASD** or **Arrow Keys** to move
   - **Space** to shoot
3. Verify:
   - Player moves within screen bounds
   - Player can shoot bullets upward
   - Enemies spawn from top
   - Enemies move downward
   - Enemies shoot bullets downward
   - Collision detection works
   - Score increases when enemies are destroyed
   - Health decreases when hit
   - Game over triggers when health reaches 0

### Optional: Add UI (Recommended)

#### Create Canvas
1. **GameObject > UI > Canvas**
2. Canvas Scaler settings:
   - UI Scale Mode: Scale With Screen Size
   - Reference Resolution: 1920x1080

#### Add Score Text
1. Right-click Canvas > **UI > Text - TextMeshPro**
   - If prompted, import TMP Essentials
2. Rename to "ScoreText"
3. Set properties:
   - Position: Top-left corner
   - Anchor: Top-Left
   - Text: "Score: 0"
   - Font Size: 36
   - Color: White

#### Add Health Text
1. Right-click Canvas > **UI > Text - TextMeshPro**
2. Rename to "HealthText"
3. Set properties:
   - Position: Top-right corner
   - Anchor: Top-Right
   - Text: "Health: 3"
   - Font Size: 36
   - Color: White

#### Add Game Over Panel
1. Right-click Canvas > **UI > Panel**
2. Rename to "GameOverPanel"
3. Set color to semi-transparent black
4. Add child Text:
   - Right-click GameOverPanel > **UI > Text - TextMeshPro**
   - Rename to "GameOverText"
   - Text: "GAME OVER\nPress R to Restart"
   - Font Size: 48
   - Alignment: Center
   - Color: Red/White
5. Disable GameOverPanel (uncheck in Inspector)

#### Link UI to GameManager
1. Select GameManager in Hierarchy
2. Drag UI elements to GameManager component:
   - ScoreText → Score Text field
   - HealthText → Health Text field
   - GameOverText → Game Over Text field
   - GameOverPanel → Game Over Panel field

## Troubleshooting

### Player can't move
- Check Input settings (Edit > Project Settings > Input Manager)
- Verify PlayerController script is attached
- Check Rigidbody2D is set to Kinematic

### Bullets don't shoot
- Verify Bullet Prefab is assigned in PlayerController/Enemy
- Check FirePoint is assigned
- Ensure bullet has Bullet script

### Collisions don't work
- Verify Tags are set correctly (Player, Enemy, EnemyBullet, PlayerBullet)
- Check Colliders have "Is Trigger" enabled
- Configure Layer Collision Matrix in Physics 2D settings
- Ensure Rigidbody2D components exist

### Enemies don't spawn
- Check Enemy Prefab is assigned to EnemySpawner
- Verify EnemySpawner GameObject is active
- Check spawn position is above camera view

### Game doesn't restart
- Ensure GameManager script is in scene
- Check scene is added to Build Settings (File > Build Settings)

## Customization Tips

### Change Colors
- Select any sprite and modify its Color property in SpriteRenderer

### Adjust Difficulty
- Increase enemy spawn rate in EnemySpawner
- Reduce player fire rate in PlayerController
- Increase enemy health in Enemy prefab
- Adjust movement speeds

### Add More Enemy Types
- Duplicate Enemy prefab
- Change color and stats
- Add to EnemySpawner's Enemy Prefabs array

### Add Sound Effects
- Add AudioSource components to prefabs
- Assign audio clips
- Call `GetComponent<AudioSource>().Play()` in scripts

## Next Steps
Once the basic game is working, consider adding:
- Power-ups (health, rapid fire, shields)
- Multiple enemy types with different behaviors
- Boss battles
- Particle effects for explosions
- Background music
- High score system
- Menu scene
- Difficulty levels

## Support
For issues or questions, refer to:
- Assets/README.md for game mechanics
- Unity Documentation: https://docs.unity3d.com/
- Script comments in Assets/Scripts/

Enjoy your 2D shooting game!
