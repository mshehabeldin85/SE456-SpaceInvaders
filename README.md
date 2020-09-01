# Space Invaders Game
Space Invaders game clone using Object Oriented Design Patterns. This project is a perfect application of 13 Design Patterns inplemented to solve many challenging problems.

# Demos:
This [Youtube Demos Playlist](https://www.youtube.com/playlist?list=PLiPAB5oCbbf-YwGOv26o44cnh6UF4SNt_) explains how the game components are implemented iteratively until achiveing the final product.

# Patterns used and their purposes
1. **Factory:** Creating 55 aliens without worrying about the actual implementation of each alien type.
2. **Singleton:** Creating managers for each system's component to easily access them.
3. **Object Pool:** Implementing the Aliens Grid animation which includes changing the X and Y positions for each Alien and its image when the grid is stepping right or left.
4. **Proxy:** A lightweight interface of the heavy alien objects. These object interfaces will expose the functions of the actual aliens but with lower cost.
5. **Flyweight:** To load all the characters in a shared place once the game is loaded, then ruse them to draw the texts on the screen.
6. **Composite:**  To implement a hierarchy of game objects that are either a composition of other objects or a single object (e.g.: Aliens Grid, Aliens, Sheilds, Sheild Bricks).
7. **Iterator:** A standard iteration interface for multiple iteration approaches on the Game Objects Trees.
8. **Command:** Implementing Timer Events that can be queued and parameterized as needed.
9. **Observer:** Build a list of observers that will be notified when the state of the related object changes. For instance, when a ship missile hits and alien: The alien should be removed from grid, a splat effect should be displayed, the ship should be able to shoot another missile, a sound effect should be played, and player’s score get updated accordingly.
10. **Visitor:** Identifying the collision Game Object pairs and apply the appropriate action accordingly through a clean and scalable way.
11. **Strategy:** Isolating the behavior of each Bomb type and implementing the dropping animation algorithm for each category.
12. **Null Object:** Having an object type in an object structure that inherits the same interface but does nothing.
13. **State:** To control the ship’s behavior by not shooting multiple missiles at a time. Fo example, the missile should hit one of the game objects before the player can launch another missile.
