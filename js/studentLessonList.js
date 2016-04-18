var countPopupTimes = 0;
var json = {};
var LessonsList;
var selectedID;
var UID = "";
var SID = "";
var SName = "";
var CSALURL = "http://ace.autotutor.org/csalpublicversion/mainpage.html";
var CSALServerURL = "http://ace.autotutor.org/csalpublicversion";
var CSALScriptfolderurl = CSALServerURL + "/scripts/";
var ETURL = "";
var getIpAddress;
var isTried = false;
var isLogin = false;
var lessonGroup;
var count = 0;
var classObj;
var classnames;

$(document).ready(function () {
    Lock();
    classnames = localStorage.className;
    getStudentLessonList();
    parent.returnURL = localStorage.returnURL;
    UID = localStorage.UID;
    //localStorage.clear;  // remember clear the localStorage
});

//lesson function
function getStudentLessonList() {
    method = "GET";
    content = { key: "getClassInfoByClassName", searchKey: classnames };
    Url = serverUrl + "/api/Classes/";
    $.ajax({

        url: Url,
        type: method,
        dataType: 'json',
        data: content,


        success: function (data) {
            lessonGroup=data[0].LessonGroup;
            localStorage.setItem("LessonGroup", data[0].LessonGroup)
            classObj = data;
            var today = new Date();
            var startTime = new Date(data[0]["StudyStartTime"]);
            if (today.getTime() < startTime.getTime() || data[0]["ClassStatus"].toLowerCase() === "inactive") {
                $(".pd-20").hide();
                $("#alert").append("<p><strong>You don't have class now. Please contact your teacher!</strong></p>");
                $("#alert").css("color", "red");
                $("#alert").css('margin-left', 100 + 'px');
                $("#alert").css('margin-top', 50 + 'px');
                //alert("")
                return;
            }
            //$("#LessonsList").append(" <thead><th width='5%'>NO.</th><th width='15%'>Lesson Name</th><th width='20%'>Lesson Video</th><th width='55%'>Description</th></thead>");
            console.log(".net Output:");
            LessonsList = $.map(data[0]["SeletedLeassons"], function (el) { return el });
            var countNO = 0;

            //revise for display video
            if (lessonGroup == "ET") {
                $("#LessonsList").append(" <thead><th width='5%'>NO.</th><th width='15%'>Lesson Name</th><th width='75%'>Description</th></thead>");
                $.each(data[0]["SeletedLeassons"], function (index, array) { //loop  items for display
                    countNO++;
                    //$("#LessonsList").append("<tr align='center'><td>" + countNO + "</td><td id=" + array['LessonID'] + " >" + array['LessonName'] + "</td><td>" + array['Description'] + "</td></tr>");
                    $("#LessonsList").append("<tr align='center'><td>" + countNO + "</td><td id=" + array['LessonID'] + " >" + array['LessonName'] + "</td><td>" + array['Description'] + "</td></tr>");

                });
            }

            if (lessonGroup == "CSAL") {
                $("#LessonsList").append(" <thead><th width='5%'>NO.</th><th width='15%'>Lesson Name</th><th width='20%'>Lesson Video</th><th width='55%'>Description</th></thead>");
                $.each(data[0]["SeletedLeassons"], function (index, array) { //loop  items for display
                    countNO++;
                    var videoUrl = array['VideoURL'];
                    if (videoUrl == null || videoUrl == "") {
                        $("#LessonsList").append("<tr align='center'><td>" + countNO + "</td><td id=" + array['LessonID'] + " >" + array['LessonName'] + "</td><td>No video</td><td>" + array['Description'] + "</td></tr>");
                    }
                    else {
                        //$("#LessonsList").append("<tr align='center'><td>" + countNO + "</td><td id=" + array['LessonID'] + " >" + array['LessonName'] + "</td><td>" + array['Description'] + "</td></tr>");
                        $("#LessonsList").append("<tr align='center'><td>" + countNO + "</td><td id=" + array['LessonID'] + " >" + array['LessonName'] + "</td><td>" + "<u style='cursor:pointer' class='text-primary' onclick='layer_show(" + "\"" + array['LessonName'] + "\"" + "," + "\"" + videoUrl + "\"" + ",\"1030\",\"640\")'>" + "Introduction Video" + "</u></td><td>" + array['Description'] + "</td></tr>");
                    }
                });
            }
            


        },
        complete: function () { //
            $('tbody > tr', $('#LessonsList')).click(function () {
                $('.selected').removeClass('selected');
                $(this).addClass('selected'); //this 
                var $td = $(this).children('td')[1];
                selectedID = $td.id;
                $('#selectItemName').html("You have selected  a lesson : " + $td.innerHTML + ".");
                Unlock("#ShowNickName");

            }).hover(		//
                   function () {
                       $(this).addClass('mouseOver');
                   },
                   function () {
                       $(this).removeClass('mouseOver');
                   }
               );

        },

    });
}


function ShowNickNameList(title, url, w, h) {
    countPopupTimes++;
    layer_show(title, url, w, h);
}

function Lock() {
    //unbinds click function and hover
    $('.btn-success').prop('disabled', true); //TO DISABLED
    //sets disabled mouse cursor
    $('.btn-success').css('cursor', 'not-allowed');

}
function Unlock(getElementID) {
    $(getElementID).prop('disabled', false); //TO DISABLED
    $(getElementID).css('cursor', 'pointer');
}

function startLesson() {
    count++;

    for (i = 0; i < LessonsList.length; i++) {
        var LessonID = LessonsList[i]["LessonID"];
        var LessonName = LessonsList[i]["LessonName"];
        var LessonGroup = LessonsList[i]["LessonGroup"];
        if (LessonsList[i]["LessonGroup"] === undefined) {
            LessonGroup = classObj[0]["ClassName"];
            LessonGroup = localStorage.getItem("LessonGroup");
        }
        var LessonURL = LessonsList[i]["LessonURL"]
        if (LessonID == selectedID) {
            json = LessonsList[i];
            PopUpLesson(LessonID, LessonName, LessonGroup, LessonURL);
            return;
        }

    }
}



function getSession() {
    alert(sessionStorage.getItem("UserName"));

}
function getValueFromPopupWindow(id, Name) {
    SID = id;
    SName = Name;
    $('#selectedNickName').html("You have selected  a Name : " + Name + ".");
    Unlock("#StartLesson");
}

function PopUpLesson(LessonID, LessonName, LessonGroup, lessonURL) {


    if (LessonGroup == "CSAL") {
        countPopupTimes++;
        lessonURL = lessonURL + "&UID=" + UID + "&SID=" + SID;

        layer_show(LessonName, lessonURL, "1030", "740");
        var PopUpIframeID = "layui-layer-iframe" + countPopupTimes;
        $("#" + PopUpIframeID).attr("scrolling", "no");
        /*
        document.getElementById(PopUpIframeID).onload = function () {

            var iframe = document.getElementById(PopUpIframeID);
            json["SID"] = SID;
            //json["SID"] = "C31";
            json["UID"] = UID;
            json["LessonActivity"] = LessonID;
            //json["VisiorIp"] = getIpAddress;
            json["ServerUrl"] = CSALServerURL;
            json["LessonURL"] = CSALServerURL + json["LessonURL"];
            json["ScriptURL"] = CSALServerURL + json["ScriptURL"];
            json["scriptfolderurl"] = CSALScriptfolderurl;

            iframe.contentWindow.postMessage(json, '*');

        }*/
    }
    else if (LessonGroup == "ET") {
        countPopupTimes++;

        ETURL = lessonURL + "&username=" + SName + "&PUUID=" + UID;
        layer_show(LessonName, ETURL, "850", "740");
        var PopUpIframeID = "layui-layer-iframe" + countPopupTimes;
    }

}


