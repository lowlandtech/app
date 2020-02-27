using Spotacard.Infrastructure;

namespace Spotacard
{
    public class StubCurrentUserAccessor : ICurrentUserAccessor
    {
        private readonly string _currentUserName;

        /// <summary>
        ///     stub the ICurrentUserAccessor with a given userName to be used in tests
        /// </summary>
        /// <param name="userName"></param>
        public StubCurrentUserAccessor(string userName)
        {
            _currentUserName = userName;
        }

        public string GetCurrentUsername()
        {
            return _currentUserName;
        }
    }
}
