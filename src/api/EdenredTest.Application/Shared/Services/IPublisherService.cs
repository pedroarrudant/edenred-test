using System;
using EdenredTest.Application.Shared.Models;
using MassTransit;
using MassTransit.Transports;

namespace EdenredTest.Application.Shared.Services
{
    public interface IPublisherService
    {

        Task Publish(Pet pet, IPublishEndpoint publishEndpoint);
    }
}

