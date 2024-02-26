﻿// <auto-generated />
using System;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20231115190932_initdb")]
    partial class initdb
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Domain.Entities.ListAggregate.ListDefinition", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2")
                        .HasColumnName("Created");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("CreatedBy");

                    b.Property<string>("DataType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("DataType");

                    b.Property<DateTime?>("Deleted")
                        .HasColumnType("datetime2")
                        .HasColumnName("Deleted");

                    b.Property<string>("DeletedBy")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("DeletedBy");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit")
                        .HasColumnName("IsDeleted");

                    b.Property<string>("KeyName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("KeyName");

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("datetime2")
                        .HasColumnName("LastModified");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("LastModifiedBy");

                    b.Property<string>("ListName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("ListName");

                    b.Property<string>("ValueName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("ValueName");

                    b.HasKey("Id");

                    b.ToTable("ListDefinition", "Workflow");
                });

            modelBuilder.Entity("Domain.Entities.ListAggregate.ListTenantWorkflow", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2")
                        .HasColumnName("Created");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("CreatedBy");

                    b.Property<DateTime?>("Deleted")
                        .HasColumnType("datetime2")
                        .HasColumnName("Deleted");

                    b.Property<string>("DeletedBy")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("DeletedBy");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit")
                        .HasColumnName("IsDeleted");

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("datetime2")
                        .HasColumnName("LastModified");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("LastModifiedBy");

                    b.Property<long>("ListId")
                        .HasColumnType("bigint");

                    b.Property<string>("TenantId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long?>("WorkflowId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("ListId");

                    b.HasIndex("WorkflowId");

                    b.ToTable("ListTenantWorkflow", "Workflow");
                });

            modelBuilder.Entity("Domain.Entities.ListAggregate.ListValue", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2")
                        .HasColumnName("Created");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("CreatedBy");

                    b.Property<DateTime?>("Deleted")
                        .HasColumnType("datetime2")
                        .HasColumnName("Deleted");

                    b.Property<string>("DeletedBy")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("DeletedBy");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit")
                        .HasColumnName("IsDeleted");

                    b.Property<string>("Key")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("datetime2")
                        .HasColumnName("LastModified");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("LastModifiedBy");

                    b.Property<long>("ListId")
                        .HasColumnType("bigint");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ListId");

                    b.ToTable("ListValue", "Workflow");
                });

            modelBuilder.Entity("Domain.Entities.WorkflowsAggregate.Workflow", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2")
                        .HasColumnName("Created");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("CreatedBy");

                    b.Property<DateTime?>("Deleted")
                        .HasColumnType("datetime2")
                        .HasColumnName("Deleted");

                    b.Property<string>("DeletedBy")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("DeletedBy");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Description");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit")
                        .HasColumnName("IsDeleted");

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("datetime2")
                        .HasColumnName("LastModified");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("LastModifiedBy");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("Name");

                    b.Property<string>("State")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("State");

                    b.Property<string>("Version")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Version");

                    b.HasKey("Id");

                    b.ToTable("Workflow", "Workflow");
                });

            modelBuilder.Entity("Domain.Entities.WorkflowsAggregate.WorkflowDynamicForm", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Code");

                    b.Property<string>("CodeFlow")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("CodeFlow");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2")
                        .HasColumnName("Created");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("CreatedBy");

                    b.Property<DateTime?>("Deleted")
                        .HasColumnType("datetime2")
                        .HasColumnName("Deleted");

                    b.Property<string>("DeletedBy")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("DeletedBy");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Description");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit")
                        .HasColumnName("IsDeleted");

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("datetime2")
                        .HasColumnName("LastModified");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("LastModifiedBy");

                    b.Property<string>("Layout")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Layout");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Status");

                    b.Property<string>("Version")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Version");

                    b.Property<long>("WorkflowId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("WorkflowId")
                        .IsUnique();

                    b.ToTable("WorkflowDynamicForm", "Workflow");
                });

            modelBuilder.Entity("Domain.Entities.WorkflowsAggregate.WorkflowProductAttributes", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("AtributteCode")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("AtributteCode");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2")
                        .HasColumnName("Created");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("CreatedBy");

                    b.Property<int>("DataType")
                        .HasColumnType("int")
                        .HasColumnName("DataType");

                    b.Property<string>("DefaultValue")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("DefaultValue");

                    b.Property<DateTime?>("Deleted")
                        .HasColumnType("datetime2")
                        .HasColumnName("Deleted");

                    b.Property<string>("DeletedBy")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("DeletedBy");

                    b.Property<long>("DynamicFormId")
                        .HasColumnType("bigint")
                        .HasColumnName("DynamicFormId");

                    b.Property<string>("ExtraInfo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("ExtraInfo");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit")
                        .HasColumnName("IsDeleted");

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("datetime2")
                        .HasColumnName("LastModified");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("LastModifiedBy");

                    b.Property<string>("Metadata")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Metadata");

                    b.Property<bool>("Optional")
                        .HasColumnType("bit")
                        .HasColumnName("Optional");

                    b.Property<long>("ProductId")
                        .HasColumnType("bigint")
                        .HasColumnName("ProductId");

                    b.Property<string>("ZipCode")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("ZipCode");

                    b.HasKey("Id");

                    b.HasIndex("DynamicFormId");

                    b.ToTable("WorkflowProductAttributes", "Workflow");
                });

            modelBuilder.Entity("Domain.Entities.ListAggregate.ListTenantWorkflow", b =>
                {
                    b.HasOne("Domain.Entities.ListAggregate.ListDefinition", "ListDefinition")
                        .WithMany("ListsTenantsWorkflows")
                        .HasForeignKey("ListId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.WorkflowsAggregate.Workflow", "Workflow")
                        .WithMany("ListTenantWorkflows")
                        .HasForeignKey("WorkflowId");

                    b.Navigation("ListDefinition");

                    b.Navigation("Workflow");
                });

            modelBuilder.Entity("Domain.Entities.ListAggregate.ListValue", b =>
                {
                    b.HasOne("Domain.Entities.ListAggregate.ListDefinition", "ListDefinition")
                        .WithMany("ListValues")
                        .HasForeignKey("ListId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ListDefinition");
                });

            modelBuilder.Entity("Domain.Entities.WorkflowsAggregate.WorkflowDynamicForm", b =>
                {
                    b.HasOne("Domain.Entities.WorkflowsAggregate.Workflow", "Workflow")
                        .WithOne()
                        .HasForeignKey("Domain.Entities.WorkflowsAggregate.WorkflowDynamicForm", "WorkflowId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Workflow");
                });

            modelBuilder.Entity("Domain.Entities.WorkflowsAggregate.WorkflowProductAttributes", b =>
                {
                    b.HasOne("Domain.Entities.WorkflowsAggregate.WorkflowDynamicForm", "DynamicForm")
                        .WithMany("Attributes")
                        .HasForeignKey("DynamicFormId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DynamicForm");
                });

            modelBuilder.Entity("Domain.Entities.ListAggregate.ListDefinition", b =>
                {
                    b.Navigation("ListValues");

                    b.Navigation("ListsTenantsWorkflows");
                });

            modelBuilder.Entity("Domain.Entities.WorkflowsAggregate.Workflow", b =>
                {
                    b.Navigation("ListTenantWorkflows");
                });

            modelBuilder.Entity("Domain.Entities.WorkflowsAggregate.WorkflowDynamicForm", b =>
                {
                    b.Navigation("Attributes");
                });
#pragma warning restore 612, 618
        }
    }
}
