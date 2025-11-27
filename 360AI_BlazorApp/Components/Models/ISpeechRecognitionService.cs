using Microsoft.JSInterop;
using System;
using System.ComponentModel;

namespace _360AI_BlazorApp.Components.Models
{
    public interface ISpeechRecognitionService : IAsyncDisposable
    {
        Task CancelSpeechRecognitionAsync(bool isAborted);

        Task<IDisposable> RecognizeSpeechAsync(
            string language,
            Func<string, Task> onRecognized,
            Func<SpeechRecognitionErrorEvent, Task>? onError = null,
            Func<Task>? onStarted = null,
            Func<Task>? onEnded = null);
    }
}
