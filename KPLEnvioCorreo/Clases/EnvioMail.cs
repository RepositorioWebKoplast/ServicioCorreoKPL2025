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
                string correoenvio = "cotizacionkoplast@koplastindustrial.com";
                string claveenvio = "K0pl@$t_2026@";
               
                Conexion oConexion = new Conexion();
                List<ListaDESTINATARIO> ListaEnvioNotificacion = new List<ListaDESTINATARIO>();
                if (tipo == 1)
                {
                    ListaEnvioNotificacion = await oConexion.OBTENER_DESTINATARIOS(tipo);
                }
                else if (tipo == 2)
                {

                    ListaEnvioNotificacion = await oConexion.OBTENER_DESTINATARIOS(tipo);

                }
                else if (tipo == 3)
                {

                    ListaEnvioNotificacion = await oConexion.OBTENER_DESTINATARIOS(tipo);

                }
                else if (tipo == 4)
                {

                    ListaEnvioNotificacion = await oConexion.OBTENER_DESTINATARIOS(tipo);

                }
                else if (tipo == 5)
                {

                    ListaEnvioNotificacion = await oConexion.OBTENER_DESTINATARIOS(tipo);

                }
                else if (tipo == 7)
                {
                    correoenvio = "avisokoplast@koplastindustrial.com";
                    claveenvio = "K0pl@st_2@26";
                    ListaEnvioNotificacion = await oConexion.OBTENER_DESTINATARIOS(tipo);

                }

                //MailMessage correo = new MailMessage(); hasta 


                if (ListaEnvioNotificacion.Count == 0)
                {
                    return;
                }
                

                    foreach (var item in ListaEnvioNotificacion)
                    {
                        if (string.IsNullOrEmpty(item.Destinatario))
                        {
                            continue;
                        }

                        using (var correo = new MailMessage())
                        {
                            correo.From = new MailAddress(correoenvio);
                            if (!string.IsNullOrEmpty(item.Destinatario))
                            {
                                // List<string> lista = item.Destinatario.Split(',').ToList();
                                string[] lista = item.Destinatario.Split(',');
                                if (tipo == 1 || tipo == 2 || tipo == 3 || tipo == 4 || tipo == 5 || tipo == 6)
                                {
                                    for (int i = 0; i < lista.Length; i++)
                                    {
                                        if (lista[i] == "")
                                        {
                                            continue;
                                        }

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
                                else
                                {
                                    for (int i = 0; i < lista.Length; i++)
                                    {
                                        if (lista[i] == "")
                                        {
                                            continue;
                                        }
                                        correo.To.Add(lista[i]);

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
                            else if (tipo == 4)
                            {
                                //correo.To.Add("riders_230588@hotmail.com");
                                //correo.To.Add("riders_230588@hotmail.com");
                                correo.Subject = $"Pedido {item.NROCOTIZACION} Sin Atención — Cliente: {item.cliente}";


                            }
                            else if (tipo == 5)
                            {
                                //correo.To.Add("riders_230588@hotmail.com");
                                //correo.To.Add("riders_230588@hotmail.com");
                                correo.Subject = $"Pedido {item.NROCOTIZACION} Sin Atención — Cliente: {item.cliente}";


                            }
                            else if (tipo == 6)
                            {
                                //correo.To.Add("riders_230588@hotmail.com");
                                //correo.To.Add("riders_230588@hotmail.com");
                                correo.Subject = $"Pedido {item.NROCOTIZACION} Sin Atención — Cliente: {item.cliente}";


                            }
                            else if (tipo == 7)
                            {
                                //correo.To.Add("riders_230588@hotmail.com");
                                //correo.To.Add("riders_230588@hotmail.com");
                                correo.Subject = $"Contrato por Vencer";


                            }
                            correo.Body = CreateBodyPorVencer(tipo, item);

                            correo.IsBodyHtml = true;

                            try
                            {
                                using (var client = new SmtpClient("smtp.office365.com", 25))
                                {
                                    client.Credentials = new System.Net.NetworkCredential(correoenvio, claveenvio);

                                    client.EnableSsl = true;
                                    await client.SendMailAsync(correo);
                                }
                            }
                            catch (Exception ex)
                            {

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
            string diasvencido = "";

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
            else if (tipo == 4)
            {
                ruta = ruta + "PedidosPendientes";
                path = path + "PortadaPedidoporVencer3Dias.html";

            }
            else if (tipo == 5)
            {
                ruta = ruta + "PedidosPendientes";
                path = path + "PortadaPedidoVencidos.html";

            }
            else if (tipo == 6)
            {
                ruta = ruta + "PedidosPendientes";
                path = path + "PortadaPedidosVencidosPerdidos.html";

            }
            else if (tipo == 7)
            {
                ruta = ruta + "PedidosPendientes";
                path = path + "PortadaContratos.html";

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


            if (tipo==7) 
            {
                string nametipo = "";
                diasvencido = "30";
                if (objeto.cliente=="1")
                {
                    nametipo = "CONTRATO";
                }
                else if (objeto.cliente=="2")
                {
                    nametipo = "GARANTIA";
                }
                else if (objeto.cliente == "3")
                {
                    nametipo = "PERMISO";
                }
                else if (objeto.cliente == "4")
                {
                    nametipo = "MANTENIMIENTO";
                }
                else if (objeto.cliente == "5")
                {
                    nametipo = "CALIBRACIONES";
                }

                body = body.Replace("{TIPOCONTRATO}", nametipo);
                body = body.Replace("{NOMBRECONTRATO}", objeto.NROCOTIZACION);
                body = body.Replace("{DIASPORVENCER}", diasvencido);

            }



            return body;

        }
    }
}
