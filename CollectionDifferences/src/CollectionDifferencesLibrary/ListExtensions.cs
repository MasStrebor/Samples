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


            if (originalList == null)
            {
                throw new ArgumentNullException(nameof(originalList));
            }

            if (newList == null)
            {
                throw new ArgumentNullException(nameof(newList));
            }

            IList<T> newItemList = new List<T>();
            IList<T> updatedList = new List<T>();
            IList<T> deletedList = originalList.ToList();

            for (int newItemIndex = 0; newItemIndex < newList.Count; newItemIndex++)
            {
                T newItem = newList[newItemIndex];

                int newItemIndexInOriginalList = originalList.IndexOf(newItem);
                if (newItemIndexInOriginalList > -1)
                {
                    //Ensure the new item does not get deleted as it existed in the original list.
                    deletedList.Remove(newItem);

                    if (newItemIndex != newItemIndexInOriginalList || !Equals(originalList[newItemIndexInOriginalList], newItem))
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
