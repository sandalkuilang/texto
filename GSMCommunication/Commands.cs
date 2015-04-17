/*
    SMS Gateway
 
    Copyright (C) 2013 Yudha - yudha_hyp@yahoo.com

    This library is free software; you can redistribute it and/or
    modify it, in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GSMCommunication
{
    public class Commands
    {
        public const string RESULT_OK = "OK";
        public const string RESULT_ERROR = "ERROR";
        public const string RESULT_CONNECT = "CONNECT";
        public const string RESULT_NODIALTONE = "NO DIALTONE";
        public const string RESULT_NOCARRIER = "NO CARRIER";
        public const string RESULT_RING = "RING";

        public const string Empty = "";
        public const string Star = "*";
        public const string A = "A";
        public const string D = "D";
        public const string E = "E";
        public const string H = "H";
        public const string I = "I";
        public const string L = "L";
        public const string M = "M";
        public const string O = "O";
        public const string P = "P";
        public const string Q = "Q";
        public const string T = "T";
        public const string V = "V";
        public const string X = "X";
        public const string Z = "Z";
        public const string AT = "AT";

        public const string ANDF = "&F";
        public const string ANDC = "&C";
        public const string ANDD = "&D";
        public const string ANDW = "&W";
        public const string BLDN = "+BLDN";
        public const string CALA = "+CALA";
        public const string CALD = "+CALD";
        public const string CAPD = "+CAPD";
        public const string CCLK = "+CCLK";
        public const string CGMM = "+CGMM";
        public const string CGMR = "+CGMR";
        public const string CHLD = "+CHLD";
        public const string CHUP = "+CHUP";
        public const string CIND = "+CIND";
        public const string CKPD = "+CKPD";
        public const string CLCK = "+CLCK";
        public const string CLIP = "+CLIP";
        public const string CMAR = "+CMAR";
        public const string CMER = "+CMER";
        public const string CPBF = "+CPBF";
        public const string CPBP = "+CPBP";
        public const string CPBR = "+CPBR";
        public const string CPBS = "+CPBS";
        public const string CPBW = "+CPBW";
        public const string CPUC = "+CPUC";
        public const string CRES = "+CRES";
        public const string CSAS = "+CSAS";
        public const string CSCA = "+CSCA";
        public const string CSCB = "+CSCB";
        public const string CSCC = "+CSCC";
        public const string CSDF = "+CSDF";
        public const string CSIL = "+CSIL";
        public const string CSNS = "+CSNS";
        public const string CSTA = "+CSTA";
        public const string CSTF = "+CSTF";
        public const string CTFR = "+CTFR";
        public const string CVIB = "+CVIB";
        public const string GMR = "+GMR";
        public const string WS46 = "+WS46";
        public const string CLAN = "+CLAN";
        public const string BRSF = "+BRSF";
        public const string BINP = "+BINP";
        public const string BVRA = "+BVRA";
        public const string NREC = "+NREC";
        public const string VGS = "+VGS";
        public const string VGM = "+VGM";
        public const string CLVL = "+CLVL";
        public const string CMUT = "+CMUT";
        public const string CRSL = "+CRSL";
        public const string CRMP = "+CRMP";
        public const string CACM = "+CACM";
        public const string CAMM = "+CAMM";
        public const string CAOC = "+CAOC";
        public const string CBC = "+CBC";
        public const string CCFC = "+CCFC";
        public const string CCID = "+CCID";
        public const string CCWA = "+CCWA";
        public const string CFUN = "+CFUN";
        public const string CGSMS = "+CGSMS";
        public const string CGSN = "+CGSN";
        public const string CIMI = "+CIMI";
        public const string CLAC = "+CLAC";
        public const string CLCC = "+CLCC";
        public const string CMEC = "+CMEC";
        public const string CMGC = "+CMGC";
        public const string CMGD = "+CMGD";
        public const string CMGF = "+CMGF";
        public const string CMGL = "+CMGL";
        public const string CMGR = "+CMGR";
        public const string CMGS = "+CMGS";
        public const string CMGW = "+CMGW";
        public const string CMMS = "+CMMS";
        public const string CMSS = "+CMSS";
        public const string CNMI = "+CNMI";
        public const string CNUM = "+CNUM";
        public const string COPN = "+COPN";
        public const string COPS = "+COPS";
        public const string CPAS = "+CPAS";
        public const string CPIN = "+CPIN";
        public const string CPMS = "+CPMS";
        public const string CPOL = "+CPOL";
        public const string CPWD = "+CPWD";
        public const string CREG = "+CREG";
        public const string CRSM = "+CRSM";
        public const string CSMS = "+CSMS";
        public const string CSQ = "+CSQ";
        public const string CSSN = "+CSSN";
        public const string CUSD = "+CUSD";
        public const string VTS = "+VTS";
        public const string CBST = "+CBST";
        public const string CDIP = "+CDIP";
        public const string CEER = "+CEER";
        public const string CGACT = "+CGACT";
        public const string CGATT = "+CGATT";
        public const string CGCLASS = "+CGCLASS";
        public const string CGCMOD = "+CGCMOD";
        public const string CGDATA = "+CGDATA";
        public const string CGDCONT = "+CGDCONT";
        public const string CGDSCONT = "+CGDSCONT";
        public const string CGEQMIN = "+CGEQMIN";
        public const string CGEQNEG = "+CGEQNEG";
        public const string CGEQREQ = "+CGEQREQ";
        public const string CGEREP = "+CGEREP";
        public const string CGMI = "+CGMI";
        public const string CGPADDR = "+CGPADDR";
        public const string CGQMIN = "+CGQMIN";
        public const string CGQREQ = "+CGQREQ";
        public const string CGREG = "+CGREG";
        public const string CGTFT = "+CGTFT";
        public const string CHSC = "+CHSC";
        public const string CHSD = "+CHSD";
        public const string CHSN = "+CHSN";
        public const string CHSR = "+CHSR";
        public const string CHSU = "+CHSU";
        public const string CLIR = "+CLIR";
        public const string CMEE = "+CMEE";
        public const string CMUX = "+CMUX";
        public const string COLP = "+COLP";
        public const string CPROT = "+CPROT";
        public const string CR = "+CR";
        public const string CRC = "+CRC";
        public const string CRLP = "+CRLP";
        public const string CSCS = "+CSCS";
        public const string CV120 = "+CV120";
        public const string CVHU = "+CVHU";
        public const string DR = "+DR";
        public const string DS = "+DS";
        public const string DT = "+DT";
        public const string FCLASS = "+FCLASS";
        public const string GCAP = "+GCAP";
        public const string GMI = "+GMI";
        public const string GMM = "+GMM";
        public const string ICF = "+ICF";
        public const string IFC = "+IFC";
        public const string ILRR = "+ILRR";
        public const string IPR = "+IPR";

        public const string WHWV = "+WHWV";
        public const string WDOP = "+WDOP";  
    }
}
