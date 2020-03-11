using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Newtonsoft.Json;
using Spotacard.Core.Enums;

namespace Spotacard.Domain
{
    public partial class Card
    {
        public Guid Id { get; set; }

        public string Slug { get; set; }

        [Required] public string Title { get; set; }

        public CardTypes Type { get; set; }

        public string Description { get; set; }

        public string Body { get; set; }

        public Person Author { get; set; }

        public List<Comment> Comments { get; set; }

        [NotMapped] public bool Favorited => CardFavorites?.Any() ?? false;

        [NotMapped] public int FavoritesCount => CardFavorites?.Count ?? 0;

        [NotMapped]
        public List<string> TagList => (CardTags?.Select(x => x.TagId) ?? Enumerable.Empty<string>()).ToList();

        [JsonIgnore] public List<CardTag> CardTags { get; set; }

        [JsonIgnore] public List<CardFavorite> CardFavorites { get; set; }

        [JsonIgnore] public List<CardAttribute> CardAttributes { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
        [JsonIgnore] public List<Edge> Parents { get; set; }
        [JsonIgnore] public List<Edge> Children { get; set; }
    }
}
