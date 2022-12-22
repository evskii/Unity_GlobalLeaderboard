<?php header('Access-Control-Allow-Origin: *');

    $con = mysqli_connect('localhost', 'root', 'root', 'unityleaderboard');

    //Check connection happened
    if(mysqli_connect_errno()){
        echo "1: Connection failed"; //Error code #1 = connection failed
        exit();
    }

    $username = $_POST["name"];


    //Check if name exists
    $namecheckquery = "SELECT username FROM Leaderboard WHERE username='$username'";

    $namecheck = mysqli_query($con, $namecheckquery) or die("2: Name check query failed"); //Error code #2 = name check query failed

    if(mysqli_num_rows($namecheck) > 0){
        echo "3: Name already exists"; //Error code #3 = Name already exists, cannot register
        exit();
    }

    //add user to the table
    $insertuserquery = "INSERT INTO Leaderboard (username, score) VALUE ('$username', 0)";

    mysqli_query($con, $insertuserquery) or die("4: Insert player query failed"); //Error code # 4 = Insert query failed

    echo("0");
?>