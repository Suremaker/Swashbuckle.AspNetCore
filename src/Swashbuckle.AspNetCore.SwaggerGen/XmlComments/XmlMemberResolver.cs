using System.Collections.Concurrent;
using System.Xml.XPath;

namespace Swashbuckle.AspNetCore.SwaggerGen
{
    public class XmlMemberResolver
    {
        private readonly XPathNavigator _xmlNavigator;
        private readonly ConcurrentDictionary<string, XPathNavigator> _memberCache = new();

        public XmlMemberResolver(IXPathNavigable xPathNavigable)
        {
            _xmlNavigator = xPathNavigable.CreateNavigator();
        }

        public XPathNavigator ResolveMember(string memberName)
        {
            var path = $"/doc/members/member[@name='{memberName}']";
            return _memberCache.GetOrAdd(path, _xmlNavigator.SelectSingleNode);
        }
    }
}