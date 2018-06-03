using Microsoft.AspNet.SignalR;

namespace WhiteRaven.API.AppProvider
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