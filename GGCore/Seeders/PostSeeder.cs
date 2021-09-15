using GGCore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GGCore.Configs.Seeders
{
    public class PostSeeder : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.HasData(
                    new Post
                    {
                        Id = 1,
                        Title = "Post 1",
                        Content = "Content of First Post"
                    },
                    new Post
                    {
                        Id = 2,
                        Title = "Post 2",
                        Content = "Content of Second Post"
                    },
                    new Post
                    {
                        Id = 3,
                        Title = "Post 3",
                        Content = "Content of Third Post"
                    }
                );

        }
    }
}
