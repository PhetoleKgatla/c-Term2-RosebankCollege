
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using RaceDay.MVC.Models;

namespace RaceDay.MVC.Services
{
    public class ParticipantProfileApiService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        private readonly JsonSerializerOptions _jsonOptions =
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

        public ParticipantProfileApiService(
            IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        // Creates an API client and attaches the JWT token
        private HttpClient CreateClient(string token)
        {
            var client =
                _httpClientFactory.CreateClient("RaceDayAPI");

            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);

            return client;
        }

        // Gets the logged-in Participant's profile
        public async Task<ParticipantProfileModel?> GetMyProfileAsync(
            string token)
        {
            var client = CreateClient(token);

            var response = await client.GetAsync(
                "api/Participants/me");

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            return await response.Content
                .ReadFromJsonAsync<ParticipantProfileModel>(
                    _jsonOptions);
        }

        // Updates the logged-in Participant's profile
        public async Task<bool> UpdateMyProfileAsync(
            ParticipantProfileModel model,
            string token)
        {
            var client = CreateClient(token);

            var response = await client.PutAsJsonAsync(
                "api/Participants/me",
                model);

            return response.IsSuccessStatusCode;
        }

        // Uploads the Participant's profile picture to Azure
        public async Task<string?> UploadProfilePictureAsync(
            IFormFile file,
            string token)
        {
            if (file == null || file.Length == 0)
            {
                return null;
            }

            // Create an authenticated API client
            var client = CreateClient(token);

            // Prepare the file for uploading
            using var content = new MultipartFormDataContent();

            using var fileContent =
                new StreamContent(file.OpenReadStream());

            fileContent.Headers.ContentType =
                new MediaTypeHeaderValue(file.ContentType);

            content.Add(
                fileContent,
                "file",
                file.FileName);

            // Send the image to the RaceDay API
            var response = await client.PostAsync(
                "api/BlobUploads/profile-picture",
                content);

            // Check if the upload was successful
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            // Read the response from the API
            var responseData =
                await response.Content.ReadFromJsonAsync<JsonElement>();

            // Get the Azure image URL
            if (responseData.TryGetProperty(
                "imageUrl",
                out var imageUrl))
            {
                return imageUrl.GetString();
            }

            return null;
        }
    }
}
