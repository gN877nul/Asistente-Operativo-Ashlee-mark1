namespace A.S.H.L.E.E._alfa.Protocolos
{
    internal interface IProtocol
    {
        string Name { get; }

        string Description { get; }

        Task ExecuteAsync();
    }
}

