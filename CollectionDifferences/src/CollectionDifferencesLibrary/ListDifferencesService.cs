using System;
using System.Collections.Generic;
using System.Linq;

namespace CollectionDifferencesLibrary
{
    public class ListDifferencesService
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="ListDifferencesService"/> class.
        /// </summary>
        public ListDifferencesService()
        {
        }

        /// <summary>
        /// Collects the new, updated, and deleted items from comparing the specified lists.
        /// </summary>
        /// <typeparam name="T">The type of item within each list.</typeparam>
        /// <param name="originalList">The original list.</param>
        /// <param name="newList">The new list of merge any changes.</param>
        public ListDifferences<T> ProcessDifferences<T>(IList<T> originalList, IList<T> newList)
        {
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

            //foreach (T newItem in newList)
            //{
                //int newItemIndex = originalList.IndexOf(newItem);
                //if (newItemIndex > -1)
                //{
                    //deletedList.Remove(newItem);

                    //T originalItem = originalList[newItemIndex];

                    //if (!Equals(originalItem, newItem))
                    //{
                        //updatedList.Add(newItem);
                    //}
                //}
                //else
                //{
                    //newItemList.Add(newItem);
                //}
            //}

            return new ListDifferences<T>(newItemList, updatedList, deletedList);
        }
    }
}
