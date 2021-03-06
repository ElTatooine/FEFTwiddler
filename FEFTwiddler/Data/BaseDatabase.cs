﻿using System;
using System.Linq;
using System.Xml.Linq;
using FEFTwiddler.Extensions;

namespace FEFTwiddler.Data
{
    public abstract class BaseDatabase
    {
        protected XElement _data;
        protected Enums.Language _language;

        public BaseDatabase(Enums.Language language)
        {
            SetLanguage(language);
        }

        public void SetLanguage(Enums.Language language)
        {
            _language = language;
        }

        protected string GetDisplayName(XElement xe)
        {
            XElement lang;

            // Get desired language
            lang = xe.Elements("text").Where((x) => x.GetAttribute("lang") == _language.ToString()).FirstOrDefault();

            // Get fallback language (English)
            if (lang == null) lang = xe.Elements("text").Where((x) => x.GetAttribute("lang") == Enums.Language.English.ToString()).FirstOrDefault();

            // Fallback on internal name if no display name exists yet
            var displayName = (lang != null ? lang.GetAttribute("displayName") : "DisplayName");
            if (displayName == "DisplayName")
            {
                return xe.GetAttribute("name");
            }
            else
            {
                return displayName;
            }
        }














        /// <summary>
        /// Leaving this in case more batch XML changing is needed
        /// </summary>
        protected void UpdateXmlStructure()
        {
            var rows = _data.Elements("class");
            for (var i = 0; i < rows.Count(); i++)
            {
                var row = rows.ElementAt(i);

                var categories = row.Elements("categories").First();
                var gender = row.GetAttribute("gender");
                if (gender == "Female")
                {
                    categories.SetAttributeValue("isFemale", "true");
                }
                else
                {
                    categories.SetAttributeValue("isFemale", "false");
                }
                categories.SetAttributeValue("isNpcOnly", "false");

                row.Attribute("gender").Remove();

                //var categories = XElement.Parse(@"<categories isPromoted=""false"" isSpecial=""false"" />");

                //row.Add(categories);


            }
            var breakpoint = true;
        }
    }
}
