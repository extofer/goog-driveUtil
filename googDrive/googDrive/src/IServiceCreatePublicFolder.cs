using System;
using Google.Apis.Drive.v2;
using Google.Apis.Drive.v2.Data;

namespace goog.driveUtil
{
    internal interface IServiceCreatePublicFolder
    {
        File Mkdir(DriveService service, String folderName);
    }
}