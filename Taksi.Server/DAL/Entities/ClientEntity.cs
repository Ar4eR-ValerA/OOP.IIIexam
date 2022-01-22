using System;
using Taksi.Server.DAL.Entities.Helpers;

namespace Taksi.Server.DAL.Entities
{
    public class ClientEntity : IIdentifiable
    {
        internal ClientEntity()
        {
        }

        public ClientEntity(string fullName)
        {
            Id = Guid.NewGuid();
            FullName = fullName;
        }

        public Guid Id { get; set; }

        public string FullName { get; set; }
    }
}