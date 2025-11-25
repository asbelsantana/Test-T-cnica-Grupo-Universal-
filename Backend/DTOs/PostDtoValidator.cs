using FluentValidation;
using PruebaTecnicaUniversal.DTOs;

namespace PruebaTecnicaUniversal.Validators
{
    /// <summary>
    /// Validador para PostDto usando FluentValidation.
    /// </summary>
    public class PostDtoValidator : AbstractValidator<PostDto>
    {
        public PostDtoValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("El título es obligatorio.")
                .MaximumLength(100).WithMessage("El título no puede superar los 100 caracteres.");

            RuleFor(x => x.Body)
                .NotEmpty().WithMessage("El contenido es obligatorio.")
                .MinimumLength(10).WithMessage("El contenido debe tener al menos 10 caracteres.");
        }
    }
}
