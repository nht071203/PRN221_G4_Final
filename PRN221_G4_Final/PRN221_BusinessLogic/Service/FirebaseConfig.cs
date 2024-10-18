using Firebase.Storage;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Microsoft.AspNetCore.Http;

namespace PRN221_BusinessLogic.Service
{
    public class FirebaseConfig
    {
        public FirebaseConfig()
        {
            if (FirebaseApp.DefaultInstance == null)
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), "firebase", "prn221-69738-firebase-adminsdk-syn4i-4dee075804.json");
                FirebaseApp.Create(new AppOptions()
                {
                    Credential = GoogleCredential.FromFile(path)
                });
            }
        }

        public async Task<string> UploadToFirebase(IFormFile fileImage)
        {
            var stream = fileImage.OpenReadStream();
            var fileName = $"image/{DateTime.UtcNow.Ticks}_{fileImage.FileName}";


            var storage = new FirebaseStorage(
            "prn221-69738.appspot.com",
            new FirebaseStorageOptions
            {
                AuthTokenAsyncFactory = () => Task.FromResult(""),
                ThrowOnCancel = true
            });

            // Upload file lên Firebase Storage và lấy URL
            var imageUrl = await storage
                .Child(fileName)
                .PutAsync(stream);

            return imageUrl;
        }

        //mai xuan
        public async Task<string> UploadToFirebase(Stream fileStream, string fileName)
        {
            var firebaseFileName = $"image/{DateTime.UtcNow.Ticks}_{fileName}";

            var storage = new FirebaseStorage(
                "prn221-69738.appspot.com",
                new FirebaseStorageOptions
                {
                    AuthTokenAsyncFactory = () => Task.FromResult(""),
                    ThrowOnCancel = true
                });

            // Upload file to Firebase Storage and get the URL
            var imageUrl = await storage
                .Child(firebaseFileName)
                .PutAsync(fileStream);

            return imageUrl;
        }

    }
}
