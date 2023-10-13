# Immersed in the Sounds of Space 
**NASA Space Apps Challenge 2023**

- [Challenge](#challenge)
- [Solution](#solution)
  * [Video demonstration](#video-demonstration)
- [Requirements](#requirements)
- [Setup](#setup)
  * [Meta™ Quest 2™ setup](#meta-quest-2-setup)
  * [Install Unity™ ](#downloadUnity)
  * [Install the CSound package in Unity™](#csound)  
- [Data](#data)

## Challenge
**Immersed in the Sounds of Space** 
>NASA offers a variety of “sonifications” – translations of 2D astronomical data into sound –that provide a new way to experience imagery and other information from space. Advanced instruments currently provide hyperspectral (many color) images from space that are 3D (two spatial dimensions and one color dimension), and sophisticated techniques can be used to enhance 2D astronomical images to make video representations called “fly-throughs” that allow viewers to experience what it would look like to move among space objects in 3D (three simulated spatial dimensions). Your challenge is to design a method to create sonifications of these 3D NASA space datasets to provide a different perceptual path that can help us understand and appreciate the wonders of the universe!

The interested reader can learn more at https://tinyurl.com/ysh68wfy

## Solution
Team SoundSurfers accept the challenge to create a different perceptual path to help users to understand and appreciate the wonders of the univers. We achieve this by desiging and devleoping immersive and interactive virtual reality (VR) sonifications targetting the Meta™ Quest 2™ VR headset.  The sonification path is guided by the user's head pose. This makes the experience of the given satellite imagery more immerive and interactive. This has potential for enhanced data analysis and public engagement applications. For accessability, the application has a default desktop scene to allow the user to experience a demo of the project's functionality without the need for any headset.

### Video demonstration
...insert video here
 
## Requirements
The goal of this project is to make the NASA sonification approach to satellite imagery experiences even more immersive and interactive. For the fully immersirsive experience it is best to use the required VR headset. This application currently targets the Meta™ Quest 2™ only. The requirements to experience fully immerive and head pose driven sonifications at the time of writting (13/10/2023) are:

1. A Meta Quest 2 VR headset https://tinyurl.com/2bz58mke
2. The Quest 2 VR headset may inititally require installation of the Oculus desktop app for configurtion on your computer: https://tinyurl.com/f4ujktz8
3.  Unity game engine, preferably version 2022.3.10f1. This is downloaded using Unity Hub™ https://unity.com/download
4.  Clone this GitHub™ repository and run the application in Unity™

## Setup
At the time of writting, this application targets the Meta™ Quest 2™ VR headset only.

### Meta Quest 2 setup
Run the Oculus desktop app on your computer to enable Unity to communicate with your Meta™ Quest 2™.

### Install Unity™ version 2022.3.10f1
The applcation was designed and developped targeting the latest version of Unity3D™ game engine. At the time of development, this was version 2022.3.10f1. This repository may work with different versions of Unity. However, to mininise the risks of conflicts and errors, SoundSurfers recommend that this repository is used with Unity version 2023.3.10f1. SoundSurfers cannot guarantee that this repository will work as is with later versions of Unity. 

### Clone the repository
This Unity™ application uses the CSound package to play a synthesis of the frequency created from the RGB values of the pixel of the setellite image that the user is looking at. This allows the user to hear what they see. The CSound package should download as part of the projet when you clone this repository. The following instructions are provided for the interested developer who wants to install CSound into their own Unity™ project.

1. Install git on you computer. You can download Git from this link: https://git-scm.com/downloads
2. Rory Walsh created a C# wrapper to allow developers to use the CSound application as a package in Unity™. Now that you have Git installed on your computer, you can install the CSound package in Unity™ by clicking on the Window -> Package Manager menu item.
3. In the top left corner of the pop-up window, click the + symbol.
4. Click Add package from Git url...
5. Paste this link into the url text box and click the add button
6. Confirm installation of all of the default (green) elements in the pop-up window.
7. Now you are ready to use CSound in your Unity project™.  

The documentation for the Unity™ C# wrapper written by Rory Walsh is available in his GitHub™ repository here https://tinyurl.com/yja6vwhw

## Data
The sonifications of the NASA satellite imagery are created in real time using fourier transforms of the images provided in the NASA Space Apps challenge "Immersed in the Sounds of Space" competition resources https://tinyurl.com/3czu6rk9
