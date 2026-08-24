# Tri-Tower RGB Chronicle Prototype

Reverse-documented from the playable build on 2026-08-25.

## Run

Open `prototype.html` in a browser. Click the game view to lock and hide the pointer.

## Controls

| Action | Input |
| :--- | :--- |
| Move / strafe | `WASD` or arrow keys |
| Sprint | `Shift` |
| Jump | `Space` |
| Camera | Mouse |
| Combo attack | `LMB` or `J` |
| Finisher | `RMB` or `K` |
| Switch weapon | `Q`, `Tab`, or weapon number |
| Attack Lab | `B` previous, `N` next, `P` preview |

## Current implementation

- Camera-relative movement with normalized diagonal input and velocity-matched 8-direction locomotion.
- Pointer-lock free look with broad pitch and collision-aware camera pull-in/ground slide.
- Variable jump with separate rising/falling gravity, coyote time, input buffer, and Jump/Fall/Land animation phases.
- ExplosiveLLC character with root motion neutralized for controller-driven movement.
- Bamboo staff attached to the right-hand weapon socket; attack trail follows the staff rather than a fixed world-space arc.
- Temporary combo `Attack6 → Attack8 → Attack9`; smooth 14 m Finisher triggers `Attack11` and damage at 90% travel.
- Tri-Tower paper-world blockout: central RGB/Pen plaza, three bridges/tower biomes, and central sky isle.

## Provisional work

The current attack set was authored for a two-handed sword and is only a selection baseline. The grip, missing left-hand IK, attack silhouettes, hit timing, and combo cadence must be revisited together after a staff-compatible animation set is chosen. Attack Lab exists specifically for this evaluation and is not player-facing production UI.

Quaternius Universal Animation Library 2 assets remain in the repository as a reference/fallback. ExplosiveLLC is the active character and animation source.

## Source and licensing

See `assets/ASSET-SOURCES.md`, `assets/LICENSE-quaternius.txt`, and the applicable Unity Asset Store license for vendor assets.
