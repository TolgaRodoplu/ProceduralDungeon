# ProceduralDungeon

A Unity-based dungeon puzzle game featuring procedurally generated maps that create a unique experience with every playthrough.

## Overview

ProceduralDungeon is a puzzle adventure game where each playthrough generates a completely new dungeon layout. Players explore interconnected rooms, solve puzzles, collect key items, and ultimately complete the game by depositing collected items into a central well.

## Features

- **Procedural Generation**: Every playthrough creates a unique dungeon layout
- **Room-Based Design**: Dungeon divided into distinct puzzle rooms
- **Binary Tree Connectivity**: Rooms connected via binary tree structure for logical progression
- **Custom Pathfinding**: Hallways connect room entrances using a greedy best-first pathfinding algorithm
- **Hallway Merging**: Hallways can converge to create interesting dungeon structures
- **Validation System**: Automatic regeneration if isolated/unconnected areas detected
- **6 Unique Puzzle Rooms**: Each room contains unique puzzles and challenges

## Technical Details

### Architecture

- Rooms stored as prefab objects with 2D array data representing shape
- 30x30 grid system used for room placement
- Binary tree determines room connectivity
- Custom greedy pathfinding connects room entrances via hallways using Manhattan distance
- Isolation detection ensures fully connected dungeons

### Pathfinding Algorithm

The project uses a custom greedy best-first search algorithm rather than Unity's NavMesh:
- Calculates Manhattan distance to target
- Moves toward the closest neighbor at each step
- Tracks visited cells to prevent loops

### Puzzle Types

Based on the included scripts:
- Chess Puzzle
- Garden Puzzle
- Maze Puzzle
- Library Puzzle
- Hub Puzzle
- Little Red Riding Hood themed puzzle

## Requirements

- **Unity Version**: 2022.3.10f1
- **Platform**: Windows / macOS

### Dependencies

- TextMeshPro (3.0.6)
- Unity AI Navigation (1.1.4) - included but not actively used
- AR Foundation (5.0.7) - optional

## Installation

1. Clone this repository:
   ```bash
   git clone https://github.com/yourusername/ProceduralDungeon.git
   ```

2. Open Unity Hub

3. Click "Add" and navigate to the cloned project folder

4. Select the project and ensure Unity 2022.3.10f1 is selected

5. Click "Open" to launch the project

## Usage

1. Open the project in Unity
2. Navigate to `Assets/Scenes/`
3. Open the main scene (SampleScene)
4. Press Play in the Unity Editor

### Controls

- **Movement**: WASD or Arrow keys
- **Interact**: E or Left Mouse Button
- **Look**: Mouse movement

## Project Structure

```
Assets/
├── Materials/          # Visual materials for rooms and objects
├── Scenes/             # Unity scene files
├── Scripts/            # C# scripts
│   ├── PlayerController.cs
│   ├── HeadBobController.cs
│   ├── Generate.cs      # Procedural generation
│   ├── Data.cs          # Data structures
│   ├── Puzzle.cs        # Base puzzle class
│   ├── ChessPuzzle.cs
│   ├── GardenPuzzle.cs
│   ├── MazePuzzle.cs
│   ├── LibraryPuzzleScript.cs
│   └── HubPuzzle.cs
├── Shaders/            # Custom shaders
└── Resources/          # Runtime loaded assets
```

## How It Works

1. **Room Placement**: Rooms (stored as prefabs with 2D array data) are randomly placed on a 30x30 grid
2. **Connectivity**: A binary tree determines which rooms connect to each other
3. **Hallway Generation**: Custom greedy pathfinding connects room entrances with hallways
4. **Hallway Merging**: Hallways can merge to create natural-feeling dungeon structures
5. **Validation**: The system checks for isolated hallways and regenerates if found

## Development

This project uses a custom grid-based pathfinding system. Room prefabs contain `Data` components that define room shapes as 2D arrays, which are placed into the grid map and connected via the binary tree structure.

## License

This project is available for educational and personal use.

## Acknowledgments

Developed as a demonstration of procedural generation techniques in Unity game development.