# Duck Friend: Technical Plan To-Do

## I. Project Setup & Structure
- [ ] Unity 2D Project: Use latest LTS and enable Android & iOS
- [ ] Initialize Git repository & add .gitignore
- [ ] Create folder hierarchy under `Assets/`: Scenes, Sprites, Animations, Prefabs, Scripts/{Core,Controllers,UI,Data,Services}
- [ ] Add documentation files: `docs/todo-duck_friend.md`, `docs/architecture.md`

## II. Core Systems (Managers & Services)
- [x] Implement DataPersistenceService (JSON save/load) and data models: UserData, Inventory, Achievements
- [x] Implement TimeService for daily rollover detection
- [ ] (Optional) Implement NotificationService for local reminders

## III. UI Framework
- [x] Set up single Canvas with child panels: MoodCheckPanel, MainInteractionPanel, EggRewardsPanel, InventoryPanel
- [x] Create UIManager to orchestrate panel transitions

## IV. Duck Character
- [x] Create Duck Prefab with SpriteRenderer & Animator (Idle, Happy, Eating, Petted, Cleaning)
- [x] Implement DuckController.cs handling interactions and exposing events: OnFeed, OnPet, OnClean

## V. Interactions
- [x] Develop FeedInteraction: spawn food prefab, animate feeding, call DuckController.Eat()
- [x] Develop PetInteraction: detect tap, trigger DuckController.Pet()
- [x] Develop CleanInteraction: implement drag "sponge" mechanic, trigger DuckController.Clean()

## VI. Reward System
- [x] Implement ChoreService to track completed actions and generate eggs
- [x] Create Egg Prefab & EggManager for spawning and hatching logic
- [x] Define reward types: CosmeticItem, FriendUnlockToken, DiaryEntry

## VII. Duck Friends & Achievements
- [x] Implement FriendManager to instantiate temporary friend prefabs and track combos
- [x] Implement AchievementManager with data models and unlock logic

## VIII. Dialogue & Affirmations
- [x] Create DialogueService with a list of gentle advice and affirmations
- [ ] Build DialogueUI for floating text bubbles

## IX. Input & Optional Features
- [ ] Enable drag & drop for duck around the screen
- [ ] Build text chat UI (optional voice input later)

## X. Testing
- [ ] Write unit tests (NUnit) for Services & Managers
- [ ] Write Play Mode tests for core interactions
- [ ] Perform device/emulator smoke tests

## XI. Deployment
- [ ] Configure Android & iOS build targets
- [ ] (Later) Set up CI pipeline for Cloud Run backend integration

## Next Steps
- [x] Scaffold Unity project & folder structure
- [x] Implement DataPersistenceService & models
- [x] Build DuckController with idle/pet/feed/clean animations
- [ ] Create MoodCheckPanel UI
- [x] Wire up daily check-in flow 