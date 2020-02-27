﻿using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Spotacard.Domain;
using Spotacard.Infrastructure;
using Spotacard.Infrastructure.Errors;
using Spotacard.Infrastructure.Security;

namespace Spotacard.Features.Users
{
    public class Login
    {
        public class UserData
        {
            public string Email { get; set; }

            public string Password { get; set; }
        }

        public class UserDataValidator : AbstractValidator<UserData>
        {
            public UserDataValidator()
            {
                RuleFor(x => x.Email).NotNull().NotEmpty();
                RuleFor(x => x.Password).NotNull().NotEmpty();
            }
        }

        public class Command : IRequest<UserEnvelope>
        {
            public UserData User { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.User).NotNull().SetValidator(new UserDataValidator());
            }
        }

        public class Handler : IRequestHandler<Command, UserEnvelope>
        {
            private readonly GraphContext _context;
            private readonly IJwtTokenGenerator _jwtTokenGenerator;
            private readonly IMapper _mapper;
            private readonly IPasswordHasher _passwordHasher;

            public Handler(GraphContext context, IPasswordHasher passwordHasher, IJwtTokenGenerator jwtTokenGenerator,
                IMapper mapper)
            {
                _context = context;
                _passwordHasher = passwordHasher;
                _jwtTokenGenerator = jwtTokenGenerator;
                _mapper = mapper;
            }

            public async Task<UserEnvelope> Handle(Command message, CancellationToken cancellationToken)
            {
                var person = await _context.Persons.Where(x => x.Email == message.User.Email)
                    .SingleOrDefaultAsync(cancellationToken);
                if (person == null)
                    throw new RestException(HttpStatusCode.Unauthorized, new {Error = "Invalid email / password."});

                if (!person.Hash.SequenceEqual(_passwordHasher.Hash(message.User.Password, person.Salt)))
                    throw new RestException(HttpStatusCode.Unauthorized, new {Error = "Invalid email / password."});

                var user = _mapper.Map<Person, User>(person);
                user.Token = await _jwtTokenGenerator.CreateToken(person.Username);
                return new UserEnvelope(user);
            }
        }
    }
}
