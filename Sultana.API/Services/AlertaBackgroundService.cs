using Sultana.API.Data;
using Sultana.API.Helpers;
using Sultana.Shared.Entities;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Sultana.API.Services
{
    public class AlertaBackgroundService : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ILogger<AlertaBackgroundService> _logger;

        // Frecuencia de ejecución (ejemplo: cada 1 hora)
        private readonly TimeSpan intervaloEjecucion = TimeSpan.FromHours(1);


        public AlertaBackgroundService(IServiceScopeFactory scopeFactory, ILogger<AlertaBackgroundService> logger)
        {
            _scopeFactory = scopeFactory;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Servicio de alertas automáticas iniciado.");

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    await GenerarAlertas(stoppingToken);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error al generar alertas automáticas.");
                }

                // Espera antes de la siguiente ejecución
                await Task.Delay(intervaloEjecucion, stoppingToken);
            }
        }
        private async Task GenerarAlertas(CancellationToken cancellationToken)
        {
            using var scope = _scopeFactory.CreateScope();

            var context = scope.ServiceProvider.GetRequiredService<DataContext>();
            var alertaHelper = scope.ServiceProvider.GetRequiredService<IAlertaHelper>();

            _logger.LogInformation("Ejecutando revisión de alertas de materia prima...");

            var lotes = await context.LoteMateriaPrimas
                .Include(l => l.MateriaPrima)
                .Include(l => l.Proveedor)
                .ToListAsync(cancellationToken);

            var alertasGeneradas = await alertaHelper.GenerarAlertasAsync(lotes);

            int nuevas = 0;

            foreach (var alerta in alertasGeneradas)
            {
                // Evitar duplicados: solo guarda si no existe una alerta igual sin notificar
                bool existe = await context.Alertas.AnyAsync(a =>
                    a.ProductoId == alerta.ProductoId &&
                    a.Tipo == alerta.Tipo &&
                    !a.Notificada,
                    cancellationToken);

                if (!existe)
                {
                    await context.Alertas.AddAsync(alerta, cancellationToken);
                    nuevas++;
                }
            }

            if (nuevas > 0)
            {
                await context.SaveChangesAsync(cancellationToken);
                _logger.LogInformation($"{nuevas} nuevas alertas registradas en la base de datos.");
            }
            else
            {
                _logger.LogInformation("No se generaron nuevas alertas en esta ejecución.");
            }
        }
    }
}
