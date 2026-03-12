using CyberQuizGrupp1.BLL.Interfaces;
using CyberQuizGrupp1.BLL.Options;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Windows.Markup;

namespace CyberQuizGrupp1.BLL.Services
{
    //denna klass använder en HTTP-klient för att kommunicera med ollama AI, ärven av IAiCoachClient och måste implementera metoden GetCoachingTextAsync från interfacet
    public class OllamaCoachClient : IAiCoachClient
    {   
        private readonly HttpClient _httpClient;
        private readonly OllamaOptions _options;

        //DI
        //OllamaOptions är en klasstyp som beskriver vad vi behöver, tex baseurl, model osv. appsettings.json innehåller värdena. via IOptions<OllamaOptions> skcikas värdena från appsettings.json in hit 
        //options är parametern för de värdena
        public OllamaCoachClient(HttpClient httpClient, IOptions<OllamaOptions> options)
        {
            _httpClient = httpClient;
            _options = options.Value; //här får vi värdena från appsettings.json

            _httpClient.BaseAddress = new Uri(_options.BaseUrl);
        }

        public async Task<string> GetCoachingTextAsync(string prompt)
        {
            var requestBody = new
            {
                model = _options.Model,
                prompt = prompt,
                stream = false,
                options = new { temperature = 0.3 } //temperature sätter på vilket sätt ain ska svara. låg temperatrue gör att ain svarar mer faktabaserad och mindre kreativ. hög temperature innebär att ain svara, mindre faktabaserat och mer kreativt/slumpmässigt 
            };

            var response = await _httpClient.PostAsJsonAsync("generate", requestBody);

            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();

                throw new Exception($"Ollama request failed: {response.StatusCode} - {errorContent}");
            }

            using var stream = await response.Content.ReadAsStreamAsync();

            using var document = await JsonDocument.ParseAsync(stream);

            if (document.RootElement.TryGetProperty("response", out var responseElement))
            {
                var text = responseElement.GetString();

                if (!string.IsNullOrWhiteSpace(text))
                {
                    return text;
                }
            }

            throw new Exception("Ollama response did not contain readable text.");
        }
    }
}

