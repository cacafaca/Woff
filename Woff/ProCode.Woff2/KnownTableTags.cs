using System;
using System.Linq;

namespace ProCode.Woff2
{
    public enum KnownTableTags
    {
        [KnownTableTagValueAttribute("cmap")]
        Cmap,
        [KnownTableTagValueAttribute("head")]
        Head,
        [KnownTableTagValueAttribute("hhea")]
        Hhea,
        [KnownTableTagValueAttribute("hmtx")]
        Hmtx,
        [KnownTableTagValueAttribute("maxp")]
        Maxp,
        [KnownTableTagValueAttribute("name")]
        Name,
        [KnownTableTagValueAttribute("OS/2")]
        Os2,
        [KnownTableTagValueAttribute("post")]
        Post,
        [KnownTableTagValueAttribute("cvt")]
        Cvt,
        [KnownTableTagValueAttribute("fpgm")]
        Fpgm,
        [KnownTableTagValueAttribute("glyf")]
        Glyf,
        [KnownTableTagValueAttribute("loca")]
        Loca,
        [KnownTableTagValueAttribute("prep")]
        Prep,
        [KnownTableTagValueAttribute("CFF")]
        Cff,
        [KnownTableTagValueAttribute("VORG")]
        Vorg,
        [KnownTableTagValueAttribute("EBDT")]
        Ebdt,
        [KnownTableTagValueAttribute("EBLC")]
        Eblc,
        [KnownTableTagValueAttribute("gasp")]
        Gasp,
        [KnownTableTagValueAttribute("hdmx")]
        Hdmx,
        [KnownTableTagValueAttribute("kern")]
        Kern,
        [KnownTableTagValueAttribute("LTSH")]
        Ltsh,
        [KnownTableTagValueAttribute("PCLT")]
        Pclt,
        [KnownTableTagValueAttribute("VDMX")]
        Vdmx,
        [KnownTableTagValueAttribute("vhea")]
        Vhea,
        [KnownTableTagValueAttribute("vmtx")]
        Vmtx,
        [KnownTableTagValueAttribute("BASE")]
        Base,
        [KnownTableTagValueAttribute("GDEF")]
        Gdef,
        [KnownTableTagValueAttribute("GPOS")]
        Gpos,
        [KnownTableTagValueAttribute("GSUB")]
        Gsub,
        [KnownTableTagValueAttribute("EBSC")]
        Ebsc,
        [KnownTableTagValueAttribute("JSTF")]
        Jstf,
        [KnownTableTagValueAttribute("MATH")]
        Math,
        [KnownTableTagValueAttribute("CBDT")]
        Cbdt,
        [KnownTableTagValueAttribute("CBLC")]
        Cblc,
        [KnownTableTagValueAttribute("COLR")]
        Colr,
        [KnownTableTagValueAttribute("CPAL")]
        Cpal,
        [KnownTableTagValueAttribute("SVG")]
        Svg,
        [KnownTableTagValueAttribute("sbix")]
        Sbix,
        [KnownTableTagValueAttribute("acnt")]
        Acnt,
        [KnownTableTagValueAttribute("avar")]
        Avar,
        [KnownTableTagValueAttribute("bdat")]
        Bdat,
        [KnownTableTagValueAttribute("bloc")]
        Bloc,
        [KnownTableTagValueAttribute("bsln")]
        Bsln,
        [KnownTableTagValueAttribute("cvar")]
        Cvar,
        [KnownTableTagValueAttribute("fdsc")]
        Fdsc,
        [KnownTableTagValueAttribute("feat")]
        FeatSmallF,
        [KnownTableTagValueAttribute("fmtx")]
        Fmtx,
        [KnownTableTagValueAttribute("fvar")]
        Fvar,
        [KnownTableTagValueAttribute("gvar")]
        Gvar,
        [KnownTableTagValueAttribute("hsty")]
        Hsty,
        [KnownTableTagValueAttribute("just")]
        Just,
        [KnownTableTagValueAttribute("lcar")]
        Lcar,
        [KnownTableTagValueAttribute("mort")]
        Mort,
        [KnownTableTagValueAttribute("morx")]
        Morx,
        [KnownTableTagValueAttribute("opbd")]
        Opbd,
        [KnownTableTagValueAttribute("prop")]
        Prop,
        [KnownTableTagValueAttribute("trak")]
        Trak,
        [KnownTableTagValueAttribute("Zapf")]
        Zapf,
        [KnownTableTagValueAttribute("Silf")]
        Silf,
        [KnownTableTagValueAttribute("Glat")]
        Glat,
        [KnownTableTagValueAttribute("Gloc")]
        Gloc,
        [KnownTableTagValueAttribute("Feat")]
        FeatCapitalF,
        [KnownTableTagValueAttribute("Sill")]
        Sill,
        ArbitraryTag
    }

    public static class KnownTableTagsHelper
    {
        public static UInt32 Value(this KnownTableTags tag)
        {
            var attributes = tag.GetType().GetField(tag.ToString()).GetCustomAttributes(typeof(KnownTableTagValueAttribute), false);
            if (attributes != null && attributes.Count() > 0)                
                return ((KnownTableTagValueAttribute)attributes.First()).Value;
            else
                throw new WoffUtility.WoffBaseException("Attribute do not exists.");
        }
    }

    public class KnownTableTagValueAttribute : Attribute
    {
        #region Constructors

        public KnownTableTagValueAttribute(UInt32 value)
        {
            Value = value;
        }

        public KnownTableTagValueAttribute(string tagString)
        {
            if (tagString == null)
                throw new ArgumentNullException();

            if (tagString.Length > 4)
                throw new ArgumentOutOfRangeException(tagString);

            tagString = tagString.PadRight(4);

            UInt32 result = 0;
            foreach (var c in tagString)
                result = result << 8 | c;
            Value = result;
            //Value = Convert.ToUInt32(value.PadRight(4 - value.Length));
        }

        #endregion

        #region Public Properties

        public UInt32 Value { get; private set; }

        #endregion
    }

}
