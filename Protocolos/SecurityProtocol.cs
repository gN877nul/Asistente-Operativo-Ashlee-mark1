using A.S.H.L.E.E._alfa.Voice;

namespace A.S.H.L.E.E._alfa.Protocolos
{
    internal class SecurityProtocol : IProtocol
    {
        public string Name => "SECURITY";

        public string Description => "Verifica accesos y bloquea el sistema ante amenazas";

        public async Task ExecuteAsync()
        {
            Console.WriteLine();
            Console.WriteLine("[ASHLEE] Ejecutando protocolo de seguridad...");

            await Task.Delay(400);
            Console.WriteLine("[ASHLEE] Verificando identidad del piloto...");
            SpeechService.Say("Verificando identidad del piloto.");

            await Task.Delay(400);
            Console.WriteLine("[ASHLEE] Escaneando accesos no autorizados...");
            SpeechService.Say("Escaneando accesos no autorizados.");

            await Task.Delay(400);
            Console.WriteLine("[ASHLEE] Comprobando integridad del firmware...");
            SpeechService.Say("Comprobando integridad del firmware.");

            await Task.Delay(400);
            Console.WriteLine("[ASHLEE] No se detectaron amenazas.");

            Console.WriteLine();
            Console.WriteLine("[ASHLEE] Sistema seguro. Acceso autorizado.");
        }
    }
}
