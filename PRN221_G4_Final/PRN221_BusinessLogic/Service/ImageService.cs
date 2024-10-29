using Microsoft.AspNetCore.Http;
using PRN221_BusinessLogic.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN221_BusinessLogic.Service
{
    public class ImageService : IImageService
    {
        private readonly FirebaseConfig _firebaseConfig;

        public ImageService(FirebaseConfig firebaseConfig)
        {
            _firebaseConfig = firebaseConfig;
        }

        public async Task<int> DeleteImageAsync(string fileUrl)
        {
             return await _firebaseConfig.DeleteImageFromFirebase(fileUrl);
        }

        public async Task<string> UploadImageAsync(IFormFile fileImage)
        {
            return await _firebaseConfig.UploadToFirebase(fileImage);
        }
    }
}
