using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using PudelkoLibrary;
using PudelkoApp;

namespace PudelkoUnitTests
{

    [TestClass]
    public static class InitializeCulture
    {
        [AssemblyInitialize]
        public static void SetEnglishCultureOnAllUnitTest(TestContext context)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");
        }
    }

    // ========================================

    [TestClass]
    public class UnitTestsPudelkoConstructors
    {
        private static double defaultSize = 0.1; // w metrach
        private static double accuracy = 0.001; //dokładność 3 miejsca po przecinku

        private void AssertPudelko(Pudelko p, double expectedA, double expectedB, double expectedC)
        {
            Assert.AreEqual(expectedA, p.A, delta: accuracy);
            Assert.AreEqual(expectedB, p.B, delta: accuracy);
            Assert.AreEqual(expectedC, p.C, delta: accuracy);
        }

        #region Constructor tests ================================

        [TestMethod, TestCategory("Constructors")]
        public void Constructor_Default()
        {
            Pudelko p = new Pudelko();

            Assert.AreEqual(defaultSize, p.A, delta: accuracy);
            Assert.AreEqual(defaultSize, p.B, delta: accuracy);
            Assert.AreEqual(defaultSize, p.C, delta: accuracy);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow(1.0, 2.543, 3.1,
                 1.0, 2.543, 3.1)]
        [DataRow(1.0001, 2.54387, 3.1005,
                 1.0, 2.543, 3.1)] // dla metrów liczą się 3 miejsca po przecinku
        public void Constructor_3params_DefaultMeters(double a, double b, double c,
                                                      double expectedA, double expectedB, double expectedC)
        {
            Pudelko p = new Pudelko(a, b, c);

            AssertPudelko(p, expectedA, expectedB, expectedC);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow(1.0, 2.543, 3.1,
                 1.0, 2.543, 3.1)]
        [DataRow(1.0001, 2.54387, 3.1005,
                 1.0, 2.543, 3.1)] // dla metrów liczą się 3 miejsca po przecinku
        public void Constructor_3params_InMeters(double a, double b, double c,
                                                      double expectedA, double expectedB, double expectedC)
        {
            Pudelko p = new Pudelko(a, b, c, unit: UnitOfMeasure.meter);

            AssertPudelko(p, expectedA, expectedB, expectedC);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow(100.0, 25.5, 3.1,
                 1.0, 0.255, 0.031)]
        [DataRow(100.0, 25.58, 3.13,
                 1.0, 0.255, 0.031)] // dla centymertów liczy się tylko 1 miejsce po przecinku
        public void Constructor_3params_InCentimeters(double a, double b, double c,
                                                      double expectedA, double expectedB, double expectedC)
        {
            Pudelko p = new Pudelko(a: a, b: b, c: c, unit: UnitOfMeasure.centimeter);

            AssertPudelko(p, expectedA, expectedB, expectedC);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow(100, 255, 3,
                 0.1, 0.255, 0.003)]
        [DataRow(100.0, 25.58, 3.13,
                 0.1, 0.025, 0.003)] // dla milimetrów nie liczą się miejsca po przecinku
        public void Constructor_3params_InMilimeters(double a, double b, double c,
                                                     double expectedA, double expectedB, double expectedC)
        {
            Pudelko p = new Pudelko(unit: UnitOfMeasure.milimeter, a: a, b: b, c: c);

            AssertPudelko(p, expectedA, expectedB, expectedC);
        }


        // ----

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow(1.0, 2.5, 1.0, 2.5)]
        [DataRow(1.001, 2.599, 1.001, 2.599)]
        [DataRow(1.0019, 2.5999, 1.001, 2.599)]
        public void Constructor_2params_DefaultMeters(double a, double b, double expectedA, double expectedB)
        {
            Pudelko p = new Pudelko(a, b);

            AssertPudelko(p, expectedA, expectedB, expectedC: 0.1);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow(1.0, 2.5, 1.0, 2.5)]
        [DataRow(1.001, 2.599, 1.001, 2.599)]
        [DataRow(1.0019, 2.5999, 1.001, 2.599)]
        public void Constructor_2params_InMeters(double a, double b, double expectedA, double expectedB)
        {
            Pudelko p = new Pudelko(a: a, b: b, unit: UnitOfMeasure.meter);

            AssertPudelko(p, expectedA, expectedB, expectedC: 0.1);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow(11.0, 2.5, 0.11, 0.025)]
        [DataRow(100.1, 2.599, 1.001, 0.025)]
        [DataRow(2.0019, 0.25999, 0.02, 0.002)]
        public void Constructor_2params_InCentimeters(double a, double b, double expectedA, double expectedB)
        {
            Pudelko p = new Pudelko(unit: UnitOfMeasure.centimeter, a: a, b: b);

            AssertPudelko(p, expectedA, expectedB, expectedC: 0.1);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow(11, 2.0, 0.011, 0.002)]
        [DataRow(100.1, 2599, 0.1, 2.599)]
        [DataRow(200.19, 2.5999, 0.2, 0.002)]
        public void Constructor_2params_InMilimeters(double a, double b, double expectedA, double expectedB)
        {
            Pudelko p = new Pudelko(unit: UnitOfMeasure.milimeter, a: a, b: b);

            AssertPudelko(p, expectedA, expectedB, expectedC: 0.1);
        }

        // -------

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow(2.5)]
        public void Constructor_1param_DefaultMeters(double a)
        {
            Pudelko p = new Pudelko(a);

            Assert.AreEqual(a, p.A);
            Assert.AreEqual(0.1, p.B);
            Assert.AreEqual(0.1, p.C);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow(2.5)]
        public void Constructor_1param_InMeters(double a)
        {
            Pudelko p = new Pudelko(a);

            Assert.AreEqual(a, p.A);
            Assert.AreEqual(0.1, p.B);
            Assert.AreEqual(0.1, p.C);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow(11.0, 0.11)]
        [DataRow(100.1, 1.001)]
        [DataRow(2.0019, 0.02)]
        public void Constructor_1param_InCentimeters(double a, double expectedA)
        {
            Pudelko p = new Pudelko(unit: UnitOfMeasure.centimeter, a: a);

            AssertPudelko(p, expectedA, expectedB: 0.1, expectedC: 0.1);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow(11, 0.011)]
        [DataRow(100.1, 0.1)]
        [DataRow(200.19, 0.2)]
        public void Constructor_1param_InMilimeters(double a, double expectedA)
        {
            Pudelko p = new Pudelko(unit: UnitOfMeasure.milimeter, a: a);

            AssertPudelko(p, expectedA, expectedB: 0.1, expectedC: 0.1);
        }

        // ---

        public static IEnumerable<object[]> DataSet1Meters_ArgumentOutOfRangeEx => new List<object[]>
        {
            new object[] {-1.0, 2.5, 3.1},
            new object[] {1.0, -2.5, 3.1},
            new object[] {1.0, 2.5, -3.1},
            new object[] {-1.0, -2.5, 3.1},
            new object[] {-1.0, 2.5, -3.1},
            new object[] {1.0, -2.5, -3.1},
            new object[] {-1.0, -2.5, -3.1},
            new object[] {0, 2.5, 3.1},
            new object[] {1.0, 0, 3.1},
            new object[] {1.0, 2.5, 0},
            new object[] {1.0, 0, 0},
            new object[] {0, 2.5, 0},
            new object[] {0, 0, 3.1},
            new object[] {0, 0, 0},
            new object[] {10.1, 2.5, 3.1},
            new object[] {10, 10.1, 3.1},
            new object[] {10, 10, 10.1},
            new object[] {10.1, 10.1, 3.1},
            new object[] {10.1, 10, 10.1},
            new object[] {10, 10.1, 10.1},
            new object[] {10.1, 10.1, 10.1}
        };

        [DataTestMethod, TestCategory("Constructors")]
        [DynamicData(nameof(DataSet1Meters_ArgumentOutOfRangeEx))]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Constructor_3params_DefaultMeters_ArgumentOutOfRangeException(double a, double b, double c)
        {
            Pudelko p = new Pudelko(a, b, c);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DynamicData(nameof(DataSet1Meters_ArgumentOutOfRangeEx))]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Constructor_3params_InMeters_ArgumentOutOfRangeException(double a, double b, double c)
        {
            Pudelko p = new Pudelko(a, b, c, unit: UnitOfMeasure.meter);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow(-1, 1, 1)]
        [DataRow(1, -1, 1)]
        [DataRow(1, 1, -1)]
        [DataRow(-1, -1, 1)]
        [DataRow(-1, 1, -1)]
        [DataRow(1, -1, -1)]
        [DataRow(-1, -1, -1)]
        [DataRow(0, 1, 1)]
        [DataRow(1, 0, 1)]
        [DataRow(1, 1, 0)]
        [DataRow(0, 0, 1)]
        [DataRow(0, 1, 0)]
        [DataRow(1, 0, 0)]
        [DataRow(0, 0, 0)]
        [DataRow(0.01, 0.1, 1)]
        [DataRow(0.1, 0.01, 1)]
        [DataRow(0.1, 0.1, 0.01)]
        [DataRow(1001, 1, 1)]
        [DataRow(1, 1001, 1)]
        [DataRow(1, 1, 1001)]
        [DataRow(1001, 1, 1001)]
        [DataRow(1, 1001, 1001)]
        [DataRow(1001, 1001, 1)]
        [DataRow(1001, 1001, 1001)]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Constructor_3params_InCentimeters_ArgumentOutOfRangeException(double a, double b, double c)
        {
            Pudelko p = new Pudelko(a, b, c, unit: UnitOfMeasure.centimeter);
        }


        [DataTestMethod, TestCategory("Constructors")]
        [DataRow(-1, 1, 1)]
        [DataRow(1, -1, 1)]
        [DataRow(1, 1, -1)]
        [DataRow(-1, -1, 1)]
        [DataRow(-1, 1, -1)]
        [DataRow(1, -1, -1)]
        [DataRow(-1, -1, -1)]
        [DataRow(0, 1, 1)]
        [DataRow(1, 0, 1)]
        [DataRow(1, 1, 0)]
        [DataRow(0, 0, 1)]
        [DataRow(0, 1, 0)]
        [DataRow(1, 0, 0)]
        [DataRow(0, 0, 0)]
        [DataRow(0.1, 1, 1)]
        [DataRow(1, 0.1, 1)]
        [DataRow(1, 1, 0.1)]
        [DataRow(10001, 1, 1)]
        [DataRow(1, 10001, 1)]
        [DataRow(1, 1, 10001)]
        [DataRow(10001, 10001, 1)]
        [DataRow(10001, 1, 10001)]
        [DataRow(1, 10001, 10001)]
        [DataRow(10001, 10001, 10001)]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Constructor_3params_InMiliimeters_ArgumentOutOfRangeException(double a, double b, double c)
        {
            Pudelko p = new Pudelko(a, b, c, unit: UnitOfMeasure.milimeter);
        }


        public static IEnumerable<object[]> DataSet2Meters_ArgumentOutOfRangeEx => new List<object[]>
        {
            new object[] {-1.0, 2.5},
            new object[] {1.0, -2.5},
            new object[] {-1.0, -2.5},
            new object[] {0, 2.5},
            new object[] {1.0, 0},
            new object[] {0, 0},
            new object[] {10.1, 10},
            new object[] {10, 10.1},
            new object[] {10.1, 10.1}
        };

        [DataTestMethod, TestCategory("Constructors")]
        [DynamicData(nameof(DataSet2Meters_ArgumentOutOfRangeEx))]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Constructor_2params_DefaultMeters_ArgumentOutOfRangeException(double a, double b)
        {
            Pudelko p = new Pudelko(a, b);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DynamicData(nameof(DataSet2Meters_ArgumentOutOfRangeEx))]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Constructor_2params_InMeters_ArgumentOutOfRangeException(double a, double b)
        {
            Pudelko p = new Pudelko(a, b, unit: UnitOfMeasure.meter);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow(-1, 1)]
        [DataRow(1, -1)]
        [DataRow(-1, -1)]
        [DataRow(0, 1)]
        [DataRow(1, 0)]
        [DataRow(0, 0)]
        [DataRow(0.01, 1)]
        [DataRow(1, 0.01)]
        [DataRow(0.01, 0.01)]
        [DataRow(1001, 1)]
        [DataRow(1, 1001)]
        [DataRow(1001, 1001)]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Constructor_2params_InCentimeters_ArgumentOutOfRangeException(double a, double b)
        {
            Pudelko p = new Pudelko(a, b, unit: UnitOfMeasure.centimeter);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow(-1, 1)]
        [DataRow(1, -1)]
        [DataRow(-1, -1)]
        [DataRow(0, 1)]
        [DataRow(1, 0)]
        [DataRow(0, 0)]
        [DataRow(0.1, 1)]
        [DataRow(1, 0.1)]
        [DataRow(0.1, 0.1)]
        [DataRow(10001, 1)]
        [DataRow(1, 10001)]
        [DataRow(10001, 10001)]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Constructor_2params_InMilimeters_ArgumentOutOfRangeException(double a, double b)
        {
            Pudelko p = new Pudelko(a, b, unit: UnitOfMeasure.milimeter);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow(-1.0)]
        [DataRow(0)]
        [DataRow(10.1)]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Constructor_1param_DefaultMeters_ArgumentOutOfRangeException(double a)
        {
            Pudelko p = new Pudelko(a);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow(-1.0)]
        [DataRow(0)]
        [DataRow(10.1)]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Constructor_1param_InMeters_ArgumentOutOfRangeException(double a)
        {
            Pudelko _p = new Pudelko(a, unit: UnitOfMeasure.meter);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow(-1.0)]
        [DataRow(0)]
        [DataRow(0.01)]
        [DataRow(1001)]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Constructor_1param_InCentimeters_ArgumentOutOfRangeException(double a)
        {
            Pudelko p = new Pudelko(a, unit: UnitOfMeasure.centimeter);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow(-1)]
        [DataRow(0)]
        [DataRow(0.1)]
        [DataRow(10001)]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Constructor_1param_InMilimeters_ArgumentOutOfRangeException(double a)
        {
            Pudelko p = new Pudelko(a, unit: UnitOfMeasure.milimeter);
        }

        #endregion

        #region ToString tests ===================================

        [TestMethod, TestCategory("String representation")]
        public void ToString_Default_Culture_EN()
        {
            var p = new Pudelko(2.5, 9.321);
            string expectedStringEN = "2.500 m × 9.321 m × 0.100 m";

            Assert.AreEqual(expectedStringEN, p.ToString());
        }

        [DataTestMethod, TestCategory("String representation")]
        [DataRow(null, 2.5, 9.321, 0.1, "2.500 m × 9.321 m × 0.100 m")]
        [DataRow("m", 2.5, 9.321, 0.1, "2.500 m × 9.321 m × 0.100 m")]
        [DataRow("cm", 2.5, 9.321, 0.1, "250.0 cm × 932.1 cm × 10.0 cm")]
        [DataRow("mm", 2.5, 9.321, 0.1, "2500 mm × 9321 mm × 100 mm")]
        public void ToString_Formattable_Culture_EN(string format, double a, double b, double c, string expectedStringRepresentation)
        {
            var p = new Pudelko(a, b, c, unit: UnitOfMeasure.meter);
            Assert.AreEqual(expectedStringRepresentation, p.ToString(format));
        }

        [TestMethod, TestCategory("String representation")]
        [ExpectedException(typeof(FormatException))]
        public void ToString_Formattable_WrongFormat_FormatException()
        {
            var p = new Pudelko(1);
            var stringformatedrepreentation = p.ToString("wrong code");
        }

        #endregion

        #region Pole, Objętość ===================================

        [DataTestMethod, TestCategory("Objetosc")]
        [DataRow(2.5, 9.321, 0.1, 2.33025)]
        [DataRow(1, 9.1, 4, 36.4)]
        [DataRow(0.1, 0.1, 0.1, 0.001)]
        [DataRow(1, 1, 1, 1)]

        public void Volume_Meter_Test(double a, double b, double c, double volumeResult)
        {
            var p = new Pudelko(a, b, c, unit: UnitOfMeasure.meter);
            Assert.AreEqual(volumeResult, p.Objetosc);
        }

        [DataTestMethod, TestCategory("Objetosc")]
        [DataRow(250, 932.1, 10, 2.33025)]
        [DataRow(100, 910, 400, 36.4)]
        [DataRow(10, 10, 10, 0.001)]
        [DataRow(250.5, 92.1, 10, 0.23046)]

        public void Volume_Centimeter_Test(double a, double b, double c, double volumeResult)
        {
            var p = new Pudelko(a, b, c, unit: UnitOfMeasure.centimeter);
            Assert.AreEqual(volumeResult, p.Objetosc);
        }

        [DataTestMethod, TestCategory("Objetosc")]
        [DataRow(100, 255, 3, 0.0000765)]
        [DataRow(100.0, 25.58, 3.13, 0.0000075)]

        public void Volume_Milimeter_Test(double a, double b, double c, double volumeResult)
        {
            var p = new Pudelko(a, b, c, unit: UnitOfMeasure.milimeter);
            Assert.AreEqual(volumeResult, p.Objetosc);
        }


        [DataTestMethod, TestCategory("Pole")]
        [DataRow(2.5, 9.321, 0.1, 48.9692)]
        [DataRow(1, 9.1, 4, 99)]
        [DataRow(0.1, 0.1, 0.1, 0.06)]
        [DataRow(1, 1, 1, 6)]

        public void Area_Meter_Test(double a, double b, double c, double areaResult)
        {
            var p = new Pudelko(a, b, c, unit: UnitOfMeasure.meter);
            Assert.AreEqual(areaResult, p.Pole);
        }

        [DataTestMethod, TestCategory("Pole")]
        [DataRow(250, 932.1, 10, 48.9692)]
        [DataRow(100, 910, 400, 99)]
        [DataRow(10, 10, 10, 0.06)]
        [DataRow(250.5, 92.1, 10, 5.2942)]

        public void Area_Centimeter_Test(double a, double b, double c, double areaResult)
        {
            var p = new Pudelko(a, b, c, unit: UnitOfMeasure.centimeter);
            Assert.AreEqual(areaResult, p.Pole);
        }

        [DataTestMethod, TestCategory("Pole")]
        [DataRow(100, 255, 3, 0.05313)]
        [DataRow(100.0, 25.58, 3.13, 0.00575)]

        public void Area_Milimeter_Test(double a, double b, double c, double areaResult)
        {
            var p = new Pudelko(a, b, c, unit: UnitOfMeasure.milimeter);
            Assert.AreEqual(areaResult, p.Pole);
        }

        #endregion

        #region Equals ===========================================

        [DataTestMethod, TestCategory("Equals")]
        [DataRow(1.0, 2.543, 3.1,
                 2.543, 1.0, 3.1, true)]
        [DataRow(1.0001, 2.54387, 3.1005,
                 1.0, 2.541, 3.1, false)]
        [DataRow(10, 2.54387, 3.1005,
                 2.54387, 3.1005, 10, true)]
        [DataRow(1.0001, 2.54387, 3.1005,
                 1.0, 2.541, 3.1, false)]
        public void Equals_Meters(double a, double b, double c,
                                                      double a2, double b2, double c2, bool expected)
        {
            Pudelko p1 = new Pudelko(a, b, c, unit: UnitOfMeasure.meter);
            Pudelko p2 = new Pudelko(a2, b2, c2, unit: UnitOfMeasure.meter);

            Assert.AreEqual(expected, p1.Equals(p2));
        }

        [DataTestMethod, TestCategory("Equals")]
        [DataRow(1.0, 2.543, 3.1,
                 254.3, 100, 310, true)]
        [DataRow(1.0001, 2.54387, 3.1005,
                 100, 254.1, 310, false)]
        [DataRow(10, 2.54387, 3.1005,
                 254.387, 310.05, 1000, true)]
        [DataRow(1.0001, 2.54387, 3.1005,
                 1000, 254.1, 310, false)]
        public void Equals_Meters_And_Centimeters(double a, double b, double c,
                                                      double a2, double b2, double c2, bool expected)
        {
            Pudelko p1 = new Pudelko(a, b, c, unit: UnitOfMeasure.meter);
            Pudelko p2 = new Pudelko(a2, b2, c2, unit: UnitOfMeasure.centimeter);

            Assert.AreEqual(expected, p1.Equals(p2));
        }

        [DataTestMethod, TestCategory("Equals")]
        [DataRow(1.0, 2.543, 3.1,
                 2543, 1000, 3100, true)]
        [DataRow(1.0001, 2.54387, 3.1005,
                 1000, 2541, 3100, false)]
        [DataRow(10, 2.54387, 3.1005,
                 2543.87, 3100.5, 10000, true)]
        [DataRow(1.0001, 2.54387, 3.1005,
                 10000, 2541, 3100, false)]
        public void Equals_Meters_And_Milimeters(double a, double b, double c,
                                                      double a2, double b2, double c2, bool expected)
        {
            Pudelko p1 = new Pudelko(a, b, c, unit: UnitOfMeasure.meter);
            Pudelko p2 = new Pudelko(a2, b2, c2, unit: UnitOfMeasure.milimeter);

            Assert.AreEqual(expected, p1.Equals(p2));
        }


        #endregion

        #region Operators overloading ===========================

        [DataTestMethod, TestCategory("EqualsOperator")]
        [DataRow(1.0, 2.543, 3.1,
                 2.543, 1.0, 3.1, true)]
        [DataRow(1.0001, 2.54387, 3.1005,
                 1.0, 2.541, 3.1, false)]
        [DataRow(10, 2.54387, 3.1005,
                 2.54387, 3.1005, 10, true)]
        [DataRow(1.0001, 2.54387, 3.1005,
                 1.0, 2.541, 3.1, false)]
        public void EqualsOperator_Meters(double a, double b, double c,
                                                      double a2, double b2, double c2, bool expected)
        {
            Pudelko p1 = new Pudelko(a, b, c, unit: UnitOfMeasure.meter);
            Pudelko p2 = new Pudelko(a2, b2, c2, unit: UnitOfMeasure.meter);

            Assert.AreEqual(expected, p1 == p2);
        }

        [DataTestMethod, TestCategory("Equals")]
        [DataRow(1.0, 2.543, 3.1,
                 254.3, 100, 310, true)]
        [DataRow(1.0001, 2.54387, 3.1005,
                 100, 254.1, 310, false)]
        [DataRow(10, 2.54387, 3.1005,
                 254.387, 310.05, 1000, true)]
        [DataRow(1.0001, 2.54387, 3.1005,
                 1000, 254.1, 310, false)]
        public void EqualsOperator_Meters_And_Centimeters(double a, double b, double c,
                                                      double a2, double b2, double c2, bool expected)
        {
            Pudelko p1 = new Pudelko(a, b, c, unit: UnitOfMeasure.meter);
            Pudelko p2 = new Pudelko(a2, b2, c2, unit: UnitOfMeasure.centimeter);

            Assert.AreEqual(expected, p1 == p2);
        }

        [DataTestMethod, TestCategory("Equals")]
        [DataRow(1.0, 2.543, 3.1,
                 2543, 1000, 3100, true)]
        [DataRow(1.0001, 2.54387, 3.1005,
                 1000, 2541, 3100, false)]
        [DataRow(10, 2.54387, 3.1005,
                 2543.87, 3100.5, 10000, true)]
        [DataRow(1.0001, 2.54387, 3.1005,
                 10000, 2541, 3100, false)]
        public void EqualsOperator_Meters_And_Milimeters(double a, double b, double c,
                                                      double a2, double b2, double c2, bool expected)
        {
            Pudelko p1 = new Pudelko(a, b, c, unit: UnitOfMeasure.meter);
            Pudelko p2 = new Pudelko(a2, b2, c2, unit: UnitOfMeasure.milimeter);

            Assert.AreEqual(expected, p1 == p2);
        }

        [DataTestMethod, TestCategory("AddOperator")]
        [DataRow(1.0, 2.543, 3.1,
                 2.543, 1.0, 3.1,
                 3.1, 3.1, 2.543)]
        [DataRow(1.0001, 2.54387, 3.1005,
                 1.0, 2.541, 3.1,
                 2.54387, 3.1005, 3.1)]
        [DataRow(10, 2.54387, 3.1005,
                 2.54387, 3.1005, 10,
                10, 10, 3.1005)]
        [DataRow(1.0001, 2.54387, 3.1005,
                 1.0, 2.541, 3.1,
                 3.1005, 2.54387, 3.1)]
        public void AddOperator_Meters(double a, double b, double c,
                                                      double a2, double b2, double c2,
                                                      double addedA, double addedB, double addedC
                                                      )
        {
            Pudelko p1 = new Pudelko(a, b, c, unit: UnitOfMeasure.meter);
            Pudelko p2 = new Pudelko(a2, b2, c2, unit: UnitOfMeasure.meter);

            Pudelko expected = new Pudelko(addedA, addedB, addedC);


            Assert.AreEqual(true, expected.Equals(p1 + p2));
        }

        [DataTestMethod, TestCategory("AddOperator")]
        [DataRow(1.0, 2.543, 3.1,
                 254.3, 100, 310,
                 3.1, 3.1, 2.543)]
        [DataRow(1.0001, 2.54387, 3.1005,
                 100, 254.1, 310,
                 2.54387, 3.1005, 3.1)]
        [DataRow(10, 2.54387, 3.1005,
                 254.387, 310.05, 1000,
                10, 10, 3.1005)]
        [DataRow(1.0001, 2.54387, 3.1005,
                 100, 254.1, 310,
                 3.1005, 2.54387, 3.1)]
        public void AddOperator_Meters_And_Centimeters(double a, double b, double c,
                                                      double a2, double b2, double c2,
                                                      double addedA, double addedB, double addedC
                                                      )
        {
            Pudelko p1 = new Pudelko(a, b, c, unit: UnitOfMeasure.meter);
            Pudelko p2 = new Pudelko(a2, b2, c2, unit: UnitOfMeasure.centimeter);

            Pudelko expected = new Pudelko(addedA, addedB, addedC);


            Assert.AreEqual(true, expected.Equals(p1 + p2));
        }


        #endregion

        #region Conversions =====================================

        [TestMethod]
        public void ExplicitConversion_ToDoubleArray_AsMeters()
        {
            var p = new Pudelko(1, 2.1, 3.231);
            double[] tab = (double[])p;
            Assert.AreEqual(3, tab.Length);
            Assert.AreEqual(p.A, tab[0]);
            Assert.AreEqual(p.B, tab[1]);
            Assert.AreEqual(p.C, tab[2]);
        }

        [TestMethod]
        public void ImplicitConversion_FromAalueTuple_As_Pudelko_InMilimeters()
        {
            var (a, b, c) = (2500, 9321, 100); // in milimeters, ValueTuple
            Pudelko p = (a, b, c);
            Assert.AreEqual((int)(p.A * 1000), a);
            Assert.AreEqual((int)(p.B * 1000), b);
            Assert.AreEqual((int)(p.C * 1000), c);
        }

        #endregion

        #region Indexer, enumeration ============================
        [TestMethod]
        public void Indexer_ReadFrom()
        {
            var p = new Pudelko(1, 2.1, 3.231);
            Assert.AreEqual(p.A, p[0]);
            Assert.AreEqual(p.B, p[1]);
            Assert.AreEqual(p.C, p[2]);
        }

        [TestMethod]
        public void ForEach_Test()
        {
            var p = new Pudelko(1, 2.1, 3.231);
            var tab = new[] { p.A, p.B, p.C };
            int i = 0;
            foreach (double x in p)
            {
                Assert.AreEqual(x, tab[i]);
                i++;
            }
        }

        #endregion

        #region Parsing =========================================

        [DataTestMethod, TestCategory("Parse")]
        [DataRow(2.5, 9.321, 0.1,
                 "2.500 m × 9.321 m × 0.100 m",
                 true)]
        [DataRow(2.5, 9.321, 0.1,
                 "2.501 m × 9.321 m × 0.100 m",
                 false)]

        public void Parse_Pudelko_Meters(double a, double b, double c, string stringToParse, bool expected)
        {
            Pudelko p1 = new Pudelko(a, b, c, unit: UnitOfMeasure.meter);


            var result = p1 == p1.Parse(stringToParse);


            Assert.AreEqual(expected, result);
        }

        [DataTestMethod, TestCategory("Parse")]
        [DataRow(250, 932.1, 10,
                 "2.500 m × 9.321 m × 0.100 m",
                 true)]
        [DataRow(250, 932.1, 10,
                 "2.501 m × 9.321 m × 0.100 m",
                 false)]

        public void Parse_Pudelko_Centimeters(double a, double b, double c, string stringToParse, bool expected
                                                      )
        {
            Pudelko p1 = new Pudelko(a, b, c, unit: UnitOfMeasure.centimeter);


            var result = p1 == p1.Parse(stringToParse);


            Assert.AreEqual(expected, result);
        }


        #endregion

        #region Kompresuj =======================================

        [DataTestMethod, TestCategory("Kompresuj")]
        [DataRow(2.5, 9.321, 0.1,
                 1.32576792)]
        [DataRow(10, 10, 10,
                 10)]
        [DataRow(2.137, 7.7, 5.49,
                 4.48699788)]


        public void Kompresuj_Pudelko(double a, double b, double c, double cubicSide)
        {
            var p1 = new Pudelko(a, b, c);
            var cubic = new Pudelko(cubicSide, cubicSide, cubicSide);

            Assert.AreEqual(cubic.Objetosc, p1.Kompresuj().Objetosc);
        }
        #endregion

    }
}
