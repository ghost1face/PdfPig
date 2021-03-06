﻿namespace UglyToad.PdfPig.Export.Alto
{
    using System;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Xml.Serialization;

    public partial class AltoDocument
    {
        /// <summary>
        /// [Alto] A unique identifier for the image file. This is drawn from MIX.
        /// 
        /// <para>This identifier must be unique within the local  
        /// To facilitate file sharing or interoperability with other systems, 
        /// fileIdentifierLocation may be added to designate the system or 
        /// application where the identifier is unique.</para>
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Serializable]
        [DebuggerStepThrough]
        [XmlType(Namespace = "http://www.loc.gov/standards/alto/ns-v4#")]
        public class AltoFileIdentifier
        {
            /// <remarks/>
            [XmlAttribute("fileIdentifierLocation")]
            public string FileIdentifierLocation { get; set; }

            /// <remarks/>
            [XmlText]
            public string Value { get; set; }
        }
    }
}
