﻿namespace UglyToad.PdfPig.Export.PAGE
{
    using System;
    using System.CodeDom.Compiler;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Xml.Serialization;

    public partial class PageXmlDocument
    {
        /// <summary>
        /// Regions containing content that is not covered
        /// by the default types(text, graphic, image,
        /// line drawing, chart, table, separator, maths,
        /// map, music, chem, advert, noise, unknown).
        /// </summary>
        [EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        [GeneratedCodeAttribute("xsd", "4.6.1055.0")]
        [SerializableAttribute()]
        [DebuggerStepThroughAttribute()]
        [DesignerCategoryAttribute("code")]
        [XmlTypeAttribute(Namespace = "http://schema.primaresearch.org/PAGE/gts/pagecontent/2019-07-15")]
        public class PageXmlCustomRegion : PageXmlRegion
        {

            private string typeField;

            /// <summary>
            /// Information on the type of content represented by this region
            /// </summary>
            [XmlAttributeAttribute("type")]
            public string Type
            {
                get
                {
                    return this.typeField;
                }
                set
                {
                    this.typeField = value;
                }
            }
        }

    }
}
