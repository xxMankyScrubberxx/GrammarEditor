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

$sql = "SELECT  * FROM GrammarTexts WHERE IsActive = 1 AND InProgress=0 LIMIT 1";
$result = $conn->query($sql);

if ($result->num_rows <= 0) {  
    $sqlUpdate = "UPDATE GrammarTexts SET IsActive = 1 WHERE InProgress=0" ;
    $resultUpdate = $conn->query($sqlUpdate);
    $result = $conn->query($sql);

    $sql = "SELECT  * FROM GrammarTexts WHERE IsActive = 1 AND InProgress=0 LIMIT 1";
    $result = $conn->query($sql);
}

function randString($min, $max){
    return rand($min, $max);
}

function generate($grammar){
    while(strpos($grammar, "[") !== false){
        $start = strpos($grammar, '[', 0);
        $stop = strpos($grammar, ']', 0);
        $options = substr($grammar, $start+1, $stop-$start-1);

        $opts = explode(",", $options);
        $cnt = count($opts);
        $rnd = randString(0, $cnt-1);
        $selection = $opts[$rnd];
        $sLen = strlen($selection);

        if($selection[0] == " "){
            $selection = substr($selection, 1, $sLen);
        }

        $newgrammar = substr($grammar, 0, $start) . $selection . substr($grammar, $stop+1, strlen($grammar));
        // = substr($grammar, 0, $start)// . $selection . substr($grammar, $stop+1, strlen($grammar));
        $grammar = $newgrammar;
   }
    return $grammar;
};



echo "{\"grammar\":[";
if ($result->num_rows > 0) {
  $cnt = 0;
  // output data of each row
  while($row = $result->fetch_assoc()) {
    $cnt += 1;

    $msg =  $row["MSG"];
    $msg = generate($msg );
    echo "\"" . $msg . "\",";
    
    $en=  $row["EN"];
    $en= generate($en);
    echo "\"" . $en. "\",";

    $ru =  $row["RU"];
    $ru = generate($ru);
    echo "\"" . $ru . "\",";

    echo "\"" . $row["Category"]. "\"";
    
    $sqlUpdate = "UPDATE GrammarTexts SET IsActive = 0 WHERE GrammarID= '" . $row["GrammarID"] . "'" ;
    $resultUpdate = $conn->query($sqlUpdate);
    }
}


echo "]}";
$conn->close();
?>