# Day 1

## Problem Description
You're given two lists of location IDs. To measure how different the lists are, sort both lists and pair up corresponding elements. Then, calculate the absolute difference for each pair and sum them all.

## Goal
Compute the total distance between the two lists by summing the absolute differences of each sorted pair.
 
## Approach
The program processes a text file with lines of paired numbers. It handles these pairs in two different ways:
In the first task, it looks at how different the two sides are when sorted.
In the second, it looks at how much overlap or "similarity" exists between them based on repeated values

## Code Logic
In the first part, the numbers from the left and right of each line are collected. Once gathered, both sets are sorted. Then, one by one, the program compares matching positions, measuring how far apart the numbers are and summing those distances to get a final value.
In the second part, the program goes through each number from the left side. For each one, it checks how many times that number appears on the right. If it's seen before, it remembers the count. Then it multiplies the number by how many times it matched and adds that to a total score.

## Reflection
This was a straightforward problem, but with just enough logic to keep it engaging. Sorting and basic counting are familiar territory, and the caching twist in part two adds a nice touch. It’s satisfying to see how a small optimization (like memoization) can make a clear difference — simple, but fun.

## Time Complexity
The first part mainly spends time sorting, which takes n log n, and then comparing, which takes n — so overall it's O(n log n).
The second part may compare each number on the left with every number on the right. In the worst case, that’s n². But if the same number appears many times, it caches the count, making it faster on average — closer to O(n·k) where k is the number of unique values.