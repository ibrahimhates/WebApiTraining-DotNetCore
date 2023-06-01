using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repositories.EFCore.Config
{
    public class BookConfig : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.HasData(
                new Book { Id = 1, CategoryId = 1,Title = "Karagoz ve Hacivat", Price = 75 },
                new Book { Id = 2, CategoryId = 2, Title = "Mesnevi", Price = 34 },
                new Book { Id = 3, CategoryId = 1, Title = "Devlet", Price = 234 }
            );
        }
    }
}
