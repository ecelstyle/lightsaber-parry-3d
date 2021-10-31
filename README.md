# lightsaber-parry-3d
Hyper Casual Game Prototype Test

## UIManager
This class use to get user input and trigger events

UI components must be set in editor
  * Sliders
  * Simulate Button
  * Collide text
  * Particle System

Events
  * OnAngleChangeEvent
      _After sliders value changed this event raises_
  * OnSimulateEvent
      _After simulate button clicked this event raises_



## SimulationManager

Events
  * OnPhysicsTickEvent
      _this event triggered every fixed(physics) step while the simulation is running _
  * OnResetEvent
      _this event raises when the simulation start_



## Saber

Visual presentation of lightsaber object must be set in editor
  * Visual Component
  * isPlayer
      _The player's lightsaber must be marked and opponent's should not be marked_

Events
  * OnCollisionEvent
      _this event triggered if collision occurs with position of collision and a boolean parameter which shows prediction or simulation_
  * OnSwingEndEvent
      _this event raises when the simulation ends without collision, has a boolean parameter which shows prediction or simulation_



## Settings

Project Settings -> Physics -> Contact Pairs Mode -> Enable Kinematic Kinematic Pairs
_we are using two kinematic body_

Tags need to be added, and need to set Rigidbody Objects and Collision Objects
  * Player
  * Opponent

###### Saber Settings

Need a parent object for pivoting.
  * One child object is rigidbody with Saber Class and Tags Setted (Player or Opponent)
  * Other child object is visual presentation of lightsaber, this object need to referenced to Saber Object (physics object)



Demo

![Class Diagram](https://github.com/ecelstyle/lightsaber-parry-3d/blob/main/demo.gif)

Scene Hierarchy

![Class Diagram](https://github.com/ecelstyle/lightsaber-parry-3d/blob/main/scene.png)


Class Diagram
![Class Diagram](https://github.com/ecelstyle/lightsaber-parry-3d/blob/main/ClassDiagram1.png)













## Gameplay: Lightsaber Parry 3D
###### Short description:
Two characters facing each other engage in a lightsaber fight. The
characters start to swing from their left side. The player sets the angle of
each lightsaber. A message appears on the screen to show if the
lightsabers will collide or not. The player will have the option to simulate
the swing.
Mechanics:
a. Use two sliders for the angles
b. The collision prediction message will update after each valid angle change
c. (Optional) Spawn a particle where the lightsabers collide
d. Expose all the configurable parameters to something easily modifiable by Game
Designers (SO, JSON, static class, etc)
