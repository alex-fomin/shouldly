using System;
using System.Collections.Generic;
using System.Diagnostics;
using JetBrains.Annotations;

namespace Shouldly
{
    [DebuggerStepThrough]
    [ShouldlyMethods]
    public static partial class ShouldBeTestExtensions
    {
        [ContractAnnotation("actual:null,expected:notnull => halt;actual:notnull,expected:null => halt")]
        public static void ShouldBe<T>(this T actual, T expected)
        {
            ShouldBe(actual, expected, () => null, null);
        }
        [ContractAnnotation("actual:null,expected:notnull => halt;actual:notnull,expected:null => halt")]
        public static void ShouldBe<T>(this T actual, T expected, IEqualityComparer<T> comparer)
        {
            ShouldBe(actual, expected, () => null, comparer);
        }              
        [ContractAnnotation("actual:null,expected:notnull => halt;actual:notnull,expected:null => halt")]
        public static void  ShouldBe<T>(this T actual, T expected, string customMessage, IEqualityComparer<T> comparer = null)
        {
            ShouldBe(actual, expected, () => customMessage, comparer);
        }
        [ContractAnnotation("actual:null,expected:notnull => halt;actual:notnull,expected:null => halt")]
        public static void ShouldBe<T>(this T actual, T expected, [InstantHandle] Func<string> customMessage, IEqualityComparer<T> comparer = null)
        {
            actual.AssertAwesomely(v => Is.Equal(v, expected, comparer), actual, expected, customMessage);
        }
      
        [ContractAnnotation("actual:null,expected:null => halt")]
        public static void ShouldNotBe<T>(this T actual, T expected, IEqualityComparer<T> comparer = null)
        {
            ShouldNotBe(actual, expected, () => null, comparer);
        }      
        [ContractAnnotation("actual:null,expected:null => halt")]
        public static void ShouldNotBe<T>(this T actual, T expected, string customMessage, IEqualityComparer<T> comparer = null)
        {
            ShouldNotBe(actual, expected, () => customMessage, comparer);
        }
        [ContractAnnotation("actual:null,expected:null => halt")]
        public static void ShouldNotBe<T>(this T actual, T expected, [InstantHandle] Func<string> customMessage, IEqualityComparer<T> comparer = null)
        {
            actual.AssertAwesomely(v => !Is.Equal(v, expected, comparer), actual, expected, customMessage);
        }
             
        public static void ShouldBe<T>(this IEnumerable<T> actual, IEnumerable<T> expected, bool ignoreOrder = false, IEqualityComparer<T> comparer = null)
        {
            ShouldBe(actual, expected, ignoreOrder, () => null, comparer);
        }
        public static void ShouldBe<T>(this IEnumerable<T> actual, IEnumerable<T> expected, bool ignoreOrder, string customMessage)
        {
            ShouldBe(actual, expected, ignoreOrder, () => customMessage, null);
        }
        public static void ShouldBe<T>(this IEnumerable<T> actual, IEnumerable<T> expected, bool ignoreOrder, string customMessage, IEqualityComparer<T> comparer)
        {
            ShouldBe(actual, expected, ignoreOrder, () => customMessage, comparer);
        }
        public static void ShouldBe<T>(this IEnumerable<T> actual, IEnumerable<T> expected, bool ignoreOrder, [InstantHandle] Func<string> customMessage, IEqualityComparer<T> comparer)
        {
            if (ignoreOrder)
            {
                actual.AssertAwesomelyIgnoringOrder(v => Is.EqualIgnoreOrder(v, expected, comparer), actual, expected, customMessage);
            }
            else
            {
                if (ShouldlyConfiguration.CompareAsObjectTypes.Contains(typeof(T).FullName))
                {
                    actual.AssertAwesomely(v => Is.Equal(v, expected, new ObjectEqualityComparer<IEnumerable<T>>()), actual, expected, customMessage);
                }
                else
                {
                    actual.AssertAwesomely(v => Is.Equal(v, expected, comparer), actual, expected, customMessage);
                }
            }
        }

        public static void ShouldBe(this IEnumerable<decimal> actual, IEnumerable<decimal> expected, decimal tolerance)
        {
            ShouldBe(actual, expected, tolerance, () => null);
        }
        public static void ShouldBe(this IEnumerable<decimal> actual, IEnumerable<decimal> expected, decimal tolerance, string customMessage)
        {
            ShouldBe(actual, expected, tolerance, () => customMessage);
        }
        public static void ShouldBe(this IEnumerable<decimal> actual, IEnumerable<decimal> expected, decimal tolerance, [InstantHandle] Func<string> customMessage)
        {
            actual.AssertAwesomely(v => Is.Equal(v, expected, tolerance), actual, expected, tolerance, customMessage);
        }

        public static void ShouldBeSameAs(this object actual, object expected)
        {
            ShouldBeSameAs(actual, expected, () => null);
        }
        public static void ShouldBeSameAs(this object actual, object expected, string customMessage)
        {
            ShouldBeSameAs(actual, expected, () => customMessage);
        }
        public static void ShouldBeSameAs(this object actual, object expected, [InstantHandle] Func<string> customMessage)
        {
            actual.AssertAwesomely(v => Is.Same(v, expected), actual, expected, customMessage);
        }

        public static void ShouldNotBeSameAs(this object actual, object expected)
        {
            ShouldNotBeSameAs(actual, expected, () => null);
        }
        public static void ShouldNotBeSameAs(this object actual, object expected, string customMessage)
        {
            ShouldNotBeSameAs(actual, expected, () => customMessage);
        }
        public static void ShouldNotBeSameAs(this object actual, object expected, [InstantHandle] Func<string> customMessage)
        {
            actual.AssertAwesomely(v => !Is.Same(v, expected), actual, expected, customMessage);
        }
    }
}
