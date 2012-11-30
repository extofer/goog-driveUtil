goog-driveUtil
==============

This is a utility tool for Google Drive to create shared folders to host HTML pages and upload files

==============

How to host a web page on Google Drive
Are you aware that you can now host web pages on your Google Drive. This is a great revelation to be able to test HTML, CSS and JavaScript driven sites. When I heard this, I gave it a whirl, only, there was a bit more involved to be able to do this. 
According to the Google Drive SDK https://developers.google.com/drive/publish-site, you need to have a public folder. Publishing the public folder should return an ID to form the “webViewLink” that you would use as a URL. I found it best, and probably recommended, to do this programmatically. I followed the “Five minute quick start” https://developers.google.com/drive/quickstart-cs and I later found, I was not the only one confused by is. 
Let’s simplify this
I wrote an application and put it on my GitHub https://github.com/extofer/goog-driveUtil. Currently (version 0.0.1) , here is what it does. At the bin of the source, you find the binaries, and you call the goog.exe application and pass it an argument “-mkdir” and the name of the public directory you want. The command will look like this
\bin> goog –mkdir wwwTestDir01
Your browser will open the API set up will request for permission to upload to your Google Drive. When you grant the access, by clicking the “Allow button”, you will get an authorization code to copy and paste onto the console when is ask for the code. Once you have done so, the application will create the public folder, display the link on the console, and will create a shortcut on your desktop. When you have completed this, create of upload an “index.htm” file into that directory from your Google Drive web. This feature is being worked on, and could use some input. For now, it’s best to use your Google Drive Client or Google Drive Web. 
How do you do this?
First step (assuming you have Google Drive) is you must enable your Drive API. By going to your Google APIs Console https://code.google.com/apis/console, and create an API Project, I called this one “goog-drive”. At this point select the Services tab and enable Drive API. 
Then click on API Access from the menus on the side, and click on “Create an OAuth 2.0 client ID”
Enter a product name and hit next, on Client ID Settings, choose “Installed Application” and on “Installed type” select “Other”
Now you will get a Client ID and a Client secreted


Install the Google API .Net Client

