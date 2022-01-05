using System;
using Taksi.Server.DAL.Entities.Helpers;

namespace Taksi.Server.DAL.Entities
{
    public class DriverEntity : IIdentifiable
    {
        public Guid Id { get; set; }
    }
}