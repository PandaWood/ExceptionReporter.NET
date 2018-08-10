<?php
$debug = true;
if($debug){
   ini_set('display_errors', 1);
   ini_set('display_startup_errors', 1);
   error_reporting(E_ALL);
}

if(!isset($_GET['o']) && !isset($_POST['Report']))
{
	die("Error, bad params.");
}else{

//TODO: maybe remove all code of library that not needed for create a new issue
	 // cant use (spl_autoload_extensions) Because all files need to be lowercase.php
   // spl_autoload_extensions(".php"); // comma-separated list
   // spl_autoload_register();

/*
// windows 10 64bits IIS 10 bug: only returns first file 
foreach (glob("libraries/php-github-api-2.9.0/lib/Github/*.*") as $filename)
{
     if(file_exists($filename))
     {
         //file exists, we can include it
         require_once $filename;
				 echo 'File ' . $filename . ' included<br />'; 
     }
     else
     {
         echo 'File ' . $filename . ' NOT FOUND!<br />';
     }
}
*/

$files = listdir('libraries/php-github-api-2.9.0/lib/Github/Api/');
//sort($files, SORT_LOCALE_STRING);
foreach ($files as $f) {
    //echo  $f, "\n";
		require_once $f;
} 

$files = listdir('libraries/php-github-api-2.9.0/lib/Github/Exception/');
//sort($files, SORT_LOCALE_STRING);
foreach ($files as $f) {
    //echo  $f, "\n";
		require_once $f;
} 

$files = listdir('libraries/php-github-api-2.9.0/lib/Github/HttpClient/');
//sort($files, SORT_LOCALE_STRING);
foreach ($files as $f) {
    //echo  $f, "\n";
		require_once $f;
} 

require_once 'libraries/php-github-api-2.9.0/lib/Github/Client.php';
require_once 'libraries/php-github-api-2.9.0/lib/Github/ResultPager.php';
require_once 'libraries/php-github-api-2.9.0/lib/Github/ResultPagerInterface.php';

// documentation: https://github.com/KnpLabs/php-github-api/tree/master/doc
// Authentication & Security
// https://github.com/KnpLabs/php-github-api/blob/master/doc/security.md
// https://github.com/settings/tokens

/*
The required value of $password depends on the chosen $method. For Github\Client::AUTH_URL_TOKEN, Github\Client::AUTH_HTTP_TOKEN and Github\Client::JWT methods you should provide the API token in $usernameOrToken variable ($password is omitted in this particular case). For the Github\Client::AUTH_HTTP_PASSWORD, you should provide the password of the account. When using Github\Client::AUTH_URL_CLIENT_ID $usernameOrToken should contain your client ID, and $password should contain client secret.
After executing the $client->authenticate($usernameOrToken, $secret, $method); method using correct credentials, all further requests are done as the given user.
*/
// authenticate
$usernameOrToken = "";
$password = "";


/*
// see Client.php
    Github\Client::AUTH_URL_TOKEN
    Github\Client::AUTH_URL_CLIENT_ID
    Github\Client::AUTH_HTTP_TOKEN
    Github\Client::AUTH_HTTP_PASSWORD
    Github\Client::AUTH_JWT
*/
$method = "AUTH_HTTP_TOKEN"; 
$client = new Github\Client();
$client->authenticate($usernameOrToken, $password, $method);

// create issue
//$client->api('issue')->create('KnpLabs', 'php-github-api-example', array('title' => 'The issue title', 'body' => 'The issue body'));
$client->api('issue')->create('JavierCanon', 'ExceptionReporter.NET', array('title' => 'The issue title Test From PHP', 'body' => 'The issue body, created from PHP script.'));


} // end if


function listdir($dir='.') {
    if (!is_dir($dir)) {
        return false;
    }
   
    $files = array();
    listdiraux($dir, $files);

    return $files;
}

function listdiraux($dir, &$files) {
    $handle = opendir($dir);
    while (($file = readdir($handle)) !== false) {
        if ($file == '.' || $file == '..') {
            continue;
        }
        $filepath = $dir == '.' ? $file : $dir . '/' . $file;
        if (is_link($filepath))
            continue;
        if (is_file($filepath))
            $files[] = $filepath;
        else if (is_dir($filepath))
            listdiraux($filepath, $files);
    }
    closedir($handle);
}




?>