﻿// <auto-generated />
using System;
using HRInPocket.DAL.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace HRInPocket.DAL.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.0-rc.2.20475.6");

            modelBuilder.Entity("EmployerSystemManager", b =>
                {
                    b.Property<Guid>("EmployersId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("SystemManagersId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("EmployersId", "SystemManagersId");

                    b.HasIndex("SystemManagersId");

                    b.ToTable("EmployerSystemManager");
                });

            modelBuilder.Entity("HRInPocket.DAL.Models.Entities.ActivityCategory", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .UseIdentityColumn();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ActivityCategories");
                });

            modelBuilder.Entity("HRInPocket.DAL.Models.Entities.Address", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .UseIdentityColumn();

                    b.Property<string>("Building")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Country")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Street")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ZipCode")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Addresses");
                });

            modelBuilder.Entity("HRInPocket.DAL.Models.Entities.Company", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .UseIdentityColumn();

                    b.Property<Guid?>("EmployerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<long?>("FactAddressId")
                        .HasColumnType("bigint");

                    b.Property<string>("Inn")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<long?>("LegalAddressId")
                        .HasColumnType("bigint");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("EmployerId");

                    b.HasIndex("FactAddressId");

                    b.HasIndex("Inn")
                        .IsUnique();

                    b.HasIndex("LegalAddressId");

                    b.ToTable("Companies");
                });

            modelBuilder.Entity("HRInPocket.DAL.Models.Entities.Resume", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .UseIdentityColumn();

                    b.Property<Guid?>("ApplicantId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("ApplicantId");

                    b.ToTable("Resumes");
                });

            modelBuilder.Entity("HRInPocket.DAL.Models.Entities.ResumeValue", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .UseIdentityColumn();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long?>("ResumeId")
                        .HasColumnType("bigint");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ResumeId");

                    b.ToTable("ResumeValue");
                });

            modelBuilder.Entity("HRInPocket.DAL.Models.Entities.Speciality", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .UseIdentityColumn();

                    b.Property<long?>("ActivityCategoryId")
                        .HasColumnType("bigint");

                    b.Property<Guid?>("ApplicantId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ActivityCategoryId");

                    b.HasIndex("ApplicantId");

                    b.ToTable("Specialties");
                });

            modelBuilder.Entity("HRInPocket.DAL.Models.Entities.Tarif", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .UseIdentityColumn();

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("Visits")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Tarifs");
                });

            modelBuilder.Entity("HRInPocket.DAL.Models.Entities.Vacancy", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .UseIdentityColumn();

                    b.Property<long?>("CompanyId")
                        .HasColumnType("bigint");

                    b.Property<int>("MaxSalary")
                        .HasColumnType("int");

                    b.Property<int>("MinSalary")
                        .HasColumnType("int");

                    b.Property<long?>("SpecialtyId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.HasIndex("SpecialtyId");

                    b.ToTable("Vacancies");
                });

            modelBuilder.Entity("HRInPocket.DAL.Models.Users.Applicant", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<long?>("AddressId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("Birthday")
                        .HasColumnType("datetime2");

                    b.Property<string>("EmailAddress")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Patronymic")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("RegisterDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("SystemManagerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<long?>("TarifId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("AddressId");

                    b.HasIndex("SystemManagerId");

                    b.HasIndex("TarifId");

                    b.ToTable("Applicants");
                });

            modelBuilder.Entity("HRInPocket.DAL.Models.Users.CompanyManager", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<long?>("CompanyId")
                        .HasColumnType("bigint");

                    b.Property<string>("EmailAddress")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("EmployerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Patronymic")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("RegisterDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.HasIndex("EmployerId");

                    b.ToTable("CompanyManagers");
                });

            modelBuilder.Entity("HRInPocket.DAL.Models.Users.Employer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("EmailAddress")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Patronymic")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("RegisterDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Employers");
                });

            modelBuilder.Entity("HRInPocket.DAL.Models.Users.SystemManager", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("EmailAddress")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Patronymic")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("RegisterDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("SystemManagers");
                });

            modelBuilder.Entity("EmployerSystemManager", b =>
                {
                    b.HasOne("HRInPocket.DAL.Models.Users.Employer", null)
                        .WithMany()
                        .HasForeignKey("EmployersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HRInPocket.DAL.Models.Users.SystemManager", null)
                        .WithMany()
                        .HasForeignKey("SystemManagersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("HRInPocket.DAL.Models.Entities.Company", b =>
                {
                    b.HasOne("HRInPocket.DAL.Models.Users.Employer", "Employer")
                        .WithMany("Companies")
                        .HasForeignKey("EmployerId");

                    b.HasOne("HRInPocket.DAL.Models.Entities.Address", "FactAddress")
                        .WithMany()
                        .HasForeignKey("FactAddressId");

                    b.HasOne("HRInPocket.DAL.Models.Entities.Address", "LegalAddress")
                        .WithMany()
                        .HasForeignKey("LegalAddressId");

                    b.Navigation("Employer");

                    b.Navigation("FactAddress");

                    b.Navigation("LegalAddress");
                });

            modelBuilder.Entity("HRInPocket.DAL.Models.Entities.Resume", b =>
                {
                    b.HasOne("HRInPocket.DAL.Models.Users.Applicant", "Applicant")
                        .WithMany("Resumes")
                        .HasForeignKey("ApplicantId");

                    b.Navigation("Applicant");
                });

            modelBuilder.Entity("HRInPocket.DAL.Models.Entities.ResumeValue", b =>
                {
                    b.HasOne("HRInPocket.DAL.Models.Entities.Resume", null)
                        .WithMany("Values")
                        .HasForeignKey("ResumeId");
                });

            modelBuilder.Entity("HRInPocket.DAL.Models.Entities.Speciality", b =>
                {
                    b.HasOne("HRInPocket.DAL.Models.Entities.ActivityCategory", "ActivityCategory")
                        .WithMany("Specialties")
                        .HasForeignKey("ActivityCategoryId");

                    b.HasOne("HRInPocket.DAL.Models.Users.Applicant", null)
                        .WithMany("Speciality")
                        .HasForeignKey("ApplicantId");

                    b.Navigation("ActivityCategory");
                });

            modelBuilder.Entity("HRInPocket.DAL.Models.Entities.Vacancy", b =>
                {
                    b.HasOne("HRInPocket.DAL.Models.Entities.Company", "Company")
                        .WithMany("Vacancies")
                        .HasForeignKey("CompanyId");

                    b.HasOne("HRInPocket.DAL.Models.Entities.Speciality", "Specialty")
                        .WithMany()
                        .HasForeignKey("SpecialtyId");

                    b.Navigation("Company");

                    b.Navigation("Specialty");
                });

            modelBuilder.Entity("HRInPocket.DAL.Models.Users.Applicant", b =>
                {
                    b.HasOne("HRInPocket.DAL.Models.Entities.Address", "Address")
                        .WithMany()
                        .HasForeignKey("AddressId");

                    b.HasOne("HRInPocket.DAL.Models.Users.SystemManager", "SystemManager")
                        .WithMany("Applicants")
                        .HasForeignKey("SystemManagerId");

                    b.HasOne("HRInPocket.DAL.Models.Entities.Tarif", "Tarif")
                        .WithMany("Applicants")
                        .HasForeignKey("TarifId");

                    b.Navigation("Address");

                    b.Navigation("SystemManager");

                    b.Navigation("Tarif");
                });

            modelBuilder.Entity("HRInPocket.DAL.Models.Users.CompanyManager", b =>
                {
                    b.HasOne("HRInPocket.DAL.Models.Entities.Company", "Company")
                        .WithMany()
                        .HasForeignKey("CompanyId");

                    b.HasOne("HRInPocket.DAL.Models.Users.Employer", null)
                        .WithMany("CompanyManagers")
                        .HasForeignKey("EmployerId");

                    b.Navigation("Company");
                });

            modelBuilder.Entity("HRInPocket.DAL.Models.Entities.ActivityCategory", b =>
                {
                    b.Navigation("Specialties");
                });

            modelBuilder.Entity("HRInPocket.DAL.Models.Entities.Company", b =>
                {
                    b.Navigation("Vacancies");
                });

            modelBuilder.Entity("HRInPocket.DAL.Models.Entities.Resume", b =>
                {
                    b.Navigation("Values");
                });

            modelBuilder.Entity("HRInPocket.DAL.Models.Entities.Tarif", b =>
                {
                    b.Navigation("Applicants");
                });

            modelBuilder.Entity("HRInPocket.DAL.Models.Users.Applicant", b =>
                {
                    b.Navigation("Resumes");

                    b.Navigation("Speciality");
                });

            modelBuilder.Entity("HRInPocket.DAL.Models.Users.Employer", b =>
                {
                    b.Navigation("Companies");

                    b.Navigation("CompanyManagers");
                });

            modelBuilder.Entity("HRInPocket.DAL.Models.Users.SystemManager", b =>
                {
                    b.Navigation("Applicants");
                });
#pragma warning restore 612, 618
        }
    }
}
