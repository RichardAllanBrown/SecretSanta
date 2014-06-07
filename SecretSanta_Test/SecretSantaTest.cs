using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using SecretSanta;

namespace SecretSanta_Test
{
    [TestClass]
    public class SecretSantaTest
    {
        IList<Participant> participants;
        IDictionary<Participant, Participant> banned;

        [TestInitialize]
        public void SetUp()
        {
            participants = new List<Participant>()
            {
                new Participant() { FirstName = "A" },
                new Participant() { FirstName = "B" },
                new Participant() { FirstName = "C" },
                new Participant() { FirstName = "D" }
            };

            banned = new Dictionary<Participant, Participant>();
            banned.Add(participants[0], participants[2]);
            banned.Add(participants[1], participants[3]);
        }

        [TestMethod]
        public void SecretSanta_Generate_ReturnsASet()
        {
            var result = SecretSantaGenerator.Generate(participants);

            CheckForValidSantaList(result);
        }

        private void CheckForValidSantaList(IDictionary<Participant, Participant> santaList)
        {
            foreach (var sender in santaList.Keys)
            {
                Assert.IsTrue(participants.Contains(sender), "A participant was not included as a gifter");
            }

            foreach (var reciever in santaList.Values)
            {
                Assert.IsTrue(participants.Contains(reciever), "A participant was not included as a giftee");
            }

            foreach (var pair in santaList)
            {
                Assert.AreNotEqual(pair.Key, pair.Value, "A participant should never have to gift to themselves");
            }
        }

        [TestMethod]
        public void SecretSanta_GernerateAll_ReturnsAllSets()
        {
            foreach (var list in SecretSantaGenerator.GenerateAll(participants))
            {
                CheckForValidSantaList(list);
            }
        }

        [TestMethod]
        public void SecretSanta_Generate_WithBanned_ReturnsASet()
        {
            var result = SecretSantaGenerator.Generate(participants, banned);

            CheckForValidSantaList(result);
            CheckResultHasNoBannedPair(result);
        }

        private void CheckResultHasNoBannedPair(IDictionary<Participant, Participant> result)
        {
            foreach (var bannedPair in banned)
            {
                Assert.IsFalse(result.Contains(bannedPair));
            }
        }

        [TestMethod]
        public void SecretSanta_GenerateAll_WithBanned_ReturnsAllSets()
        {
            var result = SecretSantaGenerator.GenerateAll(participants, banned);

            foreach (var list in result)
            {
                CheckForValidSantaList(list);
                CheckResultHasNoBannedPair(list);
            }
        }
    }
}
