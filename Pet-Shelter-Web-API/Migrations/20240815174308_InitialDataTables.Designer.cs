﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PetShelterWebAPI.Data;

#nullable disable

namespace Pet_Shelter_Web_API.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20240815174308_InitialDataTables")]
    partial class InitialDataTables
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("PetShelterWebAPI.Models.Breed", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Breeds");
                });

            modelBuilder.Entity("PetShelterWebAPI.Models.JoinTables.PetOwner", b =>
                {
                    b.Property<int>("PetId")
                        .HasColumnType("int");

                    b.Property<int>("OwnerId")
                        .HasColumnType("int");

                    b.HasKey("PetId", "OwnerId");

                    b.HasIndex("OwnerId");

                    b.ToTable("PetOwners");
                });

            modelBuilder.Entity("PetShelterWebAPI.Models.Note", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PetId")
                        .HasColumnType("int");

                    b.Property<int>("WorkerId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PetId");

                    b.HasIndex("WorkerId");

                    b.ToTable("Notes");
                });

            modelBuilder.Entity("PetShelterWebAPI.Models.Owner", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PhoneNumber")
                        .HasColumnType("int");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Owners");
                });

            modelBuilder.Entity("PetShelterWebAPI.Models.Pet", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Age")
                        .HasColumnType("int");

                    b.Property<int>("BreedId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ShelterId")
                        .HasColumnType("int");

                    b.Property<int>("SpecieId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BreedId");

                    b.HasIndex("ShelterId");

                    b.HasIndex("SpecieId");

                    b.ToTable("Pets");
                });

            modelBuilder.Entity("PetShelterWebAPI.Models.Shelter", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Shelters");
                });

            modelBuilder.Entity("PetShelterWebAPI.Models.Specie", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Species");
                });

            modelBuilder.Entity("PetShelterWebAPI.Models.Worker", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateOnly>("DateOfBirth")
                        .HasColumnType("date");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PhoneNumber")
                        .HasColumnType("int");

                    b.Property<int>("ShelterId")
                        .HasColumnType("int");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ShelterId");

                    b.ToTable("Workers");
                });

            modelBuilder.Entity("PetShelterWebAPI.Models.JoinTables.PetOwner", b =>
                {
                    b.HasOne("PetShelterWebAPI.Models.Owner", "Owner")
                        .WithMany("PetOwners")
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PetShelterWebAPI.Models.Pet", "Pet")
                        .WithMany("PetOwners")
                        .HasForeignKey("PetId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Owner");

                    b.Navigation("Pet");
                });

            modelBuilder.Entity("PetShelterWebAPI.Models.Note", b =>
                {
                    b.HasOne("PetShelterWebAPI.Models.Pet", "Pet")
                        .WithMany("Notes")
                        .HasForeignKey("PetId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PetShelterWebAPI.Models.Worker", "Worker")
                        .WithMany("Notes")
                        .HasForeignKey("WorkerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Pet");

                    b.Navigation("Worker");
                });

            modelBuilder.Entity("PetShelterWebAPI.Models.Pet", b =>
                {
                    b.HasOne("PetShelterWebAPI.Models.Breed", "Breed")
                        .WithMany("Pets")
                        .HasForeignKey("BreedId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PetShelterWebAPI.Models.Shelter", "Shelter")
                        .WithMany("Pets")
                        .HasForeignKey("ShelterId");

                    b.HasOne("PetShelterWebAPI.Models.Specie", "Specie")
                        .WithMany("Pets")
                        .HasForeignKey("SpecieId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Breed");

                    b.Navigation("Shelter");

                    b.Navigation("Specie");
                });

            modelBuilder.Entity("PetShelterWebAPI.Models.Worker", b =>
                {
                    b.HasOne("PetShelterWebAPI.Models.Shelter", "Shelter")
                        .WithMany("Workers")
                        .HasForeignKey("ShelterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Shelter");
                });

            modelBuilder.Entity("PetShelterWebAPI.Models.Breed", b =>
                {
                    b.Navigation("Pets");
                });

            modelBuilder.Entity("PetShelterWebAPI.Models.Owner", b =>
                {
                    b.Navigation("PetOwners");
                });

            modelBuilder.Entity("PetShelterWebAPI.Models.Pet", b =>
                {
                    b.Navigation("Notes");

                    b.Navigation("PetOwners");
                });

            modelBuilder.Entity("PetShelterWebAPI.Models.Shelter", b =>
                {
                    b.Navigation("Pets");

                    b.Navigation("Workers");
                });

            modelBuilder.Entity("PetShelterWebAPI.Models.Specie", b =>
                {
                    b.Navigation("Pets");
                });

            modelBuilder.Entity("PetShelterWebAPI.Models.Worker", b =>
                {
                    b.Navigation("Notes");
                });
#pragma warning restore 612, 618
        }
    }
}
