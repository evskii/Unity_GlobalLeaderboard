# Unity Global Leaderboard
This Unity project demonstrates how to set up a global leaderboard using a hosted database, PHP and Unity. This method is definitely not the best or safest method to do something like this, however for information like usernames and scores its fine. I would not recommend using this for storing users sensitive/personal information.

### How Does It Work?
There are three main components to this. The first is having a database hosted somewhere. You can do this locally by downloading [MAMP](https://www.mamp.info/en/windows/) and using its built in phpMyAdmin function to create the database, or you can use any online webhosting service to do this, as most come with a database package built in. Secondly, you will want to have several PHP scripts that communicate with your database. These PHP scripts will be hosted alongside your database, allowing them to talk to one another. Lastly you need to set up Unity to communicate with your PHP scripts, which will allow you to add and update users to the database, as well as receive information about it, all done in editor.

### Tutorial
All files are attached to this repo and can be used for whatever you wish. All Unity scripts are commented to explain what is going on, PHP scripts not as much.

##### Database Setup
You can create your own database to use, the supplied database file [DatabaseFile/unityleaderboard.sql](https://github.com/evskii/Unity_GlobalLeaderboard/tree/main/DatabaseFile) matches what the SQL queries are looking for in the PHP scripts so creating your own means also tweaking the PHP scripts. To use the supplied database, just use the "Import" function in whatever database manager you are using and you should be good to go!

##### PHP File Setup
The PHP files needed are supplied in [PHPFiles](https://github.com/evskii/Unity_GlobalLeaderboard/tree/main/PHPFiles) and just need to be hosted wherever your database is. If you are using MAMP just place them in C:\MAMP\htdocs and nothing should need to be changed in Unity to make it work. Some PHP files may need their connection information or SQL queries changed to match your database settings due to mess ups in the import process, however it should be straight-forward enough.

##### Unity Setup
If you are using MAMP and not changing any database settings everything should be set up to run immediately. [DatabaseManagement.cs](https://github.com/evskii/Unity_GlobalLeaderboard/blob/main/DatabaseLeaderboardTutorial/Assets/Scripts/DatabaseManagement.cs) is the core to the Unity side of things. Each method in this has a reference to a URL where its required PHP file should be hosted. If you are using MAMP you should be set up to go, if you are hosting these online then just change the URL to where you have those files hosted. 
