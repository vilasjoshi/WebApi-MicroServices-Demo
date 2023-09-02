using System.Text;
using Newtonsoft.Json;

public class HttpClientHelper
{
    private readonly HttpClient _httpClient;

    public HttpClientHelper()
    {
        _httpClient = new HttpClient();
    }

    public async Task<TResponse> PostAsync<TRequest, TResponse>(string requestUrl, TRequest requestData)
    { 
        try
        {
            var request = new HttpRequestMessage(HttpMethod.Post, requestUrl);
            var content = new StringContent(JsonConvert.SerializeObject(requestData), null, "application/json");
            request.Content = content;
            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
            Console.WriteLine(await response.Content.ReadAsStringAsync());

            // Check if the request was successful
            if (response.IsSuccessStatusCode)
            {
                // Read the response content
                var jsonResponse = await response.Content.ReadAsStringAsync();

                // Deserialize the JSON response into the specified response type
                var responseData = JsonConvert.DeserializeObject<TResponse>(jsonResponse);

                return responseData;
            }
            else
            {
                // Handle the case where the request to the API failed
                throw new HttpRequestException($"HTTP request failed with status code {response.StatusCode}");
            }
        }
        catch (Exception ex)
        {
            // Handle any exceptions that occur during the HTTP request
            throw new HttpRequestException("An error occurred while making the HTTP request.", ex);
        }
    }
}
