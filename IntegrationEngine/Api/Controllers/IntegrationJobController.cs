﻿using IntegrationEngine.Models;
using IntegrationEngine.Storage;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace IntegrationEngine.Api.Controllers
{
    public class IntegrationJobController : ApiController
    {
        public IRepository<IntegrationJob> Repository { get; set; }
        public IntegrationJobController()
        {
            Repository = Container.Resolve<IRepository<IntegrationJob>>();
        }

        // GET api/IntegrationJob
        public IEnumerable<IntegrationJob> GetIntegrationJobs()
        {
            return Repository.SelectAll();
        }

        // GET api/IntegrationJob/5
        [ResponseType(typeof(IntegrationJob))]
        public IHttpActionResult GetIntegrationJob(int id)
        {
            var IntegrationJob = Repository.SelectById(id);
            if (IntegrationJob == null)
                return NotFound();
            return Ok(IntegrationJob);
        }

        // PUT api/IntegrationJob/5
        public IHttpActionResult PutIntegrationJob(int id, IntegrationJob IntegrationJob)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (id != IntegrationJob.Id)
                return BadRequest();

            Repository.SetState(IntegrationJob, EntityState.Modified);

            try
            {
                Repository.Save();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IntegrationJobExists(id))
                    return NotFound();
                else
                    throw;
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST api/IntegrationJob
        [ResponseType(typeof(IntegrationJob))]
        public IHttpActionResult PostIntegrationJob(IntegrationJob IntegrationJob)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Repository.Insert(IntegrationJob);
            Repository.Save();
            return CreatedAtRoute("DefaultApi", new { id = IntegrationJob.Id }, IntegrationJob);
        }

        // DELETE api/IntegrationJob/5
        [ResponseType(typeof(IntegrationJob))]
        public IHttpActionResult DeleteIntegrationJob(int id)
        {
            var IntegrationJob = Repository.SelectById(id);
            if (IntegrationJob == null)
                return NotFound();
            Repository.Delete(IntegrationJob);
            Repository.Save();
            return Ok(IntegrationJob);
        }

        private bool IntegrationJobExists(int id)
        {
            return Repository.Exists(id);
        }
    }
}