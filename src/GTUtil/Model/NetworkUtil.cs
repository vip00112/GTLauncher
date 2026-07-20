using System.Net;

namespace GTUtil
{
    public static class NetworkUtil
    {
        /// <summary>
        /// 외부 HTTPS 통신(GitHub 등)에 필요한 보안 프로토콜을 보장한다.
        /// TLS 1.2 이상만 사용하며, 인증서 검증은 절대 우회하지 않는다.
        /// </summary>
        public static void EnsureSecureProtocol()
        {
            ServicePointManager.Expect100Continue = true;
            // 기존에 활성화된 프로토콜(TLS 1.3 등)을 유지하면서 TLS 1.2를 보장한다.
            ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls12;
        }
    }
}
