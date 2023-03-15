namespace Puzzle.Lib.Hangfire.IOC
{
    /// <summary>
    /// Custom job activator for Hangfire that uses an <see cref="IServiceProvider"/> to resolve job instances.
    /// </summary>
    internal sealed class HangfireActivator : JobActivator
    {
        private readonly IServiceProvider _serviceProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="HangfireActivator"/> class with the specified <see cref="IServiceProvider"/>.
        /// </summary>
        /// <param name="serviceProvider">The <see cref="IServiceProvider"/> to use for resolving job instances.</param>
        public HangfireActivator(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        /// <summary>
        /// Resolves a job instance from the <see cref="IServiceProvider"/>.
        /// </summary>
        /// <param name="jobType">The type of the job to activate.</param>
        /// <returns>An instance of the job type resolved from the <see cref="IServiceProvider"/>.</returns>
        public override object ActivateJob(Type jobType)
        {
            return _serviceProvider.GetService(jobType);
        }
    }
}
