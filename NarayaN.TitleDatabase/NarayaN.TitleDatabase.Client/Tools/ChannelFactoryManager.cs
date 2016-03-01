using System;
using System.Collections.Concurrent;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.MsmqIntegration;

namespace NarayaN.TitleDatabase.Client.Tools
{
    
    public class ChannelFactoryManager : IDisposable
    {
        private static readonly Uri DefaultServiceUri;
        private static readonly string DefaultBindingConfigurationName;
        private static readonly BindingType DefaultBindingType;

        private static readonly ConcurrentDictionary<Type, Lazy<ChannelFactory>> Factories = new ConcurrentDictionary<Type, Lazy<ChannelFactory>>();

        [SuppressMessage("Microsoft.Design", "CA1065:DoNotRaiseExceptionsInUnexpectedLocations", 
            Justification = "Servis erişim parametreleri konfigürasyon dosyasından okunamazsa exception atılmalı.")]
        [SuppressMessage("Microsoft.Performance", "CA1810:InitializeReferenceTypeStaticFieldsInline",
            Justification = "Static constructor gerekli.")]
        static ChannelFactoryManager()
        {
            //ServiceConnectivitySettings connectivitySettings =
            //    KIKConfigurationSettingsManager.GetServiceConnectivitySettings();

            //if (connectivitySettings.ConnectivitySchemes.Count == 0)
            //    throw new Exception("ServiceConnectivitySettings altında gerekli ConnectivitySchemes collection'ı 0 eleman içeriyor");

            //SchemeElement defaultScheme = connectivitySettings.ConnectivitySchemes[connectivitySettings.DefaultScheme];

            //if (defaultScheme.BindingMethods.Count == 0)
            //    throw new Exception("Scheme altında gerekli BindingMethods collection'ı 0 eleman içeriyor");

            //BindingMethodElement defaultBindingMethod = defaultScheme.BindingMethods[defaultScheme.DefaultBindingMethod];

            //DefaultServiceUri = defaultBindingMethod.ServiceUri;
            //DefaultBindingConfigurationName = defaultBindingMethod.BindingConfigurationName;
            //DefaultBindingType = defaultBindingMethod.BindingType;

            DefaultServiceUri = new Uri("localhost:8081/TDBServices");
            DefaultBindingConfigurationName = "NarayanCustomBinding";
            DefaultBindingType = BindingType.CustomBinding;
        }

        public virtual T CreateChannel<T>()
        {
            T local = GetFactory<T>().CreateChannel();
            return local;
        }

        protected virtual ChannelFactory<T> GetFactory<T>()
        {
            ChannelFactory factory =
                Factories.GetOrAdd(typeof(T), (newKey) => new Lazy<ChannelFactory>(() => CreateFactoryInstance<T>())).Value;

            return (factory as ChannelFactory<T>);
        }

        private ChannelFactory CreateFactoryInstance<T>()
        {
            BindingType bindingType = DefaultBindingType;
            Uri serviceUri = DefaultServiceUri;
            string configurationName = DefaultBindingConfigurationName;

            //object[] serviceConnectivityAttributes =
            //    typeof(T).GetCustomAttributes(typeof(ServiceConnectivityAttribute), false);

            //if (serviceConnectivityAttributes.Length > 0)
            //{
                //ServiceConnectivitySettings connectivitySettings =
                //    KIKConfigurationSettingsManager.GetServiceConnectivitySettings();

                //if (connectivitySettings.ConnectivitySchemes.Count == 0)
                //    throw new Exception("ServiceConnectivitySettings altında gerekli ConnectivitySchemes collection'ı 0 eleman içeriyor");

                //SchemeElement scheme = connectivitySettings.ConnectivitySchemes[((ServiceConnectivityAttribute)serviceConnectivityAttributes[0]).SchemeName];

                //if (scheme.BindingMethods.Count == 0)
                //    throw new Exception("Scheme altında gerekli BindingMethods collection'ı 0 eleman içeriyor");

                //BindingMethodElement defaultBindingMethod = ((ServiceConnectivityAttribute)serviceConnectivityAttributes[0]).BindingType == BindingType.UseDefaultBinding ?
                //                                scheme.BindingMethods[scheme.DefaultBindingMethod] :
                //                                scheme.BindingMethods[((ServiceConnectivityAttribute)serviceConnectivityAttributes[0]).BindingType];

                serviceUri = DefaultServiceUri;
                configurationName = DefaultBindingConfigurationName;
                bindingType = DefaultBindingType;
            //}

            Binding binding;

            switch (bindingType)
            {
                case BindingType.BasicHttpBinding:
                    binding = new BasicHttpBinding(configurationName);
                    break;
                case BindingType.CustomBinding:
                    binding = new CustomBinding(configurationName);
                    break;
                case BindingType.MsmqIntegrationBinding:
                    binding = new MsmqIntegrationBinding(configurationName);
                    break;
                case BindingType.NetMsmqBinding:
                    binding = new NetMsmqBinding(configurationName);
                    break;
                case BindingType.NetNamedPipeBinding:
                    binding = new NetNamedPipeBinding(configurationName);
                    break;
                //case BindingType.NetPeerTcpBinding:
                //    binding = new NetPeerTcpBinding(configurationName);
                //    break;
                case BindingType.NetTcpBinding:
                    binding = new NetTcpBinding(configurationName);
                    break;
                //case BindingType.WebHttpBinding:
                //    binding = new WebHttpBinding(configurationName);
                //    break;
                case BindingType.WSDualHttpBinding:
                    binding = new WSDualHttpBinding(configurationName);
                    break;
                case BindingType.WSFederationHttpBinding:
                    binding = new WSFederationHttpBinding(configurationName);
                    break;
                case BindingType.WSHttpBinding:
                    binding = new WSHttpBinding(configurationName);
                    break;
                default:
                    throw new InvalidOperationException("BindingMethod içerisinde verilen BindingType elemanı geçerli değil.");
            }

            string endpointPrefix = typeof(T).Name;

#if DEBUG
            EndpointAddress endpointAddress = new EndpointAddress(serviceUri.OriginalString + endpointPrefix + ".svc");
#else
            EndpointAddress endpointAddress = new EndpointAddress(serviceUri.OriginalString + endpointPrefix + ".svc");
#endif
            ServiceEndpoint serviceEndpoint = new ServiceEndpoint(ContractDescription.GetContract(typeof(T)), binding, endpointAddress);
            //serviceEndpoint.Behaviors.Add(new KIKClientInspector());

            ChannelFactory factory = new ChannelFactory<T>(serviceEndpoint);

            factory.Open();
            return factory;
        }

        public void Dispose()
        {
            Dispose(true);
        }

        [SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", 
            Justification = "Dispose methodu exception oluşturmamalı.")]
        protected virtual void Dispose(bool disposing)
        {
            if (!disposing) 
                return;
            
            foreach (ChannelFactory factory in Factories.Keys.Select(type => Factories[type].Value))
            {
                try
                {
                    factory.Close();
                }
                catch
                {
                    factory.Abort();
                }
            }

            Factories.Clear();
        }
    }

    public enum BindingType
    {
        UseDefaultBinding,
        BasicHttpBinding,
        CustomBinding,
        MsmqIntegrationBinding,
        NetMsmqBinding,
        NetNamedPipeBinding,
        NetPeerTcpBinding,
        NetTcpBinding,
        WebHttpBinding,
        WSDualHttpBinding,
        WSFederationHttpBinding,
        WSHttpBinding
    }
}
