using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.OpenApi;

namespace Shared.Extensions
{
    public class NonBodyParameter : OpenApiParameter
    {
        public required object Default { get; set; }
    }
}