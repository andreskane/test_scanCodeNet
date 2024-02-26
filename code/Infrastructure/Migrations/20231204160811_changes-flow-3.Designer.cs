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
    [Migration("20231204160811_changes-flow-3")]
    partial class changesflow3
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Domain.Entities.DynamicFormAggregate.DynamicFormPlan", b =>
                {
                    b.Property<long>("DynamicFormId")
                        .HasColumnType("bigint");

                    b.Property<long>("PlanId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("Deleted")
                        .HasColumnType("datetime2");

                    b.Property<string>("DeletedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("datetime2");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("DynamicFormId", "PlanId");

                    b.ToTable("DynamicFormPlans", "Workflow");
                });

            modelBuilder.Entity("Domain.Entities.DynamicFormAggregate.TemplateComponent", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("DataType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsHidden")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<bool>("IsRequired")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<string>("Label")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("data")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("groupID")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("placeholder")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("TemplateComponent", "Workflow");
                });

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

                    b.Property<long?>("DynamicFormTemplateId")
                        .HasColumnType("bigint");

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

                    b.HasIndex("DynamicFormTemplateId");

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

            modelBuilder.Entity("Domain.Entities.RulesAggregate.ActionParameter", b =>
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
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("Deleted")
                        .HasColumnType("datetime2")
                        .HasColumnName("Deleted");

                    b.Property<string>("DeletedBy")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("DeletedBy");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit")
                        .HasColumnName("IsDeleted");

                    b.Property<bool>("IsRequest")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("datetime2")
                        .HasColumnName("LastModified");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("LastModifiedBy");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("RuleActtioId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("RuleActtioId");

                    b.ToTable("ActionParameter", "Workflow");
                });

            modelBuilder.Entity("Domain.Entities.RulesAggregate.DynamicFormRule", b =>
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

                    b.Property<long>("DynamicFormId")
                        .HasColumnType("bigint");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit")
                        .HasColumnName("IsDeleted");

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("datetime2")
                        .HasColumnName("LastModified");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("LastModifiedBy");

                    b.Property<long>("RuleId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("DynamicFormId");

                    b.HasIndex("RuleId");

                    b.ToTable("DynamicFormRule", "Workflow");
                });

            modelBuilder.Entity("Domain.Entities.RulesAggregate.Rule", b =>
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
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Enabled")
                        .HasColumnType("bit");

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
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Version")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Rule", "Workflow");
                });

            modelBuilder.Entity("Domain.Entities.RulesAggregate.RuleAction", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<int>("ActionType")
                        .HasColumnType("int");

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
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

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
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Order")
                        .HasColumnType("int");

                    b.Property<long>("RuleId")
                        .HasColumnType("bigint");

                    b.Property<string>("Script")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Version")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("RuleId");

                    b.ToTable("RuleAction", "Workflow");
                });

            modelBuilder.Entity("Domain.Entities.WorkflowsAggregate.DynamicForm", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("CodeFlowActive")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("CodeFlowActive");

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

                    b.Property<long>("PlanId")
                        .HasColumnType("bigint");

                    b.Property<int>("State")
                        .HasColumnType("int")
                        .HasColumnName("State");

                    b.Property<int>("Version")
                        .HasColumnType("int")
                        .HasColumnName("Version");

                    b.HasKey("Id");

                    b.ToTable("DynamicForm", "Workflow");
                });

            modelBuilder.Entity("Domain.Entities.WorkflowsAggregate.DynamicFormItem", b =>
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

                    b.Property<long?>("DynamicFormId")
                        .HasColumnType("bigint");

                    b.Property<long?>("DynamicFormTemplateId")
                        .HasColumnType("bigint");

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

                    b.Property<int>("Status")
                        .HasColumnType("int")
                        .HasColumnName("Status");

                    b.Property<int>("Version")
                        .HasColumnType("int")
                        .HasColumnName("Version");

                    b.HasKey("Id");

                    b.HasIndex("DynamicFormId");

                    b.HasIndex("DynamicFormTemplateId");

                    b.ToTable("DynamicFormItem", "Workflow");
                });

            modelBuilder.Entity("Domain.Entities.WorkflowsAggregate.DynamicFormProductAttributes", b =>
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

                    b.ToTable("DynamicFormProductAttributes", "Workflow");
                });

            modelBuilder.Entity("Domain.Entities.WorkflowsAggregate.DynamicFormTemplate", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("CodeFlowActive")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

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

                    b.Property<int>("State")
                        .HasColumnType("int")
                        .HasColumnName("State");

                    b.Property<int>("Version")
                        .HasColumnType("int")
                        .HasColumnName("Version");

                    b.HasKey("Id");

                    b.ToTable("DynamicFormTemplate", "Workflow");
                });

            modelBuilder.Entity("Domain.Entities.DynamicFormAggregate.DynamicFormPlan", b =>
                {
                    b.HasOne("Domain.Entities.WorkflowsAggregate.DynamicForm", "DynamicForm")
                        .WithMany("DynamicFormPlans")
                        .HasForeignKey("DynamicFormId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DynamicForm");
                });

            modelBuilder.Entity("Domain.Entities.ListAggregate.ListTenantWorkflow", b =>
                {
                    b.HasOne("Domain.Entities.WorkflowsAggregate.DynamicFormTemplate", null)
                        .WithMany("ListTenantWorkflows")
                        .HasForeignKey("DynamicFormTemplateId");

                    b.HasOne("Domain.Entities.ListAggregate.ListDefinition", "ListDefinition")
                        .WithMany("ListsTenantsWorkflows")
                        .HasForeignKey("ListId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.WorkflowsAggregate.DynamicForm", "Workflow")
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

            modelBuilder.Entity("Domain.Entities.RulesAggregate.ActionParameter", b =>
                {
                    b.HasOne("Domain.Entities.RulesAggregate.RuleAction", "RuleAction")
                        .WithMany("Parameters")
                        .HasForeignKey("RuleActtioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("RuleAction");
                });

            modelBuilder.Entity("Domain.Entities.RulesAggregate.DynamicFormRule", b =>
                {
                    b.HasOne("Domain.Entities.WorkflowsAggregate.DynamicForm", "DynamicForm")
                        .WithMany("DynamicFormRules")
                        .HasForeignKey("DynamicFormId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Domain.Entities.RulesAggregate.Rule", "Rule")
                        .WithMany("DynamicFormRules")
                        .HasForeignKey("RuleId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("DynamicForm");

                    b.Navigation("Rule");
                });

            modelBuilder.Entity("Domain.Entities.RulesAggregate.RuleAction", b =>
                {
                    b.HasOne("Domain.Entities.RulesAggregate.Rule", "Rule")
                        .WithMany("Actions")
                        .HasForeignKey("RuleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Rule");
                });

            modelBuilder.Entity("Domain.Entities.WorkflowsAggregate.DynamicFormItem", b =>
                {
                    b.HasOne("Domain.Entities.WorkflowsAggregate.DynamicForm", "DynamicForm")
                        .WithMany("FlowList")
                        .HasForeignKey("DynamicFormId");

                    b.HasOne("Domain.Entities.WorkflowsAggregate.DynamicFormTemplate", "DynamicFormTemplate")
                        .WithMany("FlowList")
                        .HasForeignKey("DynamicFormTemplateId");

                    b.Navigation("DynamicForm");

                    b.Navigation("DynamicFormTemplate");
                });

            modelBuilder.Entity("Domain.Entities.WorkflowsAggregate.DynamicFormProductAttributes", b =>
                {
                    b.HasOne("Domain.Entities.WorkflowsAggregate.DynamicFormItem", "DynamicForm")
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

            modelBuilder.Entity("Domain.Entities.RulesAggregate.Rule", b =>
                {
                    b.Navigation("Actions");

                    b.Navigation("DynamicFormRules");
                });

            modelBuilder.Entity("Domain.Entities.RulesAggregate.RuleAction", b =>
                {
                    b.Navigation("Parameters");
                });

            modelBuilder.Entity("Domain.Entities.WorkflowsAggregate.DynamicForm", b =>
                {
                    b.Navigation("DynamicFormPlans");

                    b.Navigation("DynamicFormRules");

                    b.Navigation("FlowList");

                    b.Navigation("ListTenantWorkflows");
                });

            modelBuilder.Entity("Domain.Entities.WorkflowsAggregate.DynamicFormItem", b =>
                {
                    b.Navigation("Attributes");
                });

            modelBuilder.Entity("Domain.Entities.WorkflowsAggregate.DynamicFormTemplate", b =>
                {
                    b.Navigation("FlowList");

                    b.Navigation("ListTenantWorkflows");
                });
#pragma warning restore 612, 618
        }
    }
}
