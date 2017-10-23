namespace TPLExtensions.Net35.Extensions
{
    using System.Threading;

    public delegate void GenericEventHandler<in TEventArgs>(object sender, TEventArgs e);

    public static class GenericEventHandlerExtensions
    {
        public static void SafeInvoke<T>(this GenericEventHandler<T> @event, object sender, T param) 
            => Interlocked.CompareExchange(ref @event, null, null)?.Invoke(sender, param);
    }
}
