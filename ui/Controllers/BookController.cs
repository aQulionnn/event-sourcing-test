using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ui.Entities;

namespace ui.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BookController : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create()
    {
        var book = Book.Create("Idiot", 2.99);
        return Ok(book);
    }
}