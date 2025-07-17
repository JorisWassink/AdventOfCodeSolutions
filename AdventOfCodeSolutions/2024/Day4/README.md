# Day 4

## Problem Description
You’re given a word search consisting only of the letters 'X','M','A' and 'S', you have to search for how many times you can find the word XMAS, and how often you can find two MAS in the shape of an X.

## Goal:
Scan the string, find how often you can find an XMAS, and how often you can find two MAS in the shape of an X.
 
##Approach

#Part 1:
For each character in the grid, check if it starts with 'X'.
If so, search in all eight directions for a continuous match of the rest of the characters in "XMAS".
If all characters are found sequentially in a valid direction without going out of bounds, count that as one occurrence.
Repeat this for every character in the grid.

#Part 2:
For each cell, if it contains 'A', check both diagonal directions for matching 'M' and 'S'.
If both directions form a valid symmetric pair, count it as one CrossMas occurrence.

##Code Logic

#part 1:

Loop through each grid cell.
If the character is 'X', try to form the full "XMAS" in all 8 directions.
For each direction, step k times to match each character.
If any character doesn’t match or goes out of bounds, remove that direction.
If all characters in a direction match, increment the XMAS count.

#part 2:

For each cell, check if it’s 'A'.
Look diagonally at ↖ and ↘, and ↗ and ↙ pairs.
For each valid pair, check that one direction has 'M' and the other has 'S' (or vice versa).
If both diagonals form such pairs, count as a CrossMas..
	
	
## Reflection
This day was another fun one! For this puzzle, I didn’t need to learn anything new per se, but it was definitely great practice.
I got a bit stuck at first because I didn’t consider that one 'X' could be part of multiple "XMAS" words.
It was very satisfying to figure that out and get everything working!
