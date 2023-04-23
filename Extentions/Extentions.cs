using System.Collections;

namespace Extentions
{
    public static class Extentions
    {
        public static IEnumerable<T> EveryNth<T>(this IEnumerable<T> a, int n, int startFrom = 0) =>
            a.Skip(startFrom).Where((_, index) => index % n == 0);

        public static string Format(this object? value)
        {
            if (value is null) return "";
            if (value is string s) return s;
            if (value is IEnumerable e)
            {

                var parts = e.Cast<object>().Select(Format).ToList();
                if (parts.All(p => p.Length < 10))
                    return "[" + string.Join(",", parts) + "]";
                return "[\n" + string.Join(",\n", parts) + "\n]";

            }
            return value.ToString() ?? "";
        }

        public static T Out<T>(this T val, string prefix = "")
        {
            Console.Write(prefix);
            Console.WriteLine(val.Format());
            return val;
        }

        public static IEnumerable<IList<T>> GroupBy<T>(this IEnumerable<T> list, int groupSize)
        {
            var group = new List<T>();
            int index = 0;

            foreach (var item in list)
            {

                group.Add(item);
                index++;
                if (index == groupSize)
                {
                    yield return group;
                    group = new List<T>();
                    index = 0;
                }

            }

            if (group.Count > 0)
                yield return group;
        }
    }
}