using ProjectSigmoind.BussinesLayer.AI.Interface;
using System.Text;
using System.Text.Json;

namespace ProjectSigmoind.BussinesLayer.AI.Entity {
    public class MentorGPT : IMentorGPT{
        private readonly HttpClient _httpClient;

        public MentorGPT(HttpClient httpClient) { 
            this._httpClient = httpClient; 
        }
        
        public async Task<String> MentorResponse(string message) {
            var content = new StringContent(
                               JsonSerializer.Serialize(new { message }),
                               Encoding.UTF8,
                               "application/json");

            var response = await _httpClient.PostAsync("http://172.18.26.183:5000/chatbot", content);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();

            using var doc = JsonDocument.Parse(json);
            
            //Need TRY-CATCH
            return doc.RootElement.GetProperty("response").GetString();
        }
    }
}
