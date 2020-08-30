using System.Threading.Tasks;
using Library.Client.Models;
using Library.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace Library.Web.Controllers
{
    public class BooksController : ControllerBase
    {
        private readonly BooksService booksService;

        public BooksController(BooksService booksService)
        {
            this.booksService = booksService;

        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] BookToCreate bookToAdd)
        {
            return Ok(await booksService.Add(bookToAdd));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await booksService.GetAll());
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(string id)
        {
            return Ok(await booksService.Delete(id));
        }
    }
}