namespace ClientManagerBTG.Shared.Messages;

public class ClientEditedMessage(Guid clientId) : ValueChangedMessage<Guid>(clientId) { }

public class ClientAddedMessage(ClientModel client) : ValueChangedMessage<ClientModel>(client) { }
