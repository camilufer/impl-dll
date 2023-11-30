using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImplementacionFramework1
{
    class SmartControl
    {
        public static Control LoadSmartControl(string libreria, string componente)
        {
            Form userControl;
            Assembly assembly;
            Type type;

            assembly = Assembly.LoadFrom(libreria + ".dll");
            type = assembly.GetType(libreria + "." + componente);

            Type[] argumentType = { typeof(object[]) };
            ConstructorInfo constructorSmartControl = type.GetConstructor(argumentType);

            constructorSmartControl = type.GetConstructor(Type.EmptyTypes);
            if (constructorSmartControl == null)
                throw new Exception(type.FullName + " no tiene un constructor público sin parámetros.");

            userControl = constructorSmartControl.Invoke(null) as Form;
            userControl.AutoScroll = true;
            return userControl;
        }
    }
}
