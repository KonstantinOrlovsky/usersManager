namespace UsersManager_DAL.Helpers
{
    public static class CopyPropsHelper
    {
        public static TTo CopySimplePropsFromObjectToAnother<TFrom, TTo>(
            TFrom from,
            Action<TFrom, TTo> extraProps = default) where TTo : class, new()
        {
            var toObject = new TTo();

            var simpleToProps = toObject.GetType()
                .GetProperties()
                .Where(e => e.PropertyType.IsValueType || e.PropertyType == typeof(string));

            var simpleFromProps = from.GetType()
                    .GetProperties()
                    .Where(e => simpleToProps.Any(sfp => string.Equals(e.Name, sfp.Name, StringComparison.OrdinalIgnoreCase)));

            foreach (var prop in simpleFromProps)
            {
                var fromValue = prop.GetValue(from);
                var toProp = simpleToProps.Single(x => string.Equals(x.Name, prop.Name, StringComparison.OrdinalIgnoreCase));
               
                toProp.SetValue(toObject, fromValue);
            }
            extraProps?.Invoke(from, toObject);

            return toObject;
        }
    }
}