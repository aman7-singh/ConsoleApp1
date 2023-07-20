using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Uilities
{
    class Program
    {
        static void Main(string[] args)
        {

            var filepaths = new List<string>
            {
                @"C:\Users\320127545\Downloads\Rough\2f8b390e-aae6-490e-abdc-d38631428b30(Abdomen_Base_64).xml",
                @"C:\Users\320127545\Downloads\Rough\461e4b53-6f41-4b80-948d-17fb7d45ac80(Abdomen_kv_120-100).xml",
                @"C:\Users\320127545\Downloads\Rough\b2aafd4e-4d91-4a3f-8713-d7ead9a631d3(Abdomen_dri_27-37).xml",
                @"C:\Users\320127545\Downloads\Rough\f4c32031-5d38-44e5-a691-700ba021d355(Abdomen_MaS_281-224).xml",
                @"C:\Users\320127545\Downloads\Rough\a3ccd949-5516-4b9a-b5e6-563bcabfaadd(Abdomen_Col_64-32 0.625).xml"


                //@"C:\Users\320127545\Downloads\Rough\iPatent\fb777163-0886-498b-8ff0-50ece1177909(Abdomen_base).xml",
                //@"C:\Users\320127545\Downloads\Rough\iPatent\bede4730-807e-4660-a92f-57478a23ab48(Abdomen_KV120-100).xml",
                //@"C:\Users\320127545\Downloads\Rough\iPatent\94390ca3-869f-40d2-9bc4-25f7d9d984e6(Abdomen_MAS281-72).xml",
                //@"C:\Users\320127545\Downloads\Rough\iPatent\681cc360-a153-499d-ab58-58fef12d02ff(Abdomen_dri27-37).xml"

            };
            //string filepath = @"C:\Users\320127545\Downloads\Rough\iPatent\fb777163-0886-498b-8ff0-50ece1177909(Abdomen_base).xml";
            //string filepath = @"C:\Users\320127545\Downloads\Rough\iPatent\bede4730-807e-4660-a92f-57478a23ab48(Abdomen_KV120-100).xml";
            //string filepath = @"C:\Users\320127545\Downloads\Rough\iPatent\94390ca3-869f-40d2-9bc4-25f7d9d984e6(Abdomen_MAS281-72).xml";
            //string filepath = @"C:\Users\320127545\Downloads\Rough\iPatent\681cc360-a153-499d-ab58-58fef12d02ff(Abdomen_dri27-37).xml";
            foreach (var filepath in filepaths)
            {
                RemoveLineFile(filepath, "<ID>");
                RemoveLineFile(filepath, "<StepListID>");
                RemoveLineFile(filepath, "<AccumulatedPlannedBodyCTDI>");
                RemoveLineFile(filepath, "<CurrentEditedStepID>");
                RemoveLineFile(filepath, "<CurrentEditedResultID>");
                RemoveLineFile(filepath, "<SequenceID>");
                RemoveLineFile(filepath, "<StepID>");
                RemoveLineFile(filepath, "<ResultID>");
                RemoveLineFile(filepath, "<ReconID>");
                RemoveLineFile(filepath, "<StudyValue>");
                RemoveLineFile(filepath, "<StudyDate>");
                RemoveLineFile(filepath, "<ExamCardUID>");
                RemoveLineFile(filepath, "<PPSID>");
            }
        }

        private static void RemoveLineFile(string fileName, string textToRemoveLine)
        {
            var tempFile = Path.GetTempFileName();
            var linesToKeep = File.ReadLines(fileName).Where(l => !l.Contains(textToRemoveLine));

            File.WriteAllLines(tempFile, linesToKeep);

            File.Delete(fileName);
            File.Move(tempFile, fileName);
        }

    }
}
