# SoundSurfers
# msgpack-decorators
Typescript decorators for clear messagepack formatting with @msgpack/msgpack

![example workflow](https://github.com/camnewnham/msgpack-decorators/actions/workflows/main.yml/badge.svg)

- [Purpose](#purpose)
- [Setup](#setup)
- [Usage](#usage)
  * [Importing](#importing)
  * [Defining a type](#defining-a-type)
  * [Defining a union](#defining-a-union)
  * [Record<string,T> and T[]](#record-string-t--and-t--)
  * [Encoding and Decoding](#encoding-and-decoding)


## Purpose
This project allows you to clearly define your messagepack types to support operability with other messagepack implementation. It's designed with [Messagepack C#](https://github.com/neuecc/MessagePack-CSharp) in mind and largely follows the same schema design pattern. Performance is not (yet) a focus.

## Setup
Add to your project with npm:

```
npm install https://github.com/camnewnham/msgpack-decorators @msgpack/msgpack
```

Update your `tsconfig.json` to add support for decorators and design time type referencing:
```
{
  "compilerOptions": {
    ...
    "experimentalDecorators": true,
    "emitDecoratorMetadata": true,
  }
  ...
}
```

## Usage

### Importing
```
import { encode, decode, key, union } from msgpack-decorators
```

### Defining a type

Using numeric keys
```
class Animal {
  @key(0)
  legs: number;
}
```
or string keys
```
class Animal {
  @key("legs")
  legs: number;
}
```

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
