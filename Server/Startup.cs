using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Linq;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using HelpDesk.Server.DB;
using HelpDesk.Shared.Models;
using HelpDesk.Server.Hubs;
using System;

namespace HelpDesk.Server
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllersWithViews();
            services.AddRazorPages();

            string mySqlConnectionStr = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<HelpDeskContext>(options => options.UseMySql(mySqlConnectionStr, ServerVersion.AutoDetect(mySqlConnectionStr)));

            //SQL SERVER 
            //services.AddEntityFrameworkSqlite().AddDbContext<BlazingContext>();

            services.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            }
            ).AddCookie(options =>
            {
                options.Cookie.IsEssential = true;
                options.ExpireTimeSpan = TimeSpan.FromHours(10);
            });

            // POLÍTICAS
            services.AddAuthorizationCore(options => {

                ///SUPERADMINISTRADOR
                options.AddPolicy("EsSuperAdministrador", policy => {
                    policy.RequireClaim("Role", "SuperAdministrador");
                });

                options.AddPolicy("EsAdministrador", policy => {
                    policy.RequireClaim("Role", "SuperAdministrador", "Administrador");
                });

                //DIRECCIÓN
                options.AddPolicy("EsAdministradorDirección", policy => {
                    policy.RequireClaim("Role", "Administrador", "SuperAdministrador");
                    policy.RequireClaim("GroupSid", "Dirección", "Todos");
                });
                options.AddPolicy("EsDirección", policy => {
                    policy.RequireClaim("GroupSid", "Dirección", "Todos");
                });

                //IT
                options.AddPolicy("EsAdministradorInformática", policy => {
                    policy.RequireClaim("Role", "Administrador", "SuperAdministrador");
                    policy.RequireClaim("GroupSid", "Informática", "Todos");
                });

                options.AddPolicy("EsIT", policy => {
                    policy.RequireClaim("GroupSid", "Informática", "Todos");
                });

                //MANTENIMIENTO
                options.AddPolicy("EsAdministradorMantenimiento", policy => {
                    policy.RequireClaim("Role", "Administrador", "SuperAdministrador");
                    policy.RequireClaim("GroupSid", "Mantenimiento", "Todos");
                });
                options.AddPolicy("EsMantenimiento", policy => {
                    policy.RequireClaim("GroupSid", "Mantenimiento", "Todos");
                });

                //OBRAS
                options.AddPolicy("EsAdministradorObras", policy => {
                    policy.RequireClaim("Role", "Administrador", "SuperAdministrador");
                    policy.RequireClaim("GroupSid", "Obras", "Todos");
                });
                options.AddPolicy("EsObras", policy => {
                    policy.RequireClaim("GroupSid", "Obras", "Todos");
                });

                //RRHH
                options.AddPolicy("EsAdministradorRecursos Humanos", policy => {
                    policy.RequireClaim("Role", "Administrador", "SuperAdministrador");
                    policy.RequireClaim("GroupSid", "Recursos Humanos", "Todos");
                });
                options.AddPolicy("EsRecursos Humanos", policy => {
                    policy.RequireClaim("GroupSid", "Recursos Humanos", "Todos");
                });

                //COMERCIAL
                options.AddPolicy("EsAdministradorComercial", policy => {
                    policy.RequireClaim("Role", "Administrador", "SuperAdministrador");
                    policy.RequireClaim("GroupSid", "Comercial", "Todos");
                });
                options.AddPolicy("EsComercial", policy => {
                    policy.RequireClaim("GroupSid", "Comercial", "Todos");
                });

                //SEGURIDAD
                options.AddPolicy("EsAdministradorSeguridad", policy => {
                    policy.RequireClaim("Role", "Administrador", "SuperAdministrador");
                    policy.RequireClaim("GroupSid", "Seguridad", "Todos");
                });
                options.AddPolicy("EsSEG", policy => {
                    policy.RequireClaim("GroupSid", "Seguridad", "Todos");
                });

                //RESTAURACIÓN
                options.AddPolicy("EsAdministradorRestauración", policy => {
                    policy.RequireClaim("Role", "Administrador", "SuperAdministrador");
                    policy.RequireClaim("GroupSid", "Restauración", "Todos");
                });
                options.AddPolicy("EsRestauración", policy => {
                    policy.RequireClaim("GroupSid", "Restauración", "Todos");
                });

                //LIMPIEZA
                options.AddPolicy("EsAdministradorLimpieza", policy => {
                    policy.RequireClaim("Role", "Administrador", "SuperAdministrador");
                    policy.RequireClaim("GroupSid", "Limpieza", "Todos");
                });
                options.AddPolicy("EsLimpieza", policy => {
                    policy.RequireClaim("GroupSid", "Limpieza", "Todos");
                });

                //RECEPCIÓN
                options.AddPolicy("EsAdministradorRecepción", policy => {
                    policy.RequireClaim("Role", "Administrador", "SuperAdministrador");
                    policy.RequireClaim("GroupSid", "Recepción", "Todos");
                });
                options.AddPolicy("EsRecepción", policy => {
                    policy.RequireClaim("GroupSid", "Recepción", "Todos");
                });

                //ADMINISTRACIÓN
                options.AddPolicy("EsAdministradorAdministración", policy => {
                    policy.RequireClaim("Role", "Administrador", "SuperAdministrador");
                    policy.RequireClaim("GroupSid", "Administración", "Todos");
                });
                options.AddPolicy("EsAdministración", policy => {
                    policy.RequireClaim("GroupSid", "Administración", "Todos");
                });

                //LEGAL
                options.AddPolicy("EsAdministradorLegal", policy => {
                    policy.RequireClaim("Role", "Administrador", "SuperAdministrador");
                    policy.RequireClaim("GroupSid", "Legal", "Todos");
                });
                options.AddPolicy("EsLegal", policy => {
                    policy.RequireClaim("GroupSid", "Legal", "Todos");
                });
            });

            //Configuración general de la aplicación
            services.Configure<Configuracion>(Configuration);

            //Se añade para el chat
            services.AddResponseCompression(opts =>
            {
                opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
                    new[] { "application/octet-stream" });
            });

            services.AddSignalR();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseWebAssemblyDebugging();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseBlazorFrameworkFiles();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseResponseCompression();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();
                endpoints.MapFallbackToFile("index.html");
                endpoints.MapHub<ChatHub>("/chatHub");
            });
        }
    }
}
