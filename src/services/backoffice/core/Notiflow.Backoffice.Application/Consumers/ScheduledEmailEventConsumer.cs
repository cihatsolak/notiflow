namespace Notiflow.Backoffice.Application.Consumers;

public sealed class ScheduledEmailEventConsumer : IConsumer<ScheduledEmailEvent>
{
    private readonly IMediator _mediator;

    public ScheduledEmailEventConsumer(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task Consume(ConsumeContext<ScheduledEmailEvent> context)
    {
        var response = await _mediator.Send(new SendEmailCommand
        {
            Body = context.Message.Body,
            Subject = context.Message.Subject,
            CustomerIds = context.Message.CustomerIds,
            CcAddresses = context.Message.CcAddresses,
            BccAddresses = context.Message.BccAddresses,
            IsBodyHtml = context.Message.IsBodyHtml
        });

        await context.RespondAsync(new ScheduledResponse
        {
            Succeeded = response.Succeeded,
            ErrorMessage = response.Succeeded ? default : response.Message
        });
    }
}
