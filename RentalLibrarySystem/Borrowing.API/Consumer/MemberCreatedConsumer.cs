using AutoMapper;
using Borrowing.API.Repositories;
using Common.SharedModels;
using MassTransit;

namespace Borrowing.API.Consumer
{
    public class MemberCreatedConsumer : IConsumer<Member>
    {
        private readonly IBorrowRepository _borrowRepository;
        private readonly IMapper _mapper;
        public MemberCreatedConsumer(IBorrowRepository borrowRepository, IMapper mapper)
        {
            _borrowRepository = borrowRepository;
            _mapper = mapper;
        }

        public async Task Consume(ConsumeContext<Member> context)
        {
            var member = context.Message;

            await _borrowRepository.AddMemberAsync(_mapper.Map<Entities.Member>(member));
        }
    }
}
