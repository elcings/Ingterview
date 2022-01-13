using FluentAssertions;
using Interview.Application.Fuel.Command;
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
    public class CreateFuelLevelTests
    {
        [Test]
        public async Task ShouldCreateFuelLevelItem()
        {


            var command = new CreateFuelLevelCommand
            {
                Level = 12
            };

            var itemId = await SendAsync(command);

            var item = await FindAsync<FuelLevel>(itemId);

            item.Should().NotBeNull();
            item.Level.Should().Be(command.Level);
            item.CreatedBy.Should().Be("Service");
            item.Created.Should().BeCloseTo(DateTime.Now, TimeSpan.FromMilliseconds(10000));
            item.LastModifiedBy.Should().BeNull();
            item.LastModified.Should().BeNull();
        }
    }
}
