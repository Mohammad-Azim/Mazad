namespace Application.Helper.ServiceExtensions
{
    [AttributeUsage(AttributeTargets.All, AllowMultiple = false)]
    public class ScopedRegistrationAttribute : Attribute { }

    [AttributeUsage(AttributeTargets.All, AllowMultiple = false)]
    public class SingletonRegistrationAttribute : Attribute { }

    [AttributeUsage(AttributeTargets.All, AllowMultiple = false)]
    public class TransientRegistrationAttribute : Attribute { }
}