using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Newtonsoft.Json;

namespace Spotacard.Domain
{
    public class Card
    {
        [JsonIgnore]
        public int Id { get; set; }

        public string Slug { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Body { get; set; }

        public Person Author { get; set; }

        public List<Comment> Comments { get; set; }

        [NotMapped]
        public bool Favorited => CardFavorites?.Any() ?? false;

        [NotMapped]
        public int FavoritesCount => CardFavorites?.Count ?? 0;

        [NotMapped]
        public List<string> TagList => (CardTags?.Select(x => x.TagId) ?? Enumerable.Empty<string>()).ToList();

        [JsonIgnore]
        public List<CardTag> CardTags { get; set; }

        [JsonIgnore]
        public List<CardFavorite> CardFavorites { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}