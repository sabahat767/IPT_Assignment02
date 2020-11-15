function CalculateOld() {
    //code for server side:
    var inputSubjectList = [];
  var noOfSubjects = $("#noOfSubjects").val();

  for (i = 0; i < noOfSubjects; i++) {
    subjectName = $("#subjectName" + i).val();
    subjectObtainedMarks = $("#subjectMarksObtained" + i).val();

    var input_subject = new Object();
    input_subject.name = subjectName;
    input_subject.obtainedMarks = subjectObtainedMarks;

    inputSubjectList.push(input_subject);//appending in the list of subjects
  }
  var inputSubjectStr = JSON.stringify(inputSubjectList);
 console.log(inputSubjectStr)
 $.ajax({
     method:"GET",
     URL:"http://localhost:51043/StudentService.asmx/GenerateMarksheet",
     contentType:"application/JSON",
     data:{
         request:inputSubjectStr,
     },
     success: function (result) {
        var resultt = JSON.parse(result.d);
        $("#minMarksSubject").val(resultt.minMarksSubject );
        $("#maxMarksSubject").val(resultt.maxMarksSubject);
        $("#minMarks").val(resultt.minMarks);
        $("#maxMarks").val(resultt.maxMarks);
        $("#percentage").val((resultt.percentage)+"%");
      },
 });
}

function UIPopulate() {
    var noOfSubjects = $('#noOfSubjects').val();

    var h = '';
    for (i = 0; i < noOfSubjects; i++) {
        h += '<tr>';

        h += '<td>';
        h += '<span class="text-white">Subject ' + (i + 1) + ' -> </span>';
        h += '</td>';

        h += '<td>';
        h += '<span class="text-white">Name :</span>';
        h += '</td>';

        h += '<td>';
        h += '<input type="text"  id="subjectName' + i + '"/>';
        h += '</td>';

        h += '<td>';
        h += '<span class="text-white">Marks Obtained :</span>';
        h += '</td>';

        h += '<td>';
        h += '<input type="number" min="0" max="100"  id="subjectMarksObtained' + i + '"/>';
        h += '</td>';

        h += '</tr>';
    }

    $('#inputTable').html(h);
}