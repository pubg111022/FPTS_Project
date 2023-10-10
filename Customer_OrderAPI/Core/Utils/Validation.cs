namespace b2.Core.Regex
{
    using System.Text.RegularExpressions;
    public class Validation
    {
        private const string EmailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
        private const string PhonePattern = @"^\+?[0-9]{10}$";

        private Regex emailRegex;
        private Regex phoneRegex;

        public Validation()
        {
            emailRegex = new Regex(EmailPattern);
            phoneRegex = new Regex(PhonePattern);
        }

        public bool IsEmailValid(string email)
        {
            return emailRegex.IsMatch(email);
        }

        public bool IsPhoneValid(string ?phoneNumber)
        {
            return !string.IsNullOrEmpty(phoneNumber) && phoneRegex.IsMatch(phoneNumber);
        }
    }
}
