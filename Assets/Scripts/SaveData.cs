using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Reflection;

// found on https://stackoverflow.com/questions/1293496
// hope it works!
namespace SerializeStatic_NET
{
    public static class SerializeStatic
    {
        private static readonly BinaryFormatter formatter = new BinaryFormatter();
        public static bool Save(Type staticClass, string filename)
        {
            try
            {
                FieldInfo[] fields = staticClass.GetFields(BindingFlags.Static | BindingFlags.Public);
                object[,] a = new object[fields.Length, 2];
                int i = 0;
                foreach (FieldInfo field in fields)
                {
                    a[i, 0] = field.Name;
                    a[i, 1] = field.GetValue(null);
                    i++;
                };
                Stream f = File.Open(filename, FileMode.Create);
                formatter.Serialize(f, a);
                f.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool Load(Type staticClass, string filename)
        {
            try
            {
                FieldInfo[] fields = staticClass.GetFields(BindingFlags.Static | BindingFlags.Public);
                object[,] a;
                Stream f = File.Open(filename, FileMode.Open);
                a = formatter.Deserialize(f) as object[,];
                f.Close();
                if (a.GetLength(0) != fields.Length) return false;
                int i = 0;
                foreach (FieldInfo field in fields)
                {
                    if (field.Name == (a[i, 0] as string))
                    {
                        field.SetValue(null, a[i, 1]);
                    }
                    i++;
                };
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}


public static class SaveData
{
    public static float highScore = 0;
    public static int money = 0;

    public static int healthIdx = 0;
    public static int autoIdx = 0;
    public static int bomberIdx = 0;

    public static int Health { get { return ShopItems.hpValues[healthIdx]; } }

    public static bool HasAuto { get { return ShopItems.automaticValues[autoIdx]; } }

    public static bool HasBomber { get { return ShopItems.bomberValues[bomberIdx]; } }
}
