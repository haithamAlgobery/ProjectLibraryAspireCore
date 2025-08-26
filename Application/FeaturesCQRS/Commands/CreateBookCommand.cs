using Application.DTOs;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.FeaturesCQRS.Commands
{
    public record CreateBookCommand(ModelCreateNewBookDto Book) : IRequest<GetBookDto>;
}
