using AutoMapper;
using Borrowing.API.Repositories;
using Common.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Borrowing.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BorrowController : BaseController
    {
        private readonly IBorrowRepository  _borrowRepository;
        private readonly IMapper _mapper;
        public BorrowController(IBorrowRepository borrowRepository, IMapper mapper)
        {
            _borrowRepository = borrowRepository;
            _mapper = mapper;
        }

        [HttpGet("GetFineTest")]
        public async Task<IActionResult> GetFineAsync()
        {
            return Ok(await _borrowRepository.CalculateFineAsync(new DateTime(2023,7,20,5,8,9,DateTimeKind.Utc)));
        }
    }
}
