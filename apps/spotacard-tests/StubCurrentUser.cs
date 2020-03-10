using Spotacard.Infrastructure;

namespace Spotacard
{
    public class StubCurrentUser : ICurrentUser
    {
        private readonly string _currentUserName;

        /// <summary>
        ///     stub the ICurrentUser with a given userName to be used in tests
        /// </summary>
        /// <param name="userName"></param>
        public StubCurrentUser(string userName)
        {
            _currentUserName = userName;
        }

        public string GetCurrentUsername()
        {
            return _currentUserName;
        }
    }
}
