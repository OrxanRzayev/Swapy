namespace Swapy.Common.Exceptions
{
    public class EmailOrPhoneTakenException : Exception
    {
        public EmailOrPhoneTakenException(string message) : base(message)
        {
        }
    }
}
