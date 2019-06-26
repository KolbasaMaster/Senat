using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using SenatApi;
using AutoMapper;

namespace SenatWebTest.Controllers
{
    public class ModelIssueController : ApiController
    {
        private SenatContext db = new SenatContext();
        private RestSenatApiClient client = new RestSenatApiClient("https://dev.senat.sbt-osop-224.sigma.sbrf.ru");

        IMapper mapper = ConfigMapper1.IssueModelMap();
        // GET: api/ModelIssue
        public List<ModelIssueDto1> GetIssues()
        {
            var ListIssues = new List<ModelIssueDto1>();
            foreach (var issue in db.Issues)
            {
                var issueToAdd = mapper.Map<ModelIssueDto1>(issue);
                ListIssues.Add(issueToAdd);
            }
            return ListIssues;
        }

        // GET: api/ModelIssue/5
        [ResponseType(typeof(ModelIssue))]
        public async Task<IHttpActionResult> GetModelIssue(Guid id)
        {
          

            ModelIssue modelIssue = await db.Issues.FindAsync(id);
            var issueToAdd = mapper.Map<ModelIssueDto1>(modelIssue);
            if (issueToAdd == null)
            {
                return NotFound();
            }



            return Ok(issueToAdd);
        }

        // PUT: api/ModelIssue/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutModelIssue(Guid id, ModelIssue modelIssue)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != modelIssue.Id)
            {
                return BadRequest();
            }

            db.Entry(modelIssue).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ModelIssueExists(id))
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

        // POST: api/ModelIssue
        [ResponseType(typeof(ModelIssue))]
        public async Task<IHttpActionResult> PostModelIssue(ModelIssue modelIssue)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Issues.Add(modelIssue);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ModelIssueExists(modelIssue.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = modelIssue.Id }, modelIssue);
        }

        // DELETE: api/ModelIssue/5
        [ResponseType(typeof(ModelIssue))]
        public async Task<IHttpActionResult> DeleteModelIssue(Guid id)
        {
            ModelIssue modelIssue = await db.Issues.FindAsync(id);
            if (modelIssue == null)
            {
                return NotFound();
            }

            db.Issues.Remove(modelIssue);
            await db.SaveChangesAsync();

            return Ok(modelIssue);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ModelIssueExists(Guid id)
        {
            return db.Issues.Count(e => e.Id == id) > 0;
        }
    }
}