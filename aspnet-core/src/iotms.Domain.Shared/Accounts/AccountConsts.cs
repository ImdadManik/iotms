namespace iotms.Accounts
{
    public static class AccountConsts
    {
        private const string DefaultSorting = "{0}Name asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "Account." : string.Empty);
        }

        public const int NameMaxLength = 250;
        public const int LocationMaxLength = 250;
        public const int AddressMaxLength = 250;
        public const int WebMaxLength = 500;
    }
}