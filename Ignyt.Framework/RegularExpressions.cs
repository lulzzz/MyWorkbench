namespace Ignyt.Framework {
    public static class RegularExpressions {
        public const string Email = @"^((\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*)\s*[;,.]{0,1}\s*)+$";
        public const string EmailError = @"Email address is not in the correct format. Please enter a valid email address";
        public const string Phone = @"^\++?[1-9][0-9]\d{6,14}$";
        public const string PhoneError = @"Cell/Phone number is not in the correct format. Please enter a valid phone number using the international code format ie: +27728956548";
        public const string Value = @"^[1-9]\d*$";
        public const string ValueError = @"Value must be greater than 0";
    }
}
