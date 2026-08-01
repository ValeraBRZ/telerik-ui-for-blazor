using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Telerik.Blazor.Services;
using TelerikBlazorDemos.DataAccess;
using TelerikBlazorDemos.DataAccess.Services;
using TelerikBlazorDemos.Services;
using TelerikBlazorDemos.Services.GoogleSearch;
using TelerikBlazorDemos.Services.FileManager;
using TelerikBlazorDemos.Controllers;
using System.IO;
using Microsoft.AspNetCore.Rewrite;
using TelerikBlazorDemos.RewriteRules;
using TelerikBlazorDemos.Extensions;
using TelerikBlazorDemos.Pages;

namespace TelerikBlazorDemos
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public IHostEnvironment Environment { get; }

        public Startup(IConfiguration configuration, IHostEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            #region Localization

            services.AddControllers();
            services.AddLocalization(options => options.ResourcesPath = "Resources");
            services.Configure<RequestLocalizationOptions>(options =>
            {
                var supportedCultures = new List<CultureInfo>()
                {
                    new CultureInfo("en-US"),
                    new CultureInfo("de-DE"),
                    new CultureInfo("es-ES"),
                    new CultureInfo("bg-BG"),
                };

                options.DefaultRequestCulture = new RequestCulture("en-US");
                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;
            });

            #endregion

            services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    builder =>
                    {
                        builder.WithOrigins("https://baas.kinvey.com", "https://www.telerik.com");
                    });
            });

            services.AddMvc()
                .AddNewtonsoftJson();

            services.AddResponseCompression(opts =>
            {
                opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
                    new[] { "application/octet-stream" });
            });

            var sqlConnectionString = Configuration.GetConnectionString("DefaultConnection");

            if (string.IsNullOrWhiteSpace(sqlConnectionString))
            {
                var dbPath = $"{Environment.ContentRootPath}/demos.db";
                if (Environment.IsProduction())
                {
                    // In production, the SQLite DB is copied to a temp location to avoid locking issues.
                    File.Delete("/tmp/demos-local.db");
                    File.Copy($"{Environment.ContentRootPath}/demos.db", "/tmp/demos-local.db");
                    dbPath = "/tmp/demos-local.db";
                }

                var connectionStringBuilder2 = new SqliteConnectionStringBuilder
                {
                    DataSource = dbPath,
                    Mode = SqliteOpenMode.ReadOnly,
                    Cache = SqliteCacheMode.Shared
                };

                var connectionString2 = connectionStringBuilder2.ToString();
                var connection2 = new SqliteConnection(connectionString2)
                {
                    DefaultTimeout = 120
                };

                services.AddDbContextFactory<DemoDBContext>(options => options.UseSqlite(connection2).UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));
            }
            else
            {
                services.AddDbContextFactory<DemoDBContext>(options => options.UseSqlServer(sqlConnectionString).UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));
            }

            AddDBServices(services);

            services.AddHttpClient<GoogleSearchService>();
            services.AddHttpClient<FileManagerController>();
            services.AddHttpContextAccessor();
            services.AddScoped<JsonDataService>();
            services.AddScoped<IDemoList, DemoList>();
            services.AddScoped<LocalStorage>();
            services.AddScoped<HierarchicalFileService>();
            services.AddScoped<FlatFileService>();
            services.AddSingleton<BannerCacheService>();
            services.AddSingleton<AIPromptService>();
            services.AddSingleton<Services.Data.CustomerService>();
            services.AddSingleton<Services.Data.OrderService>();
            services.AddSingleton<Services.Data.PatientService>();
            services.AddSingleton<Services.Data.MedicalRecordService>();

            services.AddChatClient(services => services.GetRequiredService<AIPromptService>());

            services.AddRazorPages();

            services.AddRazorComponents()
                .AddInteractiveServerComponents()
                .AddHubOptions(o =>
                {
                    o.MaximumReceiveMessageSize = 4 * 1024 * 1024; // 4MB
                });

            services.AddTelerikBlazor();

            // register a custom localizer
            services.AddSingleton(typeof(ITelerikStringLocalizer), typeof(CustomStringLocalizer));
        }

        private void AddDBServices(IServiceCollection services)
        {
            services.AddScoped<CategoryService>();
            services.AddScoped<CustomerService>();
            services.AddScoped<GanttTaskService>();
            services.AddScoped<GanttDependencyService>();
            services.AddScoped<StockService>();
            services.AddScoped<ArticleService>();
            services.AddScoped<DestinationService>();
            services.AddScoped<MovieService>();
            services.AddScoped<PhotoContestService>();
            services.AddScoped<ProductService>();
            services.AddScoped<EmployeeDirectoryService>();
            services.AddScoped<EmployeeService>();
            services.AddScoped<OrderService>();
            services.AddScoped<OrderDetailService>();
            services.AddScoped<RegionService>();
            services.AddScoped<ShipperService>();
            services.AddScoped<SupplierService>();
            services.AddScoped<TerritoryService>();
            services.AddScoped<UrbanAreaService>();
            services.AddScoped<WeatherService>();
            services.AddScoped<AppointmentService>();
            services.AddScoped<MeetingService>();
            services.AddScoped<RoomService>();
            services.AddScoped<ManagerService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseRequestLocalization(app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>().Value);

            app.UseResponseCompression();

            if (env.IsDevelopment() || env.IsEnvironment("JSDebug"))
            {
                app.UseDeveloperExceptionPage();
                Console.WriteLine("If you are running this locally, make sure to use the base path in the URL: /blazor-ui/");
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            if (Environment.IsProduction())
            {
                using (StreamReader iisUrlRewriteStreamReader = File.OpenText("IISUrlRewrite.xml"))
                {
                    var options = new RewriteOptions()
                        .AddRedirectToHttpsPermanent()
                        .AddIISUrlRewrite(iisUrlRewriteStreamReader)
                        .Add(new RedirectLowerCaseRule())
                        .Add(new RedirectToNoQueryString());

                    app.UseRewriter(options);
                }
            }

            // add default custom response headers;
            app.Use(async (context, next) =>
            {
                // The app is embeded through an iframe within the Telerik Blazor product page.
                context.Response.Headers.AppendOrUpdate("Content-Security-Policy", "frame-ancestors 'self' https://www.telerik.com https://wwwuat.telerik.com;");
                context.Response.Headers.AppendOrUpdate("Referrer-Policy", "strict-origin-when-cross-origin");
                context.Response.Headers.AppendOrUpdate("Strict-Transport-Security", "max-age=31536000; includeSubDomains");
                context.Response.Headers.AppendOrUpdate("X-Content-Type-Options", "nosniff");
                context.Response.Headers.AppendOrUpdate("X-XSS-Protection", "1; mode=block");

                await next.Invoke();
            });

            if (env.IsEnvironment("JSDebug"))
            {
                // do not use HTTPS as this requires a local certificate to be configured for vite
            }
            else
            {
                app.UseHttpsRedirection();
            }

            app.UsePathBase("/blazor-ui/");

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAntiforgery();

            app.UseCors();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
                endpoints.MapControllers();

                endpoints.MapRazorComponents<App>().AddInteractiveServerRenderMode();
            });
        }
    }
}
