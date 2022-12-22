<?php header('Access-Control-Allow-Origin: *');
    $con = mysqli_connect('localhost', 'root', 'root', 'unityleaderboard');

    //Check connection happened
    if(mysqli_connect_errno()){
        echo "1: Connection failed"; //Error code #1 = connection failed
        exit();
    }

    $username = $_POST["name"];
    $newscore = $_POST["score"];

    //Check if name exists
    $namecheckquery = "SELECT username FROM Leaderboard WHERE username='$username '";

    //Double check there is only one user with this name
    $namecheck = mysqli_query($con, $namecheckquery) or die("2: Name check query failed");
    if(mysqli_num_rows($namecheck) != 1){
        echo "5: Either no user with name, or more than one"; //Error code #5 = NO user found or multiple users found
        exit();
    }

    $updatequery = "UPDATE leaderboard SET score = '$newscore' WHERE username = '$username'";
    mysqli_query($con, $updatequery) or die("7: Save query failed"); //error code #7 = UPDATE query failed

    echo "0";
?>