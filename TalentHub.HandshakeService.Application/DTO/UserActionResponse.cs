namespace TalentHub.HandshakeService.Application.DTO;

public class UserActionResponse
{
    public bool IsSuccess { get; }
    public string? Message { get; }
    public UserContactsInfo? ContactsInfo { get; }

    private UserActionResponse(bool isSuccess, string? message, UserContactsInfo? contactsInfo)
    {
        IsSuccess = isSuccess;
        Message = message;
        ContactsInfo = contactsInfo;
    }

    public static UserActionResponse SuccessWithContacts(UserContactsInfo contactsInfo)
    {
        return new UserActionResponse(true, null, contactsInfo);
    }

    public static UserActionResponse SuccessWithMessage(string message)
    {
        return new UserActionResponse(true, message, null);
    }

    public static UserActionResponse Failure(string errorMessage)
    {
        return new UserActionResponse(false, errorMessage, null);
    }
}