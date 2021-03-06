﻿namespace UglyToad.PdfPig.Filters
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Exceptions;
    using Logging;
    using Tokens;
    using Util;

    internal class MemoryFilterProvider : IFilterProvider
    {
        private readonly IReadOnlyDictionary<string, IFilter> filterInstances; 

        public MemoryFilterProvider(IDecodeParameterResolver decodeParameterResolver, IPngPredictor pngPredictor, ILog log)
        {
            var ascii85 = new Ascii85Filter();
            var asciiHex = new AsciiHexDecodeFilter();
            var ccitt = new CcittFaxDecodeFilter();
            var dct = new DctDecodeFilter();
            var flate = new FlateFilter(decodeParameterResolver, pngPredictor, log);
            var jbig2 = new Jbig2DecodeFilter();
            var jpx = new JpxDecodeFilter();
            var runLength = new RunLengthFilter();
            var lzw = new LzwFilter(decodeParameterResolver, pngPredictor);

            filterInstances = new Dictionary<string, IFilter>
            {
                {NameToken.Ascii85Decode.Data, ascii85},
                {NameToken.Ascii85DecodeAbbreviation.Data, ascii85},
                {NameToken.AsciiHexDecode.Data, asciiHex},
                {NameToken.AsciiHexDecodeAbbreviation.Data, asciiHex},
                {NameToken.CcittfaxDecode.Data, ccitt},
                {NameToken.CcittfaxDecodeAbbreviation.Data, ccitt},
                {NameToken.DctDecode.Data, dct},
                {NameToken.DctDecodeAbbreviation.Data, dct},
                {NameToken.FlateDecode.Data, flate},
                {NameToken.FlateDecodeAbbreviation.Data, flate},
                {NameToken.Jbig2Decode.Data, jbig2},
                {NameToken.JpxDecode.Data, jpx},
                {NameToken.RunLengthDecode.Data, runLength},
                {NameToken.RunLengthDecodeAbbreviation.Data, runLength},
                {NameToken.LzwDecode, lzw},
                {NameToken.LzwDecodeAbbreviation, lzw}
            };
        }

        public IReadOnlyList<IFilter> GetFilters(DictionaryToken dictionary)
        {
            if (dictionary == null)
            {
                throw new ArgumentNullException(nameof(dictionary));
            }

            if (!dictionary.TryGet(NameToken.Filter, out var token))
            {
                return EmptyArray<IFilter>.Instance;
            }

            switch (token)
            {
                case ArrayToken filters:
                    var result = new IFilter[filters.Data.Count];
                    for (var i = 0; i < filters.Data.Count; i++)
                    {
                        var filterToken = filters.Data[i];
                        var filterName = ((NameToken) filterToken).Data;
                        result[i] = GetFilterStrict(filterName);
                    }

                    return result;
                case NameToken name:
                    return new[] { GetFilterStrict(name.Data) };
                default:
                    throw new PdfDocumentFormatException($"The filter for the stream was not a valid object. Expected name or array, instead got: {token}.");
            }
        }

        public IReadOnlyList<IFilter> GetNamedFilters(IReadOnlyList<NameToken> names)
        {
            if (names == null)
            {
                throw new ArgumentNullException(nameof(names));
            }

            var result = new List<IFilter>();

            foreach (var name in names)
            {
                result.Add(GetFilterStrict(name));
            }

            return result;
        }
        
        private IFilter GetFilterStrict(string name)
        {
            if (!filterInstances.TryGetValue(name, out var factory))
            {
                throw new NotSupportedException($"The filter with the name {name} is not supported yet. Please raise an issue.");
            }

            return factory;
        }

        public IReadOnlyList<IFilter> GetAllFilters()
        {
            return filterInstances.Values.Distinct().ToList();
        }
    }
}