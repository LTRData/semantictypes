using SemanticTypes.SemanticTypeQualifiedByTypeExamples;
using Xunit;

namespace SemanticTypes.Test;


public class IdTests
{
    private class Book
    {
        public Id<Book> BookId { get; set; }
        public string Title { get; set; }
        public Id<Author> AuthorId { get; set; }
    }

    private class Author
    {
        public Id<Author> AuthorId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
    }

    private static void F(Id<Book> _)
    {
    }

    [Fact]
    public void IdGreaterThan0_Succeeds()
    {
        var author = new Author
        {
            AuthorId = new Id<Author>(1),
            Name = "Leo Tolstoy"
        };

        var book = new Book
        {
            AuthorId = author.AuthorId,
            BookId = new Id<Book>(2),
            Title = "War And Peace"
        };

        Assert.Equal(2, book.BookId.Value);

        // Doesn't compile
        // book.BookId = 5;

        // Doesn't compile
        // f(author.AuthorId);

        // Doesn't compile
        // f(5);

        // Compiles
        F(book.BookId);
    }
}
