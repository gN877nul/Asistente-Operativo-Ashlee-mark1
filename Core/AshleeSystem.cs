using A.S.H.L.E.E._alfa.Protocolos;
using A.S.H.L.E.E._alfa.Commands;
using A.S.H.L.E.E._alfa.Voice;

namespace A.S.H.L.E.E._alfa.Core
{
    internal class AshleeSystem
    {
        public string Name { get; private set; }

        public bool IsOnline { get; private set; }

        private readonly ProtocolManager protocolManager;
        private readonly CommandProcessor commandProcessor;
        private readonly VoiceController voiceController;

        // Los 8 comandos que ASHLEE reconoce, ya sea por teclado o por voz.
        private static readonly string[] KnownCommands =
        {
            "hola",
            "estado",
            "iniciar",
            "diagnostico",
            "seguridad",
            "protocolos",
            "ayuda",
            "apagar"
        };

        public AshleeSystem()
        {
            Name = "Ashlee";
            IsOnline = false;

            protocolManager = new ProtocolManager();
            commandProcessor = new CommandProcessor(protocolManager, Name);

            string modelPath = Path.Combine(
                AppContext.BaseDirectory, "Model", "vosk-model-small-es-0.42"
            );

            // Vosk reconoce directamente cualquiera de los 8 comandos.
            voiceController = new VoiceController(modelPath, KnownCommands);
            voiceController.CommandRecognized += OnVoiceCommandRecognized;
        }

        public async Task InitializeAsync()
        {
            Console.WriteLine("Inicializando ASHLEE...");

            await Task.Delay(1000);

            IsOnline = true;

            Console.WriteLine("ASHLEE ONLINE.");
            
        }

        public async Task StartAsync()
        {
            Console.WriteLine();
            Console.WriteLine($"{Name}: Sistemas preparados.");
            Console.WriteLine($"{Name}: Esperando comandos (teclado o voz).");

            voiceController.Start();

            while (IsOnline)
            {
                Console.Write("> ");

                string? input = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(input))
                    continue;

                bool shouldShutDown = await commandProcessor.ProcessAsync(input);

                if (shouldShutDown)
                    IsOnline = false;
            }

            voiceController.Stop();
        }

        // Se ejecuta en un hilo del reconocedor de voz, en segundo plano.
        // Usamos "async void" a propósito: es un manejador de evento, no
        // hay quien pueda "esperar" (await) su resultado.
        private async void OnVoiceCommandRecognized(string command)
        {
            Console.WriteLine($"[VOZ] Ejecutando: \"{command}\"");

            bool shouldShutDown = await commandProcessor.ProcessAsync(command);

            if (shouldShutDown)
                IsOnline = false;

            Console.Write("> ");
        }
    }
}
