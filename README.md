# Duck Friend

A gentle, no-pressure digital companion app built in Unity 2D.

## Prerequisites
- Unity Editor **2023.x LTS** (install via [Unity Hub](https://unity3d.com/get-unity/download)).
- Git (for cloning the repository).

## Setup & Testing

1. Clone this repository:
   ```bash
   git clone https://your-repo-url.git
   cd ducky
   ```

2. Open the project in Unity Editor:
   - Launch Unity Hub.
   - Click **Add** and select the repository folder.
   - Open the project using Unity 2023.x LTS.

3. Create or open the main scene:
   - In the **Assets/Scenes/** folder, create a new scene named `Main.unity`.
   - Add an empty GameObject, attach the following components (in order):
     1. `UIManager` (assign your panels).
     2. `DailyCheckInManager` (link `UIManager` and `DialogueUI`).
     3. `MoodCheckPanelController` (link check-in button & `DailyCheckInManager`).
     4. `DialogueUI` (add a CanvasGroup and Text child for messages).
   - Save `Main.unity`.

4. Press **Play** in the Unity Editor to verify the daily check-in flow and interactions.

5. Run automated tests:
   - Open **Window → Test Runner → PlayMode**.
   - Click **Run All** to execute the integration test `DailyCheckInIntegrationTest`.

### CLI Test Runner
You can also run tests via command line (requires Unity installation):
```bash
Unity -batchmode -projectPath "$(pwd)" -runTests -testPlatform PlayMode -testResults test-results.xml
```

## Next Steps
- Design and build the Mood Check Panel UI.
- Flesh out the main interaction scene with the duck prefab, UI, and services.
- Continue implementing remaining features as per `docs/todo-duck_friend.md`. 