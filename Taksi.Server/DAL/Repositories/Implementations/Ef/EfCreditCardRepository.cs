using Taksi.Server.DAL.Entities;

namespace Taksi.Server.DAL.Repositories.Implementations.Ef
{
    public class EfCreditCardRepository : EfRepository<CreditCardEntity>
    {
        public EfCreditCardRepository(EfContext context) : base(context, context.CreditCards)
        {
        }
    }
}