using System;
using Taksi.Server.DAL.Entities.Helpers;

namespace Taksi.Server.DAL.Entities
{
    public class CreditCardEntity : IIdentifiable
    {
        public CreditCardEntity()
        {
        }

        public CreditCardEntity(Guid clientId, decimal cardBalance)
        {
            Id = Guid.NewGuid();
            ClientId = clientId;
            CardBalance = cardBalance;
        }

        public virtual Guid Id { get; set; }
        public virtual Guid ClientId { get; set; }
        public virtual decimal CardBalance { get; set; }
    }
}