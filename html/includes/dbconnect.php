<html>
<body>
<?php
// Define constants
define("MYSQL_ADDRESS", "localhost"); // IP Address of the MySQL server
define("MYSQL_USERNAME", "phpaccess"); // Username to log into MySQL server
define("MYSQL_PASS", "xteUPG8wqQZ8yKzm"); // Password for phpaccess MySQL account (randomly generated when user was created)
define("MYSQL_DB", "project"); // Database to be used (currently the 'project' DB)

//Initialise DB connection using above variables
$conn = mysqli_connect(MYSQL_ADDRESS, MYSQL_USERNAME, MYSQL_PASS, MYSQL_DB);
// Check if DB connection was successful, if not kill the rest of the page load and display error
if (!$conn) {
    die("MySQL Connection failed: " . mysqli_connect_error());
}
?>

</body>
</html>
