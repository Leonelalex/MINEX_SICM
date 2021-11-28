using DataAccess.DBContracts.AlertDBRepository.Repositories;
using DataAccess.Entities.AlertEntities;

namespace DataRepository.AlertDB.Repositories
{
    public interface  ITezRepository: IAlertDBRepositoryBase<SICM_TEZ>
    {
        //implemetamos todos los metodos genericos que se crearon en el repositorio base para una talba en especifico
    }
}
