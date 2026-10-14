using NUnit.Framework;


namespace AD
{
    [TestFixture]
    public partial class MyQueueTests
    {
        [Test]
        public void MyQueue_1_Constructor_1_IsEmptyReturnsTrue()
        {
            // Arrange
            IMyQueue<string> q = DSBuilder.CreateMyQueueStringEmpty();
            bool expected = true;

            // Act
            bool actual = q.IsEmpty();

            // Assert
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void MyQueue_2_Enqueue_1_IsEmptyReturnsFalse()
        {
            // Arrange
            IMyQueue<string> queue = DSBuilder.CreateMyQueueStringEmpty();
            bool expected = false;

            // Act
            queue.Enqueue("a");
            bool actual = queue.IsEmpty();

            // Assert
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void MyQueue_2_Enqueue_2_GetFrontIsOkAfter1Enqueue()
        {
            // Arrange
            IMyQueue<string> queue = DSBuilder.CreateMyQueueStringEmpty();
            string expected = "a";

            // Act
            queue.Enqueue("a");
            string actual = queue.GetFront();

            // Assert
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void MyQueue_2_Enqueue_3_GetFrontIsOkAfter3Enqueue()
        {
            // Arrange
            IMyQueue<string> queue = DSBuilder.CreateMyQueueStringEmpty();
            string expected = "a";

            // Act
            queue.Enqueue("a");
            queue.Enqueue("b");
            queue.Enqueue("c");
            string actual = queue.GetFront();

            // Assert
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void MyQueue_2_Enqueue_4_DequeueIsOkAfter1Enqueue()
        {
            // Arrange
            IMyQueue<string> queue = DSBuilder.CreateMyQueueStringEmpty();
            string expected = "a";

            // Act
            queue.Enqueue("a");
            string actual = queue.Dequeue();

            // Assert
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void MyQueue_2_Enqueue_5_DequeueIsOkAfter3Enqueue()
        {
            // Arrange
            IMyQueue<string> queue = DSBuilder.CreateMyQueueStringEmpty();
            string expected = "a";

            // Act
            queue.Enqueue("a");
            queue.Enqueue("b");
            queue.Enqueue("c");
            string actual = queue.Dequeue();

            // Assert
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void MyQueue_2_Enqueue_6_TwoTimesDequeueIsOkAfter3Enqueue()
        {
            // Arrange
            IMyQueue<string> queue = DSBuilder.CreateMyQueueStringEmpty();
            string expected = "b";

            // Act
            queue.Enqueue("a");
            queue.Enqueue("b");
            queue.Enqueue("c");
            queue.Dequeue();
            string actual = queue.Dequeue();

            // Assert
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void MyQueue_3_GetFront_1_ThrowsExceptionOnEmptyStack()
        {
            // Arrange
            IMyQueue<string> queue = DSBuilder.CreateMyQueueStringEmpty();

            // Act & Assert
            Assert.Throws(typeof(MyQueueEmptyException), () => queue.GetFront());
        }

        [Test]
        public void MyQueue_3_GetFront_2_IsEmptyReturnsFalseAfterGetFrontOnOneElement()
        {
            // Arrange
            IMyQueue<string> queue = DSBuilder.CreateMyQueueStringEmpty();
            bool expected = false;

            // Act
            queue.Enqueue("a");
            queue.GetFront();
            bool actual = queue.IsEmpty();

            // Assert
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void MyQueue_4_Dequeue_1_ThrowsExceptionOnEmptyList()
        {
            // Arrange
            IMyQueue<string> queue = DSBuilder.CreateMyQueueStringEmpty();

            // Act & Assert
            Assert.Throws(typeof(MyQueueEmptyException), () => queue.Dequeue());
        }

        [Test]
        public void MyQueue_4_Dequeue_2_IsEmptyReturnsTrueAfterGetFrontOnOneElement()
        {
            // Arrange
            IMyQueue<string> queue = DSBuilder.CreateMyQueueStringEmpty();
            bool expected = true;

            // Act
            queue.Enqueue("a");
            queue.Dequeue();
            bool actual = queue.IsEmpty();

            // Assert
            Assert.That(actual, Is.EqualTo(expected));
        }
    }
}