var lessonGroupList = ["CSAL", "ET"];


function groupOption() {
    for (var i = 0; i <= lessonGroupList.length - 1; i++)
        $("#group").append("<option value='" + lessonGroupList[i] + "'>" + lessonGroupList[i] + "</option>");
}