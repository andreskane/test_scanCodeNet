using Couchbase.Core.Retry;

namespace Infrastructure.CouchbaseDB.Helper
{
    // Overriding BestEffortRetryStrategy to stop retrying in case QueryPreparedStatementFailure
    public class RetryStrategy : IRetryStrategy
    {
        private readonly IBackoffCalculator _backoffCalculator;

        public RetryStrategy() :
            this(ExponentialBackoff.Create(10, 1, 500))
        {
        }

        public RetryStrategy(IBackoffCalculator calculator)
        {
            _backoffCalculator = calculator;
        }

        public RetryAction RetryAfter(IRequest request, RetryReason reason)
        {
            if (reason == RetryReason.NoRetry)
            {
                return RetryAction.Duration(null);
            }
            // added below code to modify BestEffortRetryStrategy of couchbase to stop retrying in case of QueryPreparedStatementFailure
            else if (reason == RetryReason.QueryPreparedStatementFailure)
            {
                throw new System.Exception(reason.ToString());
            }
            if (request.Idempotent || reason.AllowsNonIdempotentRetries())
            {
                var backoffDuration = _backoffCalculator.CalculateBackoff(request);
                return RetryAction.Duration(backoffDuration);
            }

            return RetryAction.Duration(null);
        }
    }
}