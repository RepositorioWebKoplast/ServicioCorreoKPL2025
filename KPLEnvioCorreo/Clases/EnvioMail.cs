using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace KPLEnvioCorreo.Clases
{
    public class EnvioMail
    {
        public  async Task SendSmsAsync( int tipo)
        {



            try
            {
                Conexion oConexion = new Conexion();
                List<ListaDESTINATARIO> ListaEnvioCotizacion = new List<ListaDESTINATARIO>();
                if (tipo == 1)
                {
                    ListaEnvioCotizacion = await oConexion.OBTENER_DESTINATARIOS(tipo);
                }
                else if (tipo == 2)
                {

                    ListaEnvioCotizacion = await oConexion.OBTENER_DESTINATARIOS(tipo);

                }
                else if (tipo == 3)
                {

                    ListaEnvioCotizacion = await oConexion.OBTENER_DESTINATARIOS(tipo);

                }

                //MailMessage correo = new MailMessage();
             
                    
                if (ListaEnvioCotizacion.Count == 0)
                {
                    return;
                }

                foreach (var item in ListaEnvioCotizacion)
                {
                    if (string.IsNullOrEmpty(item.Destinatario))
                    {
                        continue;
                    }
                   
                    using (var correo = new MailMessage())
                    {
                        correo.From = new MailAddress("cotizacionkoplast@koplastindustrial.com");
                        if (!string.IsNullOrEmpty(item.Destinatario))
                        {
                            // List<string> lista = item.Destinatario.Split(',').ToList();
                            string[] lista = item.Destinatario.Split(',');

                            for (int i = 0; i < lista.Length; i++)
                            {
                                if (i == 0)
                                {
                                    correo.To.Add(lista[0]);
                                }
                                else
                                {
                                    correo.CC.Add(lista[i]);
                                }
                            }

                        }
                        if (tipo == 1)
                        {
                            //correo.To.Add("riders_230588@hotmail.com");
                            //correo.To.Add("lhuerta@koplastindustrial.com");
                            correo.Subject = $"Cotización {item.NROCOTIZACION} Vence en 3 días — Cliente: {item.cliente}";

                        }
                        else if (tipo == 2)
                        {
                            //correo.To.Add("lhuerta@koplastindustrial.com");
                            correo.Subject = $"Cotización {item.NROCOTIZACION} Vence Hoy — Cliente: {item.cliente}";

                        }
                        else if (tipo == 3)
                        {
                            //correo.To.Add("riders_230588@hotmail.com");
                            //correo.To.Add("riders_230588@hotmail.com");
                            correo.Subject = $"Cotización {item.NROCOTIZACION} Vencida — Cliente: {item.cliente}";


                        }
                        correo.Body = CreateBodyPorVencer(tipo, item);

                        correo.IsBodyHtml = true;


                        using (var client = new SmtpClient("smtp.office365.com", 25))
                        {
                            client.Credentials = new System.Net.NetworkCredential("cotizacionkoplast@koplastindustrial.com", "Koplast_2024");

                            client.EnableSsl = true;
                            await client.SendMailAsync(correo);
                        }
                    }                                                                        

                }

                


            }
            catch (Exception ex)
            {

            }

            // Puedes hacer algo con el smsMessage si lo necesitas
        }


        private string CreateBodyPorVencer( int tipo, ListaDESTINATARIO objeto)
        {
            //string path = @"C:\inetpub\plantilla\portada.html";
            string body = string.Empty;
            string ruta = "https://intranet.koplast.pe/";
           

            string path = ConfigurationSettings.AppSettings["RutaArchivosCorreo"];
            if (tipo == 1)
            {
                ruta = ruta + "Cotizacion/Editar/" + objeto.IDCOTIZACION;
                path = path + "PortadaCotizacionPorVencer3Dias.html";

            }
            else if (tipo==2)
            {
                ruta = ruta + "Cotizacion/Editar/" + objeto.IDCOTIZACION;
                path = path + "PortadaCotizacionVencidos.html";

            }
            else if (tipo == 3)
            {
                ruta = ruta + "Cotizacion/Editar/" + objeto.IDCOTIZACION;
                path = path + "PortaCotizacionVencidosPerdidos.html";

            }


            using (StreamReader reader = new StreamReader(path))
            {

                body = reader.ReadToEnd();

            }
            
            body = body.Replace("{NOMBRE_GESTOR}", objeto.gestor);

            body = body.Replace("{NRO_COTIZACION}", objeto.NROCOTIZACION);
            body = body.Replace("{CLIENTE}", objeto.cliente);
            body = body.Replace("{FECHA_VENCIMIENTO}", objeto.fechavencimiento.ToString("dd/MM/yyyy"));


            body = body.Replace("{CLIENTENAME}", objeto.cliente);
            body = body.Replace("{MONTO_TOTAL}", objeto.importe.ToString("#,##0.00"));
            body = body.Replace("{MONEDA}", objeto.moneda);
            body = body.Replace("{FECHA_EMISION}", objeto.fechaemision.ToString("dd/MM/yyyy"));
            
            body = body.Replace("{rutaportal}", ruta);



            return body;

        }
    }
}
