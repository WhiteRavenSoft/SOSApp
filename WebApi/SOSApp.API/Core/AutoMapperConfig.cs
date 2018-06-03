using AutoMapper;
using WhiteRaven.Data.AppModel;
using WhiteRaven.Data.DBModel;

namespace WhiteRaven.API.Core
{
    /// <summary>
    /// 
    /// </summary>
    public class AutoMapperConfig
    {
        private static volatile AutoMapperConfig instance;
        private static object syncRoot = new object();

        /// <summary>
        /// 
        /// </summary>
        public MapperConfiguration MapperConfiguration { get; set; }

        private AutoMapperConfig()
        {
            MapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Usuario, UsuarioModel>().ReverseMap();
            });
        }

        /// <summary>
        /// 
        /// </summary>
        public static AutoMapperConfig Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new AutoMapperConfig();
                    }
                }

                return instance;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void Init()
        {

        }
    }
}