using Sultana.Shared.Entities;
using Sultana.Shared.Enums;

namespace Sultana.API.Helpers
{
    public class AlertaHelper: IAlertaHelper
    {
        private const int DiasPreviosVencimiento = 7;
        private const decimal PorcentajeStockMinimo = 0.10m;

        public async Task<List<Alerta>> GenerarAlertasAsync(List<LoteMateriaPrima> lotes) 
        {
            var alertas = new List<Alerta>();

            await Task.Run(() =>
            {
                foreach (var lote in lotes)
                {
                    // Alerta por venimiento proxixmo
                    if (lote.FechaVencimiento.HasValue)
                    {
                        var diasRestantes = (lote.FechaVencimiento.Value - DateTime.UtcNow).TotalDays;
                        if (diasRestantes <= DiasPreviosVencimiento)
                        {
                            alertas.Add(new Alerta
                            {
                                Tipo = TipoAlerta.VencimientoProximo,
                                Mensaje = $"El lote{lote.NumeroLote} de {lote.MateriaPrima.Nombre} vence en {Math.Max(0,(int)diasRestantes)} dias.",
                                FechaGenerada = DateTime.UtcNow,
                                Notificada = false
                            });
                        }
                    }
                    // Alerta por stock bajo
                    if (lote.CantidadDisponible <= 0)
                    {
                        alertas.Add(new Alerta
                        {
                            ProductoId = lote.MateriaPrimaId.ToString(),
                            Tipo = TipoAlerta.StockAgotado,
                            Mensaje = $"El lote {lote.NumeroLote} de {lote.MateriaPrima.Nombre} está agotado.",
                            FechaGenerada = DateTime.UtcNow,
                            Notificada = false,
                        });
                    }
                    else
                    {
                        var stockMinimo = lote.CantidadIngresada * PorcentajeStockMinimo;

                        if (lote.CantidadDisponible <= stockMinimo) 
                        {
                            alertas.Add(new Alerta
                            {
                                ProductoId = lote.MateriaPrimaId.ToString(),
                                Tipo = TipoAlerta.StockBajo,
                                Mensaje = $"El lote {lote.NumeroLote} de {lote.MateriaPrima.Nombre} tiene un stock bajo ({lote.CantidadDisponible}/{lote.CantidadIngresada}).",
                                FechaGenerada = DateTime.UtcNow,
                                Notificada = false,
                            });
                        }
                    }
                }
            });
            return alertas;
        }
    }
}
