using EdenredTest.Application.Shared.Models;
using MassTransit;

namespace EdenredTest.Application.Entrypoint.Consumer
{
    public interface IInsertPetConsumer
    {
        Task Consume(ConsumeContext<Pet> context);
    }
}