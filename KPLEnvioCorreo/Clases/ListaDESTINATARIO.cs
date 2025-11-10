using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KPLEnvioCorreo.Clases
{
    public class ListaDESTINATARIO
    {
        public string Destinatario { get; set; }
        public string NROCOTIZACION { get; set; }
        public int IDCOTIZACION { get; set; }

        public string gestor { get; set; }
        public string cliente { get; set; }
        public DateTime fechaemision { get; set; }
        public DateTime fechavencimiento { get; set; }
        public decimal importe { get; set; }
        public string estado { get; set; }

        public string moneda { get; set; }

    }
}
