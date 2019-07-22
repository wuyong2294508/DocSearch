using NPOI.HWPF;
using NPOI.HWPF.Extractor;
using NPOI.XWPF.Extractor;
using NPOI.XWPF.UserModel;
using org.apache.pdfbox.pdmodel;
using org.apache.pdfbox.util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace DocSearch.Controller
{
    class ReadDoc
    {
        /**
         * 读取docx，doc，pdf三种文件的内容，分别使用不同的第三方库，不依赖office和adobe。
         */
        public string readContent(string path)
        {
            //读取docx
            if (path.ToLower().EndsWith(".docx"))
            {
                Stream stream = File.OpenRead(path);
                XWPFDocument docx = new XWPFDocument(stream);
                XWPFWordExtractor extractor = new XWPFWordExtractor(docx);
                return extractor.Text;
            }
            else if (path.ToLower().EndsWith(".doc"))
            {
                Stream stream = File.OpenRead(path);
                HWPFDocument doc = new HWPFDocument(stream);
                WordExtractor extractor = new WordExtractor(doc);
                return extractor.Text;
            }
            else if (path.ToLower().EndsWith(".pdf"))
            {
                PDDocument pdf = PDDocument.load(path);
                PDFTextStripper pdfStripper = new PDFTextStripper();
                return pdfStripper.getText(pdf);
            }
            return null;
        }
    }
}
