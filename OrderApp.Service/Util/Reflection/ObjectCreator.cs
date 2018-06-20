using System;
using System.Linq;
using System.Reflection;

namespace OrderApp.Service
{
    public static class ObjectCreator<T> where T : class
    {
        public static T CreateInstance(string type, string[] parameters = null)
        {
            Assembly currentAssembly = Assembly.GetExecutingAssembly();

            Type currentType = currentAssembly.GetTypes().FirstOrDefault(x => x.Name.ToLower() == type.ToLower());

            if (currentType == null)
            {
                return null;
            }

            T instance = (parameters == null || parameters.Length == 0 ? 
                         (T)Activator.CreateInstance(currentType) : 
                         (T)Activator.CreateInstance(currentType, args: new object[] { parameters }));

            return instance;
        }
    }
}