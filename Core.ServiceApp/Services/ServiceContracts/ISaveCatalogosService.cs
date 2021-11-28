using System.Threading.Tasks;

namespace Core.ServiceApp.Services.ServiceContracts
{
    public interface ISaveCatalogosService<Entidad> where Entidad : class
    {
        Task<bool> SaveNewCatalgo();
    }
}
