using AutoMapper;
using Borrowing.API.Entities;
using Borrowing.API.Repositories;
using Common.Controllers;
using MassTransit;
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
        private readonly IPublishEndpoint _publishEndpoint;
        public BorrowController(IBorrowRepository borrowRepository, IPublishEndpoint publishEndpoint, IMapper mapper)
        {
            _borrowRepository = borrowRepository;
            _mapper = mapper;
            _publishEndpoint = publishEndpoint;
        }

        [Authorize]
        [HttpGet("GetBorrowedBookByMemberId/{id?}")]
        public async Task<IActionResult> GetBorrowedBookByMemberIdAsync(int id=0)
        {
          var memberId = id > 0 ? id : Convert.ToInt32(base.CurrentUser.MemberId);
          var borrows =  await _borrowRepository.GetAllBorrowedBookByMemberIdAsync(memberId);
        
          return Ok(borrows);
        }

        [Authorize]
        [HttpPost("BookRenewRequest")]
        public async Task<IActionResult> BookRenewRequestAsync(Request request)
        {
            request.MemberId = Convert.ToInt32(base.CurrentUser.MemberId);
            request.MemberName = base.CurrentUser.MemberName;

            var result = await _borrowRepository.BookRenewRequestAsync(request);

            if (result != null)
            {
                return Ok(result);
            }
            else
                return BadRequest("Could not be reserved.");
        }

        [Authorize]
        [HttpPost("BookReserveRequest")]
        public async Task<IActionResult> BookReserveRequestAsync(Request request)
        {
            request.MemberId = Convert.ToInt32(base.CurrentUser.MemberId);
            request.MemberName = base.CurrentUser.MemberName;

            var result = await _borrowRepository.BookReserveRequestAsync(request);

            if (result != null)
            {
                var reserve_request = _mapper.Map<Common.SharedModels.Request>(result);
                reserve_request.OperationName = "reserve_request";

                await _publishEndpoint.Publish(reserve_request);
                return Ok(result); 
            }
            else
                return BadRequest("Could not be reserved.");
        }

        [Authorize(Roles = "Administrator,Librarian")]
        [HttpPost("BookIssueRequest")]
        public async Task<IActionResult> BookIssueRequestAsync(Request request)
        {
            var result = await _borrowRepository.IssueBookAsync(base.CurrentUser.UserId,request);

            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest("Could not be reserved.");
            }
        }

        [Authorize(Roles = "Administrator,Librarian")]
        [HttpPost("BookIssueRequestRejected")]
        public async Task<IActionResult> BookIssueRejectedAsync(Request request)
        {
            var result = await _borrowRepository.RejectRequestAsync(request.Id);

            if (result != null)
            {
                var reject_request = _mapper.Map<Common.SharedModels.Request>(result);
                reject_request.OperationName = "reject_request";

                await _publishEndpoint.Publish(reject_request);

                return Ok(result);
            }
            else
            {
                return BadRequest("Could not be rejected.");
            }
        }

        [Authorize]
        [HttpGet("GetBookRequests")]
        public async Task<IActionResult> GetBookRequestsAsync()
        {
            return Ok(await _borrowRepository.GetAllBookRequestAsync());
        }

        [HttpGet("GetFineTest")]
        public async Task<IActionResult> GetFineAsync()
        {
            return Ok(await _borrowRepository.CalculateFineAsync(new DateTime(2023,7,20,5,8,9,DateTimeKind.Utc)));
        }
    }
}
