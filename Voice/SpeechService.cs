using System.Speech.Synthesis;
using System.Threading;

namespace A.S.H.L.E.E._alfa.Voice
{
    // Servicio único (estático) de voz de salida. Cualquier clase del
    // proyecto puede llamar a SpeechService.Say(...) sin tener que crear
    // ni gestionar su propio SpeechSynthesizer.
    internal static class SpeechService
    {
        private static readonly SpeechSynthesizer synthesizer = new SpeechSynthesizer();

        private static int pendingSpeechCount = 0;

        static SpeechService()
        {
            synthesizer.SetOutputToDefaultAudioDevice();
            synthesizer.Rate = 1;    // -10 (lento) a 10 (rápido)
            synthesizer.Volume = 100; // 0 a 100

            synthesizer.SpeakStarted += (s, e) =>
                Interlocked.Increment(ref pendingSpeechCount);

            synthesizer.SpeakCompleted += (s, e) =>
            {
                Interlocked.Decrement(ref pendingSpeechCount);
                LastSpokeAtUtc = DateTime.UtcNow;
            };

            TrySelectSpanishVoice();
        }

        // true mientras ASHLEE está diciendo algo (o tiene frases en cola).
        // El reconocedor de voz debe ignorar el micrófono mientras esto sea true,
        // para no escucharse a sí misma por las bocinas y entrar en bucle.
        public static bool IsSpeaking => pendingSpeechCount > 0;

        // Momento en que terminó de hablar por última vez. Se usa para dar
        // un pequeño margen extra después de hablar (eco/reverberación).
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

        // Encola el texto para decirlo sin bloquear al que llama (uso normal).
        public static void Say(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return;

            synthesizer.SpeakAsync(text);
        }

        // Versión bloqueante: espera a que termine de hablar antes de
        // continuar. Se usa solo para el último mensaje antes de apagar,
        // para que no se corte a media frase cuando el proceso termina.
        public static void SaySync(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return;

            synthesizer.Speak(text);
        }
    }
}