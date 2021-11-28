using DataAccess.DBContracts.AlertDBRepository;
using DataAccess.DBContracts.AlertDBRepository.Repositories;

namespace DataRepository.AlertDB
{
    //creamo el repository base generico para los metodos a utilizar 
    public class AlertDBRepositoryBase<Entidad>: IAlertDBRepositoryBase<Entidad> where Entidad : class
    {
        //realizamos la inyeccion de dependencias de contrato con la bd y repositorio
        private readonly IAlertDBContext _dBContext;

        public AlertDBRepositoryBase(IAlertDBContext dBContext)
        {
            _dBContext = dBContext;
        }
    }
}
