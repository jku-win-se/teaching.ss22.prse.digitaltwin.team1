namespace SmartRoom.CommonBase.Utils
{
    public static class GenericMapper
    {
        public static T? MapObjects<T, S>(T to, S from)
        {
            if (to == null || from == null) return default;

            foreach (var prop in to.GetType().GetProperties())
            {
                if (from.GetType().GetProperties().Any(p => p.Name == prop.Name))
                {
                    var x = from.GetType().GetProperties().First(p => p.Name == prop.Name);
                    if (prop.CanWrite) prop.SetValue(to, x.GetValue(from));
                }
            }
            return to;
        }
    }
}
