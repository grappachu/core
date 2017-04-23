using System;
using System.Linq;
using System.Runtime.Serialization;
using Grappachu.Core.Runtime.Serialization;
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
            var checksum = 25817 ;

            var res = _sut.Serialize(data);

            res.Sum(x => x).Should().Be.EqualTo(checksum);
        }

        [Test]
        public void ShouldDeserializeAnObject()
        {
            object expected = new SerializableTestClass { Day = DayOfWeek.Friday, Name = "MyTest", Value = 4.5m };
            var data = _sut.Serialize(expected);

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
