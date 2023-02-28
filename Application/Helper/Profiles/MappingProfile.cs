using System.Reflection;
using AutoMapper;

namespace Application.Helper.Profiles
{
    // public class MappingProfile : Profile
    // {
    //     public MappingProfile()
    //     {
    //         CreateMap<CreateUserCommand, User>().ReverseMap();
    //         CreateMap<UpdateUserCommand, User>().ReverseMap();
    //         CreateMap<UpdateUserCommand, UserRegisterDto>().ReverseMap(); 

    //         CreateMap<CreateProductCommand, Product>().ReverseMap(); 
    //         CreateMap<UpdateProductCommand, Product>().ReverseMap(); 
    //         CreateMap<UpdateProductCommand, ProductDto>().ReverseMap(); 

    //         CreateMap<CreateBidCommand, Bid>().ReverseMap();  
    //         CreateMap<UpdateBidCommand, Bid>().ReverseMap(); 
    //         CreateMap<UpdateBidCommand, CreateBidCommand>().ReverseMap(); 

    //         CreateMap<UpdateCategoryCommand, CategoryDto>().ReverseMap(); // IMapFrom<Category>
    //         CreateMap<Category, CategoryDto>().ReverseMap(); 
    //         CreateMap<UpdateProductCommand, CreateProductCommand>().ReverseMap();  
    //     }
    // }

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            ApplyMappingsFromAssembly(Assembly.GetExecutingAssembly());
        }

        private void ApplyMappingsFromAssembly(Assembly assembly)
        {
            var mapFromType = typeof(IMapFrom<>);

            const string mappingMethodName = nameof(IMapFrom<object>.Mapping);

            bool HasInterface(Type t) => t.IsGenericType && t.GetGenericTypeDefinition() == mapFromType;

            var types = assembly.GetExportedTypes().Where(t => t.GetInterfaces().Any(HasInterface)).ToList();

            var argumentTypes = new Type[] { typeof(Profile) };

            foreach (var type in types)
            {
                var instance = Activator.CreateInstance(type);

                var methodInfo = type.GetMethod(mappingMethodName);

                if (methodInfo != null)
                {
                    methodInfo.Invoke(instance, new object[] { this });
                }
                else
                {
                    var interfaces = type.GetInterfaces().Where(HasInterface).ToList();

                    if (interfaces.Count > 0)
                    {
                        foreach (var @interface in interfaces)
                        {
                            var interfaceMethodInfo = @interface.GetMethod(mappingMethodName, argumentTypes);

                            interfaceMethodInfo?.Invoke(instance, new object[] { this });
                        }
                    }
                }
            }
        }

    }
}