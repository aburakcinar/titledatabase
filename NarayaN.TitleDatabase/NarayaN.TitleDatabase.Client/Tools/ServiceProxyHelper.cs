using System.ServiceModel;
using System.Threading.Tasks;

namespace NarayaN.TitleDatabase.Client.Tools
{
    public delegate TRetVal UseServiceDelegate<in T, out TRetVal>(T proxy);
    public delegate void UseServiceDelegate<in T>(T proxy);

    public static class ServiceProxyHelper<T>
    {
        public static void Call(UseServiceDelegate<T> codeBlock)
        {
            T proxy = ChannelHelper.GetProxy<T>();
            IClientChannel clientChannel = (IClientChannel)proxy;

            bool success = false;
            try
            {
                codeBlock(proxy);
                clientChannel.Close();
                success = true;
            }
            finally
            {
                if (!success)
                {
                    clientChannel.Abort();
                }
            }
        }
    }

    public static class ServiceProxyHelper<T, TRetVal>
    {
        public static TRetVal Call(UseServiceDelegate<T, TRetVal> codeBlock)
        {
            T proxy = ChannelHelper.GetProxy<T>();
            IClientChannel clientChannel = (IClientChannel)proxy;

            bool success = false;
            try
            {
                TRetVal valueReturned = codeBlock(proxy);
                clientChannel.Close();
                success = true;
                return valueReturned;
            }
            finally
            {
                if (!success)
                {
                    clientChannel.Abort();
                }
            }
        }

        public async static Task<TRetVal> CallAsync(UseServiceDelegate<T, Task<TRetVal>> codeBlock)
        {
            T proxy = ChannelHelper.GetProxy<T>();
            IClientChannel clientChannel = (IClientChannel)proxy;

            bool success = false;
            try
            {
                TRetVal valueReturned = await codeBlock(proxy);
                clientChannel.Close();
                success = true;
                return valueReturned;
            }
            finally
            {
                if (!success)
                {
                    clientChannel.Abort();
                }
            }
        }
    }


}