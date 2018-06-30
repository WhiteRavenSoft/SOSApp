using SOSApp.Svc.Cache;
using SOSApp.Core.Enum;
using SOSApp.Core.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SOSApp.Svc.Infrastructure;
using SOSApp.Svc.DataService;
using SOSApp.Data.DBModel;

namespace SOSApp.Svc.Helper
{
    public static class CodeHelper
    {
        //2 Grupos de Proveedores: Costos(C) y Gastos(G).

        //Código de ejemplo PCBR: P = Proveedor, C = Costos, BR = código ISO de la Tabla de Países.Proveedores de productos que corresponden y representan un costo.Ejemplos: Sheraton, Proveedor de Traslados.

        //Código de ejemplo PGAR: P = Proveedor, G = Gastos, AR = código ISO de la Tabla de Países.
        //Proveedores de productos que corresponden y representan un gasto.Ejemplos: Claro, Gas, Luz.


        //Código de ejemplo CIBR: C = Cliente, I = Internaciona, BR = código ISO de la Tabla de Países.
        //Código de ejemplo CNAR: C = Cliente, N = Nacional, AR = código ISO de la Tabla de Países.


        public static string LoadSupplierCode(int id, int country, bool expense = false)
        {
            //var object1 = WebCache.Instance.LoadCiudad(Ciudad);

            if (country != 0)
            {
                var type = "C";

                if (expense)
                    type = "G";

                var idcode = string.Format("{0:0000000000}", id);

                return string.Format("P{0}{1}", type, idcode).ToUpper();
            }

            return string.Empty;
        }

        public static string LoadGuideCode(int code)
        {
            return string.Format("{0:D5}", code);
        }

        public static string LoadInvoiceBranch(int number)
        {
            if (number == 0)
                return string.Empty;

            return string.Format("{0:000}", number);
        }

        public static string LoadInvoiceNumber(int number)
        {
            if (number == 0)
                return string.Empty;

            return string.Format("{0:00000000}", number);
        }

        public static string SetProductTypeCode(string ptcode)
        {
            return string.Format("{0:D5}", ptcode.ToInt());
        }

        public static string LoadInvoicingCode(int number)
        {
            var inv = string.Format("{0:D8}", number);

            var pofs = string.Format("{0:D5}", AppHelper.LoadAppSettingInteger("SOSApp.WebApp.Invoices.PointOfSale", 0));

            return string.Format("{0}-{1}", pofs, inv);
        }

        public static string Load4Code(int number)
        {
            return string.Format("{0:D4}", number);
        }

        public static string Load8Code(int number)
        {
            return string.Format("{0:D8}", number);
        }

        public static string Load3Code(int number)
        {
            return string.Format("{0:D3}", number);
        }

        public static string SetCashFlowCategoryCode(string ptcode)
        {
            return string.Format("{0:D5}", ptcode.ToInt());
        }

        public static string SetPayOrderCode(int code)
        {
            return string.Format("OP{0:D10}", code);
        }

        public static string SetChargeOrderCode(int code)
        {
            return string.Format("OC{0:D10}", code);
        }
    }
}
