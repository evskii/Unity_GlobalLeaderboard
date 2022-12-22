<?php header('Access-Control-Allow-Origin: *');

    //Connect to the database
    $con = mysqli_connect('localhost', 'root', 'root', 'UnityLeaderboard');

    //Check connection happened
    if(mysqli_connect_errno()){
        echo "1: Connection failed"; //Error code #1 = connection failed
        exit();
    }

    $leaderboardQuery = "SELECT username, score FROM Leaderboard";
    $leaderboardData = mysqli_query($con, $leaderboardQuery);

    if(mysqli_num_rows($leaderboardData) == 0){
        echo "8: No leaderboard data retreived..."; //Error code #5 = NO user found or multiple users found
        exit();
    }

    //Get leaderboard data
    $leaderboardString = "0"; //Set start of string to be 0
    //Loop through all rows of our data
    while($row = mysqli_fetch_array($leaderboardData)){
        $leaderboardString = $leaderboardString."\t".$row['username']."\t".$row['score']; //Concatinate the username and their highscore to this string
    }

    echo $leaderboardString; //Echo out the string of usernames and highscores
?>