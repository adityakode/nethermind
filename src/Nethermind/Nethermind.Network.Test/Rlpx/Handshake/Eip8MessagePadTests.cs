// SPDX-FileCopyrightText: 2022 Demerzel Solutions Limited
// SPDX-License-Identifier: LGPL-3.0-only

using Nethermind.Network.Rlpx.Handshake;
using NUnit.Framework;

namespace Nethermind.Network.Test.Rlpx.Handshake
{
    [Parallelizable(ParallelScope.Self)]
    [TestFixture]
    public class Eip8MessagePadTests
    {
        [Test]
        public void Adds_at_least_100_bytes()
        {
            byte[] message = { 1 };
            int lengthBeforePadding = message.Length;

            TestRandom testRandom = new(i => 0, i => new byte[i]);

            Eip8MessagePad pad = new(testRandom);
            message = pad.Pad(message);

            Assert.AreEqual(lengthBeforePadding + 100, message.Length, "incorrect length");
            Assert.AreEqual(message[0], 1, "first byte touched");
        }

        [Test]
        public void Adds_at_most_300_bytes()
        {
            byte[] message = { 1 };
            int lengthBeforePadding = message.Length;

            TestRandom testRandom = new(i => i - 1, i => new byte[i]);

            Eip8MessagePad pad = new(testRandom);
            message = pad.Pad(message);

            Assert.AreEqual(lengthBeforePadding + 300, message.Length, "incorrect length");
            Assert.AreEqual(message[0], 1, "first byte touched");
        }
    }
}
