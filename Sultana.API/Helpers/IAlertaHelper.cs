using Sultana.Shared.Entities;

namespace Sultana.API.Helpers
{
    public interface IAlertaHelper
    {
        Task<List<Alerta>> GenerarAlertasAsync(List<LoteMateriaPrima> lotes);
    }
}
