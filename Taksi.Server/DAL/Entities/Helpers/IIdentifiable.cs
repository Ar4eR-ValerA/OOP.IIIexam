using System;

namespace Taksi.Server.DAL.Entities.Helpers
{
    public interface IIdentifiable
    {
        Guid Id { get; set; }
    }
}