using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsuariosApp.Domain.Entities;

namespace UsuariosApp.Domain.Validators
{
    public class PerfilValidator : AbstractValidator<Perfil>
    {

        public PerfilValidator()
        {
            RuleFor(p => p.Nome)
                .NotEmpty()
                .WithMessage("O nome do perfil é obrigatório")
                .Length(6, 25)
                .WithMessage("O nome de perfil deve ter de 6 a 25 caracteres.");
        }

    }
}
