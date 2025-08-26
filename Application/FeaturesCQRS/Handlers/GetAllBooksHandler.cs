using Application.DTOs;
using Application.FeaturesCQRS.Queries;
using Application.Helper;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.FeaturesCQRS.Handlers
{
    public class GetAllBooksHandler : IRequestHandler<GetAllBooksQuery, IEnumerable<GetBookDto>>
    {
        private readonly IBookRepository _repository;

        public GetAllBooksHandler(IBookRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<GetBookDto>> Handle(GetAllBooksQuery request, CancellationToken cancellationToken)
        {
            var books = await _repository.GetAllAsync();
            return books.Select(b =>b.MapDataBook());
        }


    }
}
