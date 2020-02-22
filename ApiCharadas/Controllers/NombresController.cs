using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using ApiCharadas.Models;

namespace ApiCharadas.Controllers
{
    public class NombresController : ApiController
    {
        private CharadasEntities db = new CharadasEntities();

        // GET: api/Nombres
        public IQueryable<Nombre> GetNombre()
        {
            return db.Nombre;
        }

        // GET: api/Nombres/5
        [ResponseType(typeof(Nombre))]
        public async Task<IHttpActionResult> GetNombre(int id)
        {
            Nombre nombre = await db.Nombre.FindAsync(id);
            if (nombre == null)
            {
                return NotFound();
            }

            return Ok(nombre);
        }

        // PUT: api/Nombres/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutNombre(int id, Nombre nombre)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != nombre.Id_Nombre)
            {
                return BadRequest();
            }

            db.Entry(nombre).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NombreExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Nombres
        [ResponseType(typeof(Nombre))]
        public async Task<IHttpActionResult> PostNombre(Nombre nombre)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Nombre.Add(nombre);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = nombre.Id_Nombre }, nombre);
        }

        // DELETE: api/Nombres/5
        [ResponseType(typeof(Nombre))]
        public async Task<IHttpActionResult> DeleteNombre(int id)
        {
            Nombre nombre = await db.Nombre.FindAsync(id);
            if (nombre == null)
            {
                return NotFound();
            }

            db.Nombre.Remove(nombre);
            await db.SaveChangesAsync();

            return Ok(nombre);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool NombreExists(int id)
        {
            return db.Nombre.Count(e => e.Id_Nombre == id) > 0;
        }
    }
}