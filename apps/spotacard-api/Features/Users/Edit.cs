using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Spotacard.Domain;
using Spotacard.Infrastructure;
using Spotacard.Infrastructure.Security;

namespace Spotacard.Features.Users
{
    public class Edit
    {
        public class UserData
        {
            public string Username { get; set; }

            public string Email { get; set; }

            public string Password { get; set; }

            public string Bio { get; set; }

            public string Image { get; set; }
        }

        public class Command : IRequest<UserEnvelope>
        {
            public UserData User { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.User).NotNull();
            }
        }

        public class Handler : IRequestHandler<Command, UserEnvelope>
        {
            private readonly GraphContext _context;
            private readonly ICurrentUser _currentUser;
            private readonly IMapper _mapper;
            private readonly IPasswordHasher _passwordHasher;

            public Handler(GraphContext context, IPasswordHasher passwordHasher,
                ICurrentUser currentUser, IMapper mapper)
            {
                _context = context;
                _passwordHasher = passwordHasher;
                _currentUser = currentUser;
                _mapper = mapper;
            }

            public async Task<UserEnvelope> Handle(Command message, CancellationToken cancellationToken)
            {
                var currentUsername = _currentUser.GetCurrentUsername();
                var person = await _context.Persons.Where(x => x.Username == currentUsername)
                    .FirstOrDefaultAsync(cancellationToken);

                person.Username = message.User.Username ?? person.Username;
                person.Email = message.User.Email ?? person.Email;
                person.Bio = message.User.Bio ?? person.Bio;
                person.Image = message.User.Image ?? person.Image;

                if (!string.IsNullOrWhiteSpace(message.User.Password))
                {
                    var salt = Guid.NewGuid().ToByteArray();
                    person.Hash = _passwordHasher.Hash(message.User.Password, salt);
                    person.Salt = salt;
                }

                await _context.SaveChangesAsync(cancellationToken);

                return new UserEnvelope(_mapper.Map<Person, User>(person));
            }
        }
    }
}
