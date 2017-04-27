using System;
using System.Runtime.Serialization;
using Grappachu.Core.Preview.Runtime.Serialization;
using NUnit.Framework;
using SharpTestsEx;

namespace Grappachu.Core.Test.Runtime.Serialization
{
    [TestFixture]
    class BinarySerializerTest
    {
        private BinarySerializer _sut;


        [SetUp]
        public void SetUp()
        {
            _sut = new BinarySerializer();
        }

        [Test]
        public void ShouldSerializeAnObject()
        {
            object data = new SerializableTestClass { Day = DayOfWeek.Friday, Name = "MyTest", Value = 4.5m };
            var expected = new byte[] { 0, 1, 0, 0, 0, 255, 255, 255, 255, 1, 0, 0, 0, 0, 0, 0, 0, 12, 2, 0, 0, 0,
                74, 71, 114, 97, 112, 112, 97, 99, 104, 117, 46, 67, 111, 114, 101, 46, 84, 101, 115, 116, 44, 32,
                86, 101, 114, 115, 105, 111, 110, 61, 49, 46, 48, 46, 48, 46, 48, 44, 32, 67, 117, 108, 116, 117,
                114, 101, 61, 110, 101, 117, 116, 114, 97, 108, 44, 32, 80, 117, 98, 108, 105, 99, 75, 101, 121,
                84, 111, 107, 101, 110, 61, 110, 117, 108, 108, 5, 1, 0, 0, 0, 63, 71, 114, 97, 112, 112, 97, 99,
                104, 117, 46, 67, 111, 114, 101, 46, 84, 101, 115, 116, 46, 82, 117, 110, 116, 105, 109, 101, 46,
                83, 101, 114, 105, 97, 108, 105, 122, 97, 116, 105, 111, 110, 46, 83, 101, 114, 105, 97, 108, 105,
                122, 97, 98, 108, 101, 84, 101, 115, 116, 67, 108, 97, 115, 115, 3, 0, 0, 0, 21, 60, 78, 97, 109,
                101, 62, 107, 95, 95, 66, 97, 99, 107, 105, 110, 103, 70, 105, 101, 108, 100, 22, 60, 86, 97, 108,
                117, 101, 62, 107, 95, 95, 66, 97, 99, 107, 105, 110, 103, 70, 105, 101, 108, 100, 20, 60, 68, 97,
                121, 62, 107, 95, 95, 66, 97, 99, 107, 105, 110, 103, 70, 105, 101, 108, 100, 1, 0, 3, 5, 16, 83,
                121, 115, 116, 101, 109, 46, 68, 97, 121, 79, 102, 87, 101, 101, 107, 2, 0, 0, 0, 6, 3, 0, 0, 0,
                6, 77, 121, 84, 101, 115, 116, 3, 52, 46, 53, 4, 252, 255, 255, 255, 16, 83, 121, 115, 116, 101,
                109, 46, 68, 97, 121, 79, 102, 87, 101, 101, 107, 1, 0, 0, 0, 7, 118, 97, 108, 117, 101, 95, 95,
                0, 8, 5, 0, 0, 0, 11 };

            var res = _sut.Serialize(data);

            res.Should().Have.SameSequenceAs(expected);
        }


        [Test]
        public void ShouldDeserializeAnObject()
        {
            object expected = new SerializableTestClass { Day = DayOfWeek.Friday, Name = "MyTest", Value = 4.5m };
            var data = new byte[] { 0, 1, 0, 0, 0, 255, 255, 255, 255, 1, 0, 0, 0, 0, 0, 0, 0, 12, 2, 0, 0, 0,
                74, 71, 114, 97, 112, 112, 97, 99, 104, 117, 46, 67, 111, 114, 101, 46, 84, 101, 115, 116, 44, 32,
                86, 101, 114, 115, 105, 111, 110, 61, 49, 46, 48, 46, 48, 46, 48, 44, 32, 67, 117, 108, 116, 117,
                114, 101, 61, 110, 101, 117, 116, 114, 97, 108, 44, 32, 80, 117, 98, 108, 105, 99, 75, 101, 121,
                84, 111, 107, 101, 110, 61, 110, 117, 108, 108, 5, 1, 0, 0, 0, 63, 71, 114, 97, 112, 112, 97, 99,
                104, 117, 46, 67, 111, 114, 101, 46, 84, 101, 115, 116, 46, 82, 117, 110, 116, 105, 109, 101, 46,
                83, 101, 114, 105, 97, 108, 105, 122, 97, 116, 105, 111, 110, 46, 83, 101, 114, 105, 97, 108, 105,
                122, 97, 98, 108, 101, 84, 101, 115, 116, 67, 108, 97, 115, 115, 3, 0, 0, 0, 21, 60, 78, 97, 109,
                101, 62, 107, 95, 95, 66, 97, 99, 107, 105, 110, 103, 70, 105, 101, 108, 100, 22, 60, 86, 97, 108,
                117, 101, 62, 107, 95, 95, 66, 97, 99, 107, 105, 110, 103, 70, 105, 101, 108, 100, 20, 60, 68, 97,
                121, 62, 107, 95, 95, 66, 97, 99, 107, 105, 110, 103, 70, 105, 101, 108, 100, 1, 0, 3, 5, 16, 83,
                121, 115, 116, 101, 109, 46, 68, 97, 121, 79, 102, 87, 101, 101, 107, 2, 0, 0, 0, 6, 3, 0, 0, 0,
                6, 77, 121, 84, 101, 115, 116, 3, 52, 46, 53, 4, 252, 255, 255, 255, 16, 83, 121, 115, 116, 101,
                109, 46, 68, 97, 121, 79, 102, 87, 101, 101, 107, 1, 0, 0, 0, 7, 118, 97, 108, 117, 101, 95, 95,
                0, 8, 5, 0, 0, 0, 11 };

            var res = _sut.Deserialize(data);

            res.Should().Be.InstanceOf<SerializableTestClass>();
            ((SerializableTestClass)res).Name.Should().Be.EqualTo(((SerializableTestClass)expected).Name);
            ((SerializableTestClass)res).Value.Should().Be.EqualTo(((SerializableTestClass)expected).Value);
            ((SerializableTestClass)res).Day.Should().Be.EqualTo(((SerializableTestClass)expected).Day);

        }

        [Test]
        public void Should_ThrowExceptionWhenObjectNotSerializable()
        {
            object data = new NonSerializableTestClass { Day = DayOfWeek.Friday, Name = "MyTest", Value = 4.5m };

            Executing.This(() => _sut.Serialize(data)).Should().Throw<SerializationException>();
        }

    }

    [Serializable]
    internal class SerializableTestClass
    {
        public string Name { get; set; }
        public decimal Value { get; set; }
        public DayOfWeek Day { get; set; }
    }


    internal class NonSerializableTestClass
    {
        public string Name { get; set; }
        public decimal Value { get; set; }
        public DayOfWeek Day { get; set; }
    }

}
