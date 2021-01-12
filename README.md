# FlopBox
Small, wannabe DropBox project

How to test application locally:
-	Start the FlopBox.csproj file in FlopBox folder (after download)
-	Run it with F5 or “Start” button. 
-	Test functionalities with help of a “Postman” or similar apps for sending requests to bottom described endpoints (replace localhost:56942 with your localhost:port IP address).

Endpoints explained:

1.	Create folders and subfolders

Example endpoint:

POST: http://localhost:56942/api/Folders/CreateFolder?folderName=7878787&subfolderName=521000

GOOD RESPONSE: 201 Created
ERROR RESPONSE: 400 BadRequest
 
1st parameter is providing us the name of the folder we are going to create.
2nd parameter is optional, it’s going to give us a subfolder’s name. If there isn’t 2nd parameter, logic will create only “main” folder.

2.	Create new files in Folders

Example endpoint:

POST: http://localhost:56942/api/Files/CreateFile?fileName=yolo&folderName=5000

GOOD RESPONSE: 201 Created
ERROR RESPONSE: 400 BadRequest

1st parameter is file name
2nd parameter is created file’s folder 

3.	Search top 10 Files using “Start with” logic

Example endpoint:

GET: http://localhost:56942/api/Files/Searh?fileName=pero 

RESPONSE: 200 Ok + list of files

1st parameter will be used in “Start with” search for files query.

4.	Delete folders and files

Example endpoint:
 
GET: http://localhost:56942/api/folders/deletefolder?folderName=VsCode

GOOD RESPONSE: 200 Ok
ERROR RESPONSE: 400 BadRequest

1st parameter is providing us name of the folder we are going to delete. We are also going to delete all child folders of the “main” one and all the files in child folders and also in the “main” one.
