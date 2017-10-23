namespace TPLExtensions.Net35.Extensions
{
    using System.Threading;
    using System.Threading.Tasks;

    public static class TaskExtensions
    {
        public static async Task<T> WithWaitCancellation<T>(this Task<T> task, CancellationToken cancellationToken)
        {
            var tcs = new TaskCompletionSource<bool>();
            using (cancellationToken.Register(s => ((TaskCompletionSource<bool>)s).TrySetCanceled(), tcs))
            {
                if (task != await TaskEx.WhenAny(task, tcs.Task)) throw new TaskCanceledException();
            }

            return await task;
        }

        public static async Task WithWaitCancellation(this Task task, CancellationToken cancellationToken)
        {
            var tcs = new TaskCompletionSource<bool>();
            using (cancellationToken.Register(s => ((TaskCompletionSource<bool>)s).TrySetCanceled(), tcs))
            {
                if (task != await TaskEx.WhenAny(task, tcs.Task)) throw new TaskCanceledException();
            }
        }
    }
}
