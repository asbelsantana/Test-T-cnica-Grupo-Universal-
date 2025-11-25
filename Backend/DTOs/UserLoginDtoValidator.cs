using FluentValidation;
using PruebaTecnicaUniversal.DTOs;

namespace PruebaTecnicaUniversal.Validators
{
    /// <summary>
    /// Validador para UserLoginDto usando FluentValidation.
    /// </summary>
    public class UserLoginDtoValidator : AbstractValidator<UserLoginDto>
    {
        public UserLoginDtoValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("El correo electrónico es obligatorio.")
                .EmailAddress().WithMessage("Debe ser un correo electrónico válido.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("La contraseña es obligatoria.")
                .MinimumLength(6).WithMessage("La contraseña debe tener al menos 6 caracteres.");
        }
    }
}
