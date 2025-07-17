# Day 3

## Problem Description
You’re given a corrupted string that may contain valid multiplication instructions in the form mul(X,Y), where X and Y are 1–3 digit numbers. Some parts of the string also include do() and don't() instructions that toggle whether future mul operations are counted.

## Goal:
Scan the string, only sum the results of valid mul(X,Y) instructions that are currently enabled (default is enabled, affected by do() and don't()), and return the total sum.
 
##Approach

#Part 1:
Use Regex to find the good data in the string, then i parse the numbers in that string to integers, and multiply those with each other and done is Kees!

#Part 2:
Use Regex to find the good data in the string, keep track of if the instruction is enabled or disabled, then if the instruction is enabled parse the numbers in that string to integers, and multiply those with each other and done is Kees!

##Code Logic
#part 1:

    Read the file content.

    Find all mul(x,y) patterns using regex.

    Extract the numbers from each match.

    Multiply the numbers.

    Sum all results.

    Print the total.

#part 2:

    Read the file content.

    Find all mul(x,y), do(), and don't() patterns using regex.

    Initialize a flag instructionEnabled as true.

    Loop through matches:

        If do(): set flag to true.

        If don't(): set flag to false.

        If mul(x,y) and flag is true: extract numbers, multiply, and add to total.

    Print the total.
	
	
## Reflection
This day was my favorite one so far! I had to learn how to work with Regex for the first time (I had never done that before) and it was really fun and satusfying to learn with a new tool! i definitely learned a lot here!
