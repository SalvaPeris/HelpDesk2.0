using System;
using System.Collections.Generic;
using HelpDesk.Shared.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace HelpDesk.Server.DB
{
    public partial class HelpDeskContext : DbContext
    {
        public HelpDeskContext()
        {
        }

        public HelpDeskContext(DbContextOptions<HelpDeskContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Usuario> Usuarios { get; set; }

        public virtual DbSet<Ticket> Tickets { get; set; }
        public virtual DbSet<SuplementoTicket> SuplementosTicket { get; set; }
        public virtual DbSet<EstadoTicket> EstadosTicket { get; set; }
        public virtual DbSet<TipoTicket> TiposTicket { get; set; }
        public virtual DbSet<ZonaTicket> ZonasTicket { get; set; }

        public virtual DbSet<MensajeChat> Chats { get; set; }
        public virtual DbSet<GrupoChat> GruposChat { get; set; }

        public virtual DbSet<Departamento> Departamentos { get; set; }
        public virtual DbSet<Zona> Zonas { get; set; }
        public virtual DbSet<Situacion> Situaciones { get; set; }

        public virtual DbSet<Dispositivo> Dispositivos { get; set; }
        public virtual DbSet<TipoDispositivo> TiposDispositivo { get; set; }
        public virtual DbSet<EstadoDispositivo> EstadosDispositivo { get; set; }

        public virtual DbSet<Jornada> Jornadas { get; set; }

        public virtual DbSet<Fichaje> Fichajes { get; set; }
        public virtual DbSet<EstadoFichaje> EstadosFichajes { get; set; }

        public virtual DbSet<Ausencia> Ausencias { get; set; }
        public virtual DbSet<TipoAusencia> TiposAusencias { get; set; }
        public virtual DbSet<EstadoAusencia> EstadoAusencia { get; set; }

        public virtual DbSet<Turno> Turnos { get; set; }
        public virtual DbSet<EstadoTurno> EstadoTurnos { get; set; }

        public virtual DbSet<Archivo> Archivos { get; set; }

        public virtual DbSet<EmpresaExterna> EmpresasExternas { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Usuario>()
                .HasOne<Departamento>(s => s.Departamento)
                .WithMany(t => t.Usuarios)
                .HasForeignKey(s => s.DepartamentoId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<DispositivoUsuario>().
                HasKey(d => new { d.DispositivoId, d.UsuarioId });



            #region CHAT

            modelBuilder.Entity<GrupoChatUsuario>().
                HasKey(c => new { c.GrupoChatId, c.UsuarioId });

            modelBuilder.Entity<MensajeChatUsuario>().
                HasKey(c => new { c.MensajeChatId, c.UsuarioId });

            #endregion



            #region ARCHIVOS

            modelBuilder.Entity<ArchivoUsuario>().
                HasKey(au => new { au.ArchivoId, au.UsuarioId });

            #endregion


            #region JORNADA

            modelBuilder.Entity<Jornada>()
                .HasOne<Usuario>(j => j.Usuario)
                .WithMany(u => u.Jornadas)
                .HasForeignKey(j => j.UsuarioId)
                .OnDelete(DeleteBehavior.Cascade);



            modelBuilder.Entity<Fichaje>()
                .HasOne<Jornada>(f => f.Jornada)
                .WithMany(j => j.Fichajes)
                .HasForeignKey(f => f.JornadaId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<EstadoFichaje>()
                .HasOne<Fichaje>(s => s.Fichaje)
                .WithMany(t => t.Estados)
                .HasForeignKey(s => s.FichajeId)
                .OnDelete(DeleteBehavior.Cascade);




            modelBuilder.Entity<JornadaAusencia>().
                HasKey(ja => new { ja.JornadaId, ja.AusenciaId });

            modelBuilder.Entity<EstadoAusencia>()
                .HasOne<Ausencia>(s => s.Ausencia)
                .WithMany(t => t.Estados)
                .HasForeignKey(s => s.AusenciaId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Ausencia>()
                .HasOne<TipoAusencia>(a => a.TipoAusencia)
                .WithMany(t => t.Ausencias)
                .HasForeignKey(d => d.TipoAusenciaId)
                .OnDelete(DeleteBehavior.NoAction);




            modelBuilder.Entity<JornadaTurno>().
                HasKey(jt => new { jt.JornadaId, jt.TurnoId });

            modelBuilder.Entity<EstadoTurno>()
                .HasOne<Turno>(s => s.Turno)
                .WithMany(t => t.Estados)
                .HasForeignKey(s => s.TurnoId)
                .OnDelete(DeleteBehavior.Cascade);

            #endregion

            #region TICKETS

            modelBuilder.Entity<SuplementoTicket>()
                .HasOne<Ticket>(s => s.Ticket)
                .WithMany(t => t.Suplementos)
                .HasForeignKey(s => s.TicketId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<EstadoTicket>()
                .HasOne<Ticket>(s => s.Ticket)
                .WithMany(t => t.Estados)
                .HasForeignKey(s => s.TicketId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ZonaTicket>()
                .HasOne<Ticket>(s => s.Ticket)
                .WithMany(t => t.Zonas)
                .HasForeignKey(s => s.TicketId)
                .OnDelete(DeleteBehavior.Cascade);

        #endregion

            modelBuilder.Entity<Situacion>()
                .HasOne<Zona>(s => s.Zona)
                .WithMany(z => z.Situaciones)
                .HasForeignKey(s => s.ZonaId)
                .OnDelete(DeleteBehavior.Cascade);

        #region DISPOSITIVOS

            modelBuilder.Entity<Dispositivo>()
                .HasOne<TipoDispositivo>(d => d.Tipo)
                .WithMany(t => t.Dispositivos)
                .HasForeignKey(d => d.TipoDispositivoId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<EstadoDispositivo>()
                .HasOne<Dispositivo>(s => s.Dispositivo)
                .WithMany(d => d.Estados)
                .HasForeignKey(s => s.EstadoDispositivoId)
                .OnDelete(DeleteBehavior.Cascade);

        #endregion

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
