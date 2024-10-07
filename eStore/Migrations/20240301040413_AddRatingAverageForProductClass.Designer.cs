﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using eStore.DataAccess;

#nullable disable

namespace eStore.Migrations
{
    [DbContext(typeof(eStoreContext))]
    [Migration("20240301040413_AddRatingAverageForProductClass")]
    partial class AddRatingAverageForProductClass
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.26")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("eStore.DataAccess.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ConcurrencyStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NormalizedUserName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("eStore.DataAccess.CartItem", b =>
                {
                    b.Property<int>("CartItemId")
                        .HasColumnType("int");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<int>("ShoppingCartId")
                        .HasColumnType("int");

                    b.HasKey("CartItemId");

                    b.HasIndex("ProductId");

                    b.HasIndex("ShoppingCartId");

                    b.ToTable("CartItem", (string)null);
                });

            modelBuilder.Entity("eStore.DataAccess.Category", b =>
                {
                    b.Property<string>("CategoryId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CategoryName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CategoryId");

                    b.ToTable("Category", (string)null);
                });

            modelBuilder.Entity("eStore.DataAccess.Feedback", b =>
                {
                    b.Property<string>("FeedbackId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<double>("Rating")
                        .HasColumnType("float");

                    b.Property<DateTime>("RatingDate")
                        .HasColumnType("datetime");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("FeedbackId");

                    b.HasIndex("ProductId");

                    b.HasIndex("UserId");

                    b.ToTable("Feedback", (string)null);
                });

            modelBuilder.Entity("eStore.DataAccess.OrderItem", b =>
                {
                    b.Property<int>("OrderItemId")
                        .HasColumnType("int");

                    b.Property<int>("OrderTableId")
                        .HasColumnType("int");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<int>("Quality")
                        .HasColumnType("int");

                    b.HasKey("OrderItemId");

                    b.HasIndex("OrderTableId");

                    b.HasIndex("ProductId");

                    b.ToTable("OrderItem", (string)null);
                });

            modelBuilder.Entity("eStore.DataAccess.OrderTable", b =>
                {
                    b.Property<int>("OrderTableId")
                        .HasColumnType("int");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserAddress")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("UserPhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("OrderTableId");

                    b.HasIndex("UserId");

                    b.ToTable("OrderTable", (string)null);
                });

            modelBuilder.Entity("eStore.DataAccess.Product", b =>
                {
                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<string>("CategoryId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProductName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ReleasedDate")
                        .HasColumnType("datetime");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("UnitPrice")
                        .HasColumnType("money");

                    b.Property<int>("UnitsInstock")
                        .HasColumnType("int");

                    b.Property<int>("ratingAverage")
                        .HasColumnType("int");

                    b.HasKey("ProductId");

                    b.HasIndex("CategoryId");

                    b.ToTable("Product", (string)null);
                });

            modelBuilder.Entity("eStore.DataAccess.ProductImage", b =>
                {
                    b.Property<int>("ImageId")
                        .HasColumnType("int");

                    b.Property<byte[]>("ImageData")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("ImageName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("ImageSize")
                        .HasColumnType("float");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.HasKey("ImageId");

                    b.HasIndex("ProductId");

                    b.ToTable("ProductImage", (string)null);
                });

            modelBuilder.Entity("eStore.DataAccess.ShoppingCart", b =>
                {
                    b.Property<int>("ShoppingCartId")
                        .HasColumnType("int");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("ShoppingCartId");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("ShoppingCart", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NormalizedName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("RoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("UserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(max)");

                    b.ToTable("UserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.ToTable("UserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.ToTable("UserTokens");
                });

            modelBuilder.Entity("eStore.DataAccess.CartItem", b =>
                {
                    b.HasOne("eStore.DataAccess.Product", "Product")
                        .WithMany("CartItems")
                        .HasForeignKey("ProductId")
                        .IsRequired()
                        .HasConstraintName("FK_CartItem_Product");

                    b.HasOne("eStore.DataAccess.ShoppingCart", "ShoppingCart")
                        .WithMany("CartItems")
                        .HasForeignKey("ShoppingCartId")
                        .IsRequired()
                        .HasConstraintName("FK_CartItem_ShoppingCart");

                    b.Navigation("Product");

                    b.Navigation("ShoppingCart");
                });

            modelBuilder.Entity("eStore.DataAccess.Feedback", b =>
                {
                    b.HasOne("eStore.DataAccess.Product", "Product")
                        .WithMany("Feedbacks")
                        .HasForeignKey("ProductId")
                        .IsRequired()
                        .HasConstraintName("FK_Feedback_Product");

                    b.HasOne("eStore.DataAccess.ApplicationUser", "User")
                        .WithMany("Feedbacks")
                        .HasForeignKey("UserId")
                        .IsRequired()
                        .HasConstraintName("FK_Feedback_User");

                    b.Navigation("Product");

                    b.Navigation("User");
                });

            modelBuilder.Entity("eStore.DataAccess.OrderItem", b =>
                {
                    b.HasOne("eStore.DataAccess.OrderTable", "OrderTable")
                        .WithMany("OrderItems")
                        .HasForeignKey("OrderTableId")
                        .IsRequired()
                        .HasConstraintName("FK_OrderItem_OrderTable");

                    b.HasOne("eStore.DataAccess.Product", "Product")
                        .WithMany("OrderItems")
                        .HasForeignKey("ProductId")
                        .IsRequired()
                        .HasConstraintName("FK_OrderItem_Product");

                    b.Navigation("OrderTable");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("eStore.DataAccess.OrderTable", b =>
                {
                    b.HasOne("eStore.DataAccess.ApplicationUser", "User")
                        .WithMany("OrderTables")
                        .HasForeignKey("UserId")
                        .IsRequired()
                        .HasConstraintName("FK_OrderTable_User");

                    b.Navigation("User");
                });

            modelBuilder.Entity("eStore.DataAccess.Product", b =>
                {
                    b.HasOne("eStore.DataAccess.Category", "Category")
                        .WithMany("Products")
                        .HasForeignKey("CategoryId")
                        .IsRequired()
                        .HasConstraintName("FK_Product_Category");

                    b.Navigation("Category");
                });

            modelBuilder.Entity("eStore.DataAccess.ProductImage", b =>
                {
                    b.HasOne("eStore.DataAccess.Product", "Product")
                        .WithMany("ProductImages")
                        .HasForeignKey("ProductId")
                        .IsRequired()
                        .HasConstraintName("FK_ProductImage_Product");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("eStore.DataAccess.ShoppingCart", b =>
                {
                    b.HasOne("eStore.DataAccess.ApplicationUser", "User")
                        .WithOne("ShoppingCarts")
                        .HasForeignKey("eStore.DataAccess.ShoppingCart", "UserId")
                        .IsRequired()
                        .HasConstraintName("FK_ShoppingCart_User");

                    b.Navigation("User");
                });

            modelBuilder.Entity("eStore.DataAccess.ApplicationUser", b =>
                {
                    b.Navigation("Feedbacks");

                    b.Navigation("OrderTables");

                    b.Navigation("ShoppingCarts")
                        .IsRequired();
                });

            modelBuilder.Entity("eStore.DataAccess.Category", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("eStore.DataAccess.OrderTable", b =>
                {
                    b.Navigation("OrderItems");
                });

            modelBuilder.Entity("eStore.DataAccess.Product", b =>
                {
                    b.Navigation("CartItems");

                    b.Navigation("Feedbacks");

                    b.Navigation("OrderItems");

                    b.Navigation("ProductImages");
                });

            modelBuilder.Entity("eStore.DataAccess.ShoppingCart", b =>
                {
                    b.Navigation("CartItems");
                });
#pragma warning restore 612, 618
        }
    }
}
