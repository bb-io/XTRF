using Blackbird.Applications.Sdk.Common.Exceptions;

namespace Apps.XTRF.Utils
{
    public class URLValidationForExternalLinksDownload
    {
        public static Uri ValidateExternalUrl(string? rawUrl)
        {
            if (string.IsNullOrWhiteSpace(rawUrl) ||
                rawUrl.Length > 8192 ||
                !Uri.TryCreate(rawUrl.Trim(), UriKind.Absolute, out var uri))
            {
                throw new PluginMisconfigurationException(
                    "The linked file must have a valid absolute URL.");
            }

            // Only allow HTTPS on the standard port.
            if (uri.Scheme != Uri.UriSchemeHttps || uri.Port != 443)
            {
                throw new PluginMisconfigurationException(
                    "The linked file URL must use HTTPS on port 443.");
            }

            // Reject embedded credentials and URL fragments.
            if (!string.IsNullOrEmpty(uri.UserInfo) ||
                !string.IsNullOrEmpty(uri.Fragment))
            {
                throw new PluginMisconfigurationException(
                    "The linked file URL contains unsupported components.");
            }

            var host = uri.IdnHost.TrimEnd('.').ToLowerInvariant();

            // Reject direct IP addresses, localhost and local hostnames.
            if (uri.HostNameType != UriHostNameType.Dns ||
                Uri.CheckHostName(host) != UriHostNameType.Dns ||
                !host.Contains('.') ||
                uri.IsLoopback ||
                host.EndsWith(".localhost") ||
                host.EndsWith(".local") ||
                host.EndsWith(".internal") ||
                host.EndsWith(".home.arpa"))
            {
                throw new PluginMisconfigurationException(
                    "The linked file must use a public DNS hostname.");
            }

            return uri;
        }
    }
}
