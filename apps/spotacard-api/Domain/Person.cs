using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Spotacard.Domain
{
    public class Person
    {
        [JsonIgnore] public Guid Id { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }

        public string Bio { get; set; }

        public string Image { get; set; }

        [JsonIgnore] public List<CardFavorite> CardFavorites { get; set; }

        [JsonIgnore] public List<FollowedPeople> Following { get; set; }

        [JsonIgnore] public List<FollowedPeople> Followers { get; set; }

        [JsonIgnore] public byte[] Hash { get; set; }

        [JsonIgnore] public byte[] Salt { get; set; }
    }
}
