using Application.DTOs;
using Application.FeaturesCQRS.Commands;
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
    public class CreateBookHandler : IRequestHandler<CreateBookCommand, GetBookDto>
    {
        private readonly IBookRepository _repository;

        public CreateBookHandler(IBookRepository repository)
        {
            _repository = repository;
        }

        public async Task<GetBookDto> Handle(CreateBookCommand request, CancellationToken cancellationToken)
        {
            var book = new Book
            {
                Title = request.Book.Title,
                Author = request.Book.Author,
                Year = request.Book.Year
            };
            await _repository.AddAsync(book);
            return book.MapDataBook();
        }
    }
}
