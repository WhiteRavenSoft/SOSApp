using SOSApp.Core.Helper;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOSApp.Core.Reporting
{
    public class PdfOutput
    {
        public String OutputFilePath { get; set; }
        public Stream OutputStream { get; set; }
        public Action<PdfDocument, byte[]> OutputCallback { get; set; }
    }

    public class PdfDocument
    {
        public String Url { get; set; }
        public String HeaderUrl { get; set; }
        public String FooterUrl { get; set; }
        public object State { get; set; }
    }

    public class PdfConvertEnvironment
    {
        public String TempFolderPath { get; set; }
        public String WkHtmlToPdfPath { get; set; }
        public int Timeout { get; set; }
        public bool Debug { get; set; }
    }

    public class PdfConvert
    {
        static PdfConvertEnvironment env;

        public static PdfConvertEnvironment Environment
        {
            get
            {
                if (env == null)
                {
                    env = new PdfConvertEnvironment
                    {
                        TempFolderPath = Path.GetTempPath(),
                        Timeout = (5 * 60 * 1000)
                    };

                    if (AppHelper.LoadAppSettingboolean("Advisor.Environment.x64", true))
                        env.WkHtmlToPdfPath = @"C:\Program Files\wkhtmltopdf\bin\wkhtmltopdf.exe";
                    else
                        env.WkHtmlToPdfPath = @"C:\Program Files (x86)\wkhtmltopdf\bin\wkhtmltopdf.exe";
                }

                return env;
            }
        }

        public static void ConvertHtmlToPdf(PdfDocument document, PdfOutput output)
        {
            ConvertHtmlToPdf(document, null, output);
        }

        public static void ConvertHtmlToPdf(PdfDocument document, PdfConvertEnvironment environment, PdfOutput woutput)
        {
            if (environment == null)
                environment = Environment;

            String outputPdfFilePath;
            bool delete;
            if (woutput.OutputFilePath != null)
            {
                outputPdfFilePath = woutput.OutputFilePath;
                delete = false;
            }
            else
            {
                outputPdfFilePath = Path.Combine(environment.TempFolderPath, String.Format("{0}.pdf", Guid.NewGuid()));
                delete = true;
            }

            if (!File.Exists(environment.WkHtmlToPdfPath))
                throw new Exception(String.Format("File '{0}' not found. Check if wkhtmltopdf application is installed.", environment.WkHtmlToPdfPath));

            ProcessStartInfo si;

            StringBuilder paramsBuilder = new StringBuilder();
            paramsBuilder.Append("--page-size A4 ");
            //paramsBuilder.Append("--redirect-delay 0 "); not available in latest version
            if (!string.IsNullOrEmpty(document.HeaderUrl))
            {
                paramsBuilder.AppendFormat("--header-html {0} ", document.HeaderUrl);
                paramsBuilder.Append("--margin-top 25 ");
                paramsBuilder.Append("--header-spacing 5 ");
            }
            if (!string.IsNullOrEmpty(document.FooterUrl))
            {
                paramsBuilder.AppendFormat("--footer-html {0} ", document.FooterUrl);
                paramsBuilder.Append("--margin-SOSAppttom 25 ");
                paramsBuilder.Append("--footer-spacing 5 ");
            }

            paramsBuilder.AppendFormat("\"{0}\" \"{1}\"", document.Url, outputPdfFilePath);


            si = new ProcessStartInfo();
            si.CreateNoWindow = true; //!environment.Debug;
            si.FileName = environment.WkHtmlToPdfPath;
            si.Arguments = paramsBuilder.ToString();
            si.UseShellExecute = false;
            si.RedirectStandardError = true; //!environment.Debug;

            try
            {
                using (var process = new Process())
                {
                    process.StartInfo = si;
                    process.Start();

                    try
                    {
                        var output = process.StandardError.ReadToEnd();

                        if (!output.Trim().EndsWith("Done", StringComparison.InvariantCultureIgnoreCase))
                            throw new Exception(output);

                        process.WaitForExit();
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    finally
                    {
                        process.Close();
                    }
                }
            }
            finally
            {
                if (delete && File.Exists(outputPdfFilePath))
                    File.Delete(outputPdfFilePath);
            }
        }
    }
}