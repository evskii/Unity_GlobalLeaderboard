using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;
using UnityEngine.UI;


public class DatabaseManagement : MonoBehaviour
{
    /// <summary>
    /// This script is the meat of the whole operation. It holds coroutines for each major function
    /// of the database management. We can create new users, update existing users and then get the
    /// information of every user on the leaderboard.
    /// </summary>
    
    //Singleton setup
    public static DatabaseManagement instance;
    private void Awake() {
        instance = this;
    }

    //These methods are used so we dont publicly expose the coroutines that run everything
    public void RegisterNewUser(string username) {
        StartCoroutine(POSTRegisterNewUser(username));
    }

    public void UpdatePlayerScore(string username, int score) {
        StartCoroutine(POSTUpdatePlayerScore(username, score));
    }

    public void UpdateLeaderboard() {
        StartCoroutine(POSTLoadLeaderboard());
    }

    private IEnumerator POSTRegisterNewUser(string username) {
        //Create a form that holds our username in the "name" identifier
        WWWForm form = new WWWForm();
        form.AddField("name", username);

        //Create a new UnityWebRequest that Posts our form to the supplied URL. This URL would be changed to where
        //your php scripts are hosted if you aren't using localhosting.
        UnityWebRequest www = UnityWebRequest.Post("http://localhost/NewUser.php", form);

        yield return www.SendWebRequest(); //Send our request and wait for a return

        //www.downloadHandler stores information that gets sent back from the webrequest, if the first character of what 
        //we get back is 0, we know it worked as we tell our php to return "0" as a successful result.
        if (www.downloadHandler.text[0] == '0') {
            Debug.Log("New user created!");
        } else {
            Debug.LogError("User creation failed. Error #" + www.downloadHandler.text); //We send error text in the php aswell so we can output this into the console.
        }
    }

    //This script works the exact same as above. Create a form, send it to our hosted php file, get results and output.
    private IEnumerator POSTUpdatePlayerScore(string username, int score) {
        WWWForm form = new WWWForm();
        form.AddField("name", username);
        form.AddField("score", score);

        UnityWebRequest www = UnityWebRequest.Post("http://localhost/SaveData.php", form);
        yield return www.SendWebRequest();
        if (www.downloadHandler.text == "0") {
            Debug.Log("Game Saved.");
        } else {
            Debug.Log("Save failed. Error #" + www.downloadHandler.text);
        }
    }

    //The following mess is all about getting information from the database for the leaderboard
    public UnityEvent leaderboardUpdateEvent; //This is a public event we can reference in editor to run when the leaderboard has been updated
    public List<Player> leaderboardPlayers = new List<Player>(); //Stores all of the players in the leaderboard
    private IEnumerator POSTLoadLeaderboard() {
        leaderboardPlayers.Clear(); //Clear our current list
        //Create a web request, no form needed as we using .Get to receive information [into downloadHandler]
        UnityWebRequest www = UnityWebRequest.Get("http://localhost/Leaderboard.php");
        
        yield return www.SendWebRequest();
        
        if (www.downloadHandler.text[0] == '0') {
            //Our php script formats our data into a single string, split in funky ways. The short hand is it goes like this: 
            //0    username_1    score_1    username_2    score_2
            //So we use Split() on tabs to get our info into an array, and excluding 0, every odd number is a username and every
            //even number is the previous usernames score.
            var wwwData = www.downloadHandler.text;
            string[] parsedData = wwwData.Split('\t'); 
            
            //We use the parsed data to create instances of the Player class (below in this script) and store it to our list
            for (int i = 1; i < parsedData.Length; i ++) {
                string username = parsedData[i];
                int score = Int32.Parse(parsedData[i += 1]);
                Player newUser = new Player(username, score); 
                leaderboardPlayers.Add(newUser); //Add user to our list of users on the leaderboard
            }
            
        } else {
            Debug.LogWarning("Error #" + www.downloadHandler.text);
        }
        
        leaderboardPlayers = leaderboardPlayers.OrderByDescending(x => x.score).ToList(); //Sort by descending based on their score (uses System.Linq)
        leaderboardUpdateEvent.Invoke(); //Call this event when we have finished our coroutine
    }
}

public class Player
{
    public string username;
    public int score;

    public Player(string username, int score) {
        this.username = username;
        this.score = score;
    }
}
