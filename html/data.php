<?php
include 'includes/dbconnect.php';

$sql = "SELECT * FROM `Programs`";
$result = mysqli_query($conn, $sql);

if (mysqli_num_rows($result) > 0) {
    // output data of each row
    echo mysqli_num_rows($result);
    while($row = mysqli_fetch_assoc($result)) {
        echo "id: " . $row["Id"]. " - Program: " . $row["Description"]. "<br>";
    }
} else {
    echo "0 results";
}

mysqli_close($conn);
?>