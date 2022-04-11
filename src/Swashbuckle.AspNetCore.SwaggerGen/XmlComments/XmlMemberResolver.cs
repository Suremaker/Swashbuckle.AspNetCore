using System.Collections.Generic;
using System.Xml.XPath;

namespace Swashbuckle.AspNetCore.SwaggerGen
{
    public class XmlMemberResolver
    {
        private readonly Dictionary<string, XPathNavigator> _members = new();

        public XmlMemberResolver(IXPathNavigable xPathNavigable)
        {
            var iterator = xPathNavigable.CreateNavigator().Select("/doc/members/member");
            while (iterator.MoveNext())
            {
                var current = iterator.Current.CreateNavigator();
                _members[current.GetAttribute("name", "")] = current;
            }

        }

        public XPathNavigator ResolveMember(string memberName)
        {
            return _members.TryGetValue(memberName, out var navigator) ? navigator : null;
        }
    }
}