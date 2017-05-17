using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace CollectionDifferencesLibrary.Tests
{
    [TestClass]
    public class ListExtensionsTests
    {
        [TestMethod]
        public void MethodComapre_SupplySameList_NoDifferences()
        {
            List<int> testObject = new List<int>
            {
                1,
                2,
                3,
                4
            };

            List<int> newListTestData = new List<int>
            {
                1,
                2,
                3,
                4
            };

            ListDifferences<int> expectedResult = ListDifferences<int>.Empty;

            ListDifferences<int> actualResult = testObject.Compare(newListTestData);

            Assert.IsNotNull(actualResult);
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void MethodCompare_UseEmptyOriginalList_ResultContainsOnlyNewEntries()
        {
            const int expectedNewListCount = 4;

            List<int> testObject = new List<int>();

            List<int> newListTestData = new List<int>
            {
                1,
                2,
                3,
                4
            };

            List<int> expectedNewList = new List<int>
            {
                1,
                2,
                3,
                4
            };

            ListDifferences<int> expectedResult = new ListDifferences<int>(expectedNewList, Enumerable.Empty<int>(), Enumerable.Empty<int>());

            ListDifferences<int> actualResult = testObject.Compare<int>(newListTestData);

            Assert.IsNotNull(actualResult);
            Assert.AreEqual(expectedResult, actualResult);
            Assert.AreEqual(expectedNewListCount, actualResult.New.Count());
        }

        [TestMethod]
        public void MethodCompare_SupplyNewEntries_ResultContainsOnlyNewEntries()
        {
            const int expectedNewListCount = 2;

            List<int> testObject = new List<int>
            {
                1,
                2
            };

            List<int> newListTestData = new List<int>
            {
                1,
                2,
                3,
                4
            };

            List<int> expectedNewList = new List<int>
            {
                3,
                4
            };

            ListDifferences<int> expectedResult = new ListDifferences<int>(expectedNewList, Enumerable.Empty<int>(), Enumerable.Empty<int>());

            ListDifferences<int> actualResult = testObject.Compare<int>(newListTestData);

            Assert.IsNotNull(actualResult);
            Assert.AreEqual(expectedResult, actualResult);
            Assert.AreEqual(expectedNewListCount, actualResult.New.Count());
        }

        [TestMethod]
        public void MethodCompare_SupplyAlteredList_ResultContainsOnlyUpdatedEntries()
        {
            const int expectedUpdatedListCount = 2;

            List<int> testObject = new List<int>
            {
                1,
                2,
                3,
                4
            };

            List<int> newListTestData = new List<int>
            {
                1,
                3,
                2,
                4
            };

            List<int> expectedUpdatedList = new List<int>
            {
                3,
                2
            };

            ListDifferences<int> expectedResult = new ListDifferences<int>(Enumerable.Empty<int>(), expectedUpdatedList, Enumerable.Empty<int>());

            ListDifferences<int> actualResult = testObject.Compare(newListTestData, (item, newItem) => Equals(item, newItem), (item, newItem) =>
            {
                int originalIndex = testObject.IndexOf(newItem);
                int newIndex = newListTestData.IndexOf(newItem);

                return Equals(originalIndex, newIndex);
            });

            Assert.IsNotNull(actualResult);
            Assert.AreEqual(expectedResult, actualResult);
            Assert.AreEqual(expectedUpdatedListCount, actualResult.Updated.Count());
        }

        [TestMethod]
        public void MethodCompare_SupplyEmptyNewList_ResultContainsOnlyDeletedEntries()
        {
            const int expectedDeletedListCount = 2;

            List<int> testObject = new List<int>
            {
                1,
                2
            };

            List<int> newListTestData = new List<int>();

            List<int> expectedDeletedList = new List<int>
            {
                1,
                2
            };

            ListDifferences<int> expectedResult = new ListDifferences<int>(Enumerable.Empty<int>(), Enumerable.Empty<int>(), expectedDeletedList);

            ListDifferences<int> actualResult = testObject.Compare<int>(newListTestData);

            Assert.IsNotNull(actualResult);
            Assert.AreEqual(expectedResult, actualResult);
            Assert.AreEqual(expectedDeletedListCount, actualResult.Deleted.Count());
        }

        [TestMethod]
        public void MethodCompare_SupplyNewUpdatedAndDeletedList_ContainsNewUpdatedAndDeletedEntries()
        {
            const int expectedNewListCount = 1;
            const int expectedUpdatedListCount = 1;
            const int expectedDeletedListCount = 1;

            List<int> testObject = new List<int>
            {
                1,
                2,
                3
            };

            List<int> newListTestData = new List<int>
            {
                1,
                3,
                4
            };

            List<int> expectedNewList = new List<int>
            {
                4
            };

            List<int> expectedUpdatedList = new List<int>
            {
                3
            };

            List<int> expectedDeletedList = new List<int>
            {
                2
            };

            ListDifferences<int> expectedResult = new ListDifferences<int>(expectedNewList, expectedUpdatedList, expectedDeletedList);

            ListDifferences<int> actualResult = testObject.Compare(newListTestData, (item, newItem) => Equals(item, newItem), (item, newItem) =>
            {
                int originalIndex = testObject.IndexOf(newItem);
                int newIndex = newListTestData.IndexOf(newItem);

                return Equals(originalIndex, newIndex);
            });

            Assert.IsNotNull(actualResult);
            Assert.AreEqual(expectedResult, actualResult);

            Assert.AreEqual(expectedNewListCount, actualResult.New.Count());
            Assert.AreEqual(expectedUpdatedListCount, actualResult.Updated.Count());
            Assert.AreEqual(expectedDeletedListCount, actualResult.Deleted.Count());
        }
    }
}
