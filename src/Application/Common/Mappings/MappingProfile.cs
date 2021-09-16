using AutoMapper;
using System;
using System.Linq;
using System.Reflection;

namespace Application.Common.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            ApplyMappingsFromAssembly(Assembly.GetExecutingAssembly());
        }

        private void ApplyMappingsFromAssembly(Assembly assembly)
        {
            var types = assembly.GetExportedTypes()
                .Where(t => t.GetInterfaces().Any(i => 
                    i.IsGenericType && 
                    ((i.GetGenericTypeDefinition() == typeof(IMapFrom<>)) || 
                    i.GetGenericTypeDefinition() == typeof(IMapTo<>))
                    ))
                .ToList();

            foreach (var type in types)
            {
                var instance = Activator.CreateInstance(type);

                var iMapFromMethodInfo = type.GetMethod("Mapping") 
                    ?? type.GetInterface("IMapFrom`1").GetMethod("Mapping");

                var iMapToMethodInfo = type.GetMethod("Mapping")
                    ?? type.GetInterface("IMapTo`1").GetMethod("Mapping");

                iMapFromMethodInfo?.Invoke(instance, new object[] { this });
                iMapToMethodInfo?.Invoke(instance, new object[] { this });
            }
        }
    }
}