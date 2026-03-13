using System;
using System.Collections.Generic;
using System.Text;

namespace CyberQuizGrupp1.BLL.Options
{
    // konfigurations inställningar för Ollama AI
    public class OllamaOptions
    {
        public string BaseUrl { get; set; } = "http://localhost:11434/api/";
        public string Model { get; set; } = "gemma3";
    }
}
