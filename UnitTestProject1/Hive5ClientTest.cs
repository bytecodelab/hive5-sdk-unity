using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Hive5;

namespace UnitTestProject1
{
    [TestClass]
    public class Hive5ClientTest
    {
        Hive5Client client = new Hive5Client("a4b9dff4-3a9a-402b-bc2b-00d144ebacad", "0f607264fc6318a92b9e13c65db7cd3c", "e7c30d94-126a-440c-9ea6-40e5ebe37ae5");

        [TestMethod]
        public void TestGetItems()
        {
            var result = client.GetItems(new string[] { "items." });

            Assert.AreNotEqual(result.Count, 0);
       } 
    }
}

