using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Kentor.AuthServices.Internal;

namespace Kentor.AuthServices
{
    /// <summary>
    /// Extension methods for Saml2Condition
    /// </summary>
    public static class Saml2ConditionsExtensions
    {
        /// <summary>
        /// Writes out the conditions as an XElement.
        /// </summary>
        /// <param name="conditions">Conditions to create xml for.</param>
        /// <returns>XElement</returns>
        public static XElement ToXElement(this Saml2Conditions conditions)
        {
            if(conditions == null)
            {
                throw new ArgumentNullException("conditions");
            }

            var xml = new XElement(Saml2Namespaces.Saml2 + "Conditions");

            if (conditions.NotOnOrAfter == null)
                xml.AddAttributeIfNotNullOrEmpty("NotOnOrAfter", null);
            else
                xml.AddAttributeIfNotNullOrEmpty("NotOnOrAfter", conditions.NotOnOrAfter.Value.ToSaml2DateTimeString());

            foreach(var ar in conditions.AudienceRestrictions)
            {
                xml.Add(new XElement(Saml2Namespaces.Saml2 + "AudienceRestriction",
                    ar.Audiences.Select(a =>
                    new XElement(Saml2Namespaces.Saml2 + "Audience", a.OriginalString))));
            }

            return xml;
        }
    }
}
