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

$sql = "SELECT  * FROM GrammarSubject WHERE IsActive = 1 AND InProgress=0 LIMIT 1";
$result = $conn->query($sql);

if ($result->num_rows <= 0) {  
    $sqlUpdate = "UPDATE GrammarSubject SET IsActive = 1 WHERE InProgress=0" ;
    $resultUpdate = $conn->query($sqlUpdate);
    $result = $conn->query($sql);
}


    $sql = "SELECT  * FROM GrammarSubject WHERE IsActive = 1 AND InProgress=0 LIMIT 1";
    $result = $conn->query($sql);


echo "{\"grammar\":[";
if ($result->num_rows > 0) {
  $cnt = 0;
  // output data of each row
  while($row = $result->fetch_assoc()) {
    $cnt += 1;
     echo "\"";
     echo $row["EN"];
     echo "\",";

    echo "\"";
     echo $row["RU"];
     echo "\"";
    
    $sqlUpdate = "UPDATE GrammarSubject SET IsActive = 0 WHERE SubjectID= '" . $row["SubjectID"] . "'" ;
    $resultUpdate = $conn->query($sqlUpdate);
    }
}


echo "]}";
$conn->close();
?>