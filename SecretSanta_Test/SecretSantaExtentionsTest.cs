using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using SecretSanta.Extentions;

namespace SecretSanta_Test
{
    [TestClass]
    public class SecretSantaExtentionsTest
    {
        private IList<Participant> testList;

        [TestInitialize]
        public void SetUp()
        {
            testList = new List<Participant>();
            testList.Add(new Participant() { FirstName = "Name 1", LastName = "Last" });
            testList.Add(new Participant() { FirstName = "Name 2", LastName = "Last" });
            testList.Add(new Participant() { FirstName = "Name 3", LastName = "Last" });
            testList.Add(new Participant() { FirstName = "Name 4", LastName = "Last" });
            testList.Add(new Participant() { FirstName = "Name 5", LastName = "Last" });
        }

        [TestMethod]
        public void Helpers_GetShuffle_AllReturned_1000Tries()
        {
            for (int i = 0; i < 1000; i++)
            {
                var result = testList.GetShuffle();

                foreach (var a in testList)
                {
                    Assert.IsTrue(result.Contains(a));
                }
            }
        }

        [TestMethod]
        public void Helpers_GetPermutations_AllPermutationsReturned()
        {
            var result = testList.GetPermutations().Count();
            var expected = Factoral(testList.Count());

            Assert.AreEqual(expected, result, "There should be n! permutations, where n = {0}", testList.Count());
        }

        private int Factoral(int n)
        {
            if (n <= 1)
                return 1;

            return n * Factoral(n - 1);
        }

        [TestMethod]
        public void Helpers_GetPermutations_AllUnique()
        {
            var result = testList.GetPermutations().ToList();

            for (int current = 0; current < result.Count; current++)
            {
                for (int compare = current + 1; compare < result.Count; compare++)
                {
                    Assert.AreEqual(result[current].Count, result[compare].Count, "All lists should have the same number of elements");
                    CheckOrderingIsDifferent(result[current], result[compare]);
                }
            }
        }

        private void CheckOrderingIsDifferent<T>(IList<T> first, IList<T> second)
        {
            bool differenceDetected = false;
            for (int i = 0; i < first.Count; i++)
            {
                if (first[i].Equals(second[i]))
                {
                    differenceDetected = true;
                    break;
                }
            }

            Assert.IsTrue(differenceDetected, "No difference was found");
        }

        [TestMethod]
        public void Helpers_ToDictionary_ReturnsDictionary()
        {
            var pairs = GetEnumKVPairs();
            var result = pairs.ToDictionary();

            Assert.AreEqual(pairs.Count(), result.Count);

            foreach (var pair in pairs)
            {
                Assert.IsTrue(result.Contains(pair));
            }
        }

        private IEnumerable<KeyValuePair<Participant, Participant>> GetEnumKVPairs()
        {
            for (int i = 0; i < testList.Count; i++)
            {
                if (i < testList.Count - 1)
                {
                    yield return new KeyValuePair<Participant, Participant>(testList[i], testList[i + 1]);
                }
                else
                {
                    yield return new KeyValuePair<Participant, Participant>(testList[i], testList[0]);
                }
            }
        }

        [TestMethod]
        public void Helpers_ZipToKV_ReturnsValidZip()
        {
            var numberList = new List<int>() { 1, 2, 3, 4, 5 };
            var result = numberList.ZipToKV(numberList);

            Assert.AreEqual(numberList.Count, result.Count(), "Zipped list should eb same length");

            foreach (var pair in result)
            {
                Assert.AreEqual(pair.Key, pair.Value, "Values did not match");
            }
        }
    }
}
