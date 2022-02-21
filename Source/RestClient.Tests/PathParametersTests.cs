using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Salix.RestClient;
using Xunit;

namespace RestClient.Tests
{
    public class PathParametersTests
    {
        [Fact]
        public void New_IsEmpty()
        {
            var sut = new PathParameters();
            sut.Count.Should().Be(0);
            sut.IsEmpty.Should().BeTrue();
        }

        [Fact]
        public void New_OneParameter_GetsAdded()
        {
            var sut = new PathParameters("Numero", "Uno");
            sut.Count.Should().Be(1);
            sut.ContainsName("Numero").Should().BeTrue();
            sut["Numero"].Should().Be("Uno");
            sut.IsEmpty.Should().BeFalse();
        }

        [Fact]
        public void New_TwoParameters_GetsAdded()
        {
            var sut = new PathParameters("Numero", "Uno", "Version", 2);
            sut.Count.Should().Be(2);
            sut.ContainsName("Numero").Should().BeTrue();
            sut["Numero"].Should().Be("Uno");
            sut.ContainsName("Version").Should().BeTrue();
            sut["Version"].Should().Be("2");
        }

        [Fact]
        public void New_Dictionary_GetsAdded()
        {
            var sut = new PathParameters(new Dictionary<string, object>
            {
                { "Uno", 1 },
                { "Duo", 2 },
                { "Tres", 3 },
            });
            sut.Count.Should().Be(3);
            sut.ContainsName("Uno").Should().BeTrue();
            sut["Uno"].Should().Be("1");
            sut.ContainsName("Duo").Should().BeTrue();
            sut["Duo"].Should().Be("2");
            sut.ContainsName("Tres").Should().BeTrue();
            sut["Tres"].Should().Be("3");
        }

        [Fact]
        public void New_Anonymous_GetsAdded()
        {
            var sut = new PathParameters(new
            {
                Uno = 1,
                Duo = 2,
                Tres = 3,
            });
            sut.Count.Should().Be(3);
            sut.ContainsName("Uno").Should().BeTrue();
            sut["Uno"].Should().Be("1");
            sut.ContainsName("Duo").Should().BeTrue();
            sut["Duo"].Should().Be("2");
            sut.ContainsName("Tres").Should().BeTrue();
            sut["Tres"].Should().Be("3");
        }

        [Fact]
        public void New_Iterator_ReturnsAll()
        {
            var sut = new PathParameters(new
            {
                Uno = 1,
                Duo = 2,
                Tres = 3,
            });

            var i = 0;
            foreach (var nameValue in sut.GetAll())
            {
                switch (i)
                {
                    case 0:
                        nameValue.Name.Should().Be("Uno");
                        nameValue.Value.Should().Be("1");
                        break;
                    case 1:
                        nameValue.Name.Should().Be("Duo");
                        nameValue.Value.Should().Be("2");
                        break;
                    case 2:
                        nameValue.Name.Should().Be("Tres");
                        nameValue.Value.Should().Be("3");
                        break;
                    default:
                        "No index".Should().Be("Not entered");
                        break;
                }
                i++;
            }
        }

    }
}
