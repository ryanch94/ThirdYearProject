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
using WebApp.Models;

namespace WebApp.Controllers
{
    public class TimeTablesController : ApiController
    {
        private Sign_Me_In_DataEntities1 db = new Sign_Me_In_DataEntities1();

        // GET: api/TimeTables
        public IQueryable<TimeTable> GetTimeTables()
        {
            return db.TimeTables;
        }

        // GET: api/TimeTables/5
        [ResponseType(typeof(TimeTable))]
        public async Task<IHttpActionResult> GetTimeTable(int id)
        {
            //TimeTable timetable = await db.TimeTables.FindAsync(id);
            //if (timetable == null)
            //{
            //    return NotFound();
            //}
            List<TimeTableClass> classList = new List<TimeTableClass>()
            {
                new TimeTableClass() { Name = "Database Dev", RoomCode = "D2034", StartTime = DateTime.Parse("11:00:00").ToShortTimeString(), Day = 1, LengthHours = 1 },
                new TimeTableClass() { Name = "Web Prog 3", RoomCode = "B1041", StartTime = DateTime.Parse("11:00:00").ToShortTimeString(), Day = 2, LengthHours = 2},

            };
            return Ok(classList);
        }

        // PUT: api/TimeTables/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutTimeTable(int id, TimeTable timeTable)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != timeTable.Id)
            {
                return BadRequest();
            }

            db.Entry(timeTable).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TimeTableExists(id))
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

        // POST: api/TimeTables
        [ResponseType(typeof(TimeTable))]
        public async Task<IHttpActionResult> PostTimeTable(TimeTable timeTable)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.TimeTables.Add(timeTable);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = timeTable.Id }, timeTable);
        }

        // DELETE: api/TimeTables/5
        [ResponseType(typeof(TimeTable))]
        public async Task<IHttpActionResult> DeleteTimeTable(int id)
        {
            TimeTable timeTable = await db.TimeTables.FindAsync(id);
            if (timeTable == null)
            {
                return NotFound();
            }

            db.TimeTables.Remove(timeTable);
            await db.SaveChangesAsync();

            return Ok(timeTable);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TimeTableExists(int id)
        {
            return db.TimeTables.Count(e => e.Id == id) > 0;
        }
    }
}