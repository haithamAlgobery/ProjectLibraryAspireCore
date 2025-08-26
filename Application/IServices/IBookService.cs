using Application.DTOs;
using Application.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IServices
{
    public interface IBookService
    {

        Task<ResultProcessing<GetBookDto>> ProcessingCreateNewBook(ModelCreateNewBookDto model);
        Task<IEnumerable<GetBookDto>> GetAllBooks();
    }
}
