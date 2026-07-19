<div align="center">

<img src="assets/logo.png" alt="QuestDex logo" width="220"/>

# QuestDex

**Track Goals. Level Up. Be Legendary.**

A goal-tracking companion game where your real-life consistency raises, feeds, and evolves a creature you choose.

[![Play on Web](https://img.shields.io/badge/Play-WebGL%20Demo-e8372a?style=for-the-badge)](https://questdexf.vercel.app/)
[![Platform](https://img.shields.io/badge/Platform-Windows%20%7C%20Android%20%7C%20WebGL-2e6fba?style=for-the-badge)]()
[![Engine](https://img.shields.io/badge/Engine-Unity-f4c542?style=for-the-badge)]()

</div>

---

## Play Now

**Live WebGL demo:** [https://questdexf.vercel.app/](https://questdexf.vercel.app/)

Works directly in the browser on desktop and mobile — portrait and landscape both supported, no download required.

<div align="center">
<img src="assets/qr-code.png" alt="Scan to play QuestDex" width="180"/>

<sub>Scan to open the live demo on your phone</sub>
</div>

---

## What is QuestDex?

Most habit trackers are easy to abandon — a checkbox doesn't care if you skip a day. QuestDex turns your goal list into a Pokédex. Pick a starter, complete real tasks to earn PokeCreds, buy food, feed your creature, and watch it evolve.

Miss a goal and nothing breaks — your companion simply pauses. Progress isn't a number on a bar. It's a creature you can see, hear, and grow.

### Core loop

1. **Set a Goal** — add a real task, pick a priority (Low / Medium / High)
2. **Complete It** — mark it done, earn PokeCreds
3. **Buy Food** — spend credits in the PokeShop
4. **Feed & Train** — feed your creature, gain XP
5. **Evolve** — hit the threshold, watch it evolve

### What makes it different

- **A real economy, not a meter** — currency, a shop, an inventory, and a feeding mechanic, not just a progress bar
- **Stakes without punishment** — missed goals pause growth instead of resetting it
- **Choice creates ownership** — pick your own starter from five evolution lines

---

## Downloads

| Platform | Location | Notes |
|---|---|---|
| **Windows** | `Downloads/Windows/` | Inno Setup installer + standalone `.exe` |
| **Android** | `Downloads/Android/` | `.apk`, portrait & landscape supported |
| **WebGL** | Play live at [questdexf.vercel.app](https://questdexf.vercel.app/) | No install needed |

Project source and editable Unity files live in `Quest Dex Project Files/`. A separate exported build lives in `Web GL Build Files/` for local hosting/testing.

---

## Starters

| Starter | Type | Evolution line |
|---|---|---|
| Charmander | Fire | Charmander → Charmeleon → Charizard |
| Froakie | Water | Froakie → Frogadier → Greninja |
| Bulbasaur | Grass | Bulbasaur → Ivysaur → Venusaur |
| Pichu | Electric | Pichu → Pikachu → Raichu |
| Squirtle | Water | Squirtle → Wartortle → Blastoise |

---

## Built With

- **Unity** (C#) — core engine, gameplay logic, UI
- **TextMeshPro** — all in-game text rendering
- **JSON-based save system** — persistent goals, economy, inventory, XP, and evolution state
- **Custom particle & audio systems** — coin bursts, feed hearts, evolution sparkle, centralized `AudioSource` management
- **Async scene loading** — no freeze on transitions
- **WebGL / Windows / Android** — shared codebase, exported across all three

---

## Technical Highlights

- Nested, data-driven evolution system (`starter → stage → sprite/name`) built to extend without rewriting logic
- Persistent save/load covering goals, currency, food inventory, XP, and evolution stage across all platforms
- Responsive WebGL canvas — full-screen, orientation-aware layout tested in-browser on mobile
- Native Android build tested independently across multiple physical devices, including live rotation

---

## Project Structure

```
├── Downloads/
│   ├── Windows/              # Installer + .exe
│   └── Android/               # .apk
├── Quest Dex Project Files/   # Full Unity source project
├── Web GL Build Files/        # Exported WebGL build
├── assets/                    # README images (logo, QR code)
└── README.md
```

---

## Team / Submission Info

Built solo in Unity for [Hackathon Name] 2026.

<div align="center">

*Gotta Achieve 'Em All.*

</div>
