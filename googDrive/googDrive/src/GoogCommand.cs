using System;
using System.Diagnostics;
using DotNetOpenAuth.OAuth2;
using Google.Apis.Authentication.OAuth2;
using Google.Apis.Authentication.OAuth2.DotNetOpenAuth;
using Google.Apis.Drive.v2;
using Google.Apis.Drive.v2.Data;
using Google.Apis.Util;

namespace googDrive.Util
{
    class GoogCommand
    {
        static void Main(string[] args)
        {
            Arguments arguments = new Arguments(args);

            const string clientID = "YOUR_CLIENT_ID";
            const string clientSecret =  "YOUR_CLIENT_SECRET";

            // Register the authenticator and create the service
            var provider = new NativeApplicationClient(GoogleAuthenticationServer.Description, clientID, clientSecret);
            var auth = new OAuth2Authenticator<NativeApplicationClient>(provider, GetAuthorization);
            var service = new DriveService(auth);

            File file = new File();


            if (arguments["pub"] != null)
            {
                file = UploadDocument(service, @"www2net");

                    if (arguments["type"] != null)
                                {
                                    file = UploadDocument(service, @"www2net");
                                }
            }


            if (arguments["mkdir"] != null)
            {
                string mkdir = arguments["mkdir"];
                Console.WriteLine("Creating Public Shared Folder" + mkdir);
                file = CreatePublicFolder(service, mkdir);

                string uri = @"URL=" + "https://googledrive.com/host/" + file.Id;
                Console.WriteLine("Writting URL Shortcut to Desktop");
                Console.WriteLine(uri);

                string deskDir = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);

                using (System.IO.StreamWriter writer = new System.IO.StreamWriter(deskDir + "\\" + mkdir + ".url"))
                {
                    writer.WriteLine("[InternetShortcut]");
                    writer.WriteLine(uri);
                    writer.Flush();
                }

                //UrlShortcutToDesktop("test", uri);

            }



           

            Console.WriteLine("File id: " + file.Id);
            Console.WriteLine("Press Enter to end this process.");
            
            Console.ReadLine();
        }

        private static File UploadDocument(DriveService service, String folderName)
        {
            File body = new File();
            body.Title = "My document";
            body.Description = "A test document";
            body.MimeType = "text/plain";
            byte[] byteArray = System.IO.File.ReadAllBytes("document.txt");
            System.IO.MemoryStream stream = new System.IO.MemoryStream(byteArray);


            FilesResource.InsertMediaUpload request = service.Files.Insert(body, stream, "text/plain");
            request.Upload();

            File file = request.ResponseBody;
            return file;

        }


        private static File CreatePublicFolder(DriveService service, String folderName)
        {
            File body = new File();
            body.Title = folderName;
            body.MimeType = "application/vnd.google-apps.folder";

            File file = service.Files.Insert(body).Fetch();

            Permission permission = new Permission();
            permission.Value = "";
            permission.Type = "anyone";
            permission.Role = "reader";

            service.Permissions.Insert(permission, file.Id).Fetch();

            return file;

        }


        private static IAuthorizationState GetAuthorization(NativeApplicationClient arg)
        {
            // Get the auth URL:
            IAuthorizationState state = new AuthorizationState(scopes: new[] { DriveService.Scopes.Drive.GetStringValue() });
            state.Callback = new Uri(NativeApplicationClient.OutOfBandCallbackUrl);
            Uri authUri = arg.RequestUserAuthorization(state);

            // Request authorization from the user (by opening a browser window):
            Process.Start(authUri.ToString());
            Console.Write("  Authorization Code: ");
            string authCode = Console.ReadLine();
            Console.WriteLine();

            // Retrieve the access token by using the authorization code:
            return arg.ProcessUserAuthorization(authCode, state);
        }
    }
}
