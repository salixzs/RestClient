using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using Salix.RestClient;
using Xunit;

namespace RestClient.Tests
{
    [ExcludeFromCodeCoverage]
    public class QueryParametersTests
    {
        [Fact]
        public void Add_Integer_IsCorrect()
        {
            var testable = new QueryParameters
            {
                { "Identifier", 1001 }
            };
            _ = testable.ToString().Should().Be("Identifier=1001");
        }

        [Fact]
        public void OneValue_Constructor_IsAdded()
        {
            var testable = new QueryParameters("Identifier", 1001);
            _ = testable.ToString().Should().Be("Identifier=1001");
        }

        [Fact]
        public void Add_IntegerParameter_IsCorrect()
        {
            var testable = new QueryParameters
            {
                new QueryParameter("Identifier", 1001)
            };
            _ = testable.ToString().Should().Be("Identifier=1001");
        }

        [Fact]
        public void Add_EncodedString_IsCorrect()
        {
            var testable = new QueryParameters
            {
                { "Label", "P--Ć--П--@" }
            };
            _ = testable.ToString().Should().Be("Label=P--%C4%86--%D0%9F--%40");
        }

        [Fact]
        public void Add_EncodedString_NoEncoding()
        {
            var testable = new QueryParameters
            {
                { "Label", "PĆK@", true }
            };
            _ = testable.ToString().Should().Be("Label=PĆK@");
        }

        [Fact]
        public void Add_MultiListValues_AreProcessed()
        {
            var testable = new QueryParameters
            {
                { "Label", new List<string> { "This", "is", "Cool" } }
            };
            _ = testable.ToString().Should().Be("Label=This&Label=is&Label=Cool");
        }

        [Fact]
        public void Add_MultiArrayValues_AreProcessed()
        {
            var testable = new QueryParameters
            {
                { "Id", new int[3] { 7, 21, 33 } }
            };
            _ = testable.ToString().Should().Be("Id=7&Id=21&Id=33");
        }

        [Fact]
        public void MultiKeys_InList_AreProcessed()
        {
            var testable = new QueryParameters
            {
                { "Label", "MUU" },
                { "Desc", "What" },
                { "Misc", "Yesss" },
                { "ID", 11 },
                { "Desc", "Happens" },
            };
            _ = testable.ToString().Should().Be("Label=MUU&Desc=What&Misc=Yesss&ID=11&Desc=Happens");
        }

        [Fact]
        public void MultiKeys_AsList_AreProcessed()
        {
            var testable = new QueryParameters
            {
                { "Label", "MUU" },
                { "Desc", "What" },
                { "Misc", "Yesss" },
                { "IDs", new List<int> { 11, 17, 21 } }
            };

            _ = testable.ToString().Should().Be("Label=MUU&Desc=What&Misc=Yesss&IDs=11&IDs=17&IDs=21");
        }

        [Fact]
        public void ContainsKey_ExistsInList_IsTrue()
        {
            var testable = new QueryParameters
            {
                { "Label", "MUU" },
                { "Desc", "What" },
                { "Misc", "Yesss" },
                { "ID", 11 },
            };
            _ = testable.ToString().Should().Be("Label=MUU&Desc=What&Misc=Yesss&ID=11");
            _ = testable.ContainsKey("Misc").Should().BeTrue();
        }

        [Fact]
        public void ContainsKey_NotExistsInList_IsFalse()
        {
            var testable = new QueryParameters
            {
                { "Label", "MUU" },
                { "Desc", "What" },
                { "Misc", "Yesss" },
                { "ID", 11 },
            };
            _ = testable.ContainsKey("Disc").Should().BeFalse();
        }

        [Fact]
        public void Remove_FirstFromList_IsRemoved()
        {
            var testable = new QueryParameters
            {
                { "Label", "MUU" },
                { "Desc", "What" },
                { "Misc", "Yesss" },
                { "ID", 11 },
            };
            _ = testable.Remove("Label");
            _ = testable.ToString().Should().Be("Desc=What&Misc=Yesss&ID=11");
        }

        [Fact]
        public void Remove_MiddleFromList_IsRemoved()
        {
            var testable = new QueryParameters
            {
                { "Label", "MUU" },
                { "Desc", "What" },
                { "Misc", "Yesss" },
                { "ID", 11 },
            };
            _ = testable.Remove("Misc");
            _ = testable.ToString().Should().Be("Label=MUU&Desc=What&ID=11");
        }

        [Fact]
        public void Remove_LastFromList_IsRemoved()
        {
            var testable = new QueryParameters
            {
                { "Label", "MUU" },
                { "Desc", "What" },
                { "Misc", "Yesss" },
                { "ID", 11 },
            };
            _ = testable.Remove("ID");
            _ = testable.ToString().Should().Be("Label=MUU&Desc=What&Misc=Yesss");
        }

        [Fact]
        public void Get_FromList_ReturnsValue()
        {
            var testable = new QueryParameters
            {
                { "Label", "MUU" },
                { "Desc", "What" },
                { "Misc", "Yesss" },
                { "ID", 11 },
            };
            testable["Misc"].Should().Be("Yesss");
        }

        [Fact]
        public void GetNonExisting_FromList_ReturnsNull()
        {
            var testable = new QueryParameters
            {
                { "Label", "MUU" },
                { "Desc", "What" },
                { "Misc", "Yesss" },
                { "ID", 11 },
            };
            _ = testable["Disc"].Should().BeNull();
        }

        [Fact]
        public void GetMulti_InList_AreReturned()
        {
            var testable = new QueryParameters
            {
                { "Label", "MUU" },
                { "Desc", "What" },
                { "Misc", "Yesss" },
                { "ID", 11 },
                { "Desc", "Happens" },
            };
            var testList = (object[])testable["Desc"];
            _ = testList.Should().HaveCount(2);
            _ = testList[0].Should().Be("What");
            _ = testList[1].Should().Be("Happens");
        }

        [Fact]
        public void Add_ByIndex_IsAdded()
        {
            var testable = new QueryParameters
            {
                { "Label", "MUU" },
                { "Desc", "What" },
                { "Misc", "Yesss" },
                { "ID", 11 },
            };
            testable["Order"] = 3;
            _ = testable.ToString().Should().Be("Label=MUU&Desc=What&Misc=Yesss&ID=11&Order=3");
        }

        [Fact]
        public void AddParam_ByIndex_IsAdded()
        {
            var testable = new QueryParameters
            {
                { "Label", "MUU" },
                { "Desc", "What" },
                { "Misc", "Yesss" },
                { "ID", 11 },
            };
            testable["Order"] = new QueryParameter("Order", 3);
            _ = testable.ToString().Should().Be("Label=MUU&Desc=What&Misc=Yesss&ID=11&Order=3");
        }

        [Fact]
        public void Replace_ByIndex_IsReplaced()
        {
            var testable = new QueryParameters
            {
                { "Label", "MUU" },
                { "Desc", "What" },
                { "Misc", "Yesss" },
                { "ID", 11 },
            };
            testable["Desc"] = "Eat This";
            _ = testable.ToString().Should().Be("Label=MUU&Desc=Eat%20This&Misc=Yesss&ID=11");
        }

        [Fact]
        public void ReplaceParm_ByIndex_IsReplaced()
        {
            var testable = new QueryParameters
            {
                { "Label", "MUU" },
                { "Desc", "What" },
                { "Misc", "Yesss" },
                { "ID", 11 },
            };
            testable["Desc"] = new QueryParameter("Desc", "Eat This");
            _ = testable.ToString().Should().Be("Label=MUU&Desc=Eat%20This&Misc=Yesss&ID=11");
        }

        [Fact]
        public void Replace_ByIndex_NullRemoves()
        {
            var testable = new QueryParameters
            {
                { "Label", "MUU" },
                { "Desc", "What" },
                { "Misc", "Yesss" },
                { "ID", 11 },
            };
            testable["Desc"] = null;
            _ = testable.ToString().Should().Be("Label=MUU&Misc=Yesss&ID=11");
        }

        [Fact]
        public void NullValues_InList_AreIgnored()
        {
            var testable = new QueryParameters
            {
                { "Label", "MUU" },
                { "FirstId", null },
                { "SecondId", null },
            };
            _ = testable.ToString().Should().Be("Label=MUU&FirstId=&SecondId=");
        }

        [Fact]
        public void NullValues_Anonymous_AreIgnored()
        {
            var testable = new QueryParameters(new { label = "MUU", firstId = (int?)null, secondId = (int?)null, });
            _ = testable.ToString().Should().Be("label=MUU&firstId=&secondId=");
        }
    }
}
