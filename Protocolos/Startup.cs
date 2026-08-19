using A.S.H.L.E.E._alfa.Voice;

namespace A.S.H.L.E.E._alfa.Protocolos
{
    internal class StartupProtocol : IProtocol
    {
        public string Name => "ARMOR_STARTUP";

        public string Description => "Inicializa los sistemas de la armadura";

        public async Task ExecuteAsync()
        {
            Console.WriteLine();
            Console.WriteLine("[ASHLEE] Ejecutando protocolo de activación...");

            await Task.Delay(500);
            Console.WriteLine("[ASHLEE] Inicializando sistema de energía...");
            SpeechService.Say("Sistema de energía en línea.");

            await Task.Delay(500);
            Console.WriteLine("[ASHLEE] Inicializando sensores...");
            SpeechService.Say("Sensores activados.");

            await Task.Delay(500);
            Console.WriteLine("[ASHLEE] Inicializando actuadores...");
            SpeechService.Say("Actuadores en línea.");

            await Task.Delay(500);
            Console.WriteLine("[ASHLEE] Todos los sistemas inicializados.");

            Console.WriteLine();
            Console.WriteLine("[ASHLEE] ARMADURA OPERATIVA.");
        }
    }
}
