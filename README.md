# Pedro No Quiere Pagar Impuestos 🐱🔪

A roguelite solitaire built in Unity. You are **Pedro**, a Japanese samurai cat watching your homeland get overrun by an invading army of Chinese dogs — and the only weapon you've got left is the kitchen.

## The Story

The dogs have come for everything: the land, the honor, the taxes. Pedro isn't interested in paying up or backing down. Round after round, he ties on an apron, draws his ingredients, and fights back the only way a samurai cat with a knack for cooking knows how — by plating dishes so good they drive the invaders out.

## Gameplay

Pedro No Quiere Pagar Impuestos is a card-based roguelite in the spirit of solitaire deck-builders: each round you draw a hand of ingredient cards and combine them into the best dish you can before the round's score target runs out.

- **Ingredient cards** — every card is a **vegetable**, **protein**, or **carbohydrate**. Select a combination from your hand to cook with.
- **Dish combos** — the right mix of ingredients turns into a named dish (Hakusai No Ohitashi, Kuroge Wagyu Yakiniku, Sukiyaki Osaka-Style, and more), each worth its own score.
- **Hit the target** — every round has a score target that climbs the further you get. Cook a big enough dish to clear it and move on; fall short and it's game over.
- **Redraws** — don't like your hand? Swap out selected cards for fresh ones — you get a limited number of replacements per round.
- **The Store** — spend the money you earn between rounds on condiments and allies to boost your cooking: Soy Sauce, Ketchup, Wasabi, and a couple of very good boys — er, cats — like Catdrick Lamar and Shao Cat.
- **Score multipliers** — stack buffs to multiply your dish's value and blow past the target.

## Controls

Fully mouse-driven:

| Action | Input |
|---|---|
| Select / deselect a card | Left Click |
| Confirm your hand | Left Click (Confirm button) |
| Buy items in the Store | Left Click |

## Built With

- **Engine:** Unity 6 (6000.0.43f1)
- **Render Pipeline:** Universal Render Pipeline (URP), 2D
- **UI:** Unity UI (uGUI) + TextMesh Pro
- **Version Control:** Unity Version Control (Plastic SCM)

## Getting Started

1. Clone the repo:
   ```bash
   git clone https://github.com/max-lopzzz/pedro-no-quiere-pagar-impuestos.git
   ```
2. Open the project folder in **Unity Hub** using Unity `6000.0.43f1` (or newer).
3. Open `Assets/Scenes/Intro.unity` (or `Menu.unity` to skip straight to the menu).
4. Hit Play.

## Project Structure

```
Assets/
├── Scripts/
│   ├── Juego/          # Core game loop — cards, scoring, combos, rounds
│   ├── StoreSystem/     # Shop, inventory, and save data
│   └── Main Menu/      # Menu, settings, and scene navigation
├── Scenes/              # Intro, Menu, Main (game), Store
├── Sprites/              # Card art and backgrounds
└── Sound/               # SFX and music
```

## Roadmap / Ideas

- [ ] More dish combos and ingredient variety
- [ ] Additional shop items and passive buffs
- [ ] Balance pass on difficulty scaling per round
- [ ] Polished intro/story cutscenes

## License

*Add your license of choice here.*

## Credits

Made by [max-lopzzz](https://github.com/max-lopzzz) for Game Jam 2025.
