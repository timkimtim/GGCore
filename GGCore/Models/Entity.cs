using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GGCore.Models
{
    public record Entity
    {
        public int Id { get; init; }

        public DateTimeOffset Created { get; set; }

        public DateTimeOffset Updated { get; set; }
    }
}
