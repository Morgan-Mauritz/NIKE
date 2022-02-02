﻿// <auto-generated />
using System;
using Api;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Api.Migrations
{
    [DbContext(typeof(NIKEContext))]
    [Migration("20220201132310_20220201_AddingCategory")]
    partial class _20220201_AddingCategory
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.13");

            modelBuilder.Entity("Api.Model.Category", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Category");
                });

            modelBuilder.Entity("Api.Model.City", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasColumnName("ID");

                    b.Property<long?>("CountryId")
                        .IsRequired()
                        .HasColumnType("INTEGER")
                        .HasColumnName("CountryID");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("CountryId");

                    b.ToTable("City");
                });

            modelBuilder.Entity("Api.Model.Comment", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasColumnName("ID");

                    b.Property<string>("Comment1")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("Comment");

                    b.Property<long?>("EntryId")
                        .IsRequired()
                        .HasColumnType("INTEGER")
                        .HasColumnName("EntryID");

                    b.Property<long?>("UserId")
                        .IsRequired()
                        .HasColumnType("INTEGER")
                        .HasColumnName("UserID");

                    b.HasKey("Id");

                    b.ToTable("Comment");
                });

            modelBuilder.Entity("Api.Model.Country", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasColumnName("ID");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Country");
                });

            modelBuilder.Entity("Api.Model.Entry", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasColumnName("ID");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<long>("POIID")
                        .HasColumnType("INTEGER")
                        .HasColumnName("POIID");

                    b.Property<long?>("Rating")
                        .IsRequired()
                        .HasColumnType("INTEGER");

                    b.Property<long>("UserId")
                        .HasColumnType("INTEGER")
                        .HasColumnName("UserID");

                    b.HasKey("Id");

                    b.HasIndex("POIID");

                    b.HasIndex("UserId");

                    b.ToTable("Entry");
                });

            modelBuilder.Entity("Api.Model.LikeDislikeEntry", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasColumnName("ID");

                    b.Property<long>("EntryId")
                        .HasColumnType("INTEGER")
                        .HasColumnName("EntryId");

                    b.Property<long>("Likes")
                        .HasColumnType("INTEGER")
                        .HasColumnName("Likes");

                    b.Property<long>("UserId")
                        .HasColumnType("INTEGER")
                        .HasColumnName("UserId");

                    b.HasKey("Id");

                    b.HasIndex("EntryId");

                    b.HasIndex("UserId");

                    b.ToTable("LikeDislikeEntry");
                });

            modelBuilder.Entity("Api.Model.POI", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasColumnName("ID");

                    b.Property<long>("CategoryID")
                        .HasColumnType("INTEGER");

                    b.Property<long>("CityID")
                        .HasColumnType("INTEGER");

                    b.Property<double>("Latitude")
                        .HasColumnType("REAL")
                        .HasColumnName("Latitude");

                    b.Property<double>("Longitude")
                        .HasColumnType("REAL")
                        .HasColumnName("Longitude");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("CategoryID");

                    b.HasIndex("CityID");

                    b.ToTable("POI");
                });

            modelBuilder.Entity("Api.Model.Reaction", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasColumnName("ID");

                    b.Property<long?>("EntryId")
                        .IsRequired()
                        .HasColumnType("INTEGER")
                        .HasColumnName("EntryID");

                    b.Property<long?>("LikeDislike")
                        .IsRequired()
                        .HasColumnType("INTEGER");

                    b.Property<long?>("UserId")
                        .IsRequired()
                        .HasColumnType("INTEGER")
                        .HasColumnName("UserID");

                    b.HasKey("Id");

                    b.ToTable("Reaction");
                });

            modelBuilder.Entity("Api.Model.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasColumnName("ID");

                    b.Property<string>("ApiKey")
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("TEXT(250)");

                    b.Property<string>("Firstname")
                        .IsRequired()
                        .HasColumnType("TEXT(45)");

                    b.Property<string>("Lastname")
                        .IsRequired()
                        .HasColumnType("TEXT(100)");

                    b.Property<byte[]>("Password")
                        .IsRequired()
                        .HasColumnType("BLOB");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("User");
                });

            modelBuilder.Entity("Api.Model.City", b =>
                {
                    b.HasOne("Api.Model.Country", "Country")
                        .WithMany("Cities")
                        .HasForeignKey("CountryId")
                        .HasConstraintName("FK_Country")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Country");
                });

            modelBuilder.Entity("Api.Model.Entry", b =>
                {
                    b.HasOne("Api.Model.POI", "POI")
                        .WithMany("Entries")
                        .HasForeignKey("POIID")
                        .HasConstraintName("POIID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Api.Model.User", "User")
                        .WithMany("Entries")
                        .HasForeignKey("UserId")
                        .HasConstraintName("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("POI");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Api.Model.LikeDislikeEntry", b =>
                {
                    b.HasOne("Api.Model.Entry", "Entry")
                        .WithMany("LikeDislikeEntries")
                        .HasForeignKey("EntryId")
                        .HasConstraintName("EID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Api.Model.User", "User")
                        .WithMany("LikeDislikeEntries")
                        .HasForeignKey("UserId")
                        .HasConstraintName("UID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Entry");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Api.Model.POI", b =>
                {
                    b.HasOne("Api.Model.Category", "Category")
                        .WithMany("POIs")
                        .HasForeignKey("CategoryID")
                        .HasConstraintName("FK_Category")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Api.Model.City", "City")
                        .WithMany("POI")
                        .HasForeignKey("CityID")
                        .HasConstraintName("FK_City")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("City");
                });

            modelBuilder.Entity("Api.Model.Category", b =>
                {
                    b.Navigation("POIs");
                });

            modelBuilder.Entity("Api.Model.City", b =>
                {
                    b.Navigation("POI");
                });

            modelBuilder.Entity("Api.Model.Country", b =>
                {
                    b.Navigation("Cities");
                });

            modelBuilder.Entity("Api.Model.Entry", b =>
                {
                    b.Navigation("LikeDislikeEntries");
                });

            modelBuilder.Entity("Api.Model.POI", b =>
                {
                    b.Navigation("Entries");
                });

            modelBuilder.Entity("Api.Model.User", b =>
                {
                    b.Navigation("Entries");

                    b.Navigation("LikeDislikeEntries");
                });
#pragma warning restore 612, 618
        }
    }
}
