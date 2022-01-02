using FluentAssertions;
using Interview.Application.CarError.Command;
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
    public class CreateErrorTests
    {
        [Test]
        public async Task ShouldCreateErrorItem()
        {
            var command = new CreateErrorCommand
            {
                Description = "Error in car"
            };

            var itemId = await SendAsync(command);

            var item = await FindAsync<Error>(itemId);

            item.Should().NotBeNull();
            item.Description.Should().Be(command.Description);
            item.CreatedBy.Should().Be("Service");
            item.Created.Should().BeCloseTo(DateTime.Now, TimeSpan.FromMilliseconds(10000));
            item.LastModifiedBy.Should().BeNull();
            item.LastModified.Should().BeNull();
        }
    }
}
