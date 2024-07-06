namespace iotms.Devices
{
    public static class DeviceConsts
    {
        private const string DefaultSorting = "{0}Name asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "Device." : string.Empty);
        }

        public const int NameMaxLength = 250;
        public const short MinTempAlertMinLength = 0;
        public const short MinTempAlertMaxLength = 50;
        public const short TempAlertFreqMinLength = 5;
        public const short TempAlertFreqMaxLength = 1440;
        public const short MinLDRAlertMinLength = 0;
        public const short MinLDRAlertMaxLength = 255;
        public const short LDRAlertFreqMinLength = 5;
        public const short LDRAlertFreqMaxLength = 1440;
    }
}