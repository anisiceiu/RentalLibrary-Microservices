using AutoMapper;
using Catalog.API.DTOs;
using Catalog.API.Entities;
using Catalog.API.Repositories.Interfaces;
using Common.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static System.Net.Mime.MediaTypeNames;

namespace Catalog.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatalogController : BaseController
    {
        private readonly IBookRepository _bookRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IBindingRepository _bindingRepository;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _hostEnvironment;
        public CatalogController(IBookRepository bookRepository,IMapper mapper,
            ICategoryRepository categoryRepository, IBindingRepository bindingRepository, IWebHostEnvironment hostEnvironment)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
            _categoryRepository = categoryRepository;
            _bindingRepository = bindingRepository;
            _hostEnvironment = hostEnvironment;
        }

        
        [HttpGet("GetBooks")]
        public async Task<IEnumerable<BookDto>> GetBooksAsync()
        {
            return  _mapper.Map<IEnumerable<BookDto>>(await _bookRepository.GetBooksAsync());
        }

        [HttpGet("GetBook/{id}")]
        public async Task<BookDto> GetBooksByIdAsync(int id)
        {
            return _mapper.Map<BookDto>(await _bookRepository.GetBookAsync(id));
        }


        [HttpGet("GetIsBookAvailable/{id}")]
        public async Task<bool> GetIsBookAvailableAsync(int id)
        {
            var book = await _bookRepository.GetBookAsync(id);

            if(book != null)
                return book.NoOfAvailableCopies > 0;
            else
                return false;
        }

        [Authorize]
        [HttpPost("AddBook")]
        public async Task<IActionResult> AddBooksAsync([FromForm] BookDto book)
        {
            if(ModelState.IsValid)
            {
                var b = _mapper.Map<Book>(book);

                if (book.FormFile.Length > 0)
                {
                    using (var stream = new MemoryStream())
                    {
                        await book.FormFile.CopyToAsync(stream);
                        var bytes = stream.ToArray();
                        string fileName = Guid.NewGuid() + ".png";
                        string filePath = Path.Combine(_hostEnvironment.WebRootPath, "BookCover",fileName);
                        System.IO.File.WriteAllBytes(filePath, bytes);

                        var baseurl = base.BaseUrl;
                        b.ThumbnailImageUrl = $"{baseurl}BookCover/{fileName}";
                    }
                }

               var book_result = await _bookRepository.CreateBookAsync(b);

                return Ok(book_result);
            }

            return BadRequest();
            
        }

        [Authorize]
        [HttpPost("AddCategory")]
        public async Task<IActionResult> AddCategoryAsync(CategoryDto catergory)
        {
            var cat = await  _categoryRepository.CreateCatergoryAsync(_mapper.Map<Category>(catergory));
            return Ok(cat);
        }

        [HttpGet("GetCategories")]
        public async Task<IEnumerable<CategoryDto>> GetCategoriesAsync()
        {
            return _mapper.Map<IEnumerable<CategoryDto>>(await _categoryRepository.GetCatergoriesAsync());
        }

        [HttpGet("GetBindings")]
        public async Task<IEnumerable<BindingDto>> GetBindingsAsync()
        {
            return _mapper.Map<IEnumerable<BindingDto>>(await _bindingRepository.GetBindingsAsync());
        }

        [Authorize]
        [HttpPost("AddBinding")]
        public async Task<IActionResult> AddBindingAsync(BindingDto binding)
        {
            var bind = await _bindingRepository.CreateBindingAsync(_mapper.Map<Binding>(binding));
            return Ok(bind);
        }
    }
}
