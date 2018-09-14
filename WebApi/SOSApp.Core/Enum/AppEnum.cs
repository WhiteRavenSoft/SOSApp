using SOSApp.Core.Helper;

namespace SOSApp.Core.Enum
{
    public enum OrderDirectionEnum
    {
        [EnumDescription("ASC")]
        ACS = 10,

        [EnumDescription("DESC")]
        DESC = 20
    }

    public enum AuthenticationModeEnum
    {
        [EnumDescription("Basic")]
        Basic = 10,
        [EnumDescription("Facebook")]
        Facebook = 20,
        [EnumDescription("Google")]
        Google = 30
    }

    public enum AuthMode
    {

        [EnumDescription("Aplicación")]
        Application = 1,
        [EnumDescription("Facebook")]
        Facebook = 2,
        [EnumDescription("GooglePlus")]
        GooglePlus = 3,
        [EnumDescription("Admin")]
        Admin = 4
    }

    public enum ErrorCodeEnum
    {
        [EnumDescription("C001 Credenciales inválidas")]
        InvalidCredentials = 1,
        [EnumDescription("C002 Access Token inválido")]
        InvalidAccessToken = 2,
        [EnumDescription("C003 Email inválido")]
        InvalidEmail = 3
    }

    public enum AppErrorCodeEnum
    {
        [EnumDescription("Generic")]
        Generic = 10,
        [EnumDescription("Data Base")]
        DataBase = 20
    }

    public enum StringResourcesEnum
    {
        [EnumDescription("N/A")]
        NA = 10,

        [EnumDescription("")]
        Empty = 20,

        [EnumDescription("-")]
        Dash = 30
    }

    public enum LogSeverityEnum
    {
        Info = 10,
        Warning = 20,
        AppError = 30
    }

    public enum ContentTypeEnum
    {
        [EnumDescription("text/html")]
        HTML = 10,

        [EnumDescription("application/json")]
        JSON = 20
    }

    public enum CountryEnum
    {
        [EnumDescription("AR")]
        Argentina = 10
    }

    public enum DropDownEnum
    {
        [EnumDescription("Seleccione...")]
        Seleccione = 0,
        [EnumDescription("Ninguno...")]
        Ninguno = -1,
        [EnumDescription("Todos")]
        Todos = -2
    }

    public enum UserRoleEnum
    {
        [EnumDescription("Administrador")]
        Administrador = 1,
        [EnumDescription("Editor")]
        Editor = 2,
        [EnumDescription("Usuario Mobile")]
        UserMobile = 3
    }
}