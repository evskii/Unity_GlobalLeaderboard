using System.Collections;
using System.Collections.Generic;

using TMPro;

using UnityEngine;

public class LeaderboardPlayerInfo : MonoBehaviour
{
    /// <summary>
    /// This script is placed on the prefabs that are spawned onto the leaderboard showing player
    /// information. Pretty basic but handy to use for things like this.
    /// </summary>
    
    //Reference to the prefabs text components
    public TMP_Text rankText;
    public TMP_Text usernameText;
    public TMP_Text scoreText;

    //To be called when loading this object to display info
    public void InitPlayerInfo(int rank, string username, int score) {
        rankText.text = rank.ToString();
        usernameText.text = username;
        scoreText.text = score.ToString();
    }

}
