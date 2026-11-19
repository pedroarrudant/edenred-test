using System;
using EdenredTest.Application.Shared.Models;
using MassTransit;

namespace EdenredTest.Application.Shared.Services
{
    public class PublishService : IPublisherService
    {
        public async Task Publish(Pet pet, IPublishEndpoint publishEndpoint)
        {
            await publishEndpoint.Publish(pet);
        }
    }
}

