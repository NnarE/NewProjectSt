﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NewProject.Models;

namespace NewProject.Migrations
{
    [DbContext(typeof(NewProjectContext))]
    [Migration("20190202084901_First")]
    partial class First
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.1-servicing-10028")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("NewProject.Models.Ambion", b =>
                {
                    b.Property<int>("AmbionId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AmbionName");

                    b.HasKey("AmbionId");

                    b.ToTable("Ambion");
                });

            modelBuilder.Entity("NewProject.Models.Grade", b =>
                {
                    b.Property<int>("GradeId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("GradeName");

                    b.HasKey("GradeId");

                    b.ToTable("Grade");
                });

            modelBuilder.Entity("NewProject.Models.Student", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AmbionRefId");

                    b.Property<int>("GradeRefId");

                    b.Property<string>("LastName");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.HasIndex("AmbionRefId");

                    b.HasIndex("GradeRefId");

                    b.ToTable("Student");
                });

            modelBuilder.Entity("NewProject.Models.Student", b =>
                {
                    b.HasOne("NewProject.Models.Ambion", "Ambion")
                        .WithMany("Students")
                        .HasForeignKey("AmbionRefId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("NewProject.Models.Grade", "Grade")
                        .WithMany("Students")
                        .HasForeignKey("GradeRefId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
