using Application.Commands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validators
{
    public class CreateBookValidator : AbstractValidator<CreateBookCommand>
    {
        public CreateBookValidator()
        {
            RuleFor(x => x.Book.Title).NotEmpty();
            RuleFor(x => x.Book.Author).NotEmpty();
            RuleFor(x => x.Book.Year).GreaterThan(0);
        }
    }
}
