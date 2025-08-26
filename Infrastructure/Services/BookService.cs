using Application.DTOs;
using Application.FeaturesCQRS.Commands;
using Application.FeaturesCQRS.Queries;
using Application.Helper;
using Application.IServices;
using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class BookService : IBookService
    {
        private readonly IMediator _mediator;
        private readonly ICacheService _cacheService;

        public BookService(IMediator mediator, ICacheService cacheService)
        {
            _mediator = mediator;
            _cacheService = cacheService;
        }
       

       


        public async Task<ResultProcessing<GetBookDto>> ProcessingCreateNewBook(ModelCreateNewBookDto model)
        {
            var result= new ResultProcessing<GetBookDto>();

            try
            {
                var getNewBookDto  =  await _mediator.Send(new CreateBookCommand(model));

                result.Data = getNewBookDto;
                result.IsSccess = true;
                return result;


            } catch (Exception ex) {

                result.Message = ex.Message;
                return result;
            }

        }

        public async Task<IEnumerable<GetBookDto>> GetAllBooks()
        {
            const string cacheKey = "all_books";

            var cachedBooks = await _cacheService.GetAsync<IEnumerable<GetBookDto>>(cacheKey);
            if (cachedBooks is not null)

                return cachedBooks;

            var books = await _mediator.Send(new GetAllBooksQuery());

            await _cacheService.SetAsync(cacheKey, books, TimeSpan.FromMinutes(10));

            return books;

        }

    }
}
