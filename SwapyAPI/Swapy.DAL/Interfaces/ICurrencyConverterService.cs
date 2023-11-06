namespace Swapy.BLL.Interfaces
{
    public interface ICurrencyConverterService
    {
        decimal Convert(string fromCode, string toCode, decimal value);
    }
}
