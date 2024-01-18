using AgileTask.Models;

namespace AgileTask.Services.UserService
{
    public interface IUserService
    {
        public Task<string> authenticateUser(User user);
    }
}
