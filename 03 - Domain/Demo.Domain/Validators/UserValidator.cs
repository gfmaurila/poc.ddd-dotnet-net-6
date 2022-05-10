using Demo.Domain.Entities;
using FluentValidation;

namespace Demo.Domain.Validators
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(x => x)
                .NotEmpty()
                .WithMessage("A entidade não pode ser vazia.")

                .NotNull()
                .WithMessage("A entidade não pode ser nula.");

            RuleFor(x => x.Name)
                .NotNull()
                .WithMessage("O nome não pode ser nulo.")

                .NotEmpty()
                .WithMessage("O nome não pode ser vazio.")

                .MinimumLength(3)
                .WithMessage("O nome deve ter no mínimo 3 caracteres.")

                .MaximumLength(80)
                .WithMessage("O nome deve ter no máximo 80 caracteres.");

            RuleFor(x => x.Password)
                .NotNull()
                .WithMessage("A senha não pode ser nula.")

                .NotEmpty()
                .WithMessage("A senha não pode ser vazia.")

                .MinimumLength(6)
                .WithMessage("A senha deve ter no mínimo 6 caracteres.")

                .MaximumLength(80)
                .WithMessage("A senha deve ter no máximo 30 caracteres.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Informe a senha") // A senha deve ter no mínimo 8 caracteres e atender aos seguintes critérios:
                .MinimumLength(8).WithMessage("A senha deve ter no mínimo 8 caracteres.") // Conter no mínimo 8 caracteres.
                .Must(PasswordValidator.RequiredLowerCase).WithMessage("Senha deve ter pelo menos 1 caracter minúsculo") // Possuir pelo menos uma letra minúscula (de a a z);
                .Must(PasswordValidator.RequireUppercase).WithMessage("Senha deve ter pelo menos 1 caracter maiúsculo") // Possuir pelo menos uma letra maiúscula (de A a Z)
                .Must(PasswordValidator.RequireNonAlphanumeric).WithMessage("Digite pelo menos 1 caracter especial (p. ex): ~,!,@,#,$,%,^,&,*,(,),+,=,?") // Possuir pelo menos um caracter especial (p. ex): ~,!,@,#,$,%,^,&,*,(,),+,=,?
                .Must(PasswordValidator.RequireNotRepeated).WithMessage("Não poder ter caracteres repetidos em sequência") // Não poder ter caracteres repetidos em sequência, por exemplo: 1111, aaaa, bbbb, @@@@, BBBB. Prever case-sensitive, por exemplo: A é diferente de a.
                .Must(PasswordValidator.RequireDigit).WithMessage("Senha deve ter pelo menos 1 número"); // Possuir pelo menos um dígito (de 0 a 9);

            //RuleFor(x => x.Email)
            //    .NotNull()
            //    .WithMessage("O email não pode ser nulo.")

            //    .NotEmpty()
            //    .WithMessage("O email não pode ser vazio.")

            //    .MinimumLength(10)
            //    .WithMessage("O email deve ter no mínimo 10 caracteres.")

            //    .MaximumLength(180)
            //    .WithMessage("O email deve ter no máximo 180 caracteres.")

            //    .Matches(@"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$")
            //    .WithMessage("O email informado não é válido.");
        }
    }
}
