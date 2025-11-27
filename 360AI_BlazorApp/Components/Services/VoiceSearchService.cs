using Microsoft.AspNetCore.Components;

namespace _360AI_BlazorApp.Components.Services
{
    public class VoiceSearchService
    {
        private string text;
        public VoiceSearchService()
        {
            OnSpeechRecognized += (sender, text) =>
            {
                Console.WriteLine($"Обработанная фраза: {text}");
                this.text = text;
            };
        }

        public string HandleSearch()
        {
            return text;
        }

        public event EventHandler<string> OnSpeechRecognized;

        public void RecognizeSpeech(string text)
        {
            OnSpeechRecognized?.Invoke(this, text);
        }
    }
}
