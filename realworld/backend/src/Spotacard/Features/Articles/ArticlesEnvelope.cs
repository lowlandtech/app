using System.Collections.Generic;
using Spotacard.Domain;

namespace Spotacard.Features.Articles
{
    public class ArticlesEnvelope
    {
        public List<Article> Articles { get; set; }

        public int ArticlesCount { get; set; }
    }
}