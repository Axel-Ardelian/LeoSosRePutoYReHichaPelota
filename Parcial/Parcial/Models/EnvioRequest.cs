using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Parcial;
using Logica;

namespace Parcial.Models
{
    public class EnvioRequest
    {
        public DestinatarioRequest Destinatario { get; set; }
        public RepartidorRequest Repartidor { get; set; }
        public string NumeroDeEnvioAleatorio { get; set; }
        public enum Tipo { Pendiente = 0, AsignadoRepartidor = 1, EnCamino = 2, Entregado = 3 }
        public Tipo EstadoDeEnvio { get; set; }
        public DateTime FechaEstimadaEntrega { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaDeEntrega { get; set; }
    }
    public class PersonaRequest
    {
        public int DNI { get; set; }
        public string Nombre { get; set; }
        public int Codigo { get; set; }
        public decimal Latitud { get; set; }
        public decimal Longitud { get; set; }
        public int ? NumeroDeTelefono { get; set; }

        public Persona CrearPersona()
        {
            Persona nueva = new Persona();
            nueva.NombreYApellido = Nombre;
            nueva.DNI = DNI;
            nueva.CodigoPostal = Codigo;
            nueva.Latitud = Latitud;
            nueva.Longitud = Longitud;
            nueva.NumeroDeTelefono = NumeroDeTelefono;
            return nueva;
        }
    }

    public class DestinatarioRequest : PersonaRequest
    {


    }
    public class RepartidorRequest : PersonaRequest
    {
        public double Comision { get; set; }

    }
}