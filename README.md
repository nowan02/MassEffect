# MassEffect themed university project

### Must haves of the assignment
* Build a star system using a **graph** data structure.
* The graph is **unweighted**.
* Star systems contain missions as a **linked list**.
* Completing missions and travelling between two nodes takes up one day.
* Mission have an associated **risk and reward**.
* The player has limited amount of days.
> The goal is to collect the highest reward with the least risk possible.

### The game
My goals included making a more transparent project and code structure, while implementing data structures I've learned.
Since we were limited to console applications, the UI is very crude and text based, but explains all the notions of the game.
Instead of a pure optimization program, I decided to make it more like a game.

Since we can move freely in the graph, a knapsack algorithm would have been very complicated to implement. So instead of
an algorithm deciding the path, the player has choices. Starsystems are listing in an increasing Reward/Risk value. On the top
there will be star systems with missions which are the least risky compared to their value.
Risky missions will sometimes result in accidents, which remove available days as a penalty.

Even if the top is technically the most optimal choice, players can decide to take other missions in hope of a more
valuable down the line (Since missions are stored in a singly linked list, we can't see the next mission).
At the beginning of the game, only one star system is revealed, so rest is left to be discovered. Between discovered star systems
a simple recursive algorithm decides the shortest path, which is just the number of jumps in this case.
> The player has the same goal as the project's goal, but has an element of choice. We can choose the locally optimal mission,
> or make a choice to discover the rest of the graph and other mission chains for greater values

### Enviroment
* I used C# and .net 5, without outside dependencies.
* The project file is included, it can be compiled and run using .net 5 and above.
* The names of star systems can be modified/extended in "test". Format:
> BEGIN\
> system_name\
> mission_name;value;risk;description\
> ...\
> END
* Events can also be extended in "eventtest". Format:
> event_description;penalty_days
