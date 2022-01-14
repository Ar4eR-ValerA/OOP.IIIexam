using System;
using Taksi.Server.DAL.Entities.Helpers;

namespace Taksi.Server.DAL.Entities
{
    public class CreditCardEntity : IIdentifiable
    {
        public Guid Id { get; set; }

        public Guid ClientId { get; set; } 
        public string CardId { get; set; }
        public decimal CardBalance { get; set; }
    }
}