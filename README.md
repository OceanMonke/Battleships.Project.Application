# Battleship Game Project
#### It's a console application which simulates a match of battleships between two players.
##### This is an attempt to create a battleship application simulation for a job interview. While creating this project I've stumbled upon some examples on how to model such a game . I found most of them very interesting and used some of the ideas in this project. The rules of the game in this project are the default rules that you can find on the battleships wikipedia page: https://en.wikipedia.org/wiki/Battleship_(game)

###### Most important parts of the project:
- **placing of the ships**: I decided that the ships can touch each other, but cannot overlap. Ships are placed by an algorythm which uses a while loop as long as there are ships left to place. In this loop a random number generator is used to choose a starting square for the ship and it's orientation (it can be vertical or horizontal). If the orientation is vertical, the algorythm tries to place the ship by incrementing the y (column) value. If it encounters a square that is already occupied, it chooses another random squares and tries again. The same situation happens when a part of a ship is being placed out of bounds. If every of the chosen squares is not occupied and it is a part of the grid then the ship is placed. 
- **shooting tactics**: in battleships there are many strategies for maximizing the chances to win but most of them comes to use if we are playing against a human. There are some aspects of psychology which can help us in predicting how our opponents ships are placed. In this case however, the ships positions are completely random so a good strategy is to start with random shots, that cover every second square until a ship is found. Then shooting the neighbouring squares is crucial to determine the orientation of the ship and then sink it.

###### Interesting ideas to develop the project in the future:
- a playable version :)
- changing the placing rules, so that no ships can touch each other.
- salvo version of the game, where players shoot five shots at a time; this would make shooting more complicated while maintaining its effectiveness.
- a version of the game in which players can move one of their ship every few turns
- a version of the game which allows players to create their own shapes of ships.
- mines, which when shot at destroy everything in a set radius.
- creating a good looking frontend.
