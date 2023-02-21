using System.Text;
using CommandLine;
using CryptPdf;
using PdfSharpCore.Pdf;
using PdfSharpCore.Pdf.IO;
using PdfSharpCore.Pdf.Security;

Parser.Default.ParseArguments<Options>(args)
    .WithParsed(o => {

        String tmpFile = $"{Path.GetTempPath()}{Path.GetRandomFileName()}";
        File.Copy(o.Input, tmpFile, true);

        PdfDocument pdfDocument = PdfReader.Open(tmpFile, PdfDocumentOpenMode.Modify);
        PdfSecuritySettings securitySettings = pdfDocument.SecuritySettings;

        String? password = o.Password;
        if (String.IsNullOrEmpty(password)) {
            while (String.IsNullOrEmpty(password)) {
                Console.Write("Password: ");
                password = readKeys();
                Console.Write("Password confirmation: ");
                String? passwordConfirmation = readKeys();
                if (passwordConfirmation != password) {
                    Console.WriteLine("Password don't match !");
                    password = null;
                }
            }
        }

        securitySettings.UserPassword = password;

        securitySettings.PermitAccessibilityExtractContent = false;
        securitySettings.PermitAnnotations = false;
        securitySettings.PermitAssembleDocument = false;
        securitySettings.PermitExtractContent = false;
        securitySettings.PermitFormsFill = false;
        securitySettings.PermitFullQualityPrint = false;
        securitySettings.PermitModifyDocument = false;
        securitySettings.PermitPrint = false;
        securitySettings.PermitFullQualityPrint = false;

        pdfDocument.Save(o.Output);

        File.Delete(tmpFile);
    });

static String? readKeys() {
    StringBuilder sb = new StringBuilder();

    ConsoleKeyInfo keyInfo;
    do {
        keyInfo = Console.ReadKey(intercept: true);

        if (keyInfo.Key == ConsoleKey.Backspace && sb.Length > 0) {
            Console.Write("\b \b");
            sb.Remove(sb.Length - 1, 1);
        } else if (!char.IsControl(keyInfo.KeyChar)) {
            Console.Write("*");
            sb.Append(keyInfo.KeyChar);
        }
    } while (keyInfo.Key != ConsoleKey.Enter);

    Console.WriteLine();

    return sb.ToString();
}