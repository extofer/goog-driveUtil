using System;
using System.Configuration;
using Google.Apis.Authentication.OAuth2;
using Google.Apis.Authentication.OAuth2.DotNetOpenAuth;
using Google.Apis.Drive.v2;
using Google.Apis.Drive.v2.Data;


namespace goog.DriveUtil
{
    internal class GoogCommand
    {
        private static void Main(string[] args)
        {
            UtilArguments utilArguments = new UtilArguments(args);

            string clientID = ConfigurationManager.AppSettings["clientID"];
            string clientSecret = ConfigurationManager.AppSettings["clientSecret"];

            UtilAuthorization authorization = new UtilAuthorization();

            var provider = new NativeApplicationClient(GoogleAuthenticationServer.Description, clientID, clientSecret);
            var auth = new OAuth2Authenticator<NativeApplicationClient>(provider, authorization.GetAuthorization);
            var service = new DriveService(auth);

            File file = new File();


            if (utilArguments["pub"] != null)
            {
                ServiceUploadDocument uploadDocument = new ServiceUploadDocument();

                file = uploadDocument.UploadDocument(service, @"www2net");

                if (utilArguments["type"] != null)
                {
                    file = uploadDocument.UploadDocument(service, @"www2net");
                }
            }


            if (utilArguments["mkdir"] != null)
            {
                ServiceCreatePublicFolder serviceCreatePublicFolder = new ServiceCreatePublicFolder();

                string mkdir = utilArguments["mkdir"];
                Console.WriteLine("Creating Public Shared Folder" + mkdir);
                file = serviceCreatePublicFolder.Mkdir(service, mkdir);

                string uri = @"URL=" + "https://googledrive.com/host/" + file.Id;
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


            //ServiceUrlShortner serviceUrlShortner = new ServiceUrlShortner();
            //Console.WriteLine(serviceUrlShortner.Shorten("test"));


            Console.WriteLine("File id: " + file.Id);
            Console.WriteLine("Press Enter to end this process.");

            Console.ReadLine();
        }
    }
}