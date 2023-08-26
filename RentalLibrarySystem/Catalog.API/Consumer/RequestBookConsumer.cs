using Catalog.API.Repositories.Interfaces;
using MassTransit;

namespace Catalog.API.Consumer
{
    public class RequestBookConsumer : IConsumer<Common.SharedModels.Request>
    {
        private readonly IBookRepository _bookRepository;
        public RequestBookConsumer(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }
        public async Task Consume(ConsumeContext<Common.SharedModels.Request> context)
        {
            var req = context.Message;
            var book = await _bookRepository.GetBookAsync(req.BookId);

            if (req.OperationName == "reserve_request")
            {
                if (book != null && book.NoOfAvailableCopies > 0)
                {
                    book.NoOfAvailableCopies = book.NoOfAvailableCopies - 1;
                    await _bookRepository.UpdateBookAsync(book);
                }
            }

            else
            {  //reject request
                if (book != null && book.NoOfAvailableCopies < book.NoOfCopies)
                {
                    book.NoOfAvailableCopies = book.NoOfAvailableCopies + 1;
                    await _bookRepository.UpdateBookAsync(book);
                }
            }

        }
    }
}
