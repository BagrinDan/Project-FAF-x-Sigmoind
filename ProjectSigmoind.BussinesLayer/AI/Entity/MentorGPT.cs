using ProjectSigmoind.BussinesLayer.AI.Interface;
using System.Text;
using System.Text.Json;

namespace ProjectSigmoind.BussinesLayer.AI.Entity {
    public class MentorGPT : IMentorGPT{
        private readonly HttpClient _httpClient;

        public MentorGPT(HttpClient httpClient) { 
            this._httpClient = httpClient; 
        }

        // Функция ответа МентораGPT
        public async Task<string> MentorResponse(string message) {
            var json = JsonSerializer.Serialize(new { question = message });
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // ПОСТ Запрос
            var response = await _httpClient.PostAsync("http://172.20.10.3:5000/chatbot", content);

            // Проверка на успех
            response.EnsureSuccessStatusCode(); 

            // Чтения ответа
            var responseContent = await response.Content.ReadAsStringAsync();

            // Парсинг строку в объект
            using var doc = JsonDocument.Parse(responseContent);

            // В будущем будет трай-катч дабы ловить исключения 
            return doc.RootElement.GetProperty("answer").GetString();
        }

    }
}
