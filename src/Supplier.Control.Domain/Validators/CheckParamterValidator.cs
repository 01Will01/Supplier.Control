
namespace Supplier.Control.Domain.Validators
{
    public static  class CheckParamterValidator
    {
        public static T CheckIsNotNull<T>(T value, string nameof)
        {
            if (value is null)
                throw new ArgumentNullException(nameof);

            return value;
        }
    }
}
