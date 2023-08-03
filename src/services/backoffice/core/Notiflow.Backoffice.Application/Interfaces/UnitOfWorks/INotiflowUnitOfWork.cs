﻿namespace Notiflow.Backoffice.Application.Interfaces.UnitOfWorks;

public interface INotiflowUnitOfWork : IBaseUnitOfWork
{
    ICustomerReadRepository CustomerRead { get; }
    ICustomerWriteRepository CustomerWrite { get; }
    IDeviceReadRepository DeviceRead { get; }
    IDeviceWriteRepository DeviceWrite { get; }
    ITextMessageHistoryReadRepository TextMessageHistoryRead { get; }
    ITextMessageHistoryWriteRepository TextMessageHistoryWrite { get; }
}