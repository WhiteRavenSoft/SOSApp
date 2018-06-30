using Microsoft.AspNet.SignalR;

namespace SOSApp.API.AppProvider
{
    public class CustomUserProvider : IUserIdProvider
    {
        public string GetUserId(IRequest request)
        {
            var user = request.QueryString["user"].ToString();
            return user.ToString();
        }
    }
}