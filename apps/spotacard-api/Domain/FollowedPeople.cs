using System;

namespace Spotacard.Domain
{
    public class FollowedPeople
    {
        public Guid ObserverId { get; set; }
        public Person Observer { get; set; }

        public Guid TargetId { get; set; }
        public Person Target { get; set; }
    }
}