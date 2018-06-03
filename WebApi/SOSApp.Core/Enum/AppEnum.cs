using WhiteRaven.Core.Helper;

namespace WhiteRaven.Core.Enum
{
    public enum OrderDirectionEnum
    {
        [EnumDescription("ASC")]
        ACS = 10,

        [EnumDescription("DESC")]
        DESC = 20
    }

    public enum ProfileTypeEnum
    {
    }

    public enum AppErrorCodeEnum
    {
        [EnumDescription("Generic")]
        Generic = 10,
        [EnumDescription("Data Base")]
        DataBase = 20
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

    public enum UserType
    {
        [EnumDescription("usuario")]
        Usuario = 1,
        [EnumDescription("administrador")]
        Administrador = 2,
        [EnumDescription("empresa")]
        Empresa = 3,
        [EnumDescription("vendedor")]
        Vendedor = 4,
        [EnumDescription("institucion")]
        Institucion = 5
    }

    public enum PaymentProvider
    {
        [EnumDescription("Mercado Pago")]
        MercadoPago = 1,
    }

    public enum PaymentStatus
    {
        [EnumDescription("Exito")]
        Success = 1,
        [EnumDescription("Error")]
        Errror = 2,
    }

    public enum PaymentType
    {
        [EnumDescription("Cobro")]
        Charge = 1,
        [EnumDescription("Devolución")]
        Refund = 2,
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

    public enum LangEnum
    {
        [EnumDescription("Español")]
        ES = 10,

        [EnumDescription("English")]
        EN = 20,

        [EnumDescription("Portugués")]
        PT = 30
    }

    public enum CurrencyEnum
    {
        [EnumDescription("DOL")]
        USD = 10,
        [EnumDescription("PES")]
        ARS = 20

    }

    public enum SaleConditionEnum
    {
        [EnumDescription("Contado")]
        Cash = 0,
        [EnumDescription("Cuenta Corriente")]
        CurrentAccount = 1,
        [EnumDescription("Tarjeta Débito")]
        DebitCard = 2,
        [EnumDescription("Tarjeta Crédito")]
        CreditCard = 3,
        [EnumDescription("Cheque")]
        Check = 4,
        [EnumDescription("Ticket")]
        Ticket = 5,
        [EnumDescription("Otra")]
        Other = 6,

        //Invoicing
        [EnumDescription("Transferencia Bancaria")]
        BankTransfer = 100
    }

    public enum InvoiceTypeEnum
    {
        [EnumDescription("Factura A")]
        A = 01,
        [EnumDescription("Factura B")]
        B = 06,
        [EnumDescription("Factura C")]
        C = 11,
        [EnumDescription("Nota de Débito A")]
        NDA = 02,
        [EnumDescription("Nota de Crédito A")]
        NCA = 03,
        [EnumDescription("Nota de Débito B")]
        NDB = 07,
        [EnumDescription("Nota de Crédito B")]
        NCB = 08,
        [EnumDescription("Nota de Débito C")]
        NDC = 12,
        [EnumDescription("Nota de Crédito C")]
        NCC = 13,

        //[EnumDescription("Comprobante X")]
        //X = 500,
        //[EnumDescription("ReciWhiteRaven")]
        //R = 510,
        [EnumDescription("Invoice")]
        I = 1000,
        [EnumDescription("Credit Note")]
        CNI = 1100,
        [EnumDescription("Debit Note")]
        DNI = 1200,

        [EnumDescription("Ticket")]
        TKT = 1300
    }

    public enum LegalEntity
    {
        [EnumDescription("Persona Física")]
        F = 1,
        [EnumDescription("Persona Jurídica")]
        J = 2,
    }

    //Basados en la documentacion de AFIP
    public enum InvoiceAFIPTypeEnum
    {
        [EnumDescription("Factura A")]
        A = 01,
        [EnumDescription("Factura B")]
        B = 06,
        [EnumDescription("Factura C")]
        C = 11,
        [EnumDescription("Nota de Débito A")]
        NDA = 02,
        [EnumDescription("Nota de Crédito A")]
        NCA = 03,
        [EnumDescription("Nota de Débito B")]
        NDB = 07,
        [EnumDescription("Nota de Crédito B")]
        NCB = 08,
        [EnumDescription("Nota de Débito C")]
        NDC = 12,
        [EnumDescription("Nota de Crédito C")]
        NCC = 13
    }

    public enum ConditionalFiscalEnum
    {
        [EnumDescription("Exento")]
        E = 10,
        [EnumDescription("Resp. Inscripto")]
        I = 20,
        [EnumDescription("Monotributista")]
        M = 30,
        [EnumDescription("No Responsable")]
        NR = 40,
        [EnumDescription("Consumidor Final")]
        CF = 50,
        [EnumDescription("Responsable No Inscripto")]
        NI = 60
    }

    public enum AlicuotaEnum
    {
        [EnumDescription("0")]
        A = 3,
        [EnumDescription("10.5")]
        B = 4,
        [EnumDescription("21")]
        C = 5,
        [EnumDescription("27")]
        D = 6,
        [EnumDescription("5")]
        E = 8,
        [EnumDescription("2.5")]
        F = 9
    }

    public enum DocTypeEnum
    {
        [EnumDescription("DNI")]
        DNI = 96,
        [EnumDescription("CUIT")]
        CUIT = 80,
        [EnumDescription("CUIL")]
        CUIL = 86,
        [EnumDescription("CI Extranjera")]
        CI = 91,
        [EnumDescription("CDI")]
        CDI = 87,
        [EnumDescription("LE")]
        LE = 89,
        [EnumDescription("LC")]
        LC = 90,
        [EnumDescription("Pasaporte")]
        PAS = 94,
        [EnumDescription("Doc. (Otro)")]
        O = 99
    }

    public enum ProductTypeEnum
    {
        [EnumDescription("Servicios Turísticos")]
        S = 10

         
    }

    public enum ConceptoAfipEnum
    {
        [EnumDescription("Productos")]
        P = 1,
        [EnumDescription("Servicios")]
        S = 2,
        [EnumDescription("Productos y Servicios")]
        PS = 3
    }


    //Termino Basados Documentos AFIP
    public enum CodeErrorAfip
    {
        [EnumDescription("Error interno de aplicación.")]
        I = 500,
        [EnumDescription("Error interno de base de datos.")]
        DB = 501,
        [EnumDescription("Error interno de base de datos - Autorizador CAE / Régimen CAEA – Transacción Activa.")]
        DBAUT = 502,
        [EnumDescription("No se corresponden token y firma. Usuario no autorizado a realizar esta operación.")]
        SIGN = 600,
        [EnumDescription("CUIT representada no incluida en token.")]
        CUIT = 601,
        [EnumDescription("No existen datos en nuestros registros.")]
        NODATA = 602
    }


    public enum PageCodeEnum
    {
        [EnumDescription("ALL")]
        ALL = 10
    }

    public enum CountryEnum
    {
        [EnumDescription("AR")]
        Argentina = 10
    }

    public enum CustomerTypeEnum
    {
        [EnumDescription("I")]
        International = 10,

        [EnumDescription("N")]
        National = 20
    }

    public enum SupplierTypeEnum
    {
        [EnumDescription("Costos")]
        Cost = 10,

        [EnumDescription("Gastos")]
        Expense = 20
    }

    public enum DropDownEnum
    {
        [EnumDescription("Seleccione..")]
        Seleccione = 0,
        [EnumDescription("Ninguno..")]
        Ninguno = -1,
        [EnumDescription("Todos")]
        Todos = -2
    }

    public enum InvoiceCCTypeEnum
    {
        [EnumDescription("Factura")]
        FX = 10,
        [EnumDescription("Nota de Crédito")]
        NCX = 20
    }

    public enum PaymentMethodEnum
    {
        [EnumDescription("Efectivo")]
        Cash = 10,
        [EnumDescription("Tarjeta Débito")]
        DebitCard = 20,
        [EnumDescription("Tarjeta Crédito")]
        CreditCard = 30,
        [EnumDescription("Cheque")]
        Check = 40,
        [EnumDescription("Transferencia Banc.")]
        BankTransfer = 50,
        [EnumDescription("Depósito Banc.")]
        BankDeposit = 60,
        [EnumDescription("Otra")]
        Other = 100
    }

    public enum FileItemTypeEnum
    {
        [EnumDescription("Servicio")]
        Service = 10,
        [EnumDescription("Habitación")]
        Room = 20,
        [EnumDescription("Serv. Especial")]
        SpecialService = 30
    }

    public enum SupplierInvoiceStatusEnum
    {
        [EnumDescription("Pendiente")]
        Pending = 10,
        [EnumDescription("Vencido")]
        Overdue = 20,
        [EnumDescription("Pago")]
        Paid = 30
    }

    public enum FileStatusEnum
    {
        [EnumDescription("Cotización")]
        Quote = 5,
        [EnumDescription("Reserva")]
        Reservation = 10,
        [EnumDescription("En Proceso")]
        InProcess = 20,
        [EnumDescription("Cerrado")]
        Closed = 30,
        [EnumDescription("Facturado")]
        Invoiced = 40
    }

    public enum PayOrderTypeEnum
    {
        [EnumDescription("Out")]
        Out = 10
    }

    public enum PaxTypeEnum
    {
        [EnumDescription("ADL")]
        ADL = 10,
        [EnumDescription("CHD")]
        CHD = 20,
        [EnumDescription("INF")]
        INF = 30
    }

    public enum RateTypeEnum
    {
        //Hoteles
        [EnumDescription("Single")]
        Single = 1,
        [EnumDescription("Doble")]
        Doble = 2,
        [EnumDescription("Triple")]
        Triple = 3,

        [EnumDescription("Tour")]
        Tour = 4,
        [EnumDescription("Cruise")]
        Cruise = 5,

        //Servicios
        [EnumDescription("Regular")]
        Regular = 6,
        [EnumDescription("Privado")]
        Privado = 7,
        [EnumDescription("Menor Regular")]
        MenorRegular = 8,
        [EnumDescription("Menor Privado")]
        MenorPrivado = 9,
        [EnumDescription("Solo Show")]
        Show = 18,
        [EnumDescription("Cena Show VIP")]
        ShowVIP = 19
    }

    //Sólo es necesario para las consultas a la 
    //base de datos vieja
    public enum WildcardEnumBG
    {
        [EnumDescription("Servicio")]
        Service = 20
    }

    public enum PayOrderStatusEnum
    {
        [EnumDescription("Pendiente")]
        Pending = 10,
        [EnumDescription("Vencido")]
        Overdue = 20,
        [EnumDescription("Pago")]
        Paid = 30,
        [EnumDescription("Anulado")]
        Annulled = 40
    }

    public enum ChargeOrderStatusEnum
    {
        [EnumDescription("Pendiente")]
        Pending = 10,
        [EnumDescription("Vencido")]
        Overdue = 20,
        [EnumDescription("Pago")]
        Paid = 30,
        [EnumDescription("Anulado")]
        Annulled = 40
    }

    public enum TransactionObjectTypeEnum
    {
        [EnumDescription("Órden de Pago")]
        PayOrder = 10,
        [EnumDescription("Órden de Cobro")]
        ChargeOrder = 20,
        [EnumDescription("ReciWhiteRaven")]
        Receipt = 30
    }

    public enum FileItemDetailTypeEnum
    {
        [EnumDescription("Núm. de Vuelo")]
        FlightNumber = 10,
        [EnumDescription("Horario de Vuelo")]
        FlightSchedule = 20,
        [EnumDescription("Comentario")]
        Comment = 30,
        [EnumDescription("Horario de Salida")]
        OutSchedule = 40,
        [EnumDescription("Comentario de Hab.")]
        RoomComment = 50
    }

    public enum FlightClassTypeEnum
    {
        [EnumDescription("Primera")]
        First = 10,
        [EnumDescription("Turista")]
        Tourist = 20,
        [EnumDescription("Promoción")]
        Promotion = 30
    }

    public enum TicketTypeEnum
    {
        [EnumDescription("CaWhiteRaventaje ")]
        Domestic = 10,
        [EnumDescription("Internacional")]
        International = 20
    }
}
