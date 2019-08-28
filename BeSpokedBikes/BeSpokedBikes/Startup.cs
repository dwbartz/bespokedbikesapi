using System;
using BeSpokedBikes.DAL;
using BeSpokedBikes.Models;
using BeSpokedBikes.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BeSpokedBikes
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<BikesContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("Database")));
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddScoped<CustomersService>();
            services.AddScoped<DiscountsService>();
            services.AddScoped<ProductsService>();
            services.AddScoped<ReportsService>();
            services.AddScoped<SalesPersonsService>();
            services.AddScoped<SalesService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            SetupDatabase();

            app.UseHttpsRedirection();
            app.UseMvc();
        }

        private void SetupDatabase()
        {
            var builder = new DbContextOptionsBuilder<BikesContext>()
                .UseSqlServer(Configuration.GetConnectionString("Database"));
            var context = new BikesContext(builder.Options);

            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            using (var transaction = context.Database.BeginTransaction())
            {
                context.Database.ExecuteSqlCommand(@"SET IDENTITY_INSERT [Customers] ON");
                context.Customers.Add(new Customer
                {
                    Id = 1,
                    FirstName = "First",
                    LastName = "Last",
                    Address = "1 Street",
                    Phone = "123-123-1234",
                    StartDate = DateTime.UtcNow
                });
                context.Customers.Add(new Customer
                {
                    Id = 2,
                    FirstName = "First2",
                    LastName = "Last2",
                    Address = "2 Street",
                    Phone = "123-123-9999",
                    StartDate = DateTime.UtcNow
                });
                context.SaveChanges();
                context.Database.ExecuteSqlCommand(@"SET IDENTITY_INSERT [Customers] OFF");

                context.Database.ExecuteSqlCommand(@"SET IDENTITY_INSERT [Products] ON");
                // Commission = .125
                context.Products.Add(new Product
                {
                    Id = 1,
                    Manufacturer = "Acme",
                    Name = "Toy",
                    SalePrice = new decimal(1.25),
                    Style = "Toddler",
                    PurchasePrice = new decimal(1.50),
                    QuantityAvailable = 5,
                    CommissionPercentage = new decimal(.10)
                });
                // Commission .2625
                context.Products.Add(new Product
                {
                    Id = 2,
                    Manufacturer = "Acme",
                    Name = "Toy 2",
                    SalePrice = new decimal(1.75),
                    Style = "Toddler",
                    PurchasePrice = new decimal(1.50),
                    QuantityAvailable = 5,
                    CommissionPercentage = new decimal(.15)
                });
                // Commission .05
                context.Products.Add(new Product
                {
                    Id = 3,
                    Manufacturer = "Acme",
                    Name = "Toy 3",
                    SalePrice = new decimal(1.00),
                    Style = "Toddler",
                    PurchasePrice = new decimal(1.50),
                    QuantityAvailable = 5,
                    CommissionPercentage = new decimal(.05)
                });
                context.SaveChanges();
                context.Database.ExecuteSqlCommand(@"SET IDENTITY_INSERT [Products] OFF");

                context.Database.ExecuteSqlCommand(@"SET IDENTITY_INSERT [SalesPersons] ON");
                context.SalesPersons.Add(new SalesPerson
                {
                    Id = 1,
                    FirstName = "Good",
                    LastName = "Salesman",
                    Address = "1 Street Ln",
                    Phone = "321-321-4321",
                    TerminationDate = DateTime.MaxValue,
                    StartDate = DateTime.MinValue,
                    Manager = "Joe"
                });
                context.SalesPersons.Add(new SalesPerson
                {
                    Id = 2,
                    FirstName = "Better",
                    LastName = "Salesman",
                    Address = "2 Street Ln",
                    Phone = "321-321-4321",
                    TerminationDate = DateTime.MaxValue,
                    StartDate = DateTime.MinValue,
                    Manager = "Joe"
                });
                context.SaveChanges();
                context.Database.ExecuteSqlCommand(@"SET IDENTITY_INSERT [SalesPersons] OFF");

                context.Database.ExecuteSqlCommand(@"SET IDENTITY_INSERT [Discounts] ON");
                context.Add(new Discount
                {
                    BeginDate = DateTime.MinValue,
                    EndDate = DateTime.MaxValue,
                    ProductId = 3,
                    DiscountPercentage = new decimal(.05),
                    Id = 1
                });
                context.SaveChanges();
                context.Database.ExecuteSqlCommand(@"SET IDENTITY_INSERT [Discounts] OFF");

                context.Database.ExecuteSqlCommand(@"SET IDENTITY_INSERT [Sales] ON");
                context.Sales.Add(new Sale
                {
                    Id = 1,
                    CustomerId = 1,
                    ProductId = 1,
                    SalesPersonId = 1,
                    SalesDate = DateTime.Parse("8/27/19")
                });
                context.Sales.Add(new Sale
                {
                    Id = 2,
                    CustomerId = 1,
                    ProductId = 1,
                    SalesPersonId = 2,
                    SalesDate = DateTime.Parse("8/27/19")
                });
                context.Sales.Add(new Sale
                {
                    Id = 3,
                    CustomerId = 2,
                    ProductId = 2,
                    SalesPersonId = 1,
                    SalesDate = DateTime.Parse("8/27/19")
                });
                context.Sales.Add(new Sale
                {
                    Id = 4,
                    CustomerId = 2,
                    ProductId = 3,
                    SalesPersonId = 2,
                    SalesDate = DateTime.Parse("8/27/19")
                });
                context.SaveChanges();
                context.Database.ExecuteSqlCommand(@"SET IDENTITY_INSERT [Sales] OFF");
                transaction.Commit();
            }
        }
    }
}
