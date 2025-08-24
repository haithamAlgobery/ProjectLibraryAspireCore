using Application.DTOs;
using Application.Queries;
using Infrastructure.Roles;
using Infrastructure.Services.EmailServices;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace BookApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IEmailService _emailService;

        public BookController(IMediator mediator,IEmailService emailService)
        {
           _mediator = mediator;
            _emailService = emailService;
        }

        [HttpPost]
        [Authorize(Roles = $"{RolesName.Admin},{RolesName.Moderator}")]

        public async Task<IActionResult> CrearteNewBook(BookDto model)
        {
            var book = await _mediator.Send(model);

            if (book == null) {
                return BadRequest("Error From Input Data");
            }

            _emailService.SendWelcomeEmail("haitham@gmail.com", "wlcom to Programing ");

            return Ok(book);
        }

        [HttpGet]
        [Authorize()]
        public async Task<IActionResult> GetDataBooks()
        {
            var books = await _mediator.Send(new GetAllBooksQuery());

            return Ok(books);
        }
    }
}
