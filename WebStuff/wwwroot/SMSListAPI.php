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

$sql = "SELECT  RuNumId, RuNum, RuNam, RuGen FROM RuNum2 WHERE IsActive = 1 LIMIT 1";
$result = $conn->query($sql);

if ($result->num_rows <= 0) {  
    $sqlUpdate = "UPDATE RuNam2 SET IsActive = 1" ;
    $resultUpdate = $conn->query($sqlUpdate);
    $result = $conn->query($sql);
}

$sql = "SELECT  RuNumId, RuNum, RuNam, RuGen FROM RuNum2 WHERE IsActive = 1 LIMIT 1";
$result = $conn->query($sql);

echo "{\"sms\":[";
if ($result->num_rows > 0) {
  $cnt = 0;
  // output data of each row
  while($row = $result->fetch_assoc()) {
    $cnt += 1;
    echo "\"" . $row["RuNum"] . "\",";
    echo "\"" . $row["RuNam"] . "\",";
    echo "\"" . $row["RuGen"] . "\"";
    
    $sqlUpdate = "UPDATE RuNum2 SET IsActive = 0 WHERE RuNumID= '" . $row["RuNumId"] . "'" ;
    $resultUpdate = $conn->query($sqlUpdate);
    }
}


echo "]}";
$conn->close();
?>