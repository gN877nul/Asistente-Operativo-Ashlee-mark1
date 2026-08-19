using System.Text.Json;
using NAudio.Wave;
using Vosk;

namespace A.S.H.L.E.E._alfa.Voice
{
    internal class VoiceController : IDisposable
    {
        private readonly Model model;
        private readonly VoskRecognizer recognizer;
        private readonly WaveInEvent waveIn;

        public event Action<string>? CommandRecognized;

        public VoiceController(string modelPath, IEnumerable<string> commandWords)
        {
         
            Vosk.Vosk.SetLogLevel(-1);

            if (!Directory.Exists(modelPath))
            {
                throw new DirectoryNotFoundException(
                    $"No se encontró el modelo de voz en: {modelPath}. " +
                    "Descárgalo de https://alphacephei.com/vosk/models y colócalo ahí."
                );
            }

            model = new Model(modelPath);

         
            var words = commandWords.ToList();
            words.Add("[unk]");

            string grammarJson = JsonSerializer.Serialize(words);

            recognizer = new VoskRecognizer(model, 16000.0f, grammarJson);
            recognizer.SetWords(false);

            waveIn = new WaveInEvent
            {
                WaveFormat = new WaveFormat(16000, 1) 
            };
            waveIn.DataAvailable += OnDataAvailable;
        }

        public void Start()
        {
            waveIn.StartRecording();
            Console.WriteLine("[VOZ] Escuchando (Vosk)...");
        }

        public void Stop()
        {
            waveIn.StopRecording();
        }

    
        private static readonly TimeSpan EchoGuard = TimeSpan.FromMilliseconds(400);

        private void OnDataAvailable(object? sender, WaveInEventArgs e)
        {
    
            bool recentlySpoke =
                DateTime.UtcNow - SpeechService.LastSpokeAtUtc < EchoGuard;

            if (SpeechService.IsSpeaking || recentlySpoke)
                return;

            if (!recognizer.AcceptWaveform(e.Buffer, e.BytesRecorded))
                return;

            string text = ExtractText(recognizer.Result());

            if (string.IsNullOrWhiteSpace(text) || text == "[unk]")
                return;

            Console.WriteLine($"[VOZ][DEBUG] Reconocido: \"{text}\"");
            CommandRecognized?.Invoke(text);
        }

        private static string ExtractText(string resultJson)
        {
            using JsonDocument doc = JsonDocument.Parse(resultJson);

            if (doc.RootElement.TryGetProperty("text", out JsonElement textProp))
                return textProp.GetString()?.Trim() ?? string.Empty;

            return string.Empty;
        }

        public void Dispose()
        {
            waveIn.StopRecording();
            waveIn.Dispose();
            recognizer.Dispose();
            model.Dispose();
        }
    }
}
