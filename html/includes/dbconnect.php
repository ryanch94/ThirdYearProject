<html>
<body>
<?php
$server = "46.101.9.221"; // IP Address of the MySQL server
$username = "phpaccess"; // Username to log into MySQL server
$password = "xteUPG8wqQZ8yKzm"; // Password for phpaccess MySQL account (randomly generated
$dbname = "project"; // Database to be used (currently the 'project' DB)

//Initialise DB connection using above variables
$conn = mysqli_connect($servername, $username, $password, $dbname);

// Check if DB connection was successful, if not display error
if (!$conn) {
    die("MySQL Connection failed: " . mysqli_connect_error());
}
else {
}
?>

</body>
</html>
