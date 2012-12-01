using System;
using System.Configuration;
using Google.Apis.Authentication.OAuth2;
using Google.Apis.Authentication.OAuth2.DotNetOpenAuth;
using Google.Apis.Drive.v2;
using Google.Apis.Drive.v2.Data;
using Google.Apis.Urlshortener.v1;


namespace goog.driveUtil
{
    internal class GoogCommand
    {
        private static void Main(string[] args)
        {
            UtilArguments utilArguments = new UtilArguments(args);
            UtilAuthorization authorization = new UtilAuthorization();
            File file = new File();

            
            string clientID = ConfigurationManager.AppSettings["clientID"];
            string clientSecret = ConfigurationManager.AppSettings["clientSecret"];

            var provider = new NativeApplicationClient(GoogleAuthenticationServer.Description, clientID, clientSecret);
            var auth = new OAuth2Authenticator<NativeApplicationClient>(provider, authorization.GetAuthorization);
            var service = new DriveService(auth);


            if (utilArguments["pub"] != null)
            {
                IServiceUploadDocument uploadDocument = new ServiceUploadDocument(new File());

                file = uploadDocument.UploadDocument(service, @"www2net");

                if (utilArguments["type"] != null)
                {
                    file = uploadDocument.UploadDocument(service, @"www2net");
                }
            }


            if (utilArguments["mkdir"] != null)
            {
                IServiceCreatePublicFolder serviceCreatePublicFolder = new ServiceCreatePublicFolder(new File(), new Permission());

                string mkdir = utilArguments["mkdir"];
                Console.WriteLine("Creating Public Shared Folder" + mkdir);
                file = serviceCreatePublicFolder.Mkdir(service, mkdir);

                IServiceUrlShortner serviceUrlShortner = new ServiceUrlShortner(new UrlshortenerService());
                string uri = @"URL=" + serviceUrlShortner.Shorten("https://googledrive.com/host/" + file.Id);
                
                Console.WriteLine("Writting URL Shortcut to Desktop");
                Console.WriteLine(uri);

                string path = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);

                using (System.IO.StreamWriter streamWriter = new System.IO.StreamWriter(path + "\\" + mkdir + ".url"))
                {
                    streamWriter.WriteLine("[InternetShortcut]");
                    streamWriter.WriteLine(uri);
                    streamWriter.Flush();
                }
            }

            
            Console.WriteLine("File id: " + file.Id);
            Console.WriteLine("Press Enter to end this process.");

            Console.ReadLine();

        }
    }
}