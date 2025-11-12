using System.Threading.Tasks;

namespace Sultana.API.Helpers
{
    public interface IInventarioHelper
    {
        Task<bool> DescontarMateriaPrimaAsync(int loteId, decimal cantidad);
    }
}
