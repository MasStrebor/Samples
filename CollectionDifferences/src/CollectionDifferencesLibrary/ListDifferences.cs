using System;
using System.Collections.Generic;
using System.Linq;

namespace CollectionDifferencesLibrary
{
    /// <summary>
    /// A class representing the results of comparing two lists. This will contain any new, updated or deleted items.
    /// </summary>
    /// <typeparam name="T">The type of each list item.</typeparam>
    public class ListDifferences<T> : IEquatable<ListDifferences<T>>
    {
        private readonly static ListDifferences<T> s_Empty = new ListDifferences<T>(Enumerable.Empty<T>(), Enumerable.Empty<T>(), Enumerable.Empty<T>());

        /// <summary>
        /// Gets the empty instance of the <see cref="ListDifferences{T}"/> class.
        /// </summary>
        public static ListDifferences<T> Empty
        {
            get
            {
                return s_Empty;
            }
        }

        /// <summary>
        /// Gets the list of new items.
        /// </summary>
        public IEnumerable<T> New
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the list of updated items.
        /// </summary>
        public IEnumerable<T> Updated
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the list of deleted items.
        /// </summary>
        public IEnumerable<T> Deleted
        {
            get;
            private set;
        }

        /// <summary>
        /// Initialises new instance of the <see cref="ListDifferences{T}"/> class.
        /// </summary>
        /// <param name="newList">The list of new items.</param>
        /// <param name="updatedList">The list of items containing changes.</param>
        /// <param name="deletedList">The list of times that no longer exist within the original.</param>
        public ListDifferences(IEnumerable<T> newList, IEnumerable<T> updatedList, IEnumerable<T> deletedList)
        {
            if (newList == null)
            {
                throw new ArgumentNullException(nameof(newList));
            }

            if (updatedList == null)
            {
                throw new ArgumentNullException(nameof(updatedList));
            }

            if (deletedList == null)
            {
                throw new ArgumentNullException(nameof(deletedList));
            }

            New = newList;
            Updated = updatedList;
            Deleted = deletedList;
        }

        /// <summary>
        /// Determines whether the specified object is equal to the current object.
        /// </summary>
        /// <param name="other">The object to compare with the current object.</param>
        /// <returns>true if the specified object is equal to the current object; otherwise, false.</returns>
        public bool Equals(ListDifferences<T> other)
        {
            return other != null
                && New.SequenceEqual(other.New)
                && Updated.SequenceEqual(other.Updated)
                && Deleted.SequenceEqual(other.Deleted);
        }

        /// <summary>
        /// Determines whether the specified object is equal to the current object.
        /// </summary>
        /// <param name="obj">The object to compare with the current object.</param>
        /// <returns>true if the specified object is equal to the current object; otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(obj, null))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            ListDifferences<T> item = obj as ListDifferences<T>;
            if (item == null)
            {
                return false;
            }

            return Equals(item);
        }

        /// <summary>
        /// Serves as the default hash function.
        /// </summary>
        /// <returns>A hash code for the current object.</returns>
        public override int GetHashCode()
        {
            int returnHashcode = New.GetHashCode();
            returnHashcode = (returnHashcode * 397) ^ (Updated.GetHashCode());
            returnHashcode = (returnHashcode * 397) ^ (Deleted.GetHashCode());

            return returnHashcode;
        }
    }
}
