//------------------------------------------------------------------------------
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
    using System.Collections.Generic;
    
    public partial class TimeTable
    {
        public int Id { get; set; }
        public int ModuleId { get; set; }
        public int RoomId { get; set; }
        public System.DateTime Time { get; set; }
    
        public virtual Module Module { get; set; }
        public virtual Room Room { get; set; }
    }
}