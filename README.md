# ProceduralDungeon
 
In this project I created a dungeon puzzle game that 
has a procedurally generated map for every new play through. Game is seperated 
into different rooms and and every room houses a puzzle for the player to solve 
and gain an item. After all the puzzles are solved and all the nececerry items are 
collected player can finish the game by throwing these key items that he/she 
collected by solving the puzzles into a well. 
Rooms are stored in the game files as prefab objects and fetched in the procedural 
generation process and randomly placed into a grid and a binary tree. Than by 
using the binary tree we decide which rooms must be connected to eachother. 
Using the grid system and a pathfinding algorithm we connect the room 
enterances by placing hallways to pathfinders found path. For the purposes of 
giving a natural feeling and a interesting structure to dungeon the hallways are 
allowed to connect and merge with other hallways to create unique structures and 
ways to dungeon to be treversed. After the hallways are connected the script 
checks if there are any unconnected or isolated hallway if it finds one the program 
is terminated and generation is started again. The more rooms the game has, more 
unique combinations and structure of dungeons can be created. For the purpose 
of demonstration. 6 rooms have been designed and used in this project. 

Aim of the project is to create a game where the player is put into a dungeon which 
is created from skratch in every new playthrough. The game and puzzles are 
divided into rooms and the main puzzles are played in these rooms. The player is 
to explore the dungeon that is never the same in any playthrough find the puzzle 
rooms and solve the puzzles by interacting with various objects and the 
enviroment. 

By dividing the gameplay into chunks, in this case rooms, we can get the needed 
data for the procedural generation as 2D arrays and place them in a grid map 
randomly. Than using a pathfinding algorithm we can pathfind the hallways 
between the rooms to complate the dungeon. All the rooms house a data storage 
class that houses the shape of the room in a 2D array waiting to be fetched and 
placed into the grid map. A binary tree structure can be used to determine which 
room is connected to which. By using matrices this problem of placing and 
connecting the rooms can be reduced to some basic matrix operations. Hallways 
can be allowed to converge and connect with eachouther to create interesting 
structures.
