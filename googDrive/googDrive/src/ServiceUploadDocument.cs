using System;
using Google.Apis.Drive.v2;
using Google.Apis.Drive.v2.Data;

namespace goog.DriveUtil
{
    public class ServiceUploadDocument : IServiceUploadDocument
    {
        public File UploadDocument(DriveService service, String folderName)
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
    }
}