using CommunityToolkit.Mvvm.Messaging.Messages;

namespace RoyalParking.MAUI.Messages;

internal class LoggedInUserChangedMessage(bool value) : ValueChangedMessage<bool>(value)
{

}
