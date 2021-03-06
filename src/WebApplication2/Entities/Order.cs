﻿using System.ComponentModel.DataAnnotations;

namespace Catalog.API.Entities
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        public string UserName { get; set; }
        public string EmailAddress { get; set; }
        public string Country { get; set; }
    }
}
