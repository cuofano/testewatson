﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TesteWatson.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class personalteacherEntities : DbContext
    {
        public personalteacherEntities()
            : base("name=personalteacherEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<aluno> alunoes { get; set; }
        public virtual DbSet<analis> analises { get; set; }
        public virtual DbSet<arquivoanalis> arquivoanalises { get; set; }
        public virtual DbSet<arquivobas> arquivobases { get; set; }
        public virtual DbSet<escola> escolas { get; set; }
        public virtual DbSet<traducaoarquivo> traducaoarquivoes { get; set; }
        public virtual DbSet<usuario> usuarios { get; set; }
        public virtual DbSet<view_escola> view_escola { get; set; }
        public virtual DbSet<view_documentotraducao> view_documentotraducao { get; set; }
        public virtual DbSet<view_analises> view_analises { get; set; }
        public virtual DbSet<view_analises_arquivos> view_analises_arquivos { get; set; }
        public virtual DbSet<alunosporsala> alunosporsalas { get; set; }
        public virtual DbSet<sala> salas { get; set; }
    }
}
