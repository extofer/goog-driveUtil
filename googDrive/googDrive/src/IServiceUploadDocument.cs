using System;
using Google.Apis.Drive.v2;
using Google.Apis.Drive.v2.Data;

namespace goog.driveUtil
{
    public interface IServiceUploadDocument
    {
        File UploadDocument(DriveService service, String folderName);
    }
}