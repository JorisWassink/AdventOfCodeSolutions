# Day 6

## Problem Description
A guard  follows a strict patrol pattern: move forward unless an obstacle is ahead, in which case she turns right. Given a map of the lab, simulate the guard's movement and determine all unique positions she visits before leaving the map.
 
## Goal:
First, compute how many unique tiles the guard walks on before exiting the area. Then, determine how many different spots you could place one new obstacle to trap the guard in a loop, without blocking her initial position
 
##Approach

#Part 1:
I simulated the guard’s patrol through the lab map. The guard follows a simple rule:

    If there's an obstacle directly ahead, she turns right (90 degrees clockwise).

    Otherwise, she takes a step forward.

Starting from the initial position and direction, I let the guard walk until she leaves the map. I kept track of every unique tile she walked over and counted them to get the total number of positions visited.

#Part Two:
For part two, the goal was to place a single new obstacle somewhere on the map (but not on the starting position) so that the guard gets stuck in a loop. I approached this by testing every valid empty tile as a potential new obstacle location.

For each test position:

    I simulated the guard's movement using a depth-first search.

    If placing an obstacle there caused the guard to revisit the same (position, direction) pair — meaning she’s trapped in a cycle — I marked it as a valid option.

In the end, I counted how many of those positions could lead to an infinite loop.


##Code Logic

#Part One – Simulating the Guard's Walk

    I read the input map from a file, and found the guard’s initial position and direction (^ = up).

    In the SimulateGuard function, I looped through her movements:

        If she could walk forward, I moved her and marked that tile as visited.

        If there was an obstacle ahead, I rotated her direction 90° right.

        The loop stopped once she stepped off the map.

    I counted all distinct tiles marked as visited.

#Part Two – Detecting Loop Possibilities

    In GetAllLoopOptions, I checked each walkable tile on the map as a possible place to add a new obstacle.

    For each tile, I ran a DFS (IsLoopDFS) starting from the initial guard state.

    If I detected a cycle in the (position, direction) pairs, I counted that tile as a valid loop-causing obstruction.

    I skipped the starting position and any obstacle tiles.
	
## Reflection
This one was quite tough, I had the solution pretty quickly but it wasnt very efficient, I had to rework it to make it run a DFS algorithm! this one was pretty fun!