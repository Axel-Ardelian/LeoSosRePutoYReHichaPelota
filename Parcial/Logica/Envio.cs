using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logica
{
    public class Envio
    {
        public Destinatario Destinatario { get; set; }
        public Repartidor Repartidor { get; set; }
        public string NumeroDeEnvioAleatorio { get; set; }
        public enum Tipo { Pendiente = 0, AsignadoRepartidor = 1, EnCamino = 2, Entregado = 3 }
        public Tipo EstadoDeEnvio { get; set; }
        public DateTime FechaEstimadaEntrega { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaDeEntrega { get; set; }
        public DateTime FechaDeCreacion { get; set; }

        public string GenerarNumeroAleatorio()
        {
            Random random = new Random();
            return NumeroDeEnvioAleatorio = Convert.ToString(random.Next());
        }          
    }
}
