using Common.SharedModels;
using Identity.Entities;
using Identity.Repositories.Interfaces;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Identity.Consumer
{
    public class MemberCreatedConsumer : IConsumer<Member>
    {
        private readonly IUserRepository _userRepository;
        public MemberCreatedConsumer(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task Consume(ConsumeContext<Member> context)
        {
            var member = context.Message;
            var user = new User()
            {
                Email = member.Email,
                MemberId = member.Id,
                PasswordHash = _userRepository.ComputeSha256Hash(member.Password),
                Phone = member.ContactNo,
                Username = member.UserName,
                MemberName = member.Name
            };

            await _userRepository.AddMemberUserAsync(user);
        }
    }
}

