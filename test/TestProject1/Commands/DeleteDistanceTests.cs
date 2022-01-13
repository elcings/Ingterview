using FluentAssertions;
using Interview.Application.Common.Exceptions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IntegrationTest.Commands
{
    using static Testing;
    public class DeleteDistanceTests
    {
        //[Test]
        //public async Task ShouldRequireValidDistanceId()
        //{
        //    var command = new DeleteDistanceCommand { Id = Guid.NewGuid() };

        //    await FluentActions.Invoking(() =>
        //        SendAsync(command)).Should().ThrowAsync<NotFoundException>();
        //}


        //[Test]
        //public async Task ShouldDeleteDistanceItem()
        //{
 
        //    var disatnceId = await SendAsync(new CreateDistanceCommand
        //    {
        //        Distance=45
        //    });

        //    await SendAsync(new DeleteDistanceCommand
        //    {
        //        Id = disatnceId
        //    });

        //    var item = await FindAsync<Distance>(disatnceId);

        //    item.Should().BeNull();
        //}

    }
}
