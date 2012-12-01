using System;
using Google.Apis.Drive.v2;
using Google.Apis.Drive.v2.Data;


namespace goog.driveUtil
{
    internal class ServiceCreatePublicFolder : IServiceCreatePublicFolder
    {
        private File _body;
        private Permission _permission;

        public ServiceCreatePublicFolder(File body, Permission permission)
        {
            _body = body;
            _permission = permission;
        }

        public File Mkdir(DriveService service, String folderName)
        {
            //_body = new File();
            //_permission = new Permission();


            _body.Title = folderName;
            _body.MimeType = "application/vnd.google-apps.folder";

            File file = service.Files.Insert(_body).Fetch();

            _permission.Value = "";
            _permission.Type = "anyone";
            _permission.Role = "reader";

            service.Permissions.Insert(_permission, file.Id).Fetch();

            return file;
        }
    }
}