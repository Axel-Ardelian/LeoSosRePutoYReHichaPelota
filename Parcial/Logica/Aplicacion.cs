using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logica
{
    public sealed class Aplicacion
    {
        public static List<Envio> Envios { get; set; }
        public static List<Persona> Personas { get; set; }
        public static List<Repartidor> Repartidores { get; set; }
        public static List<InfoRepartidor> RepartidoresListos { get; set; } 


        public static string CargarEnvio(int dni, DateTime fechaestimada, string descripcion)
        {
            if (Envios == null)
                Envios = new List<Envio>();
            Persona persona = BuscarPersona(dni);
            if (persona == null)
                return null;
            else
            {
                Envio nuevoenvio = new Envio();
                nuevoenvio.NumeroDeEnvioAleatorio = nuevoenvio.GenerarNumeroAleatorio();
                nuevoenvio.Destinatario = (Destinatario)persona;
                nuevoenvio.Descripcion = descripcion;
                nuevoenvio.Repartidor = AsignarRepartidor(nuevoenvio);
                nuevoenvio.EstadoDeEnvio = Envio.Tipo.Pendiente;
                nuevoenvio.FechaEstimadaEntrega = fechaestimada;
                nuevoenvio.FechaDeCreacion = DateTime.Today;
                Envios.Add(nuevoenvio);
                return nuevoenvio.NumeroDeEnvioAleatorio;
            }
        }

        public static Persona BuscarPersona(int dni)
        {
            Persona nuevo = Personas.Find(x => x.DNI == dni);
            if (nuevo != null)
            {
                return nuevo;
            }
            return null;
        }
        public static bool Validacion(int dni)
        {
            Envio nuevavalidacion = Envios.Find(x => x.Repartidor.DNI == dni);
            if (nuevavalidacion != null || nuevavalidacion.Destinatario.NumeroDeTelefono == null)
                return false;
            else
                return true;
        }
        public static bool Actualizar(string codigo)
        {
            Envio envio = Envios.Find(x => x.NumeroDeEnvioAleatorio == codigo);
            if (envio == null)
                return false;

            envio.EstadoDeEnvio += 1;
            if (envio.EstadoDeEnvio == Envio.Tipo.Entregado)
            {
                envio.FechaDeEntrega = DateTime.Today;
                envio.Repartidor.Comision =+ ObtenerMonto();
            }
            return true;               
        }
        public static double ObtenerMonto()
        {
            return 100;
        }
        public static Repartidor AsignarRepartidor(Envio envio)
        {
            Repartidor repartidor = new Repartidor();
            double acu = 0;
            envio = Envios.Find(x => x.Destinatario.DNI == envio.Destinatario.DNI);
            if (envio != null)
            {
                foreach (var item in Repartidores)
                {
                    item.Distancia = CalcularDistancia(envio.Destinatario.Codigo);
                    if (acu == 0)
                    {
                        acu = item.Distancia;
                    } 
                    else
                    {
                        if (item.Distancia < acu)
                        {
                            acu = item.Distancia;
                        }
                    }
                }
                repartidor = Repartidores.Find(x => x.Distancia == acu);
            }
            envio.Repartidor = repartidor;
            return repartidor;
        }
        public static double CalcularDistancia (string codigo)
        {
            Envio envio = Envios.Find(x => x.NumeroDeEnvioAleatorio == codigo);
            double distance = 0;
            double Lat = (envio.Destinatario.Latitud - envio.Repartidor.Latitud) * (Math.PI / 180);
            double Lon = (envio.Destinatario.Longitud - envio.Repartidor.Longitud) * (Math.PI / 180);
            double a = Math.Sin(Lat / 2) * Math.Sin(Lat / 2) + Math.Cos(envio.Repartidor.Latitud * (Math.PI / 180)) * Math.Cos(envio.Destinatario.Latitud * (Math.PI / 180)) * Math.Sin(Lon / 2) * Math.Sin(Lon / 2);
            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            distance = 6.371 * c;
            return distance;
        }      
        public static List<InfoRepartidor> RetornarRepartidores(DateTime fechadesde, DateTime fechahasta)
        {        
            foreach (var item in Envios)
	        {
                if (RepartidoresListos == null)
                {
                    RepartidoresListos = new List<InfoRepartidor>();
                }
                if (item.EstadoDeEnvio == Envio.Tipo.Entregado)
                {
                    if ((item.FechaDeCreacion >= fechadesde) && (item.FechaDeCreacion <= fechahasta))
                    {
                        InfoRepartidor validacion = RepartidoresListos.Find(x => x.NombreYApellido == item.Repartidor.NombreYApellido);
                        if (validacion != null)
                        {
                            InfoRepartidor nueva = new InfoRepartidor();
                            nueva.NombreYApellido = item.Repartidor.NombreYApellido;
                            nueva.TotalComiciones = item.Repartidor.Comision;
                            nueva.EnviosRealizados = +1;
                            RepartidoresListos.Add(nueva);
                        }                      
                    }
                }
            }
            return RepartidoresListos;
        }
    }
}
