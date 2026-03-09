using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.Marshalling;
using System.Text;

namespace FlowerShop.Core.Entities
{
    public class Review : BaseEntity
    {
        public int Rating { get; set; }
        public string? Comment { get; set; }
        public int ProductId { get; set; }
        public Guid UserId { get; set; }
        public virtual Product Product { get; set; } = null!;
        public virtual AppUser User { get; set; } = null!;
    }
}
