using Microsoft.VisualStudio.TestTools.UnitTesting;
using praticeApp.Domain;
using System;

namespace praticeAppTests
{
    [TestClass]
    public class DateConverterTest
    {
        Notification testNotification;
        DateTime today;

        [TestInitialize]
        public void TestInitialize()
        {
            testNotification = new Notification { ID = 1, Date = DateTime.Parse("2019-05-07 15:42:45"), Subject = "test", Content = "test", Header = "test" };
        }
        [TestMethod]
        public void TestToday()
        {
            today = DateTime.Parse("2019-05-07 16:42:45");
            string expected = "15:42";
            string result = testNotification.convertDate(today);
            Assert.AreEqual(expected, result, "DateTime for current date does not display correctly");
        }

        [TestMethod]
        public void TestYesterday()
        {
            today = DateTime.Parse("2019-05-08 16:42:45");
            string expected = "Yesterday";
            string result = testNotification.convertDate(today);
            Assert.AreEqual(expected, result, "DateTime for yesterday does not display correctly");
        }

        [TestMethod]
        public void TestWeekAgo()
        {
            today = DateTime.Parse("2019-05-15 16:42:45");
            string expected = "07 May";
            string result = testNotification.convertDate(today);
            Assert.AreEqual(expected, result, "DateTime for a week ago does not display correctly");
        }

        [TestMethod]
        public void TestYearAgo()
        {
            today = DateTime.Parse("2020-05-15 16:42:45");
            string expected = "07/05/2019";
            string result = testNotification.convertDate(today);
            Assert.AreEqual(expected, result, "DateTime for a year ago does not display correctly");
        }

    }
}
