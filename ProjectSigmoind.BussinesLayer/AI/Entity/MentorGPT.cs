using ProjectSigmoind.BussinesLayer.AI.Interface;
using System.Text;
using System.Text.Json;

namespace ProjectSigmoind.BussinesLayer.AI.Entity {
    public class MentorGPT : IMentorGPT{
        private readonly HttpClient _httpClient;

        public MentorGPT(HttpClient httpClient) { 
            this._httpClient = httpClient; 
        }

        public async Task<string> MentorResponse(string message) {
            var json = JsonSerializer.Serialize(new { question = message });
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("http://192.168.43.153:5000/api/chatbot", content);

            response.EnsureSuccessStatusCode(); 

            var responseContent = await response.Content.ReadAsStringAsync();
            using var doc = JsonDocument.Parse(responseContent);
            return doc.RootElement.GetProperty("answer").GetString();
        }

    }
}
