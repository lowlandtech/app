using System;
using Newtonsoft.Json;

namespace Spotacard.Domain
{
    public class Comment
    {
        [JsonProperty("id")]
        public int CommentId { get; set; }

        public string Body { get; set; }

        public Person Author { get; set; }
        [JsonIgnore]
        public int AuthorId { get; set; }

        [JsonIgnore]
        public Card Card { get; set; }
        [JsonIgnore]
        public int CardId { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}