using AutoMapper;
using log4net;
using System.Collections.Generic;
using System.Reflection;
using System.Web.Http;
using System.Web.Http.Cors;
using SOSApp.Data.AppModel;
using SOSApp.Data.DBModel;
using SOSApp.Svc.DataService;
using SOSApp.Svc.Infrastructure;
using SOSApp.Core.Helper;
using System;
using System.Security.Claims;

namespace SOSApp.API.Core
{
    /// <summary>
    /// 
    /// </summary>
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ApiBaseController : ApiController
    {
        public int UserId
        {
            get
            {
                return Convert.ToInt32(ClaimsPrincipal.Current.FindFirst(ClaimTypes.PrimarySid).Value);
            }
        }
        /// <summary>
        /// Crea una instancia de log4net
        /// </summary>
        protected static readonly ILog logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        /// <summary>
        /// 
        /// </summary>
        protected IMapper mapper;

        protected UserSvc userSvc = null;
        protected UserGroupSvc userGroupSvc = null;
        protected UserRoleSvc userRoleSvc = null;
        protected NewsSvc newsSvc = null;
        protected NewsSentSvc newsSentSvc = null;

        /// <summary>
        /// Constructor
        /// </summary>
        public ApiBaseController()
        {
            //Inicializa instancia de Mapper
            mapper = AutoMapperConfig.Instance.MapperConfiguration.CreateMapper();

            userSvc = IoC.Resolve<UserSvc>();
            userGroupSvc = IoC.Resolve<UserGroupSvc>();
            userRoleSvc = IoC.Resolve<UserRoleSvc>();
            newsSvc = IoC.Resolve<NewsSvc>();
            newsSentSvc = IoC.Resolve<NewsSentSvc>();
        }

        #region Mappers
        #region User
        protected List<UserModel> MapToModel(List<User> list)
        {
            List<UserModel> finalList = new List<UserModel>();
            foreach (var item in list)
                finalList.Add(MapToModel(item));

            return finalList;
        }
        protected UserModel MapToModel(User item)
        {
            return new UserModel()
            {
                ID = item.ID,
                MobileID = item.MobileID,
                Name = item.Name,
                LastName = item.LastName,
                Email = item.Email,
                Phone = item.Phone,
                Address = item.Address,
                RoleId = item.RoleId,
                Birthdate = item.Birthdate.HasValue ? string.Concat(item.Birthdate.Value.Year, "-", item.Birthdate.Value.Month.ToString("00"), "-", item.Birthdate.Value.Day.ToString("00")) : null,
                Lat = item.Lat,
                Lon = item.Lon,
                Active = item.Active
            };
        }
        protected User MapToDB(UserModel item)
        {
            return new User()
            {
                ID = item.ID,
                MobileID = item.MobileID,
                Name = item.Name,
                LastName = item.LastName,
                Email = item.Email,
                Phone = item.Phone,
                Address = item.Address,
                RoleId = item.RoleId,
                Birthdate = DateTime.Parse(item.Birthdate),
                Lat = item.Lat,
                Lon = item.Lon,
                Active = item.Active,
                Password = item.Password
            };
        }
        #endregion
        #region UserGroup 
        protected List<UserGroupModel> MapToModel(List<UserGroup> list)
        {
            List<UserGroupModel> finalList = new List<UserGroupModel>();
            foreach (var item in list)
                finalList.Add(MapToModel(item));

            return finalList;
        }
        protected UserGroupModel MapToModel(UserGroup item)
        {
            return new UserGroupModel()
            {
                ID = item.ID,
                Name = item.Name,
                Description = item.Description
            };
        }
        #endregion
        #region UserRole
        protected List<UserRoleModel> MapToModel(List<UserRole> list)
        {
            List<UserRoleModel> finalList = new List<UserRoleModel>();
            foreach (var item in list)
                finalList.Add(MapToModel(item));

            return finalList;
        }
        protected UserRoleModel MapToModel(UserRole item)
        {
            return new UserRoleModel()
            {
                ID = item.ID,
                Name = item.Name,
                Description = item.Description,
                Active = item.Active
            };
        }
        protected UserRole MapToDB(UserRoleModel item)
        {
            return new UserRole()
            {
                ID = item.ID,
                Name = item.Name,
                Description = item.Description,
                Active = item.Active
            };
        }
        #endregion
        #region News
        protected List<NewsGridModel> MapToGridModel(List<News> list)
        {
            List<NewsGridModel> finalList = new List<NewsGridModel>();
            foreach (var item in list)
                finalList.Add(MapToGridModel(item));

            return finalList;
        }
        protected NewsGridModel MapToGridModel(News item)
        {
            return new NewsGridModel()
            {
                ID = item.ID,
                Title = item.Title,
                CreatedDate = item.CreatedDate,
                Date = item.Date,
                Important = item.Important,
                Active = item.Active
            };
        }

        protected List<NewsModel> MapToModel(List<News> list)
        {
            List<NewsModel> finalList = new List<NewsModel>();
            foreach (var item in list)
                finalList.Add(MapToModel(item));

            return finalList;
        }
        protected NewsModel MapToModel(News item)
        {
            DateTime newsDate = item.Date ?? new DateTime();
            return new NewsModel()
            {
                ID = item.ID,
                Title = item.Title,
                Date = string.Concat(newsDate.Year, "-", newsDate.Month.ToString("00"), "-", newsDate.Day.ToString("00")),
                Important = item.Important,
                Body = item.Body,
                Image = FileHelper.GetNewsImageUrl(item.ID),
                CreatedDate = item.CreatedDate,
                LastUpdate = item.LastUpdate,
                Deleted = item.Deleted,
                Active = item.Active
            };
        }

        protected News MapToDB(NewsModel model)
        {
            return new News()
            {
                ID = model.ID,
                Title = model.Title,
                Important = model.Important,
                Date = DateTime.Parse(model.Date),
                Body = model.Body,
                Active = model.Active
            };
        }
        #endregion
        #region NewsSent
        protected List<NewsSentGridModel> MapToGridModel(List<NewsSent> list)
        {
            List<NewsSentGridModel> finalList = new List<NewsSentGridModel>();
            foreach (var item in list)
                finalList.Add(MapToGridModel(item));

            return finalList;
        }
        protected NewsSentGridModel MapToGridModel(NewsSent item)
        {
            return new NewsSentGridModel()
            {
                ID = item.ID,
                NewsTitle = item.News.Title,
                UserGroupName = item.UserGroupId.ToString(),
                UserGroupId = item.UserGroupId,
                SentDate = item.SentDate
            };
        }
        protected List<NewsSentModel> MapToModel(List<NewsSent> list)
        {
            List<NewsSentModel> finalList = new List<NewsSentModel>();
            foreach (var item in list)
                finalList.Add(MapToModel(item));

            return finalList;
        }
        protected NewsSentModel MapToModel(NewsSent item)
        {
            return new NewsSentModel()
            {
                ID = item.ID,
                NewsID = item.NewsID,
                UserGroupId = item.UserGroupId,
                SentDate = item.SentDate,
                News = MapToModel(item.News),
                UserGroup = new UserGroupModel() { ID = item.UserGroupId }
            };
        }
        #endregion
        #endregion
    }
}