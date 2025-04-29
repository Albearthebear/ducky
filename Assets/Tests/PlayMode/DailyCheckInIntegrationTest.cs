using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;
using DuckFriend.Data;
using DuckFriend.Services;
using DuckFriend.UI;

public class DailyCheckInIntegrationTest
{
    private GameObject testContainer;
    private GameObject moodCheckPanel;
    private GameObject mainInteractionPanel;
    private UIManager uiManager;
    private DialogueUI dialogueUI;
    private DailyCheckInManager dailyCheckInManager;

    [UnitySetUp]
    public IEnumerator SetUp()
    {
        // Create a container for test objects
        testContainer = new GameObject("TestContainer");

        // Create MoodCheckPanel and MainInteractionPanel
        moodCheckPanel = new GameObject("MoodCheckPanel");
        moodCheckPanel.transform.parent = testContainer.transform;
        moodCheckPanel.SetActive(false);

        mainInteractionPanel = new GameObject("MainInteractionPanel");
        mainInteractionPanel.transform.parent = testContainer.transform;
        mainInteractionPanel.SetActive(false);

        // Create UIManager and assign panels via reflection (simulate inspector assignment)
        GameObject uiManagerObj = new GameObject("UIManager");
        uiManagerObj.transform.parent = testContainer.transform;
        uiManager = uiManagerObj.AddComponent<UIManager>();
        // Manually assign references to private serialized fields
        typeof(UIManager).GetField("moodCheckPanel", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
            .SetValue(uiManager, moodCheckPanel);
        typeof(UIManager).GetField("mainInteractionPanel", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
            .SetValue(uiManager, mainInteractionPanel);

        // Create DialogueUI with a dummy Text component and CanvasGroup
        GameObject dialogueObj = new GameObject("DialogueUI");
        dialogueObj.transform.parent = testContainer.transform;
        dialogueUI = dialogueObj.AddComponent<DialogueUI>();
        dialogueObj.AddComponent<CanvasGroup>();
        Text dialogueText = dialogueObj.AddComponent<Text>();
        // Assign dialogueText to DialogueUI
        typeof(DialogueUI).GetField("dialogueText", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
            .SetValue(dialogueUI, dialogueText);

        // Create DailyCheckInManager and assign uiManager and dialogueUI
        GameObject checkInObj = new GameObject("DailyCheckInManager");
        checkInObj.transform.parent = testContainer.transform;
        dailyCheckInManager = checkInObj.AddComponent<DailyCheckInManager>();
        typeof(DailyCheckInManager).GetField("uiManager", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
            .SetValue(dailyCheckInManager, uiManager);
        typeof(DailyCheckInManager).GetField("dialogueUI", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
            .SetValue(dailyCheckInManager, dialogueUI);

        // Ensure fresh user data (simulate not checked in today)
        UserData freshData = new UserData();
        freshData.lastCheckInDate = System.DateTime.MinValue.ToString("o");
        DataPersistenceService.SaveUserData(freshData);

        // Invoke DailyCheckInManager.Start manually to simulate scene start
        dailyCheckInManager.Start();

        yield return null;
    }

    [UnityTest]
    public IEnumerator DailyCheckInFlowTest()
    {
        // Verify that MoodCheckPanel is active because user has not checked in today
        Assert.IsTrue(moodCheckPanel.activeSelf, "MoodCheckPanel should be active if not checked in today.");
        
        // Perform daily check-in
        dailyCheckInManager.PerformDailyCheckIn();

        // Wait a short time for UI transitions
        yield return new WaitForSeconds(0.1f);

        // Check that MainInteractionPanel is now active
        Assert.IsTrue(mainInteractionPanel.activeSelf, "MainInteractionPanel should be active after check-in.");

        // Load updated user data
        UserData updatedData = DataPersistenceService.LoadUserData();
        System.DateTime lastCheckIn = System.DateTime.Parse(updatedData.lastCheckInDate);
        Assert.AreEqual(System.DateTime.Now.Date, lastCheckIn.Date, "User's last check-in date should be updated to today.");

        // Verify that DialogueUI is active (message is shown)
        Assert.IsTrue(dialogueUI.gameObject.activeSelf, "DialogueUI should be active showing an affirmation message.");

        yield return null;
    }

    [UnityTearDown]
    public IEnumerator TearDown()
    {
        Object.Destroy(testContainer);
        yield return null;
    }
} 