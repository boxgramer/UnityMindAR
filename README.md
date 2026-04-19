# MindAR + Unity WebGL Experimental Project

## Overview

This project is an experimental prototype for integrating MindAR.js with Unity WebGL.

The main goal is to use:

* **MindAR.js** for image tracking and marker detection in the browser
* **Unity WebGL** for rendering 3D content and gameplay logic
* **JavaScript ↔ Unity communication** using `unityInstance.SendMessage()` for data transfer

This architecture allows MindAR to handle AR tracking while Unity focuses on interactive 3D rendering.

---

## Demo & References

### Live Demo

[https://capable-quokka-72530b.netlify.app/](https://capable-quokka-72530b.netlify.app/)

### MindAR Documentation

[https://hiukim.github.io/mind-ar-js-doc/](https://hiukim.github.io/mind-ar-js-doc/)

### Test Marker Image

[https://cdn.jsdelivr.net/gh/hiukim/mind-ar-js@1.2.5/examples/image-tracking/assets/card-example/card.png](https://cdn.jsdelivr.net/gh/hiukim/mind-ar-js@1.2.5/examples/image-tracking/assets/card-example/card.png)

---

## Project Purpose

This is an **experimental research project** created to test:

* Web-based AR using image tracking
* Transparent Unity WebGL canvas overlay
* MindAR marker transform transfer to Unity
* JavaScript to Unity communication
* Mobile browser compatibility
* Touch interaction with Unity objects inside WebAR

This project is intended for prototyping and technical validation, not final production use.

---

## Core Architecture

```text
Camera Feed (Browser)
        ↓
MindAR.js (Image Tracking)
        ↓
A-Frame / Three.js Marker Detection
        ↓
JavaScript Bridge
        ↓
unityInstance.SendMessage(...)
        ↓
Unity WebGL
        ↓
3D Model Rendering + Interaction
```

---

## Features

### MindAR.js

* Image target detection
* Marker tracking
* `targetFound` / `targetLost` event handling
* Marker world position extraction
* Marker rotation extraction

### Unity WebGL

* Transparent canvas rendering
* 3D model rendering
* Touch drag rotation
* Marker position synchronization
* Marker rotation synchronization

### JavaScript Bridge

* Position transfer
* Rotation transfer
* Unity message communication
* Canvas synchronization
* Mobile responsive WebGL setup

---

## Key Technical Challenges

This project explores solutions for:

### Transparent Unity WebGL Canvas

This project uses:

* Built-in Render Pipeline
* transparent background
* `.jslib` WebGL override
* Gamma Color Space

---

Special handling for:

* responsive canvas sizing
* touch input
* mobile viewport scaling
* performance optimization
* browser compatibility

---

## Unity Version

Tested with:

```text
Unity 6000.3
```

Recommended:

```text
Built-in Render Pipeline
```

---

## Important Notes

### This is Experimental

This project is still under active testing.

Known issues may include:

* mobile rendering inconsistencies
* rotation offset calibration
* marker scale mismatch
* browser-specific behavior
* WebGL transparency limitations

---

### Recommended Browser

### Android

```text
Google Chrome
```

###

```text
```

---

## Communication Example

### JavaScript → Unity

```javascript
unityInstance.SendMessage(
    "ARManager",
    "SetMarkerPosition",
    `${x},${y},${z}`
);
```

### Unity C#

```csharp
public void SetMarkerPosition(string value)
{
    Debug.Log(value);
}
```

---

## Disclaimer

This project is for research and experimentation only.

Implementation details may change significantly as testing continues.

