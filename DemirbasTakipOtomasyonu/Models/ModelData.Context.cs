﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DemirbasTakipOtomasyonu.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class OtomasyonEntities : DbContext
    {
        public OtomasyonEntities()
            : base("name=OtomasyonEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Demirbas> Demirbas { get; set; }
        public virtual DbSet<DemirbasTuru> DemirbasTuru { get; set; }
        public virtual DbSet<Departman> Departman { get; set; }
        public virtual DbSet<Fakulte> Fakulte { get; set; }
        public virtual DbSet<Oda> Oda { get; set; }
        public virtual DbSet<OdaDemirbasAtama> OdaDemirbasAtama { get; set; }
        public virtual DbSet<Personel> Personel { get; set; }
        public virtual DbSet<Kullanici> Kullanici { get; set; }
    }
}