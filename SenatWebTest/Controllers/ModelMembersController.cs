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
    public class ModelMembersController : ApiController
    {
        private SenatContext db = new SenatContext();

        // GET: api/ModelMembers
        public IQueryable<ModelMember> GetMembers()
        {
            return db.Members;
        }

        // GET: api/ModelMembers/5
        [ResponseType(typeof(ModelMember))]
        public async Task<IHttpActionResult> GetModelMember(Guid id)
        {
            ModelMember modelMember = await db.Members.FindAsync(id);
            if (modelMember == null)
            {
                return NotFound();
            }

            return Ok(modelMember);
        }

        // PUT: api/ModelMembers/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutModelMember(Guid id, ModelMember modelMember)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != modelMember.MemberId)
            {
                return BadRequest();
            }

            db.Entry(modelMember).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ModelMemberExists(id))
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

        // POST: api/ModelMembers
        [ResponseType(typeof(ModelMember))]
        public async Task<IHttpActionResult> PostModelMember(ModelMember modelMember)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Members.Add(modelMember);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ModelMemberExists(modelMember.MemberId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = modelMember.MemberId }, modelMember);
        }

        // DELETE: api/ModelMembers/5
        [ResponseType(typeof(ModelMember))]
        public async Task<IHttpActionResult> DeleteModelMember(Guid id)
        {
            ModelMember modelMember = await db.Members.FindAsync(id);
            if (modelMember == null)
            {
                return NotFound();
            }

            db.Members.Remove(modelMember);
            await db.SaveChangesAsync();

            return Ok(modelMember);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ModelMemberExists(Guid id)
        {
            return db.Members.Count(e => e.MemberId == id) > 0;
        }
    }
}