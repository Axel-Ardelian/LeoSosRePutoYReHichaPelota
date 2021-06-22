using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Parcial;
using Logica;
using Newtonsoft.Json;

namespace Parcial.Controllers
{
    [RoutePrefix("API/Envios")] 
    public class EnvioController : ApiController
    {
        [Route("Envios")]
        public IHttpActionResult Get()
        {
            return Ok(Aplicacion.Envios);
        }

        [Route("Envios/{dni}")]
        public IHttpActionResult Get(int dni)
        {
            if (Aplicacion.BuscarPersona(dni) == null || Aplicacion.Validacion(dni) == false)
                return Content(HttpStatusCode.BadRequest, Aplicacion.Envios);
            
            return Ok(Aplicacion.BuscarPersona(dni));
        }

        [Route("Envios/{dni}/{fecha}/{descripcion}")]
        public IHttpActionResult Post(int dni, DateTime fecha, string descripcion,[FromBody] string value)
        {
            if ((Aplicacion.CargarEnvio(dni, fecha, descripcion) == null) || Aplicacion.Validacion(dni) == false)
                return BadRequest();
            
            return Created("", Aplicacion.CargarEnvio(dni, fecha, descripcion));
        }
        [Route("Envios/{envio}")]
        public IHttpActionResult Put(Envio envio,[FromBody] string value)
        {
            if (Aplicacion.AsignarRepartidor(envio) == null)
                return BadRequest();
            
            return Content(HttpStatusCode.OK, envio.Repartidor);
        }
        [Route("Envios/{codigo}")]
        public IHttpActionResult Put(string codigo,[FromBody]string value)
        {
            if (Aplicacion.Actualizar(codigo) == true)
                return Ok();

            return BadRequest();
        }
    }
}
