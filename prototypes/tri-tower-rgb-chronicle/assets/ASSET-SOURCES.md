# Runtime animation assets

## Universal Animation Library 2 — retained reference

- Source: Universal Animation Library 2 [Standard] by Quaternius
- Original page: https://quaternius.com/packs/universalanimationlibrary2.html
- Download page: https://quaternius.itch.io/universal-animation-library-2
- License: CC0 1.0 Universal
- Local runtime/reference assets: official non-root-motion `UAL2_Standard.glb` and embedded data.
- Current status: superseded by ExplosiveLLC in the playable prototype; retained for comparison and fallback only.

The original license text is preserved in `LICENSE-quaternius.txt`.

## ExplosiveLLC RPG Character Mecanim Animation Pack FREE

- Source: Unity Asset Store package 65284 by ExplosiveLLC
- Runtime model: `RPG-Character.FBX`
- Runtime locomotion: 8-direction Strafe and Run clips
- Runtime jump chain: Jump (frames 0–18), Fall (0–32 loop), Land (21–34); Unity `.meta` ranges are recreated in Three.js.
- Temporary bamboo staff combo: Attack6, Attack8, Attack9, then Attack11 near the end of the finisher lunge.
- Attack evaluation set: Attack1–11 embedded for the prototype Attack Lab.
- Root-motion treatment: X/Z root translation is stripped/locked while vertical skeletal motion is preserved.
- Weapon treatment: procedural bamboo staff attached to `B_R_Hand`; trail samples actual staff points during attacks.
- License: Standard Unity Asset Store EULA (vendor files remain under `ExplosiveLLC/`)
- Prototype delivery: selected FBX files are embedded in `explosive-rpg-data.js`
