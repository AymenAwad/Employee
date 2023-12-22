using Hangfire.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Dynamic;
using System.Linq;
using System.Linq.Expressions;

namespace Shared.Exceptions
{


    public static class CollectionsExtensions
    {
        private static readonly Random rnd = new Random();


        public static bool ContainsAllItems<T>(this List<T> a, List<T> b)
        {
            return !b.Except(a).Any();
        }


        public static TValue GetValueOrDefault<TKey, TValue>(
            this IDictionary<TKey, TValue> dictionary,
            TKey key,
            TValue defaultValue = default)
        {
            if (dictionary == null)
            {
                throw new ArgumentNullException(nameof(dictionary));
            }

            return dictionary.TryGetValue(key, out TValue value) ? value : defaultValue;
        }

        public static IEnumerable<IEnumerable<T>> Partition<T>(this IEnumerable<T> source, int size)
        {
            T[] array = null;
            var count = 0;
            foreach (var item in source)
            {
                if (array == null)
                {
                    array = new T[size];
                }

                array[count] = item;
                count++;
                if (count != size)
                {
                    continue;
                }

                yield return new ReadOnlyCollection<T>(array);
                array = null;
                count = 0;
            }

            if (array == null)
            {
                yield break;
            }

            Array.Resize(ref array, count);
            yield return new ReadOnlyCollection<T>(array);
        }

        public static T RandomSelectItem<T>(this List<T> list)
        {
            var r = rnd.Next(list.Count);
            return list[r];
        }

        public static object ToAnonymousObject(this IDictionary<string, object> @this)
        {
            var obj = new ExpandoObject();
            var dic = (IDictionary<string, object>)obj;

            foreach (var keyValuePair in @this)
            {
                dic.Add(keyValuePair);
            }

            return dic;
        }

        public static string ToCommaSeparatedString(this List<string> items, string separator = ", ")
        {
            return string.Join(separator, items);
        }

        public static string FormatAsList(this List<string> items)
        {
            string message = "<ul>";
            foreach (var item in items)
            {
                message += "<li>" + item + "</li>";
            }
            message += "</ul>";
            return message;
        }

        public static IDictionary<TK, TV> ToDictionary<TK, TV>(this IEnumerable<KeyValuePair<TK, TV>> list)
        {
            return list.ToDictionary(kv => kv.Key, kv => kv.Value);
        }

        public static KeyValuePair<TK, TV> WithKey<TK, TV>(this KeyValuePair<TK, TV> kv, TK newKey)
        {
            return new KeyValuePair<TK, TV>(newKey, kv.Value);
        }

        public static KeyValuePair<TK, TV> WithValue<TK, TV>(this KeyValuePair<TK, TV> kv, TV newValue)
        {
            return new KeyValuePair<TK, TV>(kv.Key, newValue);
        }

        public static RadioButtonList ToRadioButtonList<T>(this IEnumerable<T> items, Expression<Func<T, object>> dataValueField, Expression<Func<T, object>> dataLabelField, object selectedValue = null)
        {

            return new RadioButtonList(items, dataValueField.GetPropertyName(), dataLabelField.GetPropertyName(), selectedValue);
        }

        public static string GetPropertyName<T>(this Expression<Func<T, object>> expression)
        {
            if (expression.Body is MemberExpression memberExpression)
            {
                return memberExpression.Member.Name;
            }
            else
            {
                var op = ((UnaryExpression)expression.Body).Operand;
                return ((MemberExpression)op).Member.Name;
            }
        }


        public static bool IsNullOrEmpty<T>([CanBeNull] this ICollection<T> source)
        {
            return source == null || source.Count <= 0;
        }

        public static bool AddIfNotContains<T>([NotNull] this ICollection<T> source, T item)
        {
            Check.NotNull(source, nameof(source));

            if (source.Contains(item))
            {
                return false;
            }

            source.Add(item);
            return true;
        }

        public static IEnumerable<T> AddIfNotContains<T>([NotNull] this ICollection<T> source, IEnumerable<T> items)
        {
            Check.NotNull(source, nameof(source));

            var addedItems = new List<T>();

            foreach (var item in items)
            {
                if (source.Contains(item))
                {
                    continue;
                }

                source.Add(item);
                addedItems.Add(item);
            }

            return addedItems;
        }

        public static bool AddIfNotContains<T>([NotNull] this ICollection<T> source, [NotNull] Func<T, bool> predicate, [NotNull] Func<T> itemFactory)
        {
            Check.NotNull(source, nameof(source));
            Check.NotNull(predicate, nameof(predicate));
            Check.NotNull(itemFactory, nameof(itemFactory));

            if (source.Any(predicate))
            {
                return false;
            }

            source.Add(itemFactory());
            return true;
        }

        public static IList<T> RemoveAll<T>([NotNull] this ICollection<T> source, Func<T, bool> predicate)
        {
            var items = source.Where(predicate).ToList();

            foreach (var item in items)
            {
                source.Remove(item);
            }

            return items;
        }

    }
    public sealed class RadioButtonList
    {
        #region Constructs
        public RadioButtonList(IEnumerable items, string dataValueField, string dataLabelField, object selectedValue = null)
        {
            Items = items;
            DataValueField = dataValueField;
            DataLabelField = dataLabelField;
            SelectedValue = selectedValue;
        }

        #endregion Constructs

        #region Property
        public string DataValueField { get; private set; }
        public string DataLabelField { get; private set; }
        public object SelectedValue { get; set; }
        public IEnumerable Items { get; private set; }
        #endregion Property

    }

}