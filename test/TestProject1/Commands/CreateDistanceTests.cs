using FluentAssertions;
using Interview.Application.Distances.Command;
using Interview.Domain.Entities;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IntegrationTest.Commands
{
    using static Testing;
    public class CreateDistanceTests:TestBase
    {
        [Test]
        public async Task ShouldCreateDistanceItem()
        {


            var command = new CreateDistanceCommand
            {
                Distance = 135
            };

            var itemId = await SendAsync(command);

            var item = await FindAsync<Distance>(itemId);

            item.Should().NotBeNull();
            item.distance.Should().Be(command.Distance);
            item.CreatedBy.Should().Be("Service");
            item.Created.Should().BeCloseTo(DateTime.Now, TimeSpan.FromMilliseconds(10000));
            item.LastModifiedBy.Should().BeNull();
            item.LastModified.Should().BeNull();
        }
    }
}
