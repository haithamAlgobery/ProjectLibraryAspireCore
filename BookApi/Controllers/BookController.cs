using Application.DTOs;
using Application.FeaturesCQRS.Queries;
using Application.IServices;
using Infrastructure.Roles;
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
        private readonly IBookService _bookService;
        private readonly IEmailService _emailService;

        public BookController(IBookService bookService,IEmailService emailService)
        {
            _bookService = bookService;
            _emailService = emailService;
        }

        [HttpPost]
        [Authorize(Roles = $"{RolesName.Admin},{RolesName.Moderator}")]

        public async Task<IActionResult> CrearteNewBook(ModelCreateNewBookDto model)
        {
            if(!ModelState.IsValid) 
                return BadRequest(ModelState);


           var result=await _bookService.ProcessingCreateNewBook(model);

            if (!result.IsSccess) 
                return BadRequest(result.Message);


            _emailService.SendWelcomeEmail("Yussof@gmail.com", "Hello Yussof ");
           
            return Ok(result.Data);
        }


        [HttpGet]
        [Authorize()]
        public async Task<IActionResult> GetDataBooks()
        {
            return Ok(await _bookService.GetAllBooks());
        }
    }
}
