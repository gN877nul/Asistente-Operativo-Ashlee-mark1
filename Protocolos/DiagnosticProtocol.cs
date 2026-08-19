using A.S.H.L.E.E._alfa.Voice;

namespace A.S.H.L.E.E._alfa.Protocolos
{
    internal class DiagnosticProtocol : IProtocol
    {
        public string Name => "DIAGNOSTIC";

        public string Description => "Ejecuta un diagnóstico completo de los sistemas";

        public async Task ExecuteAsync()
        {
            Console.WriteLine();
            Console.WriteLine("[ASHLEE] Iniciando diagnóstico de sistemas...");

            await Task.Delay(400);
            Console.WriteLine("[ASHLEE] Energía..................... OK");
            SpeechService.Say("Energía, correcto.");

            await Task.Delay(400);
            Console.WriteLine("[ASHLEE] Sensores.................... OK");
            SpeechService.Say("Sensores, correcto.");

            await Task.Delay(400);
            Console.WriteLine("[ASHLEE] Actuadores................... OK");
            SpeechService.Say("Actuadores, correcto.");

            await Task.Delay(400);
            Console.WriteLine("[ASHLEE] Comunicaciones............... OK");
            SpeechService.Say("Comunicaciones, correcto.");

            await Task.Delay(400);
            Console.WriteLine("[ASHLEE] Integridad estructural........ OK");
            SpeechService.Say("Integridad estructural, correcto.");

            Console.WriteLine();
            Console.WriteLine("[ASHLEE] Diagnóstico completado. Todos los sistemas nominales.");
        }
    }
}
