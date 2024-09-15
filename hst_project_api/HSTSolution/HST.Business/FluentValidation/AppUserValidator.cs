
using FluentValidation;
using HST.Entity.Entities;

namespace HST.Business.FluentValidation
{
    public class AppUserValidator : AbstractValidator<AppUser>
    {
        public AppUserValidator()
        {
            RuleFor(x => x.Name)
           .NotEmpty().WithMessage("İsim gerekli.")
           .Length(1, 50).WithMessage("İsim 1 ile 50 karakter arasında olmalıdır.");

            RuleFor(x => x.Surname)
                .NotEmpty().WithMessage("Soyisim gerekli.")
                .Length(1, 50).WithMessage("Soyisim 1 ile 50 karakter arasında olmalıdır.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("E-posta gerekli.")
                .EmailAddress().WithMessage("Geçersiz e-posta formatı.");

            RuleFor(x => x.PhoneNumber)
                .NotEmpty().WithMessage("Telefon numarası gerekli.")
                .Matches(@"^\+?[1-9]\d{1,14}$").WithMessage("Geçersiz telefon numarası formatı.");

            RuleFor(x => x.WebSiteAddress)
                .Matches(@"^(https?://)?([\da-z\.-]+)\.([a-z\.]{2,6})([/\w \.-]*)*/?$").WithMessage("Geçersiz web sitesi adresi formatı.");

        }
    }
}
