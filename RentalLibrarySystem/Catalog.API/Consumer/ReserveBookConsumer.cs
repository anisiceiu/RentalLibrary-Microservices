using Catalog.API.Repositories.Interfaces;
using MassTransit;

namespace Catalog.API.Consumer
{
    public class ReserveBookConsumer : IConsumer<Common.SharedModels.Request>
    {
        private readonly IBookRepository _bookRepository;
        public ReserveBookConsumer(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }
        public async Task Consume(ConsumeContext<Common.SharedModels.Request> context)
        {
            var req = context.Message;
            var book = await _bookRepository.GetBookAsync(req.BookId);

            if(book != null && book.NoOfAvailableCopies > 0)
            {
                book.NoOfAvailableCopies = book.NoOfAvailableCopies - 1;
                await _bookRepository.UpdateBookAsync(book);
            }

        }
    }
}
