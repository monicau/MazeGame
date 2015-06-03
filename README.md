# Maze Game

A mini-game that generates a random maze on each play.  The game was created in Unity3d and written in C#.

The game starts with the player in an above ground location.  There is a one-way entrance and a one-way exit to the underground maze.  The maze contains three sets of rooms.  Each set has a primary room and a secondary room.  Secondary rooms are initially locked.  In order to unlock them, the correct projectile must be fired at them.  The last set of room leads to the exit of the maze that leads back to the above ground world.

##How to play
 - Move character with keys W,A,S,D
 - Move the camera with the mouse
 - Press R to restart or X to exit
 - Pick up projectile with key "."
 - Drop projectile with key ","
 - Shoot projectile by clicking left mouse button

##Play
[Download for Windows](http://monicaung.com/files/MazeGame.zip)

##Game implementation
###Creating the maze
We first create the floor (30x30 grid).  Each cell is stored in the 2D array called cells[30,30].

The start room leads to cells[0, 0] and the exit room can be accessed from cells[29,29].

First we create walls for each cell.  Each wall has a 1/4 probability of being created 1/4.  We do this to make sure we have a braided maze.  After walls are created, we start to destroy the walls in a fashion similar to Prim's algorithm.

We have two lists: 
- visitedList : a list containing visited cells 
- cellList:  a list that contains cells that we want to travel from

We randomly choose a cell c from cellList, get a list of its unvisited neighbours, randomly choose a neighbour to "travel to" by destroying the wall between c and the neighbour. Then we add neighbour to visitedList and to cellList.  If there are no unvisited neighbours for cell c, we remove cell c from cellList.

We start the algorithm by adding cell(0,0) to cellList.  

###Rooms and projectiles
Each secondary room is directly connected to its corresponding primary room.  The order of rooms that we must visit in order to win the game is: red room, green room, blue room. (R->G->B)

The red and green roomsets are randomly placed in the maze while the blue set is placed at the end of the maze so that it can be directly connected to the exit room.

The start room (external to the 30x30 grid) contains the projectile to unlock the red room's door.  The red secondary room contains the projectile to unlock the green room's door.  The green secondary room contains the projectile to unlock the blue room's door, whose secondary room leads to stairs to go back to the world.  We place the projectiles this way so that the player is forced to visit all secondary rooms and it guarantees that the puzzle is solvable.
