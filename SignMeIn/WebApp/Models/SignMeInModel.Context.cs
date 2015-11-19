﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebApp.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class projectdbEntities : DbContext
    {
        public projectdbEntities()
            : base("name=projectdbEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Course> Courses { get; set; }
        public virtual DbSet<Log> Logs { get; set; }
        public virtual DbSet<Module> Modules { get; set; }
        public virtual DbSet<Privacy> Privacies { get; set; }
        public virtual DbSet<Program> Programs { get; set; }
        public virtual DbSet<Room> Rooms { get; set; }
        public virtual DbSet<RoomType> RoomTypes { get; set; }
        public virtual DbSet<TimeTable> TimeTables { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<WatchRoom> WatchRooms { get; set; }
    
        public virtual int CreateUserMaster(string studentno, string fname, string lname, string email)
        {
            var studentnoParameter = studentno != null ?
                new ObjectParameter("studentno", studentno) :
                new ObjectParameter("studentno", typeof(string));
    
            var fnameParameter = fname != null ?
                new ObjectParameter("fname", fname) :
                new ObjectParameter("fname", typeof(string));
    
            var lnameParameter = lname != null ?
                new ObjectParameter("lname", lname) :
                new ObjectParameter("lname", typeof(string));
    
            var emailParameter = email != null ?
                new ObjectParameter("email", email) :
                new ObjectParameter("email", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("CreateUserMaster", studentnoParameter, fnameParameter, lnameParameter, emailParameter);
        }
    
        public virtual int InsertUser(string type, string number, string fname, string lname, string email, Nullable<byte> prilvl)
        {
            var typeParameter = type != null ?
                new ObjectParameter("type", type) :
                new ObjectParameter("type", typeof(string));
    
            var numberParameter = number != null ?
                new ObjectParameter("number", number) :
                new ObjectParameter("number", typeof(string));
    
            var fnameParameter = fname != null ?
                new ObjectParameter("fname", fname) :
                new ObjectParameter("fname", typeof(string));
    
            var lnameParameter = lname != null ?
                new ObjectParameter("lname", lname) :
                new ObjectParameter("lname", typeof(string));
    
            var emailParameter = email != null ?
                new ObjectParameter("email", email) :
                new ObjectParameter("email", typeof(string));
    
            var prilvlParameter = prilvl.HasValue ?
                new ObjectParameter("prilvl", prilvl) :
                new ObjectParameter("prilvl", typeof(byte));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("InsertUser", typeParameter, numberParameter, fnameParameter, lnameParameter, emailParameter, prilvlParameter);
        }
    
        public virtual int Loginsproc(string email, ObjectParameter userid, ObjectParameter fname)
        {
            var emailParameter = email != null ?
                new ObjectParameter("email", email) :
                new ObjectParameter("email", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("Loginsproc", emailParameter, userid, fname);
        }
    
        public virtual ObjectResult<Nullable<System.DateTime>> adminupdatetimetable()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<System.DateTime>>("adminupdatetimetable");
        }
    
        public virtual ObjectResult<RyansAwesomeCode_Result> RyansAwesomeCode(Nullable<int> studentID)
        {
            var studentIDParameter = studentID.HasValue ?
                new ObjectParameter("StudentID", studentID) :
                new ObjectParameter("StudentID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<RyansAwesomeCode_Result>("RyansAwesomeCode", studentIDParameter);
        }
    }
}
