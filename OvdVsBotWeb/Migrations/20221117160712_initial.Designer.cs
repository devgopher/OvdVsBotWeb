// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using OvdVsBotWeb.DataAccess;

#nullable disable

namespace OvdVsBotWeb.Migrations
{
    [DbContext(typeof(OvdDbContext))]
    [Migration("20221117160712_initial")]
    partial class initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.0");

            modelBuilder.Entity("OvdVsBotWeb.Models.Data.Chat", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("ChatId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Chats");
                });
#pragma warning restore 612, 618
        }
    }
}
