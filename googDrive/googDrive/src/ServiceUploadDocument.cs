using System;
using Google.Apis.Drive.v2;
using Google.Apis.Drive.v2.Data;

namespace goog.driveUtil
{
    public class ServiceUploadDocument : IServiceUploadDocument
    {
        private File _body;

        public ServiceUploadDocument(File body)
        {
            _body = body;
        }

        public File UploadDocument(DriveService service, String folderName)
        {
           
            _body.Title = "My document";
            _body.Description = "A test document";
            _body.MimeType = "text/plain";
            byte[] byteArray = System.IO.File.ReadAllBytes("document.txt");
            System.IO.MemoryStream stream = new System.IO.MemoryStream(byteArray);


            FilesResource.InsertMediaUpload request = service.Files.Insert(_body, stream, "text/plain");
            request.Upload();

            File file = request.ResponseBody;
            return file;
        }


    }
}