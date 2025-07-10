# Day 1

## Problem Description
You are given multiple reports, each a sequence of numeric levels. A report is considered safe if its levels are either strictly increasing or strictly decreasing, and every adjacent difference is between 1 and 3 (inclusive).

##Part One Goal:
Count how many reports are safe based on the above rules.

##Part Two Goal:
Now, a single "bad" level can be removed from an otherwise unsafe report to try to make it safe. Count how many reports become safe if you are allowed to remove at most one level
 
##Approach

#Part 1:
Check if the entire report is either strictly increasing or strictly decreasing. For each adjacent pair, ensure the difference is between 1 and 3 inclusive. If all pairs follow these rules, the report is safe.

#Part 2:
If the report is unsafe, try removing one level at a time (only the level causing the violation or its adjacent one) and check if the resulting report is safe. If removing a single level fixes the report, it counts as safe.

##Code Logic
    Determine the direction (increasing or decreasing) from the first pair.

    Iterate through pairs checking:

        Direction consistency (all must increase or all must decrease).

        Difference range (1 to 3).

    If violation found:

        Try removing the element at current index and check safety.

        Try removing the element at next index and check safety.

    Return true if any of these are safe; otherwise false.
	
	
## Reflection
the difficulty in this day was definitely an increase from day one, and i really had to scratch my head for the second part, im exited to see more!
