using System.Collections.Generic;
using Spotacard.Domain;

namespace Spotacard.Features.Comments
{
    public class CommentsEnvelope
    {
        public CommentsEnvelope(List<Comment> comments)
        {
            Comments = comments;
        }

        public List<Comment> Comments { get; }
    }
}