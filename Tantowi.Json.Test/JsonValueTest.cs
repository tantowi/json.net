using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Tantowi.Json;

namespace Tantowi.Json.Test
{

    [TestFixture]
    class JsonValueTest
    {

        [SetUp]
        protected void SetUp()
        {
        }


        [Test]
        public void IsStringTest1()
        {
            JsonValue j = JsonValue.Of("TANTOWI\nMUSTOFA");
            Assert.IsTrue(j.IsString());
        }

        [Test]
        public void IsStringTest2()
        {
            JsonValue j = JsonValue.Of(1234567);
            Assert.IsFalse(j.IsString());
        }

    }
}
