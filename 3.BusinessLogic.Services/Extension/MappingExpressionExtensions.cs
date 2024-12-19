using AutoMapper;

// https://chatgpt.com/share/673c8148-81d8-8008-b4b8-afd0e082fe5a

namespace _3.BusinessLogic.Services.Extension
{
    public static class MappingExpressionExtensions
    {

        public static IMappingExpression<TSource, TDestination> IgnoreNullProperty<TSource, TDestination>(
            this IMappingExpression<TSource, TDestination> mappingExpression)
        {
            mappingExpression.ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            return mappingExpression;
        }

        public static IMappingExpression<TSource, TDestination> MapObjectProperties<TSource, TDestination>(
            this IMappingExpression<TSource, TDestination> mappingExpression)
        {

            return mappingExpression.AfterMap((src, dest) => {
                var destType = dest!.GetType();
                var sourceType = src!.GetType();

                foreach (var destProp in destType.GetProperties())
                {
                    var matchingSource = sourceType.GetProperties()
                        .FirstOrDefault(srcProp =>
                        {
                            if (srcProp.PropertyType.IsClass && srcProp.PropertyType != typeof(string))
                            {
                                var innerProp = srcProp.PropertyType.GetProperties()
                                    .FirstOrDefault(p => p.Name == destProp.Name);
                                return innerProp != null;
                            }
                            return srcProp.Name == destProp.Name;
                        });

                    if (matchingSource != null)
                    {
                        if (matchingSource.PropertyType.IsClass && matchingSource.PropertyType != typeof(string))
                        {
                            var innerPropValue = matchingSource.GetValue(src);
                            var innerProp = matchingSource.PropertyType.GetProperties()
                                .FirstOrDefault(p => p.Name == destProp.Name);
                            if (innerProp != null)
                            {
                                var value = innerProp.GetValue(innerPropValue);
                                destProp.SetValue(dest, value);
                            }
                        }
                        else
                        {
                            var value = matchingSource.GetValue(src);
                            destProp.SetValue(dest, value);
                        }
                    }
                }
            });                
        }

        public static IMappingExpression<TSource, TDestination> MapNestedProperties<TSource, TDestination>(
            this IMappingExpression<TSource, TDestination> mappingExpression)
        {
            return mappingExpression.AfterMap((source, destination) =>
            {
                PopulateNestedProperties(source!, destination!);
            });        
        }

        private static void PopulateNestedProperties(object source, object destination)
        {
            var destType = destination.GetType();
            var sourceType = source.GetType();

            foreach (var destProp in destType.GetProperties())
            {
                var matchingSource = sourceType.GetProperties()
                    .FirstOrDefault(srcProp =>
                    {
                        if (srcProp.PropertyType.IsClass && srcProp.PropertyType != typeof(string))
                        {
                            var innerProp = srcProp.PropertyType.GetProperties()
                                .FirstOrDefault(p => p.Name == destProp.Name);
                            return innerProp != null;
                        }
                        return srcProp.Name == destProp.Name;
                    });

                if (matchingSource != null)
                {
                    if (matchingSource.PropertyType.IsClass && matchingSource.PropertyType != typeof(string))
                    {
                        var innerPropValue = matchingSource.GetValue(source);
                        var innerProp = matchingSource.PropertyType.GetProperties()
                            .FirstOrDefault(p => p.Name == destProp.Name);
                        if (innerProp != null)
                        {
                            var value = innerProp.GetValue(innerPropValue);
                            destProp.SetValue(destination, value);
                        }
                    }
                    else
                    {
                        var value = matchingSource.GetValue(source);
                        destProp.SetValue(destination, value);
                    }
                }
            }
        }
    }
}