using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace KPLEnvioCorreo
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static async Task  Main(string[] args)
        {
            ServiceBase[] ServicesToRun;


            if (Environment.UserInteractive)
            {
                var oApp = new EnvioCorreoSmtp();
                //oApp.IniciarComoConsola (args);
                //Console.WriteLine("Servicio iniciado... Presione Enter para detenerlo.");
                //Console.ReadLine();
                //oApp.DetenerComoConsola();
                await oApp.EnvioCorreo(1);
            }
            else
            {
                ServicesToRun = new ServiceBase[]
                    {
                        new EnvioCorreoSmtp()
                    };
                ServiceBase.Run(ServicesToRun);
            }



        }
    }
}
