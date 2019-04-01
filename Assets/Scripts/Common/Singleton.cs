// using System.Collections;
// using System.Collections.Generic;
// using System.Reflection;
// using UnityEngine;

// public abstract class Singleton<T> where T : Singleton<T>
// {
//     static T instance;

//     public static T GetInstance()
//     {
//         if (instance == null)
//         {
//             var type = typeof(T);
//             var instance = type.GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
//         }

//         return instance;
//     }
// }
