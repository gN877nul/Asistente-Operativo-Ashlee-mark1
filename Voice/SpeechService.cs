using System.Speech.Synthesis;
using System.Threading;

namespace A.S.H.L.E.E._alfa.Voice
{

    internal static class SpeechService
    {
        private static readonly SpeechSynthesizer synthesizer = new SpeechSynthesizer();

        private static int pendingSpeechCount = 0;

        static SpeechService()
        {
            synthesizer.SetOutputToDefaultAudioDevice();
            synthesizer.Rate = 1;   
            synthesizer.Volume = 100; 

            synthesizer.SpeakStarted += (s, e) =>
                Interlocked.Increment(ref pendingSpeechCount);

            synthesizer.SpeakCompleted += (s, e) =>
            {
                Interlocked.Decrement(ref pendingSpeechCount);
                LastSpokeAtUtc = DateTime.UtcNow;
            };

            TrySelectSpanishVoice();
        }

       
        public static bool IsSpeaking => pendingSpeechCount > 0;

        public static DateTime LastSpokeAtUtc { get; private set; } = DateTime.MinValue;

        private static void TrySelectSpanishVoice()
        {
            var spanishVoice = synthesizer
                .GetInstalledVoices()
                .Select(v => v.VoiceInfo)
                .FirstOrDefault(v => v.Culture.TwoLetterISOLanguageName == "es");

            if (spanishVoice != null)
            {
                synthesizer.SelectVoice(spanishVoice.Name);
            }
            else
            {
                Console.WriteLine(
                    "[VOZ-SALIDA] Aviso: no hay voz en español instalada, " +
                    "se usará la voz por defecto de Windows."
                );
            }
        }

       
        public static void Say(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return;

            synthesizer.SpeakAsync(text);
        }

    
        public static void SaySync(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return;

            synthesizer.Speak(text);
        }
    }
}
