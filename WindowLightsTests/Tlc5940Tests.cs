using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using WindowLights;

namespace WindowLightsTests
{
    [TestClass]
    public class Tlc5940Tests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void SetShouldFailOutOfBounds()
        {
            var mockPort = new Mock<ISerialPortWrapper>();
            var tlc = new Tlc5940(mockPort.Object, "COM1", 1);
            tlc.Set(32, 32);
        }

        [TestMethod]
        public void SetShouldWorkByteAligned()
        {
            var mockPort = new Mock<ISerialPortWrapper>();
            var tlc = new Tlc5940(mockPort.Object, "COM1", 1);
            tlc.Set(15, 0xFF);
            Assert.AreEqual(0xFF, tlc.TlcGsData[0]);
        }

        [TestMethod]
        public void SetShouldWorkNotByteAligned()
        {
            var mockPort = new Mock<ISerialPortWrapper>();
            var tlc = new Tlc5940(mockPort.Object, "COM1", 1);
            tlc.Set(14, 0xFF);
            Assert.AreEqual(0x0F, tlc.TlcGsData[1]);
            Assert.AreEqual(0xF0, tlc.TlcGsData[2]);
        }

        [TestMethod]
        public void SetAllShouldWork()
        {
            var mockPort = new Mock<ISerialPortWrapper>();
            var tlc = new Tlc5940(mockPort.Object, "COM1", 1);
            tlc.SetAll(0xFF);
            CollectionAssert.AreEqual(new byte[] { 0xFF, 0x0F, 0xF0, 
                                         0xFF, 0x0F, 0xF0, 
                                         0xFF, 0x0F, 0xF0, 
                                         0xFF, 0x0F, 0xF0, 
                                         0xFF, 0x0F, 0xF0, 
                                         0xFF, 0x0F, 0xF0, 
                                         0xFF, 0x0F, 0xF0, 
                                         0xFF, 0x0F, 0xF0}, tlc.TlcGsData);
        }

        [TestMethod]
        public void ClearShouldWork()
        {
            var mockPort = new Mock<ISerialPortWrapper>();
            var tlc = new Tlc5940(mockPort.Object, "COM1", 1);
            tlc.SetAll(0xFF);
            tlc.Clear();
            Assert.IsTrue(tlc.TlcGsData.All(i => i == 0));
        }

        [TestMethod]
        public void WriteShouldWork()
        {
            var mockPort = new Mock<ISerialPortWrapper>(MockBehavior.Strict);
            mockPort.Setup(m => m.Open("COM1"));
            mockPort.Setup(m => m.Write(It.IsAny<byte[]>(), 0, It.IsAny<int>()))
                    .Callback<byte[], int, int>((bytes, offset, length) => Assert.AreEqual(bytes.Length, length));
            mockPort.Setup(m => m.Close());
            mockPort.Setup(m => m.Dispose());

            using (var tlc = new Tlc5940(mockPort.Object, "COM1", 1))
            {
                tlc.SetAll(0xFF);
                tlc.Update();
            }

            mockPort.VerifyAll();
        }
    }
}
