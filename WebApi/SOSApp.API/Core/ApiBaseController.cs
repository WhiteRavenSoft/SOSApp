using AutoMapper;
using log4net;
using System.Collections.Generic;
using System.Reflection;
using System.Web.Http;
using System.Web.Http.Cors;
using WhiteRaven.Data.AppModel;
using WhiteRaven.Data.DBModel;
using WhiteRaven.Svc.DataService;
using WhiteRaven.Svc.Infrastructure;

namespace WhiteRaven.API.Core
{
    /// <summary>
    /// 
    /// </summary>
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ApiBaseController : ApiController
    {
        /// <summary>
        /// Crea una instancia de log4net
        /// </summary>
        protected static readonly ILog logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        /// <summary>
        /// 
        /// </summary>
        protected IMapper mapper;

        protected AccionUsuarioToRolSvc accionUsuarioToRolSvc = null;
        protected AdsSvc adsSvc = null;
        protected AdsCategoriaSvc adsCategoriaSvc = null;
        protected BancoSvc bancoSvc = null;
        protected BeneficioSvc beneficioSvc = null;
        protected Beneficio_DetalleSvc beneficio_DetalleSvc = null;
        protected CalificacionSvc calificacionSvc = null;
        protected CategoriasPorAdsSvc categoriasPorAdsSvc = null;
        protected CiudadSvc ciudadSvc = null;
        protected EmpresaSvc empresaSvc = null;
        protected EmpresaConfiguracionesSvc empresaConfiguracionesSvc = null;
        protected EmpresaCuentaSvc empresaCuentaSvc = null;
        protected EmpresaHorariosSvc empresaHorariosSvc = null;
        protected EmpresaHorariosEspecialesSvc empresaHorariosEspecialesSvc = null;
        protected EmpresaRedesSocialesSvc empresaRedesSocialesSvc = null;
        protected EmpresaRubroSvc empresaRubroSvc = null;
        protected EmpresaSucursalSvc empresaSucursalSvc = null;
        protected FormaPagoSvc formaPagoSvc = null;
        protected FormaPagoEmpresaSvc formaPagoEmpresaSvc = null;
        protected FormaPagoVendedorSvc formaPagoVendedorSvc = null;
        protected HorariosSvc horariosSvc = null;
        protected InstitucionSocialSvc institucionSocialSvc = null;
        protected InstitucionSocialConfiguracionesSvc institucionSocialConfiguracionesSvc = null;
        protected InstitucionSocialCuentaSvc institucionSocialCuentaSvc = null;
        protected InstitucionSocialRedesSocialesSvc institucionSocialRedesSocialesSvc = null;
        protected MonedaSvc monedaSvc = null;
        protected PaisSvc paisSvc = null;
        protected PlanSvc planSvc = null;
        protected Plan_CaracteristicaSvc plan_CaracteristicaSvc = null;
        protected ProvinciaSvc provinciaSvc = null;
        protected RedesSocialesSvc redesSocialesSvc = null;
        protected UserSessionSvc userSessionSvc = null;
        protected UsuarioSvc usuarioSvc = null;
        protected UsuarioAccionSvc usuarioAccionSvc = null;
        protected UsuarioRolSvc usuarioRolSvc = null;
        protected VendedorSvc vendedorSvc = null;
        protected VendedorConfiguracionesSvc vendedorConfiguracionesSvc = null;
        protected VendedorCuentaSvc vendedorCuentaSvc = null;
        protected VendedorRedesSocialesSvc vendedorRedesSocialesSvc = null;
        protected VentasSvc ventasSvc = null;


        /// <summary>
        /// Constructor
        /// </summary>
        public ApiBaseController()
        {
            //Inicializa instancia de Mapper
            mapper = AutoMapperConfig.Instance.MapperConfiguration.CreateMapper();

            usuarioSvc = IoC.Resolve<UsuarioSvc>();
            accionUsuarioToRolSvc = IoC.Resolve<AccionUsuarioToRolSvc>();
            adsSvc = IoC.Resolve<AdsSvc>();
            adsCategoriaSvc = IoC.Resolve<AdsCategoriaSvc>();
            bancoSvc = IoC.Resolve<BancoSvc>();
            beneficioSvc = IoC.Resolve<BeneficioSvc>();
            beneficio_DetalleSvc = IoC.Resolve<Beneficio_DetalleSvc>();
            calificacionSvc = IoC.Resolve<CalificacionSvc>();
            categoriasPorAdsSvc = IoC.Resolve<CategoriasPorAdsSvc>();
            ciudadSvc = IoC.Resolve<CiudadSvc>();
            empresaSvc = IoC.Resolve<EmpresaSvc>();
            empresaConfiguracionesSvc = IoC.Resolve<EmpresaConfiguracionesSvc>();
            empresaCuentaSvc = IoC.Resolve<EmpresaCuentaSvc>();
            empresaHorariosSvc = IoC.Resolve<EmpresaHorariosSvc>();
            empresaHorariosEspecialesSvc = IoC.Resolve<EmpresaHorariosEspecialesSvc>();
            empresaRedesSocialesSvc = IoC.Resolve<EmpresaRedesSocialesSvc>();
            empresaRubroSvc = IoC.Resolve<EmpresaRubroSvc>();
            empresaSucursalSvc = IoC.Resolve<EmpresaSucursalSvc>();
            formaPagoSvc = IoC.Resolve<FormaPagoSvc>();
            formaPagoEmpresaSvc = IoC.Resolve<FormaPagoEmpresaSvc>();
            formaPagoVendedorSvc = IoC.Resolve<FormaPagoVendedorSvc>();
            horariosSvc = IoC.Resolve<HorariosSvc>();
            institucionSocialSvc = IoC.Resolve<InstitucionSocialSvc>();
            institucionSocialConfiguracionesSvc = IoC.Resolve<InstitucionSocialConfiguracionesSvc>();
            institucionSocialCuentaSvc = IoC.Resolve<InstitucionSocialCuentaSvc>();
            institucionSocialRedesSocialesSvc = IoC.Resolve<InstitucionSocialRedesSocialesSvc>();
            monedaSvc = IoC.Resolve<MonedaSvc>();
            paisSvc = IoC.Resolve<PaisSvc>();
            planSvc = IoC.Resolve<PlanSvc>();
            plan_CaracteristicaSvc = IoC.Resolve<Plan_CaracteristicaSvc>();
            provinciaSvc = IoC.Resolve<ProvinciaSvc>();
            redesSocialesSvc = IoC.Resolve<RedesSocialesSvc>();
            userSessionSvc = IoC.Resolve<UserSessionSvc>();
            usuarioSvc = IoC.Resolve<UsuarioSvc>();
            usuarioAccionSvc = IoC.Resolve<UsuarioAccionSvc>();
            usuarioRolSvc = IoC.Resolve<UsuarioRolSvc>();
            vendedorSvc = IoC.Resolve<VendedorSvc>();
            vendedorConfiguracionesSvc = IoC.Resolve<VendedorConfiguracionesSvc>();
            vendedorCuentaSvc = IoC.Resolve<VendedorCuentaSvc>();
            vendedorRedesSocialesSvc = IoC.Resolve<VendedorRedesSocialesSvc>();
            ventasSvc = IoC.Resolve<VentasSvc>();
        }

        #region Mappers
        #region AccionUsuarioToRol
        protected List<AccionUsuarioToRolModel> MapToModel(List<AccionUsuarioToRol> list)
        {
            List<AccionUsuarioToRolModel> finalList = new List<AccionUsuarioToRolModel>();
            foreach (var item in list)
                finalList.Add(MapToModel(item));

            return finalList;
        }
        protected AccionUsuarioToRolModel MapToModel(AccionUsuarioToRol item)
        {
            return new AccionUsuarioToRolModel()
            {
                ID = item.ID,
                ID_AccionUsuario = item.ID_AccionUsuario,
                ID_RolUsuario = item.ID_RolUsuario,
                SoloLectura = item.SoloLectura,
                LecturaYEscritura = item.LecturaYEscritura,
                Active = item.Active,
                Deleted = item.Deleted
            };
        }
        protected AccionUsuarioToRol MapToDB(AccionUsuarioToRolModel item)
        {
            return new AccionUsuarioToRol()
            {
                ID = item.ID,
                ID_AccionUsuario = item.ID_AccionUsuario,
                ID_RolUsuario = item.ID_RolUsuario,
                SoloLectura = item.SoloLectura,
                LecturaYEscritura = item.LecturaYEscritura,
                Active = item.Active,
                Deleted = item.Deleted
            };
        }
        #endregion

        #region AdsCategoria
        protected List<AdsCategoriaModel> MapToModel(List<AdsCategoria> list)
        {
            List<AdsCategoriaModel> finalList = new List<AdsCategoriaModel>();
            foreach (var item in list)
                finalList.Add(MapToModel(item));

            return finalList;
        }
        protected AdsCategoriaModel MapToModel(AdsCategoria item)
        {
            return new AdsCategoriaModel()
            {
                ID = item.ID,
                ID_CategoriaPadre = item.ID_CategoriaPadre,
                Nombre = item.Nombre,
                Descripcion = item.Descripcion,
                Icono = item.Icono,
                BackgrundImage = item.BackgrundImage,
                Active = item.Active,
                Deleted = item.Deleted
            };
        }
        protected AdsCategoria MapToDB(AdsCategoriaModel item)
        {
            return new AdsCategoria()
            {
                ID = item.ID,
                ID_CategoriaPadre = item.ID_CategoriaPadre,
                Nombre = item.Nombre,
                Descripcion = item.Descripcion,
                Icono = item.Icono,
                BackgrundImage = item.BackgrundImage,
                Active = item.Active,
                Deleted = item.Deleted
            };
        }
        #endregion

        #region Ads
        protected List<AdsModel> MapToModel(List<Ads> list)
        {
            List<AdsModel> finalList = new List<AdsModel>();
            foreach (var item in list)
                finalList.Add(MapToModel(item));

            return finalList;
        }
        protected AdsModel MapToModel(Ads item)
        {
            return new AdsModel()
            {
                ID = item.ID,
                ID_Empresa = item.ID_Empresa,
                Titulo = item.Titulo,
                Texto = item.Texto,
                Largo = item.Largo,
                Alto = item.Alto,
                ColorPrincipal = item.ColorPrincipal,
                ColorSecundario = item.ColorSecundario,
                ColorTexto = item.ColorTexto,
                Imagen = item.Imagen,
                Principal = item.Principal,
                Destacado = item.Destacado,
                Active = item.Active,
                Deleted = item.Deleted
            };
        }
        protected Ads MapToDB(AdsModel item)
        {
            return new Ads()
            {
                ID = item.ID,
                ID_Empresa = item.ID_Empresa,
                Titulo = item.Titulo,
                Texto = item.Texto,
                Largo = item.Largo,
                Alto = item.Alto,
                ColorPrincipal = item.ColorPrincipal,
                ColorSecundario = item.ColorSecundario,
                ColorTexto = item.ColorTexto,
                Imagen = item.Imagen,
                Principal = item.Principal,
                Destacado = item.Destacado,
                Active = item.Active,
                Deleted = item.Deleted
            };
        }
        #endregion

        #region Banco
        protected List<BancoModel> MapToModel(List<Banco> list)
        {
            List<BancoModel> finalList = new List<BancoModel>();
            foreach (var item in list)
                finalList.Add(MapToModel(item));

            return finalList;
        }
        protected BancoModel MapToModel(Banco item)
        {
            return new BancoModel()
            {
                ID = item.ID,
                Nombre = item.Nombre,
                Detalle = item.Detalle,
                Active = item.Active,
                Deleted = item.Deleted
            };
        }
        protected Banco MapToDB(BancoModel item)
        {
            return new Banco()
            {
                ID = item.ID,
                Nombre = item.Nombre,
                Detalle = item.Detalle,
                Active = item.Active,
                Deleted = item.Deleted
            };
        }
        #endregion

        #region Beneficio
        protected List<BeneficioModel> MapToModel(List<Beneficio> list)
        {
            List<BeneficioModel> finalList = new List<BeneficioModel>();
            foreach (var item in list)
                finalList.Add(MapToModel(item));

            return finalList;
        }
        protected BeneficioModel MapToModel(Beneficio item)
        {
            return new BeneficioModel()
            {
                ID = item.ID,
                ID_InstitucionSocial = item.ID_InstitucionSocial,
                Descripcion = item.Descripcion,
                Periodo = item.Periodo,
                Monto = item.Monto,
                FechaDeseada = item.FechaDeseada,
                FechaLograda = item.FechaLograda,
                FechaPago = item.FechaPago,
                Active = item.Active,
                Deleted = item.Deleted
            };
        }
        protected Beneficio MapToDB(BeneficioModel item)
        {
            return new Beneficio()
            {
                ID = item.ID,
                ID_InstitucionSocial = item.ID_InstitucionSocial,
                Descripcion = item.Descripcion,
                Periodo = item.Periodo,
                Monto = item.Monto,
                FechaDeseada = item.FechaDeseada,
                FechaLograda = item.FechaLograda,
                FechaPago = item.FechaPago,
                Active = item.Active,
                Deleted = item.Deleted
            };
        }
        #endregion

        #region Beneficio_Detalle
        protected List<Beneficio_DetalleModel> MapToModel(List<Beneficio_Detalle> list)
        {
            List<Beneficio_DetalleModel> finalList = new List<Beneficio_DetalleModel>();
            foreach (var item in list)
                finalList.Add(MapToModel(item));

            return finalList;
        }
        protected Beneficio_DetalleModel MapToModel(Beneficio_Detalle item)
        {
            return new Beneficio_DetalleModel()
            {
                ID = item.ID,
                ID_Beneficio = item.ID_Beneficio,
                ID_Vendedor = item.ID_Vendedor,
                Monto = item.Monto,
                Nota = item.Nota,
                Fecha = item.Fecha,
                Active = item.Active,
                Deleted = item.Deleted
            };
        }
        protected Beneficio_Detalle MapToDB(Beneficio_DetalleModel item)
        {
            return new Beneficio_Detalle()
            {
                ID = item.ID,
                ID_Beneficio = item.ID_Beneficio,
                ID_Vendedor = item.ID_Vendedor,
                Monto = item.Monto,
                Nota = item.Nota,
                Fecha = item.Fecha,
                Active = item.Active,
                Deleted = item.Deleted,
            };
        }
        #endregion

        #region Calificacion
        protected List<CalificacionModel> MapToModel(List<Calificacion> list)
        {
            List<CalificacionModel> finalList = new List<CalificacionModel>();
            foreach (var item in list)
                finalList.Add(MapToModel(item));

            return finalList;
        }
        protected CalificacionModel MapToModel(Calificacion item)
        {
            return new CalificacionModel()
            {
                ID = item.ID,
                ID_Empresa = item.ID_Empresa,
                Nombre = item.Nombre,
                Email = item.Email,
                CalificacionNumero = item.CalificacionNumero,
                Comentario = item.Comentario,
                Active = item.Active,
                Deleted = item.Deleted
            };
        }
        protected Calificacion MapToDB(CalificacionModel item)
        {
            return new Calificacion()
            {
                ID = item.ID,
                ID_Empresa = item.ID_Empresa,
                Nombre = item.Nombre,
                Email = item.Email,
                CalificacionNumero = item.CalificacionNumero,
                Comentario = item.Comentario,
                Active = item.Active,
                Deleted = item.Deleted
            };
        }
        #endregion

        #region CategoriasPorAds
        protected List<CategoriasPorAdsModel> MapToModel(List<CategoriasPorAds> list)
        {
            List<CategoriasPorAdsModel> finalList = new List<CategoriasPorAdsModel>();
            foreach (var item in list)
                finalList.Add(MapToModel(item));

            return finalList;
        }
        protected CategoriasPorAdsModel MapToModel(CategoriasPorAds item)
        {
            return new CategoriasPorAdsModel()
            {
                ID = item.ID,
                ID_Ads = item.ID_Ads,
                ID_Categoria = item.ID_Categoria,
                Active = item.Active,
                Deleted = item.Deleted
            };
        }
        protected CategoriasPorAds MapToDB(CategoriasPorAdsModel item)
        {
            return new CategoriasPorAds()
            {
                ID = item.ID,
                ID_Ads = item.ID_Ads,
                ID_Categoria = item.ID_Categoria,
                Active = item.Active,
                Deleted = item.Deleted
            };
        }
        #endregion

        #region Ciudad
        protected List<CiudadModel> MapToModel(List<Ciudad> list)
        {
            List<CiudadModel> finalList = new List<CiudadModel>();
            foreach (var item in list)
                finalList.Add(MapToModel(item));

            return finalList;
        }
        protected CiudadModel MapToModel(Ciudad item)
        {
            return new CiudadModel()
            {
                ID = item.ID,
                Nombre = item.Nombre,
                CodigoPostal = item.CodigoPostal,
                Active = item.Active,
                Deleted = item.Deleted
            };
        }
        protected Ciudad MapToDB(CiudadModel item)
        {
            return new Ciudad()
            {
                ID = item.ID,
                ID_Provincia = item.ID_Provincia,
                Nombre = item.Nombre,
                CodigoPostal = item.CodigoPostal,
                Active = item.Active,
                Deleted = item.Deleted
            };
        }
        #endregion

        #region Empresa
        protected List<EmpresaModel> MapToModel(List<Empresa> list)
        {
            List<EmpresaModel> finalList = new List<EmpresaModel>();
            foreach (var item in list)
                finalList.Add(MapToModel(item));

            return finalList;
        }
        protected EmpresaModel MapToModel(Empresa item)
        {
            return new EmpresaModel()
            {
                ID = item.ID,
                ID_Rubro = item.ID_Rubro,
                ID_Plan = item.ID_Plan,
                RazonSocial = item.RazonSocial,
                CUIT = item.CUIT,
                Password = item.Password,
                PerfilLogo = item.PerfilLogo,
                PerfilCabecera = item.PerfilCabecera,
                Email = item.Email,
                Telefono = item.Telefono,
                Direccion = item.Direccion,
                ID_Ciudad = item.ID_Ciudad,
                Mapa_Latitud = item.Mapa_Latitud,
                Mapa_Longitud = item.Mapa_Longitud,
                W3w = item.W3w,
                GoogleMaps = item.GoogleMaps,
                Web = item.Web,
                FechaAlta = item.FechaAlta,
                MostrarCalificaciones = item.MostrarCalificaciones,
                Active = item.Active,
                Deleted = item.Deleted
            };
        }
        protected Empresa MapToDB(EmpresaModel item)
        {
            return new Empresa()
            {
                ID = item.ID,
                ID_Rubro = item.ID_Rubro,
                ID_Plan = item.ID_Plan,
                RazonSocial = item.RazonSocial,
                CUIT = item.CUIT,
                Password = item.Password,
                PerfilLogo = item.PerfilLogo,
                PerfilCabecera = item.PerfilCabecera,
                Email = item.Email,
                Telefono = item.Telefono,
                Direccion = item.Direccion,
                ID_Ciudad = item.ID_Ciudad,
                Mapa_Latitud = item.Mapa_Latitud,
                Mapa_Longitud = item.Mapa_Longitud,
                W3w = item.W3w,
                GoogleMaps = item.GoogleMaps,
                Web = item.Web,
                FechaAlta = item.FechaAlta,
                MostrarCalificaciones = item.MostrarCalificaciones,
                Active = item.Active,
                Deleted = item.Deleted
            };
        }
        #endregion

        #region EmpresaConfiguraciones
        protected List<EmpresaConfiguracionesModel> MapToModel(List<EmpresaConfiguraciones> list)
        {
            List<EmpresaConfiguracionesModel> finalList = new List<EmpresaConfiguracionesModel>();
            foreach (var item in list)
                finalList.Add(MapToModel(item));

            return finalList;
        }
        protected EmpresaConfiguracionesModel MapToModel(EmpresaConfiguraciones item)
        {
            return new EmpresaConfiguracionesModel()
            {
                ID = item.ID,
                ID_Empresa = item.ID_Empresa,
                StringKey = item.StringKey,
                StringValue = item.StringValue,
                CreatedDate = item.CreatedDate,
                LastUpdate = item.LastUpdate,
                Published = item.Published,
                Deleted = item.Deleted
            };
        }
        protected EmpresaConfiguraciones MapToDB(EmpresaConfiguracionesModel item)
        {
            return new EmpresaConfiguraciones()
            {
                ID = item.ID,
                ID_Empresa = item.ID_Empresa,
                StringKey = item.StringKey,
                StringValue = item.StringValue,
                CreatedDate = item.CreatedDate,
                LastUpdate = item.LastUpdate,
                Published = item.Published,
                Deleted = item.Deleted
            };
        }
        #endregion

        #region EmpresaCuenta
        protected List<EmpresaCuentaModel> MapToModel(List<EmpresaCuenta> list)
        {
            List<EmpresaCuentaModel> finalList = new List<EmpresaCuentaModel>();
            foreach (var item in list)
                finalList.Add(MapToModel(item));

            return finalList;
        }
        protected EmpresaCuentaModel MapToModel(EmpresaCuenta item)
        {
            return new EmpresaCuentaModel()
            {
                ID = item.ID,
                ID_Empresa = item.ID_Empresa,
                Monto = item.Monto,
                MontoBloqueado = item.MontoBloqueado,
                MontoGastado = item.MontoGastado,
                Fecha = item.Fecha,
                Active = item.Active,
                Deleted = item.Deleted
            };
        }
        protected EmpresaCuenta MapToDB(EmpresaCuentaModel item)
        {
            return new EmpresaCuenta()
            {
                ID = item.ID,
                ID_Empresa = item.ID_Empresa,
                Monto = item.Monto,
                MontoBloqueado = item.MontoBloqueado,
                MontoGastado = item.MontoGastado,
                Fecha = item.Fecha,
                Active = item.Active,
                Deleted = item.Deleted
            };
        }
        #endregion

        #region EmpresaHorarios
        protected List<EmpresaHorariosModel> MapToModel(List<EmpresaHorarios> list)
        {
            List<EmpresaHorariosModel> finalList = new List<EmpresaHorariosModel>();
            foreach (var item in list)
                finalList.Add(MapToModel(item));

            return finalList;
        }
        protected EmpresaHorariosModel MapToModel(EmpresaHorarios item)
        {
            return new EmpresaHorariosModel()
            {
                ID = item.ID,
                ID_Empresa = item.ID_Empresa,
                Dia = item.Dia,
                HoraApertura = item.HoraApertura,
                HoraCierre = item.HoraCierre,
                Active = item.Active,
                Deleted = item.Deleted
            };
        }
        protected EmpresaHorarios MapToDB(EmpresaHorariosModel item)
        {
            return new EmpresaHorarios()
            {
                ID = item.ID,
                ID_Empresa = item.ID_Empresa,
                Dia = item.Dia,
                HoraApertura = item.HoraApertura,
                HoraCierre = item.HoraCierre,
                Active = item.Active,
                Deleted = item.Deleted
            };
        }
        #endregion

        #region EmpresaHorariosEspeciales
        protected List<EmpresaHorariosEspecialesModel> MapToModel(List<EmpresaHorariosEspeciales> list)
        {
            List<EmpresaHorariosEspecialesModel> finalList = new List<EmpresaHorariosEspecialesModel>();
            foreach (var item in list)
                finalList.Add(MapToModel(item));

            return finalList;
        }
        protected EmpresaHorariosEspecialesModel MapToModel(EmpresaHorariosEspeciales item)
        {
            return new EmpresaHorariosEspecialesModel()
            {
                ID = item.ID,
                ID_Empresa = item.ID_Empresa,
                Dia = item.Dia,
                Abierto = item.Abierto,
                HoraApertura = item.HoraApertura,
                HoraCierre = item.HoraCierre,
                Active = item.Active,
                Deleted = item.Deleted
            };
        }
        protected EmpresaHorariosEspeciales MapToDB(EmpresaHorariosEspecialesModel item)
        {
            return new EmpresaHorariosEspeciales()
            {
                ID = item.ID,
                ID_Empresa = item.ID_Empresa,
                Dia = item.Dia,
                Abierto = item.Abierto,
                HoraApertura = item.HoraApertura,
                HoraCierre = item.HoraCierre,
                Active = item.Active,
                Deleted = item.Deleted
            };
        }
        #endregion

        #region EmpresaRedesSociales
        protected List<EmpresaRedesSocialesModel> MapToModel(List<EmpresaRedesSociales> list)
        {
            List<EmpresaRedesSocialesModel> finalList = new List<EmpresaRedesSocialesModel>();
            foreach (var item in list)
                finalList.Add(MapToModel(item));

            return finalList;
        }
        protected EmpresaRedesSocialesModel MapToModel(EmpresaRedesSociales item)
        {
            return new EmpresaRedesSocialesModel()
            {
                ID = item.ID,
                ID_Empresa = item.ID_Empresa,
                ID_RedSocial = item.ID_RedSocial,
                SocialUserName = item.SocialUserName,
                Active = item.Active,
                Deleted = item.Deleted
            };
        }
        protected EmpresaRedesSociales MapToDB(EmpresaRedesSocialesModel item)
        {
            return new EmpresaRedesSociales()
            {
                ID = item.ID,
                ID_Empresa = item.ID_Empresa,
                ID_RedSocial = item.ID_RedSocial,
                SocialUserName = item.SocialUserName,
                Active = item.Active,
                Deleted = item.Deleted
            };
        }
        #endregion

        #region EmpresaRubro
        protected List<EmpresaRubroModel> MapToModel(List<EmpresaRubro> list)
        {
            List<EmpresaRubroModel> finalList = new List<EmpresaRubroModel>();
            foreach (var item in list)
                finalList.Add(MapToModel(item));

            return finalList;
        }
        protected EmpresaRubroModel MapToModel(EmpresaRubro item)
        {
            return new EmpresaRubroModel()
            {
                ID = item.ID,
                Nombre = item.Nombre,
                Active = item.Active,
                Deleted = item.Deleted
            };
        }
        protected EmpresaRubro MapToDB(EmpresaRubroModel item)
        {
            return new EmpresaRubro()
            {
                ID = item.ID,
                Nombre = item.Nombre,
                Active = item.Active,
                Deleted = item.Deleted
            };
        }
        #endregion

        #region EmpresaSucursal
        protected List<EmpresaSucursalModel> MapToModel(List<EmpresaSucursal> list)
        {
            List<EmpresaSucursalModel> finalList = new List<EmpresaSucursalModel>();
            foreach (var item in list)
                finalList.Add(MapToModel(item));

            return finalList;
        }
        protected EmpresaSucursalModel MapToModel(EmpresaSucursal item)
        {
            return new EmpresaSucursalModel()
            {
                ID = item.ID,
                ID_Empresa = item.ID_Empresa,
                Nombre = item.Nombre,
                Direccion = item.Direccion,
                Telefono = item.Telefono,
                Email = item.Email,
                Nota = item.Nota,
                Active = item.Active,
                Deleted = item.Deleted
            };
        }
        protected EmpresaSucursal MapToDB(EmpresaSucursalModel item)
        {
            return new EmpresaSucursal()
            {
                ID = item.ID,
                ID_Empresa = item.ID_Empresa,
                Nombre = item.Nombre,
                Direccion = item.Direccion,
                Telefono = item.Telefono,
                Email = item.Email,
                Nota = item.Nota,
                Active = item.Active,
                Deleted = item.Deleted
            };
        }
        #endregion

        #region FormaPago
        protected List<FormaPagoModel> MapToModel(List<FormaPago> list)
        {
            List<FormaPagoModel> finalList = new List<FormaPagoModel>();
            foreach (var item in list)
                finalList.Add(MapToModel(item));

            return finalList;
        }
        protected FormaPagoModel MapToModel(FormaPago item)
        {
            return new FormaPagoModel()
            {
                ID = item.ID,
                Nombre = item.Nombre,
                Logo = item.Logo,
                Active = item.Active,
                Deleted = item.Deleted
            };
        }
        protected FormaPago MapToDB(FormaPagoModel item)
        {
            return new FormaPago()
            {
                ID = item.ID,
                Nombre = item.Nombre,
                Logo = item.Logo,
                Active = item.Active,
                Deleted = item.Deleted
            };
        }
        #endregion

        #region FormaPagoEmpresa
        protected List<FormaPagoEmpresaModel> MapToModel(List<FormaPagoEmpresa> list)
        {
            List<FormaPagoEmpresaModel> finalList = new List<FormaPagoEmpresaModel>();
            foreach (var item in list)
                finalList.Add(MapToModel(item));

            return finalList;
        }
        protected FormaPagoEmpresaModel MapToModel(FormaPagoEmpresa item)
        {
            return new FormaPagoEmpresaModel()
            {
                ID = item.ID,
                ID_FormaPago = item.ID_FormaPago,
                ID_Empresa = item.ID_Empresa,
                Prioridad = item.Prioridad,
                Active = item.Active,
                Deleted = item.Deleted
            };
        }
        protected FormaPagoEmpresa MapToDB(FormaPagoEmpresaModel item)
        {
            return new FormaPagoEmpresa()
            {
                ID = item.ID,
                ID_FormaPago = item.ID_FormaPago,
                ID_Empresa = item.ID_Empresa,
                Prioridad = item.Prioridad,
                Active = item.Active,
                Deleted = item.Deleted
            };
        }
        #endregion

        #region FormaPagoVendedor
        protected List<FormaPagoVendedorModel> MapToModel(List<FormaPagoVendedor> list)
        {
            List<FormaPagoVendedorModel> finalList = new List<FormaPagoVendedorModel>();
            foreach (var item in list)
                finalList.Add(MapToModel(item));

            return finalList;
        }
        protected FormaPagoVendedorModel MapToModel(FormaPagoVendedor item)
        {
            return new FormaPagoVendedorModel()
            {
                ID = item.ID,
                ID_FormaPago = item.ID_FormaPago,
                ID_Vendedor = item.ID_Vendedor,
                Prioridad = item.Prioridad,
                Titular = item.Titular,
                Numero = item.Numero,
                Documento = item.Documento,
                Entidad = item.Entidad,
                Active = item.Active,
                Deleted = item.Deleted
            };
        }
        protected FormaPagoVendedor MapToDB(FormaPagoVendedorModel item)
        {
            return new FormaPagoVendedor()
            {
                ID = item.ID,
                ID_FormaPago = item.ID_FormaPago,
                ID_Vendedor = item.ID_Vendedor,
                Prioridad = item.Prioridad,
                Titular = item.Titular,
                Numero = item.Numero,
                Documento = item.Documento,
                Entidad = item.Entidad,
                Active = item.Active,
                Deleted = item.Deleted
            };
        }
        #endregion

        #region Horarios
        protected List<HorariosModel> MapToModel(List<Horarios> list)
        {
            List<HorariosModel> finalList = new List<HorariosModel>();
            foreach (var item in list)
                finalList.Add(MapToModel(item));

            return finalList;
        }
        protected HorariosModel MapToModel(Horarios item)
        {
            return new HorariosModel()
            {
                ID = item.ID,
                ID_Empresa = item.ID_Empresa,
                Dia = item.Dia,
                Abre = item.Abre,
                Cierra = item.Cierra,
                Active = item.Active,
                Deleted = item.Deleted,
            };
        }
        protected Horarios MapToDB(HorariosModel item)
        {
            return new Horarios()
            {
                ID = item.ID,
                ID_Empresa = item.ID_Empresa,
                Dia = item.Dia,
                Abre = item.Abre,
                Cierra = item.Cierra,
                Active = item.Active,
                Deleted = item.Deleted,
            };
        }
        #endregion

        #region InstitucionSocial
        protected List<InstitucionSocialModel> MapToModel(List<InstitucionSocial> list)
        {
            List<InstitucionSocialModel> finalList = new List<InstitucionSocialModel>();
            foreach (var item in list)
                finalList.Add(MapToModel(item));

            return finalList;
        }
        protected InstitucionSocialModel MapToModel(InstitucionSocial item)
        {
            return new InstitucionSocialModel()
            {
                ID = item.ID,
                RazonSocial = item.RazonSocial,
                CUIT = item.CUIT,
                Password = item.Password,
                PerfilLogo = item.PerfilLogo,
                PerfilCabecera = item.PerfilCabecera,
                Email = item.Email,
                Telefono = item.Telefono,
                Direccion = item.Direccion,
                ID_Ciudad = item.ID_Ciudad,
                Mapa_Latitud = item.Mapa_Latitud,
                Mapa_Longitud = item.Mapa_Longitud,
                Web = item.Web,
                FechaAlta = item.FechaAlta,
                Active = item.Active,
                Deleted = item.Deleted,
            };
        }
        protected InstitucionSocial MapToDB(InstitucionSocialModel item)
        {
            return new InstitucionSocial()
            {
                ID = item.ID,
                RazonSocial = item.RazonSocial,
                CUIT = item.CUIT,
                Password = item.Password,
                PerfilLogo = item.PerfilLogo,
                PerfilCabecera = item.PerfilCabecera,
                Email = item.Email,
                Telefono = item.Telefono,
                Direccion = item.Direccion,
                ID_Ciudad = item.ID_Ciudad,
                Mapa_Latitud = item.Mapa_Latitud,
                Mapa_Longitud = item.Mapa_Longitud,
                Web = item.Web,
                FechaAlta = item.FechaAlta,
                Active = item.Active,
                Deleted = item.Deleted,
            };
        }
        #endregion

        #region InstitucionSocialConfiguraciones
        protected List<InstitucionSocialConfiguracionesModel> MapToModel(List<InstitucionSocialConfiguraciones> list)
        {
            List<InstitucionSocialConfiguracionesModel> finalList = new List<InstitucionSocialConfiguracionesModel>();
            foreach (var item in list)
                finalList.Add(MapToModel(item));

            return finalList;
        }
        protected InstitucionSocialConfiguracionesModel MapToModel(InstitucionSocialConfiguraciones item)
        {
            return new InstitucionSocialConfiguracionesModel()
            {
                ID = item.ID,
                ID_InstitucionSocial = item.ID_InstitucionSocial,
                StringKey = item.StringKey,
                StringValue = item.StringValue,
                CreatedDate = item.CreatedDate,
                LastUpdate = item.LastUpdate,
                Published = item.Published,
                Deleted = item.Deleted
            };
        }
        protected InstitucionSocialConfiguraciones MapToDB(InstitucionSocialConfiguracionesModel item)
        {
            return new InstitucionSocialConfiguraciones()
            {
                ID = item.ID,
                ID_InstitucionSocial = item.ID_InstitucionSocial,
                StringKey = item.StringKey,
                StringValue = item.StringValue,
                CreatedDate = item.CreatedDate,
                LastUpdate = item.LastUpdate,
                Published = item.Published,
                Deleted = item.Deleted
            };
        }
        #endregion

        #region InstitucionSocialCuenta
        protected List<InstitucionSocialCuentaModel> MapToModel(List<InstitucionSocialCuenta> list)
        {
            List<InstitucionSocialCuentaModel> finalList = new List<InstitucionSocialCuentaModel>();
            foreach (var item in list)
                finalList.Add(MapToModel(item));

            return finalList;
        }
        protected InstitucionSocialCuentaModel MapToModel(InstitucionSocialCuenta item)
        {
            return new InstitucionSocialCuentaModel()
            {
                ID = item.ID,
                ID_InstitucionSocial = item.ID_InstitucionSocial,
                Monto = item.Monto,
                MontoBloqueado = item.MontoBloqueado,
                MontoGastado = item.MontoGastado,
                Fecha = item.Fecha,
                Active = item.Active,
                Deleted = item.Deleted
            };
        }
        protected InstitucionSocialCuenta MapToDB(InstitucionSocialCuentaModel item)
        {
            return new InstitucionSocialCuenta()
            {
                ID = item.ID,
                ID_InstitucionSocial = item.ID_InstitucionSocial,
                Monto = item.Monto,
                MontoBloqueado = item.MontoBloqueado,
                MontoGastado = item.MontoGastado,
                Fecha = item.Fecha,
                Active = item.Active,
                Deleted = item.Deleted
            };
        }
        #endregion

        #region InstitucionSocialRedesSociales
        protected List<InstitucionSocialRedesSocialesModel> MapToModel(List<InstitucionSocialRedesSociales> list)
        {
            List<InstitucionSocialRedesSocialesModel> finalList = new List<InstitucionSocialRedesSocialesModel>();
            foreach (var item in list)
                finalList.Add(MapToModel(item));

            return finalList;
        }
        protected InstitucionSocialRedesSocialesModel MapToModel(InstitucionSocialRedesSociales item)
        {
            return new InstitucionSocialRedesSocialesModel()
            {
                ID = item.ID,
                ID_InstitucionSocial = item.ID_InstitucionSocial,
                ID_RedSocial = item.ID_RedSocial,
                SocialUserName = item.SocialUserName,
                Active = item.Active,
                Deleted = item.Deleted
            };
        }
        protected InstitucionSocialRedesSociales MapToDB(InstitucionSocialRedesSocialesModel item)
        {
            return new InstitucionSocialRedesSociales()
            {
                ID = item.ID,
                ID_InstitucionSocial = item.ID_InstitucionSocial,
                ID_RedSocial = item.ID_RedSocial,
                SocialUserName = item.SocialUserName,
                Active = item.Active,
                Deleted = item.Deleted
            };
        }
        #endregion

        #region Moneda
        protected List<MonedaModel> MapToModel(List<Moneda> list)
        {
            List<MonedaModel> finalList = new List<MonedaModel>();
            foreach (var item in list)
                finalList.Add(MapToModel(item));

            return finalList;
        }
        protected MonedaModel MapToModel(Moneda item)
        {
            return new MonedaModel()
            {
                ID = item.ID,
                Nombre = item.Nombre,
                Codigo = item.Codigo,
                Formato = item.Formato,
                Orden = item.Orden,
                Active = item.Active,
                Deleted = item.Deleted
            };
        }
        protected Moneda MapToDB(MonedaModel item)
        {
            return new Moneda()
            {
                ID = item.ID,
                Nombre = item.Nombre,
                Codigo = item.Codigo,
                Formato = item.Formato,
                Orden = item.Orden,
                Active = item.Active,
                Deleted = item.Deleted
            };
        }
        #endregion

        #region Pais
        protected List<PaisModel> MapToModel(List<Pais> list)
        {
            List<PaisModel> finalList = new List<PaisModel>();
            foreach (var item in list)
                finalList.Add(MapToModel(item));

            return finalList;
        }
        protected PaisModel MapToModel(Pais item)
        {
            return new PaisModel()
            {
                ID = item.ID,
                Nombre = item.Nombre,
                Active = item.Active,
                Deleted = item.Deleted
            };
        }
        protected Pais MapToDB(PaisModel item)
        {
            return new Pais()
            {
                ID = item.ID,
                Nombre = item.Nombre,
                Active = item.Active,
                Deleted = item.Deleted
            };
        }
        #endregion

        #region Plan
        protected List<PlanModel> MapToModel(List<Plan> list)
        {
            List<PlanModel> finalList = new List<PlanModel>();
            foreach (var item in list)
                finalList.Add(MapToModel(item));

            return finalList;
        }
        protected PlanModel MapToModel(Plan item)
        {
            return new PlanModel()
            {
                ID = item.ID,
                Nombre = item.Nombre,
                Descripcion = item.Descripcion,
                Active = item.Active,
                Deleted = item.Deleted
            };
        }
        protected Plan MapToDB(PlanModel item)
        {
            return new Plan()
            {
                ID = item.ID,
                Nombre = item.Nombre,
                Descripcion = item.Descripcion,
                Active = item.Active,
                Deleted = item.Deleted
            };
        }
        #endregion

        #region Plan_Caracteristica
        protected List<Plan_CaracteristicaModel> MapToModel(List<Plan_Caracteristica> list)
        {
            List<Plan_CaracteristicaModel> finalList = new List<Plan_CaracteristicaModel>();
            foreach (var item in list)
                finalList.Add(MapToModel(item));

            return finalList;
        }
        protected Plan_CaracteristicaModel MapToModel(Plan_Caracteristica item)
        {
            return new Plan_CaracteristicaModel()
            {
                ID = item.ID,
                ID_Plan = item.ID_Plan,
                Caracteristica = item.Caracteristica,
                Active = item.Active,
                Deleted = item.Deleted
            };
        }
        protected Plan_Caracteristica MapToDB(Plan_CaracteristicaModel item)
        {
            return new Plan_Caracteristica()
            {
                ID = item.ID,
                ID_Plan = item.ID_Plan,
                Caracteristica = item.Caracteristica,
                Active = item.Active,
                Deleted = item.Deleted
            };
        }
        #endregion

        #region Provincia
        protected List<ProvinciaModel> MapToModel(List<Provincia> list)
        {
            List<ProvinciaModel> finalList = new List<ProvinciaModel>();
            foreach (var item in list)
                finalList.Add(MapToModel(item));

            return finalList;
        }
        protected ProvinciaModel MapToModel(Provincia item)
        {
            return new ProvinciaModel()
            {
                ID = item.ID,
                ID_Pais = item.ID_Pais,
                Nombre = item.Nombre,
                Active = item.Active,
                Deleted = item.Deleted
            };
        }
        protected Provincia MapToDB(ProvinciaModel item)
        {
            return new Provincia()
            {
                ID = item.ID,
                ID_Pais = item.ID_Pais,
                Nombre = item.Nombre,
                Active = item.Active,
                Deleted = item.Deleted
            };
        }
        #endregion

        #region RedesSociales
        protected List<RedesSocialesModel> MapToModel(List<RedesSociales> list)
        {
            List<RedesSocialesModel> finalList = new List<RedesSocialesModel>();
            foreach (var item in list)
                finalList.Add(MapToModel(item));

            return finalList;
        }
        protected RedesSocialesModel MapToModel(RedesSociales item)
        {
            return new RedesSocialesModel()
            {
                ID = item.ID,
                Nombre = item.Nombre,
                Icono = item.Icono,
                RootURL = item.RootURL,
                Active = item.Active,
                Deleted = item.Deleted
            };
        }
        protected RedesSociales MapToDB(RedesSocialesModel item)
        {
            return new RedesSociales()
            {
                ID = item.ID,
                Nombre = item.Nombre,
                Icono = item.Icono,
                RootURL = item.RootURL,
                Active = item.Active,
                Deleted = item.Deleted
            };
        }
        #endregion

        #region UserSession
        protected List<UserSessionModel> MapToModel(List<UserSession> list)
        {
            List<UserSessionModel> finalList = new List<UserSessionModel>();
            foreach (var item in list)
                finalList.Add(MapToModel(item));

            return finalList;
        }
        protected UserSessionModel MapToModel(UserSession item)
        {
            return new UserSessionModel()
            {
                UserSessionGUID = item.UserSessionGUID,
                ID_Usuario = item.ID_Usuario,
                UltimoAccesso = item.UltimoAccesso,
                UltimaTrx = item.UltimaTrx
            };
        }
        protected UserSession MapToDB(UserSessionModel item)
        {
            return new UserSession()
            {
                UserSessionGUID = item.UserSessionGUID,
                ID_Usuario = item.ID_Usuario,
                UltimoAccesso = item.UltimoAccesso,
                UltimaTrx = item.UltimaTrx
            };
        }
        #endregion

        #region Usuario
        protected List<UsuarioModel> MapToModel(List<Usuario> list)
        {
            List<UsuarioModel> finalList = new List<UsuarioModel>();
            foreach (var item in list)
                finalList.Add(MapToModel(item));

            return finalList;
        }
        protected UsuarioModel MapToModel(Usuario item)
        {
            return new UsuarioModel()
            {
                ID = item.ID,
                Nombre = item.Nombre,
                Email = item.Email,
                AuthenticationMode = item.AuthenticationMode,
                Pwd = item.Pwd,
                Telefono = item.Telefono,
                Direccion = item.Direccion,
                UltimoAcceso = item.UltimoAcceso,
                ID_RolUsuario = item.ID_RolUsuario,
                Active = item.Active,
                Deleted = item.Deleted
            };
        }
        protected Usuario MapToDB(UsuarioModel item)
        {
            return new Usuario()
            {
                ID = item.ID,
                Nombre = item.Nombre,
                Email = item.Email,
                AuthenticationMode = item.AuthenticationMode,
                Pwd = item.Pwd,
                Telefono = item.Telefono,
                Direccion = item.Direccion,
                UltimoAcceso = item.UltimoAcceso,
                ID_RolUsuario = item.ID_RolUsuario,
                Active = item.Active,
                Deleted = item.Deleted
            };
        }
        #endregion

        #region UsuarioAccion
        protected List<UsuarioAccionModel> MapToModel(List<UsuarioAccion> list)
        {
            List<UsuarioAccionModel> finalList = new List<UsuarioAccionModel>();
            foreach (var item in list)
                finalList.Add(MapToModel(item));

            return finalList;
        }
        protected UsuarioAccionModel MapToModel(UsuarioAccion item)
        {
            return new UsuarioAccionModel()
            {
                ID = item.ID,
                Nombre = item.Nombre,
                Codigo = item.Codigo,
                Deleted = item.Deleted,
                Active = item.Active
            };
        }
        protected UsuarioAccion MapToDB(UsuarioAccionModel item)
        {
            return new UsuarioAccion()
            {
                ID = item.ID,
                Nombre = item.Nombre,
                Codigo = item.Codigo,
                Deleted = item.Deleted,
                Active = item.Active
            };
        }
        #endregion

        #region UsuarioRol
        protected List<UsuarioRolModel> MapToModel(List<UsuarioRol> list)
        {
            List<UsuarioRolModel> finalList = new List<UsuarioRolModel>();
            foreach (var item in list)
                finalList.Add(MapToModel(item));

            return finalList;
        }
        protected UsuarioRolModel MapToModel(UsuarioRol item)
        {
            return new UsuarioRolModel()
            {
                ID = item.ID,
                Nombre = item.Nombre,
                Codigo = item.Codigo,
                Deleted = item.Deleted,
                Active = item.Active
            };
        }
        protected UsuarioRol MapToDB(UsuarioRolModel item)
        {
            return new UsuarioRol()
            {
                ID = item.ID,
                Nombre = item.Nombre,
                Codigo = item.Codigo,
                Deleted = item.Deleted,
                Active = item.Active
            };
        }
        #endregion

        #region Vendedor
        protected List<VendedorModel> MapToModel(List<Vendedor> list)
        {
            List<VendedorModel> finalList = new List<VendedorModel>();
            foreach (var item in list)
                finalList.Add(MapToModel(item));

            return finalList;
        }
        protected VendedorModel MapToModel(Vendedor item)
        {
            return new VendedorModel()
            {
                ID = item.ID,
                Nombre = item.Nombre,
                Apellido = item.Apellido,
                Email = item.Email,
                CUIT = item.CUIT,
                Password = item.Password,
                Telefono = item.Telefono,
                Avatar = item.Avatar,
                Direccion = item.Direccion,
                ID_Ciudad = item.ID_Ciudad,
                Active = item.Active,
                Deleted = item.Deleted
            };
        }
        protected Vendedor MapToDB(VendedorModel item)
        {
            return new Vendedor()
            {
                ID = item.ID,
                Nombre = item.Nombre,
                Apellido = item.Apellido,
                Email = item.Email,
                CUIT = item.CUIT,
                Password = item.Password,
                Telefono = item.Telefono,
                Avatar = item.Avatar,
                Direccion = item.Direccion,
                ID_Ciudad = item.ID_Ciudad,
                Active = item.Active,
                Deleted = item.Deleted
            };
        }
        #endregion

        #region VendedorConfiguraciones
        protected List<VendedorConfiguracionesModel> MapToModel(List<VendedorConfiguraciones> list)
        {
            List<VendedorConfiguracionesModel> finalList = new List<VendedorConfiguracionesModel>();
            foreach (var item in list)
                finalList.Add(MapToModel(item));

            return finalList;
        }
        protected VendedorConfiguracionesModel MapToModel(VendedorConfiguraciones item)
        {
            return new VendedorConfiguracionesModel()
            {
                ID = item.ID,
                ID_Vendedor = item.ID_Vendedor,
                StringKey = item.StringKey,
                StringValue = item.StringValue,
                CreatedDate = item.CreatedDate,
                LastUpdate = item.LastUpdate,
                Published = item.Published,
                Deleted = item.Deleted
            };
        }
        protected VendedorConfiguraciones MapToDB(VendedorConfiguracionesModel item)
        {
            return new VendedorConfiguraciones()
            {
                ID = item.ID,
                ID_Vendedor = item.ID_Vendedor,
                StringKey = item.StringKey,
                StringValue = item.StringValue,
                CreatedDate = item.CreatedDate,
                LastUpdate = item.LastUpdate,
                Published = item.Published,
                Deleted = item.Deleted
            };
        }
        #endregion

        #region VendedorCuenta
        protected List<VendedorCuentaModel> MapToModel(List<VendedorCuenta> list)
        {
            List<VendedorCuentaModel> finalList = new List<VendedorCuentaModel>();
            foreach (var item in list)
                finalList.Add(MapToModel(item));

            return finalList;
        }
        protected VendedorCuentaModel MapToModel(VendedorCuenta item)
        {
            return new VendedorCuentaModel()
            {
                ID = item.ID,
                ID_Vendedor = item.ID_Vendedor,
                Monto = item.Monto,
                MontoBloqueado = item.MontoBloqueado,
                MontoGastado = item.MontoGastado,
                Fecha = item.Fecha,
                Active = item.Active,
                Deleted = item.Deleted
            };
        }
        protected VendedorCuenta MapToDB(VendedorCuentaModel item)
        {
            return new VendedorCuenta()
            {
                ID = item.ID,
                ID_Vendedor = item.ID_Vendedor,
                Monto = item.Monto,
                MontoBloqueado = item.MontoBloqueado,
                MontoGastado = item.MontoGastado,
                Fecha = item.Fecha,
                Active = item.Active,
                Deleted = item.Deleted
            };
        }
        #endregion

        #region VendedorRedesSociales
        protected List<VendedorRedesSocialesModel> MapToModel(List<VendedorRedesSociales> list)
        {
            List<VendedorRedesSocialesModel> finalList = new List<VendedorRedesSocialesModel>();
            foreach (var item in list)
                finalList.Add(MapToModel(item));

            return finalList;
        }
        protected VendedorRedesSocialesModel MapToModel(VendedorRedesSociales item)
        {
            return new VendedorRedesSocialesModel()
            {
                ID = item.ID,
                ID_Vendedor = item.ID_Vendedor,
                ID_RedSocial = item.ID_RedSocial,
                SocialUserName = item.SocialUserName,
                Active = item.Active,
                Deleted = item.Deleted
            };
        }
        protected VendedorRedesSociales MapToDB(VendedorRedesSocialesModel item)
        {
            return new VendedorRedesSociales()
            {
                ID = item.ID,
                ID_Vendedor = item.ID_Vendedor,
                ID_RedSocial = item.ID_RedSocial,
                SocialUserName = item.SocialUserName,
                Active = item.Active,
                Deleted = item.Deleted
            };
        }
        #endregion

        #region Ventas
        protected List<VentasModel> MapToModel(List<Ventas> list)
        {
            List<VentasModel> finalList = new List<VentasModel>();
            foreach (var item in list)
                finalList.Add(MapToModel(item));

            return finalList;
        }
        protected VentasModel MapToModel(Ventas item)
        {
            return new VentasModel()
            {
                ID = item.ID,
                ID_Empresa = item.ID_Empresa,
                ID_Vendedor = item.ID_Vendedor,
                Periodo = item.Periodo,
                FechaMovimiento = item.FechaMovimiento,
                FechaLiquidacion = item.FechaLiquidacion,
                MontoACobrar = item.MontoACobrar,
                MontoAPagar = item.MontoAPagar,
                MontoSocial = item.MontoSocial,
                Cobrado = item.Cobrado,
                Pagado = item.Pagado,
                Active = item.Active,
                Deleted = item.Deleted
            };
        }
        protected Ventas MapToDB(VentasModel item)
        {
            return new Ventas()
            {
                ID = item.ID,
                ID_Empresa = item.ID_Empresa,
                ID_Vendedor = item.ID_Vendedor,
                Periodo = item.Periodo,
                FechaMovimiento = item.FechaMovimiento,
                FechaLiquidacion = item.FechaLiquidacion,
                MontoACobrar = item.MontoACobrar,
                MontoAPagar = item.MontoAPagar,
                MontoSocial = item.MontoSocial,
                Cobrado = item.Cobrado,
                Pagado = item.Pagado,
                Active = item.Active,
                Deleted = item.Deleted
            };
        }
        #endregion
        #endregion
    }
}