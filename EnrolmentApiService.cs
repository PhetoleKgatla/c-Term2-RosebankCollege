using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using RaceDay.MVC.Models;

namespace RaceDay.MVC.Services
{
    public class EnrolmentApiService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public EnrolmentApiService(
            IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        private HttpClient CreateClient(string token)
        {
            var client = _httpClientFactory
                .CreateClient("RaceDayAPI");

            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue(
                    "Bearer", token);

            return client;
        }

        // Create a new enrolment
        public async Task<(bool Success, string Message)>
            CreateEnrolmentAsync(
                int eventId,
                int categoryId,
                string token)
        {
            try
            {
                var client = CreateClient(token);

                var enrolment = new
                {
                    EventID = eventId,
                    CategoryID = categoryId
                };

                var response = await client.PostAsJsonAsync(
                    "api/Enrolments",
                    enrolment);

                if (response.IsSuccessStatusCode)
                {
                    return (
                        true,
                        "You have successfully enrolled in this event!"
                    );
                }

                var error = await response.Content
                    .ReadAsStringAsync();

                if (response.StatusCode ==
                    System.Net.HttpStatusCode.Conflict)
                {
                    return (
                        false,
                        "You have already enrolled in this event."
                    );
                }

                if (response.StatusCode ==
                    System.Net.HttpStatusCode.Unauthorized)
                {
                    return (
                        false,
                        "Your session has expired. Please log in again."
                    );
                }

                if (response.StatusCode ==
                    System.Net.HttpStatusCode.Forbidden)
                {
                    return (
                        false,
                        "You are not authorised to enrol in this event."
                    );
                }

                return (
                    false,
                    string.IsNullOrWhiteSpace(error)
                        ? "Enrolment failed. Please try again."
                        : error
                );
            }
            catch (Exception ex)
            {
                return (
                    false,
                    "Unable to connect to the API: " + ex.Message
                );
            }
        }

        // Retrieve the logged-in participant's enrolments
        public async Task<List<EnrolmentModel>>
            GetMyEnrolmentsAsync(string token)
        {
            try
            {
                var client = CreateClient(token);

                var response = await client.GetAsync(
                    "api/Enrolments/mine");

                if (response.IsSuccessStatusCode)
                {
                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };

                    return await response.Content
                        .ReadFromJsonAsync<List<EnrolmentModel>>(
                            options
                        ) ?? new List<EnrolmentModel>();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(
                    "Error retrieving enrolments: " + ex.Message
                );
            }

            return new List<EnrolmentModel>();
        }
    }
}