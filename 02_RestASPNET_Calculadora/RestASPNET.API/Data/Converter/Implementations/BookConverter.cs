using RestASPNET.API.Data.Converter.Contract;
using RestASPNET.API.Data.DTO;
using RestASPNET.API.Model;

namespace RestASPNET.API.Data.Converter.Implementations
{
    public class BookConverter : IParser<BookDTO, Book>, IParser<Book, BookDTO>
    {
        public Book Parse(BookDTO origin)
        {
            if (origin == null) return null;

            return new Book
            {
                Id = origin.Id,
                Author = origin.Author,
                Launch_date = origin.Launch_date,
                Price = origin.Price,
                Title = origin.Title
            };
        }


        public List<Book> Parse(List<BookDTO> origin)
        {
            if (origin == null) return null;
            return origin.Select(item => Parse(item)).ToList();
        }

        public List<BookDTO> Parse(List<Book> origin)
        {
            if (origin == null) return null;
            return origin.Select(item => Parse(item)).ToList();
        }


        public BookDTO Parse(Book origin)
        {
            if (origin == null) return null;

            return new BookDTO
            {
                Id = origin.Id,
                Author = origin.Author,
                Launch_date = origin.Launch_date,
                Price = origin.Price,
                Title = origin.Title
            };
        }
    };


}
