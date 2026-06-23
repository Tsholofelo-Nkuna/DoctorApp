using System;
using System.Collections.Generic;
using System.Text;

namespace DoctorManagement.MAUI.Helpers
{
    public static class MediaHelper
    {
        public static async Task<(string photoFileName,string photoMimeType,byte[] photoContents)> TakePhoto()
        { 
            if (MediaPicker.Default.IsCaptureSupported)
            {
                // Launches the native OS camera UI
                FileResult photo =  await MediaPicker.Default.CapturePhotoAsync();

                if (photo != null)
                {
                    // Save or process the local file path
                    string localFilePath = Path.Combine(FileSystem.CacheDirectory, photo.FileName);
                    using Stream sourceStream =  await photo.OpenReadAsync();
                    var photoBytes = new byte[sourceStream.Length];
                    await sourceStream.ReadAsync(photoBytes, 0, photoBytes.Length);
                 
                    using FileStream localFileStream = File.OpenWrite(localFilePath);
                    await sourceStream.CopyToAsync(localFileStream);
                    return (photo.FileName, photo.ContentType, photoBytes);
                }
                else
                {
                   return (string.Empty, string.Empty, []);
                }
            }
            else
            {
                return (string.Empty, string.Empty, []);
            }

        }
    }
}
