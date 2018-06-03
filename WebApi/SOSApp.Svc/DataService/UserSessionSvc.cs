using System;
using System.Linq;
using WhiteRaven.Data.DBModel;
using WhiteRaven.Svc.GenericDataService;

namespace WhiteRaven.Svc.DataService
{
    public class UserSessionSvc : GenericSvc<UserSession, WhiteAdsEntities>
    {
        public UserSession Save(UserSession x)
        {
            if (x.UserSessionGUID == Guid.Empty)
            {
                x.UserSessionGUID = Guid.NewGuid();
                return CreateEntity(x);
            }

            return UpdateEntity(x);
        }

        public UserSession LoadByUser(int user)
        {
            if (user == 0)
                return null;

            var query = from x in Context.UserSession
                        where x.ID_Usuario == user
                        orderby x.UltimoAccesso descending
                        select x;

            var session = query.FirstOrDefault();

            return session;
        }

        public IQueryable<UserSession> LoadAll()
        {
            var query = from x in Context.UserSession
                        orderby x.UltimoAccesso descending
                        select x;

            return query;
        }
    }
}
