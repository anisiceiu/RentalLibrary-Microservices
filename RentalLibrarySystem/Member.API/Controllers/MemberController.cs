using AutoMapper;
using Common.Controllers;
using MassTransit;
using Member.API.DTOs;
using Member.API.Entities;
using Member.API.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Member.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MemberController : BaseController
    {
        private readonly IMemberRepository _memberRepository;
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly IMapper _mapper;


        public MemberController(IMemberRepository memberRepository, IPublishEndpoint publishEndpoint, IMapper mapper)
        {
            _memberRepository = memberRepository;
            _publishEndpoint = publishEndpoint;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("GetMembers")]
        public async Task<List<MemberDetail>> GetMembersAsync()
        {
            return await _memberRepository.GetMembersAsync();
        }

        [HttpGet]
        [Route("GetMember/{id}")]
        public async Task<MemberDetail?> GetMemberAsync(int id)
        {
            return await _memberRepository.GetMemberByIdAsync(id);
        }

        [HttpPost]
        [Route("AddMember")]
        public async Task<MemberDetail?> AddMemberAsync(MemberDto member)
        {

            var memberDetail = _mapper.Map<MemberDetail>(member);
            await _memberRepository.AddMemberAsync(memberDetail);
            var new_member = _mapper.Map<Common.SharedModels.Member>(memberDetail);
            new_member.Password = member.Password;

            await _publishEndpoint.Publish(new_member);

            return memberDetail;
        }
    }
}
