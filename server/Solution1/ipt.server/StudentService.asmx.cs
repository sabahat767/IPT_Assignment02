using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;

namespace ipt.server
{
    /// <summary>
    /// Summary description for StudentService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class StudentService : System.Web.Services.WebService
    {
        public static List<subjectModel> subjects = new List<subjectModel>();

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = true)]
        public string GenerateMarksheet()
        {
            string subjectStr = HttpContext.Current.Request.Params["request"];
            subjectModel subject = JsonConvert.DeserializeObject<subjectModel>(subjectStr);
            //subjects.Add(subject);
            int totalMarks = 0;
            int minMarks = subjects[0].ObtainMarks;
            string minSubject = subjects[0].subjName;
            int maxMarks = subjects[0].ObtainMarks;
            string maxSubject = subjects[0].subjName;
            for (int i = 0; i < subjects.Count; i++)
            {
                totalMarks += subjects[i].ObtainMarks;
                if (minMarks > subjects[i].ObtainMarks)
                {
                    minMarks = subjects[i].ObtainMarks;
                    minSubject = subjects[i].subjName;
                }

                if (maxMarks < subjects[i].ObtainMarks)
                {
                    maxMarks = subjects[i].ObtainMarks;
                    maxSubject = subjects[i].subjName;
                }
            }

            int per = (totalMarks / (subjects.Count * 100)) * 100;


            MarksheetGenModel marksheetModel = new MarksheetGenModel();
            marksheetModel.percentage = per;
            marksheetModel.minMarks = minMarks;
            marksheetModel.maxMarks = maxMarks;
            marksheetModel.minMarksSubject = minSubject;
            marksheetModel.maxMarksSubject = maxSubject;

            string str = JsonConvert.SerializeObject(marksheetModel);
            return str;
        }
        public class MarksheetGenModel
        {
            public string minMarksSubject { get; set; }
            public int minMarks { get; set; }
            public string maxMarksSubject { get; set; }
            public int maxMarks { get; set; }
            public int percentage { get; set; }

        }

        public class subjectModel
        {
            public string subjName { get; set; }
            public int ObtainMarks { get; set; }

        }
    }
}
