using NUnit.Framework;
using CustomCollections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomCollections;

namespace CustomCollectionsTests
{
    [TestFixture]
    public class QueueTests
    {
        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void QueueConstructorTest_NegativeCapacity_ShouldThrowArgumentException()
        {
            CustomCollections.Queue<int> queue = new CustomCollections.Queue<int>(-1);
        }

        [Test]
        [TestCase(new[] { 2, 4, 245, 6, 9, 234, 8, 52 })]
        public void GetEnumeratorTest_DifferentValues_ShouldReturnCorrectSequence(int[] array)
        {
            CustomCollections.Queue<int> queue = new CustomCollections.Queue<int>(array);
            int[] actual = new int[queue.Size];
            int i = 0;

            foreach (var item in queue)
            {
                actual[i++] = item;
            }

            Assert.AreEqual(array, actual);
        }

        [Test]
        [TestCase(new[] { 2, 4, 245, 6, 9, 234, 8, 52 })]
        public void CopyToArrayTest_DifferentValues_ShouldReturnCorrectSequence(int[] array)
        {
            CustomCollections.Queue<int> queue = new CustomCollections.Queue<int>(array);
            int[] actual = new int[queue.Size];
            queue.CopyTo(actual, 0);

            Assert.AreEqual(array, actual);
        }

        [Test]
        [TestCase(new[] { 3, 4, 5, 68, -4, 6, 2 }, 6, ExpectedResult = true)]
        [TestCase(new[] { 3, 456, 5, 68, -4, 6, 2 }, 457, ExpectedResult = false)]
        public bool ContainsTest_SomeValues_ShouldReturnCorrect(int[] array, int value)
        {
            CustomCollections.Queue<int> queue = new CustomCollections.Queue<int>(array);

            return queue.Contains(value);
        }

        [Test]
        [TestCase(new[] { 1, 2, 3, 4, 5 })]
        public void EnqueueTest_SomeValues_ShouldReturnCorrect(int[] array)
        {
            CustomCollections.Queue<int> queue = new CustomCollections.Queue<int>();
            queue.Enqueue(1);
            queue.Enqueue(1);
            queue.Enqueue(1);
            queue.Enqueue(1);

            queue.Dequeue();
            queue.Dequeue();
            queue.Dequeue();
            queue.Dequeue();

            queue.Enqueue(1);
            queue.Enqueue(2);
            queue.Enqueue(3);
            queue.Enqueue(4);
            queue.Enqueue(5);

            int[] actual = new int[queue.Size];
            queue.CopyTo(actual, 0);

            Assert.AreEqual(array, actual);
        }
    }
}