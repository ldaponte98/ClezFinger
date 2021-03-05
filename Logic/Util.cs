using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Neurotec.Biometrics;
using System.IO;
using System.Reflection;
using System.Diagnostics;
using Neurotec;

namespace Logic
{
    public static class Util
    {

        public static bool GetTrialModeFlag()
        {
            var filePath = @"./../Licenses/TrialFlag.txt";
            if (File.Exists(filePath))
            {
                var lines = File.ReadAllLines(filePath);
                if (lines.Length > 0 && lines[0].Trim().ToLower().Equals("true"))
                {
                    return true;
                }
            }
            else
            {
                Console.WriteLine("Failed to locate file: " + Path.GetFullPath(filePath));
            }
            return false;
        }
        public static void PrintTutorialHeader(string[] args)
        {
            string description = ((AssemblyDescriptionAttribute)Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false)[0]).Description;
            string version = FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).FileVersion;
            string copyright = ((AssemblyCopyrightAttribute)Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false)[0]).Copyright;
            Console.WriteLine(GetAssemblyName());
            Console.WriteLine("");
            Console.WriteLine(@"{0} (Version: {1})", description, version);
            Console.WriteLine(copyright.Replace("?", "(C)"));
            Console.WriteLine();
            if (args != null && args.Length > 0)
            {
                Console.WriteLine("Arguments:");
                foreach (string item in args)
                {
                    Console.WriteLine("\t{0}", item);
                }
                Console.WriteLine();
            }
        }

        public static string GetAssemblyName()
        {
            return Assembly.GetExecutingAssembly().GetName().Name;
        }

        public static int PrintException(Exception ex)
        {
            int errorCode = -1;
            Console.WriteLine(ex);

            while ((ex is AggregateException) && (ex.InnerException != null))
                ex = ex.InnerException;

            var neurotecException = ex as INeurotecException;
            if (neurotecException != null)
            {
                errorCode = neurotecException.Code;
            }
            return errorCode;
        }
    }
}
