using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace SDTSLib.ISO8211Data
{
    public enum DirectoryDataType
    {
        [Description( "UNKNOWN DATA TYPE" )]
        Unknown,
        [Description( "EXTERNAL FILE TITLE" )]
        Filename,
        [Description( "DDF RECORD IDENTIFIER" )]
        DDFRecordIdentifier,
        [Description( "AREA FILL REPRESENTATION" )]
        AFIL,
        [Description( "ARC" )]
        ARC,
        [Description( "ARC ADDRESS" )]
        ARAD,
        [Description( "AREA ID" )]
        ARID,
        [Description( "ATTRIBUTE ID" )]
        ATID,
        [Description( "ATTRIBUTE PRIMARY" )]
        ATPR,
        [Description( "ATTRIBUTE SECONDARY" )]
        ATSC,
        [Description( "PRIMARY ATTRIBUTES" )]
        ATTP,
        [Description( "SECONDARY ATTRIBUTES" )]
        ATTS,
        [Description( "CURVE ADDRESS" )]
        CADR,
        [Description( "CATALOG/DIRECTORY" )]
        CATD,
        [Description( "CATALOG/CROSS-REFERENCE" )]
        CATX,
        [Description( "CATALOG/SPATIAL DOMAIN" )]
        CATS,
        [Description( "CHAIN COMPONENT ID" )]
        CCID,
        [Description( "CELL" )]
        CELL,
        [Description( "CHAIN ID" )]
        CHID,
        [Description( "COLOR INDEX" )]
        CLRX,
        [Description( "COMPOSITE" )]
        COMP,
        [Description( "CONFORMANCE" )]
        CONF,
        [Description( "COMPOSITE ID" )]
        CPID,
        [Description( "CELL VALUES" )]
        CVLS,
        [Description( "DIMENSION AXIS LABEL" )]
        DAL1,
        [Description( "DIMENSION AXIS LABEL" )]
        DAL2,
        [Description( "DIMENSION ATTRIBUTE ID" )]
        DATP,
        [Description( "DATA DICTIONARY/DEFINITION" )]
        DDDF,
        [Description( "DATA DICTIONARY/DOMAIN" )]
        DDOM,
        [Description( "DATA DICTIONARY/SCHEMA" )]
        DDSH,
        [Description( "DIMENSION DEFINITION" )]
        DMDF,
        [Description( "DIMENSION ID" )]
        DMID,
        [Description( "DOMAIN SPATIAL ADDRESS" )]
        DMSA,
        [Description( "DIMENSION INDEX" )]
        DNDX,
        [Description( "ATTRIBUTE ACCURACY" )]
        DQAA,
        [Description( "COMPLETENESS" )]
        DQCG,
        [Description( "LINEAGE" )]
        DQHL,
        [Description( "LOGICAL CONSISTENCY" )]
        DQLC,
        [Description( "POSITIONAL ACCURACY" )]
        DQPA,
        [Description( "EXTERNAL REFERENCE SPATIAL ADDRESS" )]
        EADS,
        [Description( "ELLIPSE ADDRESS" )]
        ELAD,
        [Description( "ENDNODE ID" )]
        ENID,
        [Description( "FONT" )]
        FONT,
        [Description( "FOREIGN ID" )]
        FRID,
        [Description( "INTERNAL REFERENCE SPATIAL ADDRESS" )]
        IADS,
        [Description( "IDENTIFICATION" )]
        IDEN,
        [Description( "INTERNAL SPATIAL REFERENCE" )]
        IREF,
        [Description( "INTERNAL SPATIAL ID" )]
        ISID,
        [Description( "LINE OR ARC FOREIGN ID" )]
        LAID,
        [Description( "LAYER ATTRIBUTE ID" )]
        LATP,
        [Description( "LAYER DEFINITION" )]
        LDEF,
        [Description( "LAYER DIMENSION EXTENT" )]
        LDXT,
        [Description( "LINE" )]
        LINE,
        [Description( "LINE ID" )]
        LNID,
        [Description( "LINE REPRESENTATION" )]
        LNRP,
        [Description( "LAYER ID" )]
        LYID,
        [Description( "SPATIAL OBJECT ID" )]
        OBID,
        [Description( "ORIENTATION SPATIAL ADDRESS" )]
        OSAD,
        [Description( "ATTRIBUTE PRIMARY FOREIGN ID" )]
        PAID,
        [Description( "POLYGON ID LEFT" )]
        PIDL,
        [Description( "POLYGON ID RIGHT" )]
        PIDR,
        [Description( "POLYGON ID" )]
        PLID,
        [Description( "POINT-NODE" )]
        PNTS,
        [Description( "POLYGON" )]
        POLY,
        [Description( "RASTER ATTRIBUTE ID" )]
        RATP,
        [Description( "RASTER DIMENSION EXTENT" )]
        RDXT,
        [Description( "RING ID" )]
        RFID,
        [Description( "REGISTRATION" )]
        RGIS,
        [Description( "RING" )]
        RING,
        [Description( "REPRESENTATION MODULE ID" )]
        RPID,
        [Description( "RASTER DEFINITION" )]
        RSDF,
        [Description( "SPATIAL ADDRESS" )]
        SADR,
        [Description( "SOUNDING ATTRIBUTES" )]
        SATT,
        [Description( "SECURITY" )]
        SCUR,
        [Description( "STARTNODE ID" )]
        SNID,
        [Description( "SPATIAL DOMAIN" )]
        SPDM,
        [Description( "SYMBOL ORIENTATION SPATIAL ADDRESS" )]
        SSAD,
        [Description( "TRANSFER STATISTICS" )]
        STAT,
        [Description( "SYMBOL REPRESENTATION" )]
        SYRP,
        [Description( "TEXT REPRESENTATION" )]
        TEXT,
        [Description( "VERTICAL ATTRIBUTES" )]
        VATT,
        [Description( "EXTERNAL SPATIAL REFERENCE" )]
        XREF,
        [Description( "X-AXIS LABEL" )]
        XXLB,
        [Description( "Y-AXIS LABEL" )]
        YXLB,
        [Description( "Z-AXIS LABEL" )]
        ZXLB
    };
}
