﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Documents.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class DocumentsEntities : DbContext
    {
        public DocumentsEntities()
            : base("name=DocumentsEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<documents> documents { get; set; }
        public virtual DbSet<documentTypes> documentTypes { get; set; }
        public virtual DbSet<files> files { get; set; }
        public virtual DbSet<organizationLists> organizationLists { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<vehicleTypeLists> vehicleTypeLists { get; set; }
        public virtual DbSet<cars> cars { get; set; }
    }
}
