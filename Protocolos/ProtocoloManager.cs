using System.Collections.Generic;

namespace A.S.H.L.E.E._alfa.Protocolos
{
    internal class ProtocolManager
    {
        private readonly Dictionary<string, IProtocol> protocols;

        public ProtocolManager()
        {
            protocols = new Dictionary<string, IProtocol>();

            RegisterProtocol(new StartupProtocol());
            RegisterProtocol(new DiagnosticProtocol());
            RegisterProtocol(new SecurityProtocol());
        }

        public int Count => protocols.Count;

        public void RegisterProtocol(IProtocol protocol)
        {
            protocols[protocol.Name] = protocol;
        }

        public async Task ExecuteAsync(string protocolName)
        {
            if (!protocols.TryGetValue(protocolName, out IProtocol? protocol))
            {
                Console.WriteLine(
                    $"[ASHLEE] Protocolo no encontrado: {protocolName}"
                );

                return;
            }

            Console.WriteLine(
                $"[ASHLEE] Protocolo seleccionado: {protocol.Name}"
            );

            await protocol.ExecuteAsync();
        }

        public void ListProtocols()
        {
            Console.WriteLine();
            Console.WriteLine("PROTOCOLOS DISPONIBLES");
            Console.WriteLine("-----------------------");

            foreach (var protocol in protocols.Values)
            {
                Console.WriteLine(
                    $"{protocol.Name} - {protocol.Description}"
                );
            }

            Console.WriteLine();
        }
    }
}
