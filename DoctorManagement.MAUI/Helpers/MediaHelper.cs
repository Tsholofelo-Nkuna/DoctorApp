using System;
using System.Collections.Generic;
using System.Text;

namespace DoctorManagement.MAUI.Helpers
{
    public static class MediaHelper
    {
        public static void TakePhoto(out string photoFileName, out string photoMimeType, out byte[] photoContents)
        {
            if (MediaPicker.Default.IsCaptureSupported)
            {
                // Launches the native OS camera UI
                FileResult photo =  MediaPicker.Default.CapturePhotoAsync().Result;

                if (photo != null)
                {
                    // Save or process the local file path
                    string localFilePath = Path.Combine(FileSystem.CacheDirectory, photo.FileName);
                    using Stream sourceStream =  photo.OpenReadAsync().Result;
                    var photoBytes = new byte[sourceStream.Length];
                    sourceStream.Read(photoBytes, 0, photoBytes.Length);
                    photoContents = photoBytes;
                    photoFileName = photo.FileName;
                    photoMimeType = photo.ContentType;
                    using FileStream localFileStream = File.OpenWrite(localFilePath);
                    sourceStream.CopyToAsync(localFileStream).Wait();

                }
                else
                {
                    photoFileName = string.Empty;
                    photoMimeType = string.Empty;
                    photoContents = [];
                }
            }
            else
            {
                photoFileName = string.Empty;
                photoMimeType = string.Empty;
                photoContents = [];
            }

        }
    }
}
