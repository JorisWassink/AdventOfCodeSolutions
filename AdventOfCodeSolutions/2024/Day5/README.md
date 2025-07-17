# Day 5

## Problem Description
 You have a set of page ordering rules defining precedence between pages (e.g., X must appear before Y if both are present). Each update lists pages to print in a sequence. Your task is to verify if the update respects all applicable ordering rules, ignoring pages not present in that update. For correctly ordered updates, sum their middle pages. For incorrectly ordered updates, reorder them to satisfy the rules and then sum their middle pages.
 
## Goal:
Determine which updates have their pages printed in the correct order based on given ordering rules, sum the middle page numbers of those correct updates, then fix the incorrectly ordered updates and sum their middle page numbers after correction.
 
##Approach

#Part 1:
For each update, consider only the rules involving pages present in that update. Check if the update’s page sequence respects all these rules by verifying no page appears before another page that must come before it.

#Part 2:
Take updates that fail the order check and reorder their pages to satisfy all applicable rules by repeatedly swapping pages that violate any precedence constraints until the entire update respects the rules.

##Code Logic

#part 1:
Scan the update’s pages pairwise; if any adjacent pair violates an ordering rule (i.e., a later page appears before an earlier required page), mark the update as invalid. Otherwise, it is valid and its middle page number contributes to the total sum.

#part 2:
Use a bubble sort-like method to fix the order: compare adjacent pages and swap them if they violate a rule. Continue until no swaps are needed. Then sum the middle pages of these corrected updates.
	
## Reflection
Hooray! My first sorting algorithm! for this assignment I decided to use bubble sort, because its pretty easy, efficient and works well with this particular assignment! this was another fun one for sure!