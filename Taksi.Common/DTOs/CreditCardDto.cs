using System;

namespace Taksi.DTO.DTOs
{
    public class CreditCardDto
    {
        public Guid Id { get; set; }

        public Guid ClientId { get; set; }
        public string CardId { get; set; }
        public decimal CardBalance { get; set; }
    }
}