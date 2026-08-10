# Camera Shake

A reusable camera shake system for Unity using Cinemachine.

Provides configurable camera shake effects that can be triggered from gameplay systems without requiring each system to manage camera shake directly.

## Features

* Centralized camera shake management
* Cinemachine integration
* Configurable shake settings
* Reusable camera shake presets
* Event-based shake triggering
* Plug-and-play manager prefab
* Example scene included as a package sample

## Installation

Install the package through the Unity Package Manager using your preferred package registry.

The package depends on:

* **Singleton**
* **Cinemachine**

## Basic Usage

Add the provided `CameraShakeManager` prefab to your scene.

The manager can then be accessed through its singleton instance.

```csharp
CameraShakeManager.Instance
```

Trigger camera shake effects using the provided camera shake configuration and event system.

## Samples

An example scene is available through the Unity Package Manager:

**Package Manager → Camera Shake → Samples → Example**

The sample demonstrates the camera shake system and its available settings.

## Requirements

* Unity 6000.3 or later
* Singleton package
* Cinemachine

## License

See [LICENSE.md](LICENSE.md).