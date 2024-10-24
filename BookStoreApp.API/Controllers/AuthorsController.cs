﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookStoreApp.API.Data;
using BookStoreApp.API.Models.Author;
using AutoMapper;
using BookStoreApp.API.Static;
using Microsoft.AspNetCore.Authorization;
using AutoMapper.QueryableExtensions;

namespace BookStoreApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<AuthorsController> _logger;

        public AuthorsController(BookStoreDbContext context, IMapper mapper, ILogger<AuthorsController> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        //GET : api/authors/?startindex=0&pagesize=15
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AuthorReadOnlyDto>>> GetAuthors()
        {
            try
            {
                // Get authors
                var author = await _context.Authors.ToListAsync();
                // Map authors to DTO and set in variable
                var authorDtos = _mapper.Map<IEnumerable<AuthorReadOnlyDto>>(author);
                // Return variable
                return Ok(authorDtos);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error Performing GET Operation in {nameof(GetAuthors)}.");
                return StatusCode(500, Messages.Error500Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AuthorDetailsDto>> GetAuthor(int id)
        {
            try
            {
                // Find the author
                var author = await _context.Authors
                    .Include(a => a.Books)
                    .ProjectTo<AuthorDetailsDto>(_mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync(a => a.Id == id);

                if (author == null)
                {
                    _logger.LogWarning($"Record Not Found: {nameof(GetAuthor)} - ID: {id}");
                    return NotFound();
                }

                return Ok(author);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error Performing GET Operation in {nameof(GetAuthor)}.");
                return StatusCode(500, Messages.Error500Message);
            }
        }

        //PUT : Update or create new author
        [HttpPut("{id}")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> PutAuthor(int id, AuthorUpdateDto authorDto)
        {
            if (id != authorDto.Id)
            {
                return BadRequest();
            }

            // Map author to DTO and set in variable
            var author = await _context.Authors.FindAsync(id);

            if (author == null)
            {
                _logger.LogWarning($"Record Not Found: {nameof(PutAuthor)} - ID: {id}");
                return NotFound();
            }
            // Map them
            _mapper.Map(authorDto, author);
            _context.Entry(author).State = EntityState.Modified;
            _logger.LogInformation($"PUT Operation on Record: {id} - Success");

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!await AuthorExists(id))
                {
                    return NotFound();
                }
                else
                {
                    _logger.LogError(ex, $"Error Performing PUT Operation in {nameof(PutAuthor)}.");
                    return StatusCode(500, Messages.Error500Message);
                }
            }

            return NoContent();
        }

        //POST : Creating new author
        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult<AuthorCreateDto>> PostAuthor(AuthorCreateDto authorDto)
        {
            try
            {
                // Map DTO to Author entity 
                var author = _mapper.Map<Author>(authorDto);
                // Add author to database
                await _context.Authors.AddAsync(author);
                // Save Changes
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetAuthor), new { id = author.Id }, author);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error Performing POST Operation in {nameof(PostAuthor)}");
                return StatusCode(500, Messages.Error500Message);
            }
        }

        //DELETE : Deleting
        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            try
            {
                var author = await _context.Authors.FindAsync(id);
                if (author == null)
                {
                    _logger.LogWarning($"Record Not Found: {nameof(DeleteAuthor)} - ID: {id}");
                    return NotFound();
                }

                _context.Authors.Remove(author);
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error Performing DELETE Operation in {nameof(DeleteAuthor)}");
                return StatusCode(500, Messages.Error500Message);
            }
        }

        private async Task<bool> AuthorExists(int id)
        {
            return await _context.Authors.AnyAsync(e => e.Id == id);
        }
    }
}
