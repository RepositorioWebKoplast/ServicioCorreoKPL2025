using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sap.Data.Hana;

namespace KPLEnvioCorreo.Clases
{
    public class Conexion
    {

            //string _cadenaappkoplast = CredencialesXML.IniFileRead();

            string _cadenaappkoplast = "Server =192.168.5.226:30015;current schema=APPKOPLAST; User Id=KPLSAP;Password=Soporte01$";

            // string _cadenasap = "Server =192.168.1.51:30015;current schema=KOPLAST_INDUSTRIAL; User Id=KPLSAP;Password=Soporte01$";
            string _cadenasapindustrial = "Server =192.168.5.226:30015;current schema=KOPLAST_INDUSTRIAL; User Id=KPLSAP;Password=Soporte01$";

            string _cadenasapindustrialprueba = "Server =192.168.5.226:30015;current schema=ZZ_KPL_INDUSTRIAL_090224; User Id=KPLSAP;Password=Soporte01$";

        //public HanaConnection oHanaConnection = new HanaConnection(_cadena);

        // public HanaConnection oHanaConnection = new HanaConnection("Server = " + EstructuraCN.Server + ";current schema=" + EstructuraCN.CompanyBDINT + "; User Id=" + EstructuraCN.DbUserName + ";Password=" + EstructuraCN.DbPassword);


        public async Task<List<ListaDESTINATARIO>> OBTENER_DESTINATARIOS( int tipo)
        {
            //bool msg = false;
            List<ListaDESTINATARIO> objrest = new List<ListaDESTINATARIO>();

            HanaConnection cnx = new HanaConnection(_cadenaappkoplast);

            cnx.Open();
            //var transtt = cnx.BeginTransaction();

            try
            {

                HanaCommand cmdcab = new HanaCommand();
                cmdcab.Connection = cnx;
                //cmdcab.Transaction = transtt;
                cmdcab.CommandText = "OBTENER_DESTINATARIOSEMAIL_SERVICIO";
                cmdcab.CommandType = CommandType.StoredProcedure;



            

                HanaParameter Paridtipo = new HanaParameter();
                Paridtipo.ParameterName = "TIPO";
                Paridtipo.HanaDbType = HanaDbType.Integer;
                Paridtipo.Value = tipo;
                cmdcab.Parameters.Add(Paridtipo);



                HanaDataReader dr = await cmdcab.ExecuteReaderAsync();
                while (dr.Read())
                {
                    ListaDESTINATARIO objdocuments;
                    objdocuments = new ListaDESTINATARIO()
                    {
                        IDCOTIZACION = Convert.ToInt32(dr["IDCOTIZACION"].ToString()),
                        NROCOTIZACION = dr["NROCOTIZACION"].ToString(),
                      
                      
                        gestor = dr["USERNAME"].ToString(),
                        cliente = dr["CARDNAME"].ToString(),
                        fechaemision = Convert.ToDateTime(dr["DOCDATE"].ToString()),
                        fechavencimiento = Convert.ToDateTime(dr["FVALIDOFERTA"].ToString()),
                        importe = Convert.ToDecimal(dr["SUBIMPORTE"].ToString()),
                        estado = dr["ESTADO"].ToString(),
                        moneda = dr["MONEDA"].ToString(),

                        Destinatario = dr["DESTINATARIOS"].ToString(),



                    };

                    objrest.Add(objdocuments);

                }




            }
            catch (Exception ex)
            {

                //throw new Exception(ex.Message);
            }
            finally
            {
                if (cnx.State == ConnectionState.Open) cnx.Close();
            }

            return objrest;

        }

    }
}
