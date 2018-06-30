using Microsoft.Owin.Security.Infrastructure;
using System;
using System.Threading.Tasks;

namespace SOSApp.API.AppProvider
{
    public class AppRefreshTokenProvider : IAuthenticationTokenProvider
    {
        public void Create(AuthenticationTokenCreateContext context)
        {
            throw new NotImplementedException();
        }

        public Task CreateAsync(AuthenticationTokenCreateContext context)
        {
            // Expiration time in seconds
            int expire = 5 * 60;
            context.Ticket.Properties.ExpiresUtc = new DateTimeOffset(DateTime.Now.AddSeconds(expire));
            context.SetToken(context.SerializeTicket());
            return Task.FromResult<object>(null);
        }

        public void Receive(AuthenticationTokenReceiveContext context)
        {
            throw new NotImplementedException();
        }

        public Task ReceiveAsync(AuthenticationTokenReceiveContext context)
        {
            throw new NotImplementedException();
        }

        //public async Task CreateAsync(AuthenticationTokenCreateContext context)
        //{
        //    var clientid = context.Ticket.Properties.Dictionary["as:client_id"];

        //    if (string.IsNullOrEmpty(clientid))
        //    {
        //        return;
        //    }

        //    var refreshTokenId = Guid.NewGuid().ToString("n");

        //    using (AuthRepository _repo = new AuthRepository())
        //    {
        //        var refreshTokenLifeTime = context.OwinContext.Get<string>("as:clientRefreshTokenLifeTime");

        //        var token = new RefreshToken()
        //        {
        //            Id = Helper.GetHash(refreshTokenId),
        //            ClientId = clientid,
        //            Subject = context.Ticket.Identity.Name,
        //            IssuedUtc = DateTime.UtcNow,
        //            ExpiresUtc = DateTime.UtcNow.AddMinutes(Convert.ToDouble(refreshTokenLifeTime))
        //        };

        //        context.Ticket.Properties.IssuedUtc = token.IssuedUtc;
        //        context.Ticket.Properties.ExpiresUtc = token.ExpiresUtc;

        //        token.ProtectedTicket = context.SerializeTicket();

        //        var result = await _repo.AddRefreshToken(token);

        //        if (result)
        //        {
        //            context.SetToken(refreshTokenId);
        //        }

        //    }
        //}
    }
}