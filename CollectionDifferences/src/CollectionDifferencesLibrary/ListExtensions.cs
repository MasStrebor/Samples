using System;
using System.Collections.Generic;
using System.Linq;

namespace CollectionDifferencesLibrary
{
    /// <summary>
    /// Provides additional functionality to the <see cref="IList{T}"/> interface.
    /// </summary>
    public static class ListExtensions
    {
        /// <summary>
        /// Compares this instance with the specified new list and returns the new, updated and deleted items.
        /// </summary>
        /// <typeparam name="T">The type of item within each list.</typeparam>
        /// <param name="originalList">The original set of items.</param>
        /// <param name="newList">The new list to compare with the original.</param>
        /// <returns>The results of the list comparison including new, updated and deleted items.</returns>
        public static ListDifferences<T> Compare<T>(this IList<T> originalList, IList<T> newList)
        {
            if (originalList == null)
            {
                throw new ArgumentNullException(nameof(originalList));
            }

            if (newList == null)
            {
                throw new ArgumentNullException(nameof(newList));
            }

            return Compare(originalList, newList, (item, newItem) => Equals(item, newItem));
        }

        /// <summary>
        /// Compares this instance with the specified new list and returns the new, updated and deleted items.
        /// </summary>
        /// <typeparam name="T">The type of item within each list.</typeparam>
        /// <param name="originalList">The original set of items.</param>
        /// <param name="newList">The new list to compare with the original.</param>
        /// <param name="keyEqualityFunction">A function to represent the equality to find a matching item within the original list.</param>
        /// <param name="equalityFunction">A function to determine whether or not the editable information of an item has changed from the original.</param>
        /// <returns>The results of the list comparison including new, updated and deleted items.</returns>
        public static ListDifferences<T> Compare<T>(this IList<T> originalList, IList<T> newList, Func<T, T, bool> equalityFunction)
        {
            if (originalList == null)
            {
                throw new ArgumentNullException(nameof(originalList));
            }

            if (newList == null)
            {
                throw new ArgumentNullException(nameof(newList));
            }

            if (equalityFunction == null)
            {
                throw new ArgumentNullException(nameof(equalityFunction));
            }

            return Compare(originalList, newList, equalityFunction, equalityFunction);
        }

        /// <summary>
        /// Compares this instance with the specified new list and returns the new, updated and deleted items.
        /// </summary>
        /// <typeparam name="T">The type of item within each list.</typeparam>
        /// <param name="originalList">The original set of items.</param>
        /// <param name="newList">The new list to compare with the original.</param>
        /// <param name="keyEqualityFunction">A function to represent the equality to find a matching item within the original list.</param>
        /// <param name="equalityFunction">A function to determine whether or not the editable information of an item has changed from the original.</param>
        /// <returns>The results of the list comparison including new, updated and deleted items.</returns>
        public static ListDifferences<T> Compare<T>(this IList<T> originalList, IList<T> newList, Func<T, T, bool> keyEqualityFunction,  Func<T, T, bool> equalityFunction)
        {
            if (originalList == null)
            {
                throw new ArgumentNullException(nameof(originalList));
            }

            if (newList == null)
            {
                throw new ArgumentNullException(nameof(newList));
            }

            if (keyEqualityFunction == null)
            {
                throw new ArgumentNullException(nameof(keyEqualityFunction));
            }

            if (equalityFunction == null)
            {
                throw new ArgumentNullException(nameof(equalityFunction));
            }

            IList<T> newItemList = new List<T>();
            IList<T> updatedList = new List<T>();
            IList<T> deletedList = originalList.ToList();

            foreach (T newItem in newList)
            {
                T originalItem = originalList.FirstOrDefault((item) => keyEqualityFunction(item, newItem));
                if (!Equals(originalItem, default(T)))
                {
                    //Ensure the new item does not get deleted as it existed in the original list.
                    deletedList.Remove(originalItem);

                    if (!equalityFunction(originalItem, newItem))
                    {
                        updatedList.Add(newItem);
                    }
                }
                else
                {
                    newItemList.Add(newItem);
                }
            }

            return new ListDifferences<T>(newItemList, updatedList, deletedList);
        }
    }
}
