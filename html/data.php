<?php
include 'includes/dbconnect.php';

$sql = "SELECT * FROM `Programs`";
$result = mysqli_query($conn, $sql);

if (mysqli_num_rows($result) > 0) {
    // output data of each row
    mysqli_num_rows($result);
    while($row = mysqli_fetch_assoc($result)) {
        echo "id: " . $row["Id"]. " - Program: " . $row["Description"]. "<br>";
    }
} else {
    echo "0 results";
}
?>
<br><br>
<?php
echo "<select name='PcID'>";
while ($row = mysql_fetch_array($result)) {
    echo "<option value='" . $row['PcID'] . "'>" . $row['PcID'] . "</option>";
}
echo "</select>";

mysqli_close($conn);
?>