﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace flight_management.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class IntVirtualEntities : DbContext
    {
        public IntVirtualEntities()
            : base("name=IntVirtualEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<AEROPORTURI> AEROPORTURIs { get; set; }
        public virtual DbSet<BILETE> BILETEs { get; set; }
        public virtual DbSet<COMPANII_AERIENE> COMPANII_AERIENE { get; set; }
        public virtual DbSet<PASAGERI> PASAGERIs { get; set; }
        public virtual DbSet<ZBORURI> ZBORURIs { get; set; }
    }
}