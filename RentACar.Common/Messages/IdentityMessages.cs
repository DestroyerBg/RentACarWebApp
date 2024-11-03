namespace RentACar.Common.Messages
{
    public static class IdentityMessages
    {
        public static class Result
        {
            public const string UserLoggedIn = "User logged in.";
            public const string UserCreatedAccount = "User created a new account with password.";
            public const string UserLogout = "User logged out.";

        }

        public static class Warning
        {
            public const string UserLocked = "User account locked out.";
        }

        public static class Error
        {
            public const string InvalidLoginAttempt = "Invalid login attempt.";
        }
    }
}
