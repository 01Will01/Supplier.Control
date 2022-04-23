namespace Supplier.Control.Domain.Extensions
{
    public static class Generalizer
    {
        public static bool IsNull<T>(this T value) => value is null;
        public static bool IsNotNull<T>(this T value) => !IsNull(value);
        public static bool IsIqual(this object principal, object compare)
        {
            bool iqual = true;

            if(principal is null && compare is null) return iqual;

            if(principal is null || compare is null) return !iqual;

            foreach (var property in principal.GetType().GetProperties())
            {
                if (iqual is false)
                    return iqual;

                var objValue = property.GetValue(principal);
                var anotherValue = property.GetValue(compare);

                iqual = objValue.Equals(anotherValue);
            }

            return iqual;
        }

        public static bool IsNotIqual(this object principal, object compare) => !IsIqual(principal, compare);

        public static List<T> TransformInList<T>(this T value) => new() { value };
    }
}
