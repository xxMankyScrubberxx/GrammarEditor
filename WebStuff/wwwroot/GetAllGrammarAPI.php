<?php
$servername = "";
$username = "";
$password = "";
$dbname = "";
mb_internal_encoding('UTF-8');
mb_http_output('UTF-8');
mb_http_input('UTF-8');

// Create connection
$conn = new mysqli($servername, $username, $password, $dbname);
mysqli_set_charset($conn, "utf8");
// Check connection
if ($conn->connect_error) {
  die("Connection failed: " . $conn->connect_error);
}

$sql = "SELECT  * FROM GrammarTexts";
$result = $conn->query($sql);

if ($result->num_rows <= 0) {  
    $sqlUpdate = "UPDATE GrammarTexts SET IsActive = 1 WHERE AND InProgress=0" ;
    $resultUpdate = $conn->query($sqlUpdate);
    $result = $conn->query($sql);
}



echo "[";
if ($result->num_rows > 0) {
  $cnt = 0;
  // output data of each row
  while($row = $result->fetch_assoc()) {
    if($cnt > 0) {
        echo ",";
    }
    $cnt += 1;
    echo "{";
    echo "\"MSG_ID\":\"" . $row["GrammarID"]. "\",";
    echo "\"MSG\":\"" . $row["MSG"]. "\",";
    
    echo "\"EN\":\"" . $row["EN"]. "\",";

    echo "\"RU\":\"" . $row["RU"]. "\",";

    echo "\"CATEGORY\":\"" . $row["Category"]. "\",";
    echo "\"NOTES\":\"" . $row["Notes"]. "\",";
    echo "\"RU_STATUS\":\"" . $row["RU_STATUS"]. "\",";
    echo "\"EN_STATUS\":\"" . $row["EN_STATUS"]. "\",";
    echo "\"BIBLIOGRAPHY\":\"" . $row["Bibliography"]. "\",";
    echo "\"CATEGORIES\":\"" . $row["Categories"]. "\"";
    echo "}";
    //$sqlUpdate = "UPDATE GrammarTexts SET IsActive = 0 WHERE GrammarID= '" . $row["GrammarID"] . "'" ;
    //$resultUpdate = $conn->query($sqlUpdate);
    }
}


echo "]";
$conn->close();
?>