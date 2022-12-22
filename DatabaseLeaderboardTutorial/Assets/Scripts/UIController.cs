using System;
using System.Collections;
using System.Collections.Generic;

using TMPro;

using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    /// <summary>
    /// This script just controls the UI in the scene, it sort of works as a showcase
    /// as to how you can use the data that gets sent and received in the DatabaseManagement
    /// script.
    /// </summary>
    
    private DatabaseManagement databaseManager;

    [Header("UI References")]
    public TMP_InputField createUsernameInput;
    public Button createUserButton;
    
    public TMP_InputField updateUsernameInput;
    public TMP_InputField updateScoreInput;
    public Button updatePlayerButton;

    public Transform scrollviewContentTransform; //Where the player information prefabs will get spawned in the scrollview
    public GameObject leaderboardPlayerInfoPrefab;

    private void Start() {
        databaseManager = DatabaseManagement.instance; //DatabaseManagement is set up as a singleton in the scene so we can get a quick reference
    }
    
    
    public void CreateNewUser() {
        databaseManager.RegisterNewUser(createUsernameInput.text);
    }

    public void UpdateUser() {
        //Int32.Parse attempts to parse a string into an integer [numbers yano]
        databaseManager.UpdatePlayerScore(updateUsernameInput.text, Int32.Parse(updateScoreInput.text));
    }

    //This section is quite convoluted but it works. The "Refresh" button calls RefreshLeaderboard, which then calls
    //The UpdateLeaderboard coroutine in databaseManager, then that coroutine runs a Unity Event when its done that called
    //UpdateLeaderboard on this script. I had to do it this way because the coroutine couldnt return a list as it's a coroutine,
    //so it was a big jerry rigged.
    public void RefreshLeaderboard() {
        databaseManager.UpdateLeaderboard();
    }

    public void UpdateLeaderboard() {
        //Clear our already spawned player info prefabs
        foreach (Transform player in scrollviewContentTransform) {
            Destroy(player.gameObject);
        }
        
        //Get our list of players from the databaseManager
        List<Player> leaderboardPlayers = databaseManager.leaderboardPlayers;
        
        var placeNum = 1; //Use this increment the players rankings on the board. A for loop could have done this natively, but go fuck yourself.
        foreach (var player in leaderboardPlayers) {
            var content = Instantiate(leaderboardPlayerInfoPrefab, scrollviewContentTransform);
            //Call the script on these prefabs that allows you to pass info in to display
            var controller = content.GetComponent<LeaderboardPlayerInfo>(); 
            controller.InitPlayerInfo(placeNum, player.username, player.score);
            placeNum++;
        }
    }
}
