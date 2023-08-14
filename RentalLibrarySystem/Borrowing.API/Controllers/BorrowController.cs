using AutoMapper;
using Borrowing.API.Entities;
using Borrowing.API.Repositories;
using Common.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestSharp;

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

        [Authorize]
        [HttpPost("BookReserveRequest")]
        public async Task<IActionResult> BookReserveRequestAsync(Request request)
        {
            request.MemberId = Convert.ToInt32(base.CurrentUser.MemberId);
            request.MemberName = base.CurrentUser.MemberName;

            var result = await _borrowRepository.BookReserveRequestAsync(request);

            if (result != null)
                return Ok(result);
            else
                return BadRequest("Could not be reserved.");
        }

        [HttpGet("GetFineTest")]
        public async Task<IActionResult> GetFineAsync()
        {
            return Ok(await _borrowRepository.CalculateFineAsync(new DateTime(2023,7,20,5,8,9,DateTimeKind.Utc)));
        }
    }
}
