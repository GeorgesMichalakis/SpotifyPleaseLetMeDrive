Here’s a quick **README** for your interactive road-curving script:

---

# MeshCurve – Interactive Road Deformation

This script lets you bend a road mesh **vertically in real time** while keeping the borders fixed.
It’s useful for experimenting with mesh vertex manipulation and understanding how Unity handles deformable geometry.

---

## Features

* Real-time vertex deformation in **Update()**
* Adjustable **height profile** using an `AnimationCurve`
* Fades deformation toward the edges to preserve road borders
* Parameters are tweakable **live in Play Mode**
* Preserves original mesh shape so changes don’t stack each frame

---

## Requirements

* Unity 2020.3+ (works in URP/HDRP/Built-in)
* Road mesh must have **Read/Write Enabled** in its **Import Settings**
* Mesh must have its **length aligned along the local X-axis** and **width along Z-axis**

---

## How It Works

1. On `Start()`, the script stores the **original vertex positions**.
2. Each frame (`Update()`), it:

   * Calculates where each vertex lies along the road length (0–1).
   * Calculates distance from the road’s centerline in **Z**.
   * Applies a height offset from `curveShape` scaled by `fadeDistance` so borders move less.
3. Updated vertices are applied back to the mesh, and normals are recalculated for correct lighting.

---

## Parameters

| Name             | Description                                                      |
| ---------------- | ---------------------------------------------------------------- |
| `curveShape`     | AnimationCurve defining the vertical shape along the road length |
| `heightStrength` | Maximum height offset applied at the road center                 |
| `fadeDistance`   | Distance from center where height effect fades to zero           |

---

## Usage

1. Select your road mesh in the **Project window**.
2. In the **Inspector**, enable **Read/Write Enabled**.
3. Add the `MeshCurve` script to the GameObject with the **MeshFilter**.
4. In **Play Mode**, tweak parameters in the Inspector:

   * Drag `heightStrength` for bigger/smaller hills.
   * Adjust `fadeDistance` to control how far from center the effect reaches.
   * Edit `curveShape` to design the road profile.

---

## Example

* `heightStrength`: `2`
* `fadeDistance`: `1`
* `curveShape`: simple arc → road rises in the middle and flattens at ends.

