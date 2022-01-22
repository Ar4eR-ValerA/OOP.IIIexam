using System;
using Taksi.Server.DAL.Entities.Helpers;

namespace Taksi.Server.DAL.Entities
{
    public class ClientEntity : IIdentifiable
    {
        public ClientEntity()
        {
        }

        public ClientEntity(string fullName)
        {
            Id = Guid.NewGuid();
            FullName = fullName;
        }

        public virtual Guid Id { get; set; }

        public virtual string FullName { get; set; }
    }
}