using A.S.H.L.E.E._alfa.Protocolos;
using A.S.H.L.E.E._alfa.Voice;

namespace A.S.H.L.E.E._alfa.Commands
{
    internal class CommandProcessor
    {
        private readonly ProtocolManager protocolManager;
        private readonly string systemName;

        public CommandProcessor(ProtocolManager protocolManager, string systemName)
        {
            this.protocolManager = protocolManager;
            this.systemName = systemName;
        }

        // Devuelve true si el comando implica apagar el sistema
        // (para que quien llama sepa si debe salir del bucle principal).
        public async Task<bool> ProcessAsync(string input)
        {
            string command = input.Trim().ToLower();

            switch (command)
            {
                case "hola":
                    Console.WriteLine($"{systemName}: Buenos días.");
                    SpeechService.Say("Buenos días Jefe");
                    return false;

                case "estado":
                    ShowStatus();
                    return false;

                case "iniciar":
                    SpeechService.Say("Iniciando protocolo de encendido.");
                    await protocolManager.ExecuteAsync("ARMOR_STARTUP");
                    SpeechService.Say("Mark dos. En linea. Bienvenido Santiago Navarro");
                    return false;

                case "diagnostico":
                    SpeechService.Say("Iniciando diagnóstico de sistemas.");
                    await protocolManager.ExecuteAsync("DIAGNOSTIC");
                    SpeechService.Say("Diagnóstico completado. Todo en orden.");
                    return false;

                case "seguridad":
                    SpeechService.Say("Ejecutando protocolo de seguridad.");
                    await protocolManager.ExecuteAsync("SECURITY");
                    SpeechService.Say("Sistema seguro. Acceso autorizado. Bienvenido. Jefe!");
                    return false;

                case "protocolos":
                    protocolManager.ListProtocols();
                    SpeechService.Say(
                        $"Tengo {protocolManager.Count} protocolos disponibles."
                    );
                    return false;

                case "ayuda":
                    ShowHelp();
                    SpeechService.Say("Te mostré la lista de comandos disponibles.");
                    return false;

                case "apagar":
                    Console.WriteLine($"{systemName}: Apagando sistema...");
                    // Bloqueante a propósito: si no, el proceso puede
                    // cerrarse antes de que termine de decir la frase.
                    SpeechService.SaySync("Apagando sistema. Hasta pronto.");
                    return true;

                default:
                    Console.WriteLine(
                        $"{systemName}: No he podido interpretar el comando \"{command}\"."
                    );
                    SpeechService.Say("No entendí ese comando.");
                    return false;
            }
        }

        private void ShowStatus()
        {
            Console.WriteLine();
            Console.WriteLine("ESTADO DEL SISTEMA");
            Console.WriteLine("-------------------");
            Console.WriteLine($"Nombre.............. {systemName}");
            Console.WriteLine("Estado.............. Operativo");
            Console.WriteLine($"Protocolos cargados: {protocolManager.Count}");
            Console.WriteLine();

            SpeechService.Say(
                $"Sistema operativo. Tengo {protocolManager.Count} protocolos cargados."
            );
        }

        private void ShowHelp()
        {
            Console.WriteLine();
            Console.WriteLine("COMANDOS DISPONIBLES");
            Console.WriteLine("--------------------");
            Console.WriteLine("hola          - saludo");
            Console.WriteLine("estado        - muestra el estado del sistema");
            Console.WriteLine("iniciar       - ejecuta el protocolo de encendido de la armadura");
            Console.WriteLine("diagnostico   - ejecuta un chequeo completo de sistemas");
            Console.WriteLine("seguridad     - ejecuta el protocolo de seguridad");
            Console.WriteLine("protocolos    - lista los protocolos disponibles");
            Console.WriteLine("ayuda         - muestra esta lista");
            Console.WriteLine("apagar        - apaga el sistema");
            Console.WriteLine();
        }
    }
}
