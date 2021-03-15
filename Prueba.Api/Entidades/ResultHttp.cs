using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prueba.Api.Entidades
{
    public class ResultHttp
    {
        public bool Success { get; set; }
        public string Mensaje { get; set; }
        public object Resultado { get; set; }
    }
}
