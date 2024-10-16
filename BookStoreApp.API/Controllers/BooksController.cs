using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookStoreApp.API.Data;
using BookStoreApp.API.Models.Book;
using AutoMapper;
using BookStoreApp.API.Static;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Authorization;

namespace BookStoreApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<BooksController> _logger;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public BooksController(BookStoreDbContext context, IMapper mapper, ILogger<BooksController> logger, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookReadOnlyDto>>> GetBooks()
        {
            try
            {
                // Get all books with author's first and last name combined
                var books = await _context.Books
                    .Include(q => q.Author)
                    .ProjectTo<BookReadOnlyDto>(_mapper.ConfigurationProvider)
                    .ToListAsync();

                return Ok(books);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error Performing GET Operation in {nameof(GetBooks)}.");
                return StatusCode(500, Messages.Error500Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BookDetailsDto>> GetBook(int id)
        {
            try
            {
                var book = await _context.Books
                .Include(q => q.Author)
                .ProjectTo<BookDetailsDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(q => q.Id == id);

                if (book == null)
                {
                    _logger.LogWarning($"Record Not Found: {nameof(GetBook)} - ID: {id}");
                    return NotFound();
                }

                return book;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error Performing GET Operation in {nameof(GetBooks)} - ID: {id}");
                return StatusCode(500, Messages.Error500Message);
            }
        }

        // PUT
        [HttpPut("{id}")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> PutBook(int id, BookUpdateDto bookDto)
        {
            try
            {
                if (id != bookDto.Id)
                {
                    return BadRequest();
                }

                var book = await _context.Books.FindAsync(id);

                // If book isn't there
                if (book == null)
                {
                    _logger.LogWarning($"Record Not Found: {nameof(PutBook)} - ID: {id}");
                    return NotFound();
                }

                // If there's an image present already
                if (!string.IsNullOrEmpty(bookDto.ImageData))
                {
                    // Create the new file
                    bookDto.Image = CreateFile(bookDto.ImageData, bookDto.ImageName);
                    // Get name of picture and path 
                    var picName = Path.GetFileName(bookDto.ImageData);
                    var path = $"{_webHostEnvironment.WebRootPath}\\bookcoverimages\\{picName}";

                    // If path exists delete the path   
                    if (System.IO.File.Exists(path))
                    {
                        System.IO.File.Delete(path);
                    }
                }

                _mapper.Map(bookDto, book);
                _context.Entry(book).State = EntityState.Modified;

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await BookExistsAsync(id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error Performing PUT Operation in {nameof(PutBook)}.");
                return StatusCode(500, Messages.Error500Message);
            }
        }

        // POST
        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult<BookCreateDto>> PostBook(BookCreateDto bookDto)
        {
            try
            {
                var book = _mapper.Map<Book>(bookDto);

                // Set book image to new image 
                book.Image = CreateFile(bookDto.ImageData, bookDto.ImageName);
                await _context.Books.AddAsync(book);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetBook", new { id = book.Id }, book);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error Performing POST Operation in {nameof(PostBook)}");
                return StatusCode(500, Messages.Error500Message);
            }
        }

        // DELETE
        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            try
            {
                var book = await _context.Books.FindAsync(id);
                if (book == null)
                {
                    return NotFound();
                }

                _context.Books.Remove(book);
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error Performing DELETE Operation in {nameof(DeleteBook)}");
                return StatusCode(500, Messages.Error500Message);
            }
        }

        private string CreateFile(string imageBase64, string imageName)
        {
            try
            {
                // Get app URL (Domain name)
                var url = HttpContext.Request.Host.Value;
                // Get file Extension
                var ext = Path.GetExtension(imageName);
                // Make a unique file name
                var fileName = $"{Guid.NewGuid()}{ext}";

                var path = $"{_webHostEnvironment.WebRootPath}\\bookcoverimages\\{fileName}";

                // Convert from the base64 string 
                byte[] image = Convert.FromBase64String(imageBase64);
                // Create path & file
                var fileStream = System.IO.File.Create(path);
                // Write and save it
                fileStream.Write(image, 0, image.Length);
                fileStream.Close();


                return $"https://{url}/bookcoverimages/{fileName}";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error Performing Operation in {nameof(CreateFile)}");
                throw;
            }
        }
        private async Task<bool> BookExistsAsync(int id)
        {
            return await _context.Books.AnyAsync(e => e.Id == id);
        }
    }
}