<?php
/*
* Save reports from ExceptionReporter.net from POST.
* TODO: Mínimal security, ex. antispam. 
* 
* OPTIONS param ["o"] :
* 1. Send to an email.    
* 2. Send to github bug.
* 3. Save to local DB (mysql, sqlite, etc.).
* 4. Save to local disk directory.
* TODO: combine all options, ex. if t=12 then github and email; t=124 then github, email, disk ; 
*/

  use PHPMailer\PHPMailer\PHPMailer;
	use PHPMailer\PHPMailer\SMTP;
	use PHPMailer\PHPMailer\Exception;

$debug = true;
if($debug){
   ini_set('display_errors', 1);
   ini_set('display_startup_errors', 1);
   error_reporting(E_ALL);
	 ini_set('SMTP','localhost' );
   ini_set('smtp_port', 25);
}

/*******************************************
* Set Global Config */
/* email */
$mailServerHost = "localhost";
$mailServerPort = "25";
$mailServerSSL = false;
$mailServerUsername = "";
$mailServerPassword = "";
$mailServerTo = "javier@javiercanon.com";
$mailServerFrom = "javier@javiercanon.com";
$mailServerSubject = "[BUG REPORT]";

/* github */
$GithubUsernameToken = "xxx";

/* db */

/* disk */
 

/********************************************/

if(!isset($_GET['o']) && !isset($_POST['Report']))
{
	die("Error, bad params.");
}else{

	if($_GET['o'] == "1"){
		/*
		if($debug) {
			echo "SendToEmail()";		
			$from = "someonelse@example.com";
			$headers = "From:" . $from;
			echo mail("someonelse@example.com" ,"testmailfunction" , "OK msg",$headers);
		}
		*/
		SendToEmail();
	}
	
	if($_GET['o'] == "2"){
		if($debug) echo "SendToGithub()";

		 // full api not working in IIS, not tested in otheres webservers like apache file: ERServerGithub.php
		 // https://github.com/KnpLabs/php-github-api

		 // simple php request using http and user token authorization 
		 SendToGithub();
	}

}

//-------------------------------------------------------------------------------

function SendToEmail(){

	Global $debug,
	$mailServerHost,
	$mailServerPort,
	$mailServerSSL,
	$mailServerUsername,
	$mailServerPassword,
	$mailServerTo,
	$mailServerFrom,
	$mailServerSubject
	;

	// echo (extension_loaded('openssl')?'<h1>SSL loaded</h1>':'<h2>SSL not loaded</h2>')."\n";

	require 'libraries/phpmailer-6.0.5/src/PHPMailer.php';
	require 'libraries/phpmailer-6.0.5/src/SMTP.php';
	require 'libraries/phpmailer-6.0.5/src/Exception.php';

	// TODO: basic security, only can send one message per session or (x) minutes
	// session_start();
	
	// SMTP needs accurate times, and the PHP time zone MUST be set
	// This should be done in your php.ini, but this is how to do it if you don't have access to that
	// date_default_timezone_set('America/Bogota');

	// set to...
	$err = false;
	$to = $mailServerTo;
	
	if($to == ''){
    die("Error, BAD CONFIG: NO DOMAIN EMAIL FOUND");
	}
	
	if (array_key_exists('Report', $_POST)) {
			$msg = '';
			$email = '';
			
			//Apply some basic validation and filtering to the subject
			/*
			if (array_key_exists('subject', $_POST)) {
					$subject = substr(strip_tags($_POST['subject']), 0, 255);
			} else {
					$subject = 'No subject';
			}
			*/
			$subject = $mailServerSubject;

			//Apply some basic validation and filtering to the query
			if (array_key_exists('Report', $_POST)) {
					//Limit length and strip HTML tags
					$query = substr(strip_tags($_POST['Report']), 0, 16384);
			} else {
					$query = '';
					$msg = 'Error, No message!';
					$err = true;
			}
			
			//Apply some basic validation and filtering to the name
			/*
			if (array_key_exists('name', $_POST)) {
					//Limit length and strip HTML tags
					$name = substr(strip_tags($_POST['name']), 0, 255);
			} else {
					$name = '';
			}
			*/

			//Validate to address
			//Never allow arbitrary input for the 'to' address as it will turn your form into a spam gateway!
			//Substitute appropriate addresses from your own domain, or simply use a single, fixed address
			/*
			if (array_key_exists('to', $_POST) and in_array($_POST['to'], ['sales', 'support', 'accounts'])) {
			$to = $_POST['to'] . '@example.com';
			} else {
			$to = 'support@example.com';
			}
			 */

			//Make sure the address they provided is valid before trying to use it
			/*
			if (array_key_exists('email', $_POST) and PHPMailer::validateAddress($_POST['email'])) {
					$email = $_POST['email'];
			} else {
					$msg .= "Error: invalid email address provided";
					$err = true;
			}
			*/


			if (!$err) {

					$mail = new PHPMailer(true);
					if($debug) $mail->SMTPDebug = 2;

					$mail->isSMTP();

					if($mailServerSSL){
						$mail->SMTPAuth = $mailServerSSL;
						$mail->SMTPSecure = "tls";
					}

					$mail->SMTPOptions = array(
											'ssl' => array(
													'verify_peer' => false,
													'verify_peer_name' => false,
													'allow_self_signed' => true
											)
									);

					$mail->Host = $mailServerHost;
					$mail->Port = $mailServerPort;
					$mail->Username = $mailServerUsername;
					$mail->Password = $mailServerPassword;

					$mail->CharSet = 'utf-8';
					//It's important not to use the submitter's address as the from address as it's forgery,
					//which will cause your messages to fail SPF checks.
					//Use an address in your own domain as the from address, put the submitter's address in a reply-to
					$mail->setFrom($mailServerFrom,$mailServerFrom);
					$mail->addAddress($to);
					//$mail->addBCC("@");
					//$mail->addReplyTo($email, $name);
					$mail->Subject = $mailServerSubject;

					$mail->Body = $_POST['Report'];

					/*
					foreach($_POST as $key => $value) {
							//echo "POST parameter '$key' has '$value'";
							$mail->Body .= "$key: $value\n\n";
					}
					*/

					if (!$mail->send()) {
							$msg .= "Mailer Error: " . $mail->ErrorInfo;
					} else {
							$msg .= "Mail Sent.";
					}

					echo $msg;
			}
	}		
} // end email


//-------------------------------------------------------------------------------


function SendToGithub(){
  // https://developer.github.com/v3/issues/#create-an-issue

  // test token access:
	//$repos = github_request('https://api.github.com/user/repos?page=1&per_page=100');
	//print_r($repos);
	//$events = github_request('https://api.github.com/users/:username/events?page=1&per_page=5');
	//$events = github_request('https://api.github.com/users/:username/events/public?page=1&per_page=5');
	//print_r($events);
	//$feeds = github_request('https://api.github.com/feeds/:username?page=1&per_page=5');
	//print_r($feeds);

	if(!isset($_GET['AppVersion']) && !isset($_POST['Report']))
	{
		die("Error, bad params.");
	}

	//TODO: you can add labels, milestone, assignees
	/*
	{
  "title": "Found a bug",
  "body": "I'm having a problem with this.",
  "assignees": [
    "octocat"
  ],
  "milestone": 1,
  "labels": [
    "bug"
  ]
}	*/

	$named_array = array(
						"title" => ('New issue '.$_POST['AppVersion']) 
 					 ,"body" => ($_POST['Report'])  

			);

	$data = createJsonBody($named_array);
	print_r($data);

	$newIssue = github_request(GetGithubIssueURL("JavierCanon", "ExceptionReporter.NET"), $data);
	print_r($newIssue);

} // end github

// Github REST API example
function github_request($url, $data){
	  Global $GithubUsernameToken;
	
    $ch = curl_init();
    
    // Basic Authentication with token
    // https://developer.github.com/v3/auth/
    // https://github.com/blog/1509-personal-api-tokens
    // https://github.com/settings/tokens
    $access = $GithubUsernameToken;
    curl_setopt($ch, CURLOPT_USERPWD, $access);
		
    curl_setopt($ch, CURLOPT_URL, $url);

    curl_setopt($ch, CURLOPT_HTTPHEADER, array(
		 'Accept: application/vnd.github.v3+json'
		,'Content-Type: application/json')
		);

    curl_setopt($ch, CURLOPT_USERAGENT, 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/60.0.3112.113 Safari/537.36');
    curl_setopt($ch, CURLOPT_HEADER, 0);

		curl_setopt($ch, CURLOPT_POSTFIELDS, $data);
		
    curl_setopt($ch, CURLOPT_TIMEOUT, 30);
    curl_setopt($ch, CURLOPT_RETURNTRANSFER, true);
    curl_setopt($ch, CURLOPT_SSL_VERIFYHOST, 0);
    curl_setopt($ch, CURLOPT_SSL_VERIFYPEER, 0);
    
		$output = curl_exec($ch);
    curl_close($ch);
    $result = json_decode(trim($output), true);
    return $result;
}

function GetGithubIssueURL($username, $repository){

   return ('https://api.github.com/repos/'.rawurlencode($username).'/'.rawurlencode($repository).'/issues');
}

function createJsonBody(array $parameters)
{
        return (count($parameters) === 0) ? null : json_encode($parameters, empty($parameters) ? JSON_FORCE_OBJECT : 0);
}


//-------------------------------------------------------------------------------

function SendToDB(){

} // end db

//-------------------------------------------------------------------------------
function SendToDisk(){
} // end disk

//-------------------------------------------------------------------------------
?>