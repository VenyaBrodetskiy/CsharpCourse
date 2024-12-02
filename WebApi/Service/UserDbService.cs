using WebApi.Models;

namespace WebApi.Service;

public class UserDbService
{
    public User GetUserInfo(string userName)
    {
        return new()
        {
            Id = 1,
            DeviceToken = "as34b123",
            Email = $"{userName}@email.com",
            PhoneNumber = "+9720507654321",
            UserName = userName
        };
    }
}
