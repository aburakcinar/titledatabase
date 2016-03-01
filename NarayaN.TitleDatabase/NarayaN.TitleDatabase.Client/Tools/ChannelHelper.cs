namespace NarayaN.TitleDatabase.Client.Tools
{
    internal static class ChannelHelper
    {
        private static readonly ChannelFactoryManager FactoryManager = new ChannelFactoryManager();

        public static T GetProxy<T>()
        {
            T channelProxy = FactoryManager.CreateChannel<T>();
            return channelProxy;
        }
    }
}