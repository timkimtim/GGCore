using GGCore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GGCore.Configs.Seeders
{
    public class CommentSeeder : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.HasData(
                    new Comment
                    {
                        Id = 1,
                        Text = "Comment 1",
                        PostId = 1
                    },
                    new Comment
                    {
                        Id = 2,
                        Text = "Comment 2",
                        PostId = 2
                    },
                    new Comment
                    {
                        Id = 3,
                        Text = "Comment 3",
                        PostId = 3
                    }
                );

        }
    }
}
