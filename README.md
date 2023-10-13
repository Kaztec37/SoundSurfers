# Immersed in the Sounds of Space 
**Team SoundSurfers**
NASA Space Apps Challenge 2023 *regional submission hackathlone2023*

- [Challenge](#challenge)
- [Solution](#solution)
- [Setup](#setup)
  * [Install Unity™ ](#downloadUnity)
  * [Install the CSound package in Unity™](#csound)
  * [Defining a union](#defining-a-union)
  * [Record<string,T> and T[]](#record-string-t--and-t--)
  * [Encoding and Decoding](#encoding-and-decoding)
- [Data](#data)

## Challenge
**Immersed in the Sounds of Space** 
>NASA offers a variety of “sonifications” – translations of 2D astronomical data into sound –that provide a new way to experience imagery and other information from space. Advanced instruments currently provide hyperspectral (many color) images from space that are 3D (two spatial dimensions and one color dimension), and sophisticated techniques can be used to enhance 2D astronomical images to make video representations called “fly-throughs” that allow viewers to experience what it would look like to move among space objects in 3D (three simulated spatial dimensions). Your challenge is to design a method to create sonifications of these 3D NASA space datasets to provide a different perceptual path that can help us understand and appreciate the wonders of the universe!

The interested reader can learn more at https://www.spaceappschallenge.org/2023/challenges/immersed-in-the-sounds-of-space/

## Solution
Team SoundSurfers accept the challenge to create a different perceptual path to help users to understand and appreciate the wonders of the univers. We achieve this by desiging and devleoping immersive and interactive virtual reality (VR) sonifications targetting the Meta™ Quest 2™ VR headset.

## Setup
### Install Unity™ version 2022.3.10f1
The applcation was designed and developped targeting the latest version of Unity3D™ game engine. At the time of development, this was version 2022.3.10f1. This repository may work with different versions of Unity. However, to mininise the risks of conflicts and errors, SoundSurfers recommend that this repository is used with Unity version 2023.3.10f1. SoundSurfers cannot guarantee that this repository will work as is with later versions of Unity. 

You can download and intall Unity by following the link https://unity.com/download.

### CSound
This Unity™ application uses the CSound package to play a synthesis of the frequency created from the scene texture. This allows the user to hear what they see. This makes the experience of the given sateliite imagry more immerive and interactive. This has applications for data analysis and public engagement. 

To install the CSound package, you need to:

Step 1: Install git on you computer. You can download Git from this link: https://git-scm.com/downloads

Step 2: 


### Defining a union
Unions are serialized to match [Messagepack C#](https://github.com/neuecc/MessagePack-CSharp#union), however are defined on the child type rather than the parent class.

```
abstract class Animal {
  @key(0)
  name: string;
  @key(1)
  legs: number;
}

@union(0, Animal)
class Lizard extends Animal{
  @key(3)
  scales: number
}

@union(1, Animal)
class Bird extends Animal {
  @key(4)
  feathers: number
}
```

### Record<string,T> and T[]
To serialize objects with non-primitive keys or non-primitve arrays, the collection type must be specified and the type must contain keys or a union.
```
abstract class Animal {
  @key(0, Animal)
  friends: Animal[]
}
```

Map types (where `typeof key !== string`) are not supported.

### Encoding and Decoding
```
const animal: Animal = new Lizard();
animal.legs = 2;

const encoded:Uint8Array = encode(animal, Animal);
const decoded:Animal = decode(encoded, Animal);
```
If no type is specified, it will encode as the child type (Lizard).
To encode as the union type, it must be specified (Animal)

For a demonstration of available types, see the [tests](https://github.com/camnewnham/msgpack-decorators/blob/main/tests/index.test.ts)

## Data
The sonifications of the game scenes in this project are created in real time using fourier transforms of the images provided in the NASA Sapce Apps challenge "Immersed in the Sounds of Space" competition resources found here https://www.spaceappschallenge.org/2023/challenges/immersed-in-the-sounds-of-space/?tab=resources 
