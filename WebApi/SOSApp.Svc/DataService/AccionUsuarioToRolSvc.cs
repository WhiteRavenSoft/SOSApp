using WhiteRaven.Data.DBModel;
using WhiteRaven.Svc.GenericDataService;

namespace WhiteRaven.Svc.DataService
{
    public class AccionUsuarioToRolSvc : GenericSvc<AccionUsuarioToRol, WhiteAdsEntities>
    {
        public AccionUsuarioToRol Save(AccionUsuarioToRol x)
        {
            if (x.ID == 0)
                return CreateEntity(x);

            return UpdateEntity(x);
        }

        public void DeleteLogic(int id)
        {
            if (id == 0)
                return;

            var x = base.Load(id);

            if (x != null)
            {
                x.Deleted = true;
                x.Active = false;
                UpdateEntity(x);
            }
        }
    }
}