using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Prueba.Api.Entidades
{
    public class ProductoValidator : AbstractValidator<Producto>
    {
        public ProductoValidator()
        {
            RuleFor(x => x.Nombre).NotEmpty();
            RuleFor(x => x.Precio).NotEmpty();
            RuleFor(x => x.Stock).NotEmpty();

        }
    }
}
