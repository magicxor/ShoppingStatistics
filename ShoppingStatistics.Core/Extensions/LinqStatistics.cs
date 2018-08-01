using System;
using System.Collections.Generic;
using System.Linq;

namespace ShoppingStatistics.Core.Extensions
{
    public static class LinqStatistics
    {
        /// <summary>
        /// Returns all non-null items in a sequence
        /// </summary>
        /// <typeparam name="T">The type of the sequence</typeparam>
        /// <param name="source">The Sequence</param>
        /// <returns>All non-null elements in the sequence</returns>
        public static IEnumerable<T> AllValues<T>(this IEnumerable<T?> source) where T : struct
        {
            if (source == null)
                throw new ArgumentNullException("source", "The source enumeration cannot be null");

            return source.Where(x => x.HasValue).Select(x => (T)x);
        }

        /// <summary>
        /// Returns all elements in a sequence of Tuples where the Tuple's are not null
        /// </summary>
        /// <typeparam name="T">The type of the Tuple's Items</typeparam>
        /// <param name="source">The sequence</param>
        /// <returns>All Tuples in the sequence with non-null items</returns>
        public static IEnumerable<Tuple<T, T>> AllValues<T>(this IEnumerable<Tuple<T?, T?>> source) where T : struct
        {
            if (source == null)
                throw new ArgumentNullException("source", "The source enumeration cannot be null");

            return source.Where(x => x.Item1.HasValue && x.Item2.HasValue).Select(t => new Tuple<T, T>((T)t.Item1, (T)t.Item2));
        }

        /// <summary>
        /// Computes the Median of a sequence of int values. (see <a href="https://github.com/dkackman/LinqStatistics">LinqStatistics</a>)
        /// </summary>
        /// <param name="source">A sequence of int values to calculate the Median of.</param>
        /// <returns>The Median of the sequence of values.</returns>
        public static double Median(this IEnumerable<int> source)
        {
            if (source == null)
                throw new ArgumentNullException("source");

            var sortedList = (from number in source
                              orderby number
                              select (double)number).ToList();

            int count = sortedList.Count;
            int itemIndex = count / 2;

            if (count % 2 == 0)
            {
                // Even number of items.
                return (sortedList[itemIndex] + sortedList[itemIndex - 1]) / (double)2;
            }

            // Odd number of items.
            return sortedList[itemIndex];
        }

        /// <summary>
        /// Computes the Median of a sequence of int values. (see <a href="https://github.com/dkackman/LinqStatistics">LinqStatistics</a>)
        /// </summary>
        /// <param name="source">A sequence of int values to calculate the Median of.</param>
        /// <returns>The Median of the sequence of values.</returns>
        public static double Median(this IEnumerable<long> source)
        {
            if (source == null)
                throw new ArgumentNullException("source");

            var sortedList = (from number in source
                              orderby number
                              select (double)number).ToList();

            int count = sortedList.Count;
            int itemIndex = count / 2;

            if (count % 2 == 0)
            {
                // Even number of items.
                return (sortedList[itemIndex] + sortedList[itemIndex - 1]) / (double)2;
            }

            // Odd number of items.
            return sortedList[itemIndex];
        }


        /// <summary>
        /// Computes the Median of a sequence of double values. (see <a href="https://github.com/dkackman/LinqStatistics">LinqStatistics</a>)
        /// </summary>
        /// <param name="source">A sequence of double values to calculate the Median of.</param>
        /// <returns>The Median of the sequence of values.</returns>
        public static double Median(this IEnumerable<double> source)
        {
            if (source == null)
                throw new ArgumentNullException("source");

            var sortedList = (from number in source
                              orderby number
                              select (double)number).ToList();

            int count = sortedList.Count;
            int itemIndex = count / 2;

            if (count % 2 == 0)
            {
                // Even number of items.
                return (sortedList[itemIndex] + sortedList[itemIndex - 1]) / (double)2;
            }

            // Odd number of items.
            return sortedList[itemIndex];
        }

        /// <summary>
        /// Computes the Median of a sequence of mullable decimal values, or null if the source sequence is
        ///     empty or contains only values that are null.
        /// </summary>
        /// <param name="source">A sequence of nullable decimal values to calculate the Median of.</param>
        /// <returns>The Median of the sequence of values.</returns>
        public static decimal? Median(this IEnumerable<decimal?> source)
        {
            IEnumerable<decimal> values = source.AllValues();
            if (values.Any())
                return values.Median();

            return null;
        }

        /// <summary>
        /// Computes the Median of a sequence of decimal values.
        /// </summary>
        /// <param name="source">A sequence of decimal values to calculate the Median of.</param>
        /// <returns>The Median of the sequence of values.</returns>
        public static decimal Median(this IEnumerable<decimal> source)
        {
            if (source == null)
                throw new ArgumentNullException("source");

            var sortedList = (from number in source
                              orderby number
                              select (decimal)number).ToList();

            int count = sortedList.Count;
            int itemIndex = count / 2;

            if (count % 2 == 0)
            {
                // Even number of items.
                return (sortedList[itemIndex] + sortedList[itemIndex - 1]) / (decimal)2;
            }

            // Odd number of items.
            return sortedList[itemIndex];
        }

        /// <summary>
        ///     Computes the Median of a sequence of nullable decimal values that are obtained
        ///     by invoking a transform function on each element of the input sequence.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <param name="source">A sequence of values to calculate the Median of.</param>
        /// <param name="selector">A transform function to apply to each element.</param>
        /// <returns>The Median of the sequence of values.</returns>
        public static decimal? Median<TSource>(this IEnumerable<TSource> source, Func<TSource, decimal?> selector)
        {
            if (source == null)
                throw new ArgumentNullException("source");

            if (selector == null)
                throw new ArgumentNullException("selector");

            return source.Select(selector).Median();
        }

        /// <summary>
        ///     Computes the Median of a sequence of decimal values that are obtained
        ///     by invoking a transform function on each element of the input sequence.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <param name="source">A sequence of values to calculate the Median of.</param>
        /// <param name="selector">A transform function to apply to each element.</param>
        /// <returns>The Median of the sequence of values.</returns>
        public static decimal Median<TSource>(this IEnumerable<TSource> source, Func<TSource, decimal> selector)
        {
            if (source == null)
                throw new ArgumentNullException("source");

            if (selector == null)
                throw new ArgumentNullException("selector");

            return source.Select(selector).Median();
        }
    }
}
