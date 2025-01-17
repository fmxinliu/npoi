﻿using NPOI.OpenXmlFormats.Shared;
using NPOI.XWPF.UserModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace NPOI.XWPF.Usermodel
{
    public abstract class MathContainer : IRunBody
    {
        protected IRunBody parent;
        protected XWPFDocument document;
        protected IOMathContainer container;

        protected List<XWPFSharedRun> runs;
        protected List<XWPFNary> naries;
        protected List<XWPFAcc> accs;
        protected List<XWPFSSub> sSubs;
        protected List<XWPFF> fs;

        public MathContainer(IOMathContainer c, IRunBody p)
        {
            container = c;
            parent = p;
            document = p.Document;

            FillLists(c.Items);
        }

        public XWPFDocument Document
        {
            get { return document; }
        }

        public POIXMLDocumentPart Part { get { return parent.Part; } }

        private void FillLists(ArrayList items)
        {
            runs = new List<XWPFSharedRun>();
            accs = new List<XWPFAcc>();
            naries = new List<XWPFNary>();
            sSubs = new List<XWPFSSub>();
            fs = new List<XWPFF>();

            BuildListsInOrderFromXml(items);
        }

        private void BuildListsInOrderFromXml(ArrayList items)
        {
            foreach (object o in items)
            {
                if (o is CT_R)
                {
                    runs.Add(new XWPFSharedRun(o as CT_R, this));
                }
                if (o is CT_Acc)
                {
                    accs.Add(new XWPFAcc(o as CT_Acc, this));
                }

                if (o is CT_Nary)
                {
                    naries.Add(new XWPFNary(o as CT_Nary, this));
                }

                if (o is CT_SSub)
                {
                    sSubs.Add(new XWPFSSub(o as CT_SSub, this));
                }

                if (o is CT_F)
                {
                    fs.Add(new XWPFF(o as CT_F, this));
                }
            }
        }

        public XWPFSharedRun CreateRun()
        {
            XWPFSharedRun run = new XWPFSharedRun(container.AddNewR(), this);
            runs.Add(run);
            return run;
        }

        /// <summary>
        /// Create Accent
        /// </summary>
        /// <returns></returns>
        public XWPFAcc CreateAcc()
        {
            XWPFAcc acc = new XWPFAcc(container.AddNewAcc(), this);
            accs.Add(acc);
            return acc;
        }

        /// <summary>
        /// Create n-ary Operator Object
        /// </summary>
        /// <returns></returns>
        public XWPFNary CreateNary()
        {
            XWPFNary nary = new XWPFNary(container.AddNewNary(), this);
            naries.Add(nary);
            return nary;
        }

        /// <summary>
        /// Subscript Object
        /// </summary>
        /// <returns></returns>
        public XWPFSSub CreateSSub()
        {
            XWPFSSub ssub = new XWPFSSub(container.AddNewSSub(), this);
            sSubs.Add(ssub);
            return ssub;
        }

        /// <summary>
        /// Fraction Object
        /// </summary>
        /// <returns></returns>
        public XWPFF CreateF()
        {
            XWPFF f = new XWPFF(container.AddNewF(), this);
            fs.Add(f);
            return f;
        }

        public IList<XWPFSharedRun> Runs
        {
            get
            {
                return runs.AsReadOnly();
            }
        }

        public IList<XWPFAcc> Accs
        {
            get
            {
                return accs.AsReadOnly();
            }
        }

        public IList<XWPFNary> Naries
        {
            get
            {
                return naries.AsReadOnly();
            }
        }

        public IList<XWPFSSub> SSubs
        {
            get
            {
                return sSubs.AsReadOnly();
            }
        }

        public IList<XWPFF> Fs
        {
            get
            {
                return fs.AsReadOnly();
            }
        }


    }
}
