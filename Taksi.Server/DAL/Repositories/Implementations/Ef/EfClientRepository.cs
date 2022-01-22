using Taksi.Server.DAL.Entities;

namespace Taksi.Server.DAL.Repositories.Implementations.Ef
{
    public class EfClientRepository : EfRepository<ClientEntity>
    {
        public EfClientRepository(EfContext context) : base(context, context.Clients)
        {
        }
    }
}