using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using KPLEnvioCorreo.Clases;
using System.Timers;

namespace KPLEnvioCorreo
{
    partial class EnvioCorreoSmtp : ServiceBase
    {
        private Timer timerpedido;
        private Timer timercotizacionporvencer3dias;
        private Timer timercotizacionporvencervencido;
        private Timer timercotizacionporvencervencidoPerdido;

        public EnvioCorreoSmtp()
        {
            InitializeComponent();
        }
        public void IniciarComoConsola(string[] args)
        {
            OnStart(args);
        }

        public void DetenerComoConsola()
        {
            OnStop();
        }

        protected override void OnStart(string[] args)
        {
            // TODO: Add code here to start your service.
            EventLog.WriteEntry("Inicio servicio", EventLogEntryType.Information);

            var now = DateTime.Now;
            var nextRun = DateTime.Today.AddHours(7); // Mediodía hoy
            if (now > nextRun)
            {
                nextRun = nextRun.AddDays(1);
            }

            var initialDelay = nextRun - now;


            timercotizacionporvencer3dias = new Timer();
            timercotizacionporvencer3dias.Interval = 120000;
            timercotizacionporvencer3dias.AutoReset = false;
            timercotizacionporvencer3dias.Elapsed += OnElapsedTimertimercotizacionporvencer3dias;
            timercotizacionporvencer3dias.Start();

            timercotizacionporvencervencido = new Timer();
            timercotizacionporvencervencido.Interval = 120000;
            timercotizacionporvencervencido.AutoReset = false;
            timercotizacionporvencervencido.Elapsed += OnElapsedtimercotizacionporvencervencido;
            timercotizacionporvencervencido.Start();


            timercotizacionporvencervencidoPerdido = new Timer();
            timercotizacionporvencervencidoPerdido.Interval = 120000;
            timercotizacionporvencervencidoPerdido.AutoReset = false;
            timercotizacionporvencervencidoPerdido.Elapsed += OnElapsedtimercotizacionporvencervencidoPerdido;
            timercotizacionporvencervencidoPerdido.Start();


            //timercotizacion = new Timer();
            //timercotizacion.Interval = 60000; // 5 segundos
            //timercotizacion.AutoReset = false;
            //timercotizacion.Elapsed += OnElapsedTimeCotizacion;
            //timercotizacion.Start();
            //// TODO: agregar código aquí para iniciar el servicio.
        }
        private async void OnElapsedTimertimercotizacionporvencer3dias(object sender, ElapsedEventArgs e)
        {
            try
            {
                // EventLog.WriteEntry("Proceso Timer", EventLogEntryType.Information);
                // parametros que es pedido
                //TimeSpan.FromDays(1).TotalMilliseconds;
                int paraTipo = 1;
                await EnvioCorreo(paraTipo);
                timercotizacionporvencer3dias.Interval = 120000;
                timercotizacionporvencer3dias.AutoReset = true;
                timercotizacionporvencer3dias.Start();
            }
            catch (Exception ex)
            {

            }

          
        }

        private async void OnElapsedtimercotizacionporvencervencido(object sender, ElapsedEventArgs e)
        {
            try
            {
                // EventLog.WriteEntry("Proceso Timer", EventLogEntryType.Information);
                // parametros que es pedido
                int paraTipo = 2;
                await EnvioCorreo(paraTipo);
                timercotizacionporvencervencido.Interval = 120000;
                timercotizacionporvencervencido.AutoReset = true;
                timercotizacionporvencervencido.Start();
            }
            catch (Exception ex)
            {

            }


        }

        private async void OnElapsedtimercotizacionporvencervencidoPerdido(object sender, ElapsedEventArgs e)
        {
            try
            {
                // EventLog.WriteEntry("Proceso Timer", EventLogEntryType.Information);
                // parametros que es pedido
                int paraTipo = 3;
                await EnvioCorreo(paraTipo);
                timercotizacionporvencervencidoPerdido.Interval = 120000;
                timercotizacionporvencervencidoPerdido.AutoReset = true;
                timercotizacionporvencervencidoPerdido.Start();
            }
            catch (Exception ex)
            {

            }


        }
        private async void OnElapsedTimeCotizacion(object sender, ElapsedEventArgs e)
        {
            try
            {
                //EventLog.WriteEntry("Proceso Timer", EventLogEntryType.Information);
                // parametros que es cotizacion
                int paraTipo = 2;
                await EnvioCorreo(paraTipo);
                //timer.Interval = TimeSpan.FromDays(1).TotalMilliseconds;
                ////timer.AutoReset = true;
                //timer.Start();
            }
            catch (Exception ex)
            {

            }
           
        }

        public async Task EnvioCorreo(int tipo)
        {
            EnvioMail envio = new EnvioMail();
            await envio.SendSmsAsync(tipo);


        }
        protected override void OnStop()
        {
            // TODO: Add code here to perform any tear-down necessary to stop your service.
            //timerpedido.Stop();
            if (timerpedido != null)
            {
                timerpedido.Stop();
                timerpedido.Dispose();
            }

            if (timercotizacionporvencer3dias != null)
            {
                timercotizacionporvencer3dias.Stop();
                timercotizacionporvencer3dias.Dispose();
            }
            if (timercotizacionporvencervencido != null)
            {
                timercotizacionporvencervencido.Stop();
                timercotizacionporvencervencido.Dispose();
            }
            if (timercotizacionporvencervencidoPerdido != null)
            {
                timercotizacionporvencervencidoPerdido.Stop();
                timercotizacionporvencervencidoPerdido.Dispose();
            }
        }
    }
}
