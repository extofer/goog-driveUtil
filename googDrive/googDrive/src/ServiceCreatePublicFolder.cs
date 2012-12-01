using System;
using Google.Apis.Drive.v2;
using Google.Apis.Drive.v2.Data;


namespace goog.DriveUtil
{
    internal class ServiceCreatePublicFolder : IServiceCreatePublicFolder
    {
        public File Mkdir(DriveService service, String folderName)
        {
            File body = new File();
            Permission permission = new Permission();


            body.Title = folderName;
            body.MimeType = "application/vnd.google-apps.folder";

            File file = service.Files.Insert(body).Fetch();

            permission.Value = "";
            permission.Type = "anyone";
            permission.Role = "reader";

            service.Permissions.Insert(permission, file.Id).Fetch();

            return file;
        }
    }
}