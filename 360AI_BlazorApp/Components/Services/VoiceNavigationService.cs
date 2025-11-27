using Microsoft.AspNetCore.Components;

namespace _360AI_BlazorApp.Components.Services
{
    public class VoiceNavigationService
    {
        private readonly NavigationManager manager;

        public VoiceNavigationService(NavigationManager NavManager)
        {
            manager = NavManager;

            OnSpeechRecognized += (sender, text) =>
            {
                Console.WriteLine($"Обработанная фраза: {text}");

                if (HandleNavigation(text)) return;
            };
        }

        private bool HandleNavigation(string text)
        {
            var thisText = text.ToLowerInvariant();
            var naviagateTriggerWords = new string[] { "перейди", "переместись", "открой" };

            if (naviagateTriggerWords.Any(trigger => thisText.StartsWith(trigger)))
            {
                var pageName = thisText.Substring(naviagateTriggerWords.FirstOrDefault(trigger => thisText.StartsWith(trigger)).Length).Trim().Trim('.', ' ', '"', '\'');
                switch (pageName)
                {
                    case "главную":
                    case "главная":
                    case "главное":
                    case "домашнюю":
                    case "main":
                    case "мейн":
                    case "домой":
                        manager.NavigateTo("/");
                        return true;
                    default:
                        Console.WriteLine($"Не найденная страница: {pageName}");
                        return false;
                }
            }
            
            Console.WriteLine("Нет ключевого слова!");
            return false;

        }

        public event EventHandler<string> OnSpeechRecognized;

        public void RecognizeSpeech(string text)
        {
            OnSpeechRecognized?.Invoke(this, text);
        }
    }
}
