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
using SenatApi;

namespace SenatWebTest.Controllers
{
    public class ModelMeetingsController : ApiController
    {
        private SenatContext db = new SenatContext();

        // GET: api/ModelMeetings
        public IQueryable<ModelMeeting> GetMeetings()
        {
            return db.Meetings;
        }

        // GET: api/ModelMeetings/5
        [ResponseType(typeof(ModelMeeting))]
        public async Task<IHttpActionResult> GetModelMeeting(Guid id)
        {
            ModelMeeting modelMeeting = await db.Meetings.FindAsync(id);
            if (modelMeeting == null)
            {
                return NotFound();
            }

            return Ok(modelMeeting);
        }

        // PUT: api/ModelMeetings/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutModelMeeting(Guid id, ModelMeeting modelMeeting)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != modelMeeting.Id)
            {
                return BadRequest();
            }

            db.Entry(modelMeeting).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ModelMeetingExists(id))
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

        // POST: api/ModelMeetings
        [HttpPost]
        [ResponseType(typeof(ModelMeeting))]
        public async Task<IHttpActionResult> PostModelMeeting(ModelMeeting modelMeeting)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Meetings.Add(modelMeeting);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ModelMeetingExists(modelMeeting.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = modelMeeting.Id }, modelMeeting);
        }

        // DELETE: api/ModelMeetings/5
        [ResponseType(typeof(ModelMeeting))]
        public async Task<IHttpActionResult> DeleteModelMeeting(Guid id)
        {
            ModelMeeting modelMeeting = await db.Meetings.FindAsync(id);
            if (modelMeeting == null)
            {
                return NotFound();
            }

            db.Meetings.Remove(modelMeeting);
            await db.SaveChangesAsync();

            return Ok(modelMeeting);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ModelMeetingExists(Guid id)
        {
            return db.Meetings.Count(e => e.Id == id) > 0;
        }
    }
}