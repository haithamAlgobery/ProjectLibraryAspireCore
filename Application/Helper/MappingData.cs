using Application.DTOs;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Helper
{
    public static class MappingData
    {
        public static GetBookDto MapDataBook(this Book entity)
        {
            return new GetBookDto
            {
                Title = entity.Title,
                Author = entity.Author,
                Id = entity.Id,
                Year = entity.Year,
            };
        }
    }
}
