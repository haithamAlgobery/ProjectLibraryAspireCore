using Application.DTOs;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.FeaturesCQRS.Queries
{
    public record GetAllBooksQuery() : IRequest<IEnumerable<GetBookDto>>;
}
