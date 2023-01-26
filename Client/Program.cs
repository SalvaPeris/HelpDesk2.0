using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using HelpDesk.ViewModels;
using Microsoft.AspNetCore.Components.Authorization;
using Radzen;
using HelpDesk.Client.Services;
using HelpDesk.Client.Authentication;

namespace HelpDesk.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");

            builder.Services.AddTransient(sp => 
                new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            builder.Services.AddHttpClient<IProfileViewModel, ProfileViewModel>
                ("HelpDeskClient", client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress));

            builder.Services.AddHttpClient<ISettingsViewModel, SettingsViewModel>
                ("HelpDeskClient", client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress));

            builder.Services.AddHttpClient<ILoginViewModel, LoginViewModel>
                ("HelpDeskClient", client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress));

            builder.Services.AddHttpClient<IMainViewModel, MainViewModel>
                ("HelpDeskClient", client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress));

            builder.Services.AddHttpClient<ITicketViewModel, TicketViewModel>
                ("HelpDeskClient", client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress));

            builder.Services.AddHttpClient<ISingleTicketViewModel, SingleTicketViewModel>
                ("HelpDeskClient", client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress));

            //Para no refrescar cada vez que se abra la ventana, le añado el Singleton

            //USUARIO ACTUAL
            builder.Services.AddSingleton<IMainViewModel, MainViewModel>();
            builder.Services.AddSingleton<IProfileViewModel, ProfileViewModel>();
            builder.Services.AddSingleton<ISettingsViewModel, SettingsViewModel>();

                //MIS DISPOSITIVOS
                builder.Services.AddScoped<IDispositivoViewModel, DispositivoViewModel>();

                //MIS JORNADAS
                builder.Services.AddScoped<IJornadaViewModel, JornadaViewModel>();

                //CONTRASEÑA
                builder.Services.AddScoped<IRecuperarContrasenaViewModel, RecuperarContrasenaViewModel>();


            //TICKETS
            builder.Services.AddSingleton<ITicketViewModel, TicketViewModel>();
            builder.Services.AddScoped<ISingleTicketViewModel, SingleTicketViewModel>();
            builder.Services.AddScoped<ITipoTicketViewModel, TipoTicketViewModel>();
            builder.Services.AddScoped<ISuplementoViewModel, SuplementoViewModel>();
            builder.Services.AddScoped<IZonaTicketViewModel, ZonaTicketViewModel>();
            builder.Services.AddScoped<IDepartamentoViewModel, DepartamentoViewModel>();
            builder.Services.AddScoped<IZonaViewModel, ZonaViewModel>();

            //CHAT
            builder.Services.AddScoped<IUsuariosChatViewModel, UsuariosChatViewModel>();
            builder.Services.AddScoped<IGrupoChatViewModel, GrupoChatViewModel>();

            //AGENDA
            builder.Services.AddScoped<IAgendaViewModel, AgendaViewModel>();


            //DISPOSITIVOS
            builder.Services.AddScoped<IDispositivosViewModel, DispositivosViewModel>();
            builder.Services.AddScoped<ISingleDispositivoViewModel, SingleDispositivoViewModel>();
            builder.Services.AddScoped<ITipoDispositivoViewModel, TipoDispositivoViewModel>();


            //USUARIOS
            builder.Services.AddScoped<IUsuariosViewModel, UsuariosViewModel>();
            builder.Services.AddScoped<ISingleUsuarioViewModel, SingleUsuarioViewModel>();


            //JORNADAS
            builder.Services.AddScoped<IJornadasViewModel, JornadasViewModel>();
            builder.Services.AddScoped<ISingleJornadaViewModel, SingleJornadaViewModel>();


            //SHARED
            builder.Services.AddScoped<IEmpresaExternaViewModel, EmpresaExternaViewModel>();



            //AUTENTICACIÓN
            builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();

            builder.Services.AddOptions();
            builder.Services.AddAuthorizationCore(options => {

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

                options.AddPolicy("EsInformática", policy => {
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
                options.AddPolicy("EsSeguridad", policy => {
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

            //Servicios RADZEN
            builder.Services.AddScoped<DialogService>();
            builder.Services.AddScoped<NotificationService>();
            builder.Services.AddScoped<TooltipService>();
            builder.Services.AddScoped<ContextMenuService>();

            //Servicio para compartir ShowNotifications()
            builder.Services.AddScoped<MessageService>();

            await builder.Build().RunAsync();
        }
    }
}
