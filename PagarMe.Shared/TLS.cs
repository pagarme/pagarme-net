using System;
using System.Net;
using System.Threading.Tasks;

namespace PagarMe
{
    public class TLS
    {
        private TLS(){ }
        public static TLS Instance = new TLS();

        #if !PCL
        private SecurityProtocolType @default;
        #endif
        
        #if NET40
        private const SecurityProtocolType tls12 = (SecurityProtocolType) 3072;
        #elif !PCL
        private const SecurityProtocolType tls12 = SecurityProtocolType.Tls12;
        #endif

        /// <summary>
        /// Changes the default protocol to TLS 1.2
        /// to run the request that needs this protocol
        /// then return to previous configuration
        /// </summary>
        public async Task<T> UseTLS12IfAvailable<T>(Func<Task<T>> requestCode)
        {
            goToTLS12();
            var result = await requestCode();
            restoreDefaultTLS();
            return result;
        }

        /// <summary>
        /// Changes the default protocol to TLS 1.2
        /// to run the request that needs this protocol
        /// then return to previous configuration
        /// </summary>
        public T UseTLS12IfAvailable<T>(Func<T> requestCode)
        {
            goToTLS12();
            var result = requestCode();
            restoreDefaultTLS();
            return result;
        }

        private void goToTLS12()
        {
            #if !PCL
            @default = ServicePointManager.SecurityProtocol;
            ServicePointManager.SecurityProtocol = tls12;
            #endif
        }

        private void restoreDefaultTLS()
        {
            #if !PCL
            ServicePointManager.SecurityProtocol = @default;
            #endif
        }
    }
}