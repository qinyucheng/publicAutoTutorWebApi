﻿var json = {};
var selectedID;
var lessonGroup;
var count = 0;
var classObj;
var classStatusAction;
var pagePath = "teacher_reviseClass.html";
var allClassInformation;
var selectedClassInformation;
var TeacherEmail;

$(document).ready(function () {
   
});



function search() {
    var searchKey = $('#searchKey').val().trim();
    if (searchKey == null || searchKey == "") {
        content = "";

    }
    else {
        content = { "key": "advanceSearch", "searchKey": $('#searchKey').val().trim()};
    }
    callAPI(content);

}

function callAPI(content) {
    method = "GET";
    //content = { "key": "advanceSearch", "searchKey": $('#searchKey').val().trim(),"TeacherEmail":"" };
     Url = serverUrl+'/api/Classes';
    $.ajax({

        url: Url,
        type: method,
        dataType: 'json',
        data: content,
        success: function (data) {
            $("#ClassList tr").remove();
            $("#ClassList thead").remove();
            // $("#LessonsList tbody").remove();

            console.log(".net Output:");
            //classInformation = $.map(data, function (el) { return el });
            $("#ClassList").append("<thead><tr class='text-c'><th width='5%'>NO.</th><th width='15%'>Class Name</th><th width='25%'>Study StartTime</th><th width='25%'>Class Status</th><th width='25%'>Operations</th></tr></thead>");

            var countNO = 0;
           /* $.each(data, function (index, array) { //loop  items for display  
                countNO++;
                $("#ClassList").append("<tr class='text-c'><td>" + countNO + "</td><td><u id=" + array['ClassName'] + " style='cursor:pointer' class='text-primary' onclick='lessonAndStudent(id,\"lessonStudentInfo.html\",id,\"900\",\"700\")'>  " + array['ClassName'] + "</u></td><td>" + array['StudyStartTime'] + "</td><td>" + array['ClassStatus'] + "</td>" + "</td><td class='td-manage'><a style='text-decoration:none' id=" + array['LessonID'] + " onclick='lesson_stop(this,id)' href='javascript:;' title='Inactive'><i class='Hui-iconfont'>&#xe631;</i></a> <a title='Edit' id=" + array['LessonID'] + " href='javascript:;' onclick= 'class_edit(id,\"Edit\",\"admin_addLesson.html\",\"800\",\"500\")'; class='ml-5' style='text-decoration:none'><i class='Hui-iconfont'>&#xe6df;</i></a></td></tr>");
            });*/
            $.each(data, function (index, array) { //loop  items for display  
                if (array['TeacherEmail'] == TeacherEmail) {
                    countNO++;
                    var pageName = array['ClassName'];
                    pagePath = pagePath + "?ClassName=" + pageName;
                    var dt = new Date(array['StudyStartTime']);
                    var time = (dt.getMonth() + 1).toString() + "-" + dt.getDate() + "-" + dt.getFullYear();
                    if (array['ClassStatus'] == "active") {                  
                        //$("#ClassList").append("<tr class='text-c'><td>" + countNO + "</td><td><u id=" + array['ClassName'] + " style='cursor:pointer' class='text-primary' onclick='lessonAndStudent(id,\"lessonStudentInfo.html\",id,\"900\",\"600\")'>  " + array['ClassName'] + "</u></td><td>" + array['StudyStartTime'] + "</td><td class='td-status'>" + '<span class="label label-success radius">Active</span>' + "</td>" + "</td><td class='td-manage'><a style='text-decoration:none' id=" + array['ClassName'] + " onclick='class_stop(this,id)' href='javascript:;' title='Inactive'><i class='Hui-iconfont'>&#xe631;</i></a> <a _href=\"teacher_reviseClass.html\" data-title='My Profile' href=\"javascript:void(0)\">My Profile</a></td></tr>");
                        //$("#ClassList").append("<tr class='text-c'><td>" + countNO + "</td><td><u id=" + array['ClassName'] + " style='cursor:pointer' class='text-primary' onclick='lessonAndStudent(id,\"lessonStudentInfo.html\",id,\"600\",\"700\")'>  " + array['ClassName'] + "</u></td><td>" + array['StudyStartTime'] + "</td><td  class='td-status'>" + '<span class="label label-success radius">Active</span>' + "</td>" + "</td><td class='td-manage'><a style='text-decoration:none' id=" + array['ClassName'] + " onclick='class_stop(this,id)' href='javascript:;' title='Inactive'><i class='Hui-iconfont'>&#xe631;</i></a> <a title='Edit' id=" + array['ClassName'] + " href='javascript:void(0)' _href='teacher_reviseClass.html' data-title='My Report' onclick= 'class_edit(id,\"Edit\",\"teacher_reviseClass.html\",\"800\",\"500\")'; class='ml-5' style='text-decoration:none'><i class='Hui-iconfont'>&#xe6df;</i></a></td></tr>");
                        $("#ClassList").append("<tr class='text-c'><td>" + countNO + "</td><td><u id=" + array['ClassName'] + " style='cursor:pointer' class='text-primary' onclick='lessonAndStudent(id,\"lessonStudentInfo.html\",id,\"900\",\"600\")'>  " + array['ClassName'] + "</u></td><td>" + time + "</td><td class='td-status'>" + '<span class="label label-success radius">Active</span>' + "</td>" + "</td><td class='td-manage'><a style='text-decoration:none' id=" + array['ClassName'] + " onclick='class_stop(this,id)' href='javascript:;' title='Inactive'><i class='Hui-iconfont'>&#xe631;</i></a>&nbsp;&nbsp;&nbsp;&nbsp;</span><a _href='" + pagePath + "' data-title='" + pageName + "' href='javascript:void(0)'  id=" + array['ClassName'] + " class='openClassTab' onClick='openClassTab(this,id)'><i class='Hui-iconfont'>&#xe6df;</i></a></td></tr>");
                    }
                    else
                        //$("#ClassList").append("<tr class='text-c'><td>" + countNO + "</td><td><u id=" + array['ClassName'] + " style='cursor:pointer' class='text-primary' onclick='lessonAndStudent(id,\"lessonStudentInfo.html\",id,\"900\",\"600\")'>  " + array['ClassName'] + "</u></td><td>" + array['StudyStartTime'] + "</td><td class='td-status'>" + '<span class="label  radius">Inactive</span>' + "</td>" + "</td><td class='td-manage'><a style='text-decoration:none' id=" + array['ClassName'] + " onclick='class_start(this,id)' href='javascript:;' title='Inactive'><i class='Hui-iconfont'>&#xe615;</i></a> <a title='Edit' id=" + array['ClassName'] + " href='javascript:;' onclick= 'class_edit(id,\"Edit\",\"admin_addLesson.html\",\"800\",\"500\")'; class='ml-5' style='text-decoration:none'><i class='Hui-iconfont'>&#xe6df;</i></a></td></tr>");
                        $("#ClassList").append("<tr class='text-c'><td>" + countNO + "</td><td><u id=" + array['ClassName'] + " style='cursor:pointer' class='text-primary' onclick='lessonAndStudent(id,\"lessonStudentInfo.html\",id,\"900\",\"600\")'>  " + array['ClassName'] + "</u></td><td>" + time + "</td><td class='td-status'>" + '<span class="label  radius">Inactive</span>' + "</td>" + "</td><td class='td-manage'><a style='text-decoration:none' id=" + array['ClassName'] + " onclick='class_start(this,id)' href='javascript:;' title='Inactive'><i class='Hui-iconfont'>&#xe615;</i></a>&nbsp;&nbsp;&nbsp;&nbsp;<a _href='" + pagePath + "' data-title='" + pageName + "' href='javascript:void(0)'  id=" + array['ClassName'] + " class='openClassTab' onClick='openClassTab(this,id)'><i class='Hui-iconfont'>&#xe6df;</i></a></td></tr>");
                    }
                });
        },
        complete: function () { //
            $('tbody > tr', $('#ClassList')).click(function () {
                $('.selected').removeClass('selected');
                $(this).addClass('selected'); //this 
                var $td = $(this).children('td')[1];
                selectedID = $td.id;
                $('#selectItemName').html("You have selected  a lesson : " + $td.innerHTML + ".");
                //Unlock("#ShowNickName");

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
function getClassList() {
    var lessonsInfo = JSON.parse(localStorage.getItem('PersonInfo'));
    TeacherEmail = lessonsInfo.Email;
    method = "GET";
    content = { "TeacherEmail":TeacherEmail };                            //reset to teache eamil
     Url = serverUrl+'/api/Classes';
    $.ajax({

        url: Url,
        type: method,
        dataType: 'json',
        data: content,
        success: function (data) {
            $("#ClassList tr").remove();
            $("#ClassList thead").remove();
            // $("#LessonsList tbody").remove();
            if (data.length == 0) {
                $(".pd-20").hide();
                $("#alert").append("<p><strong>You don't have any class now. Please build class first!</strong></p>");
                $("#alert").css("color", "red");
                $("#alert").css('margin-left', 100 + 'px');
                $("#alert").css('margin-top', 50 + 'px');
                
            }
            else {
                $(".pd-20").show();
                allClassInformation = $.map(data, function (el) { return el });
                $("#ClassList").append(" <thead><tr class='text-c'><th width='5%'>NO.</th><th width='15%'>Class Name</th><th width='25%'>Study StartTime</th><th width='25%'>Class Status</th><th width='25%'>Operations</th></tr></thead>");
                if (data.length == 0) {

                }
                var countNO = 0;
                $.each(data, function (index, array) { //loop  items for display  
                    countNO++;
                    var pageName = array['ClassName'];
                    pagePath = pagePath + "?ClassName=" + pageName;
                    var dt = new Date(array['StudyStartTime']);
                    var time = (dt.getMonth() + 1).toString() + "-" + dt.getDate() + "-" + dt.getFullYear();

                    //var time = array['StudyStartTime'].match(/^\d{4}-\d\d-\d\d/);
                    if (array['ClassStatus'] == "active") {
                        //$("#ClassList").append("<tr class='text-c'><td>" + countNO + "</td><td><u id=" + array['ClassName'] + " style='cursor:pointer' class='text-primary' onclick='lessonAndStudent(id,\"lessonStudentInfo.html\",id,\"900\",\"700\")'>  " + array['ClassName'] + "</u></td><td>" + array['StudyStartTime'] + "</td><td class='td-status'>" + '<span class="label label-success radius">Active</span>' + "</td>" + "</td><td class='td-manage'><a style='text-decoration:none' id=" + array['ClassName'] + " onclick='class_stop(this,id)' href='javascript:;' title='Inactive'><i class='Hui-iconfont'>&#xe631;</i></a> <a title='Edit' id=" + array['ClassName'] + " href='javascript:;' onclick= 'class_edit(id,\"Edit\",\"admin_addLesson.html\",\"800\",\"500\")'; class='ml-5' style='text-decoration:none'><i class='Hui-iconfont'>&#xe6df;</i></a></td></tr>");
                        //$("#ClassList").append("<tr class='text-c'><td>" + countNO + "</td><td><u id=" + array['ClassName'] + " style='cursor:pointer' class='text-primary' onclick='lessonAndStudent(id,\"lessonStudentInfo.html\",id,\"600\",\"700\")'>  " + array['ClassName'] + "</u></td><td>" + array['StudyStartTime'] + "</td><td>" + '<span class="label label-success radius">Active</span>' + "</td>" + "</td><td class='td-manage'><a style='text-decoration:none' id=" + array['ClassName'] + " onclick='class_stop(this,id)' href='javascript:;' title='Inactive'><i class='Hui-iconfont'>&#xe631;</i></a> <a title='Edit' id=" + array['ClassName'] + " href='javascript:void(0)' _href='teacher_reviseClass.html' data-title='My Report' onclick= 'class_edit(id,\"Edit\",\"teacher_reviseClass.html\",\"800\",\"500\")'; class='ml-5' style='text-decoration:none'><i class='Hui-iconfont'>&#xe6df;</i></a></td></tr>");
                        $("#ClassList").append("<tr class='text-c'><td>" + countNO + "</td><td><u id=" + array['ClassName'] + " style='cursor:pointer' class='text-primary' onclick='lessonAndStudent(id,\"lessonStudentInfo.html\",id,\"900\",\"600\")'>  " + array['ClassName'] + "</u></td><td>" + time + "</td><td class='td-status'>" + '<span class="label label-success radius">Active</span>' + "</td>" + "</td><td class='td-manage'><a style='text-decoration:none' id=" + array['ClassName'] + " onclick='class_stop(this,id)' href='javascript:;' title='Inactive'><i class='Hui-iconfont'>&#xe631;</i></a>&nbsp;&nbsp;&nbsp;&nbsp;<a _href='" + pagePath + "' data-title='" + pageName + "' href='javascript:void(0)'  id=" + array['ClassName'] + " class='openClassTab' onClick='openClassTab(this,id)'><i class='Hui-iconfont'>&#xe6df;</i></a></td></tr>");

                    }
                    else
                        $("#ClassList").append("<tr class='text-c'><td>" + countNO + "</td><td><u id=" + array['ClassName'] + " style='cursor:pointer' class='text-primary' onclick='lessonAndStudent(id,\"lessonStudentInfo.html\",id,\"900\",\"600\")'>  " + array['ClassName'] + "</u></td><td>" + time + "</td><td class='td-status'>" + '<span class="label  radius">Inactive</span>' + "</td>" + "</td><td class='td-manage'><a style='text-decoration:none' id=" + array['ClassName'] + " onclick='class_start(this,id)' href='javascript:;' title='Inactive'><i class='Hui-iconfont'>&#xe615;</i></a>&nbsp;&nbsp;&nbsp;&nbsp;<a _href='" + pagePath + "' data-title='" + pageName + "' href='javascript:void(0)'  id=" + array['ClassName'] + " class='openClassTab' onClick='openClassTab(this,id)'><i class='Hui-iconfont'>&#xe6df;</i></a></td></tr>");
                    //$("#ClassList").append("<tr class='text-c'><td>" + countNO + "</td><td><u id=" + array['ClassName'] + " style='cursor:pointer' class='text-primary' onclick='lessonAndStudent(id,\"lessonStudentInfo.html\",id,\"600\",\"700\")'>  " + array['ClassName'] + "</u></td><td>" + array['StudyStartTime'] + "</td><td>" + '<span class="label  radius">Inactive</span>' + "</td>" + "</td><td class='td-manage'><a style='text-decoration:none' id=" + array['ClassName'] + " onclick='class_stop(this,id)' href='javascript:;' title='Inactive'><i class='Hui-iconfont'>&#xe631;</i></a><a _href='teacher_MyClass.html' data-title='My Classes' href='javascript:void(0)'>My Classes</a></td></tr>");
                });

            }
           
           
        },
        complete: function () { //
            $('tbody > tr', $('#ClassList')).click(function () {
                $('.selected').removeClass('selected');
                $(this).addClass('selected'); //this 
                var $td = $(this).children('td')[1];
                selectedID = $td.id;
                $('#selectItemName').html("You have selected  a lesson : " + $td.innerHTML + ".");

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

/*Lesson active*/
function openClassTab(obj,id) {
    $.each(allClassInformation, function (index, array) {
        if (array["ClassName"] === id) {
            selectedClassInformation = array;
            return false;
        }
    }
);

    localStorage.setItem('seletedClass', JSON.stringify(selectedClassInformation));
    Hui_admin_tab(obj, id);
}

//jump to build class
function class_edit(id, page, height, width, z) {
    $.each(allClassInformation, function (index, array) {
        if (array["ClassName"] === id) {
            selectedClassInformation = array;
            return false;
        }
    }
);

localStorage.setItem('seletedClass', JSON.stringify(selectedClassInformation));
}
//set class active to inactive
function class_stop(obj,id) {
    layer.confirm('Whether you want to inactive this class?', function (index) {
        changeClassStatus(obj, id, "inactive");
        classStatusAction = "inactive";
        StatusActionObj = obj;

    });
}
function class_start(obj, id) {
    layer.confirm('Do you want to active this class?', function (index) {
        changeClassStatus(obj, id, "active");
        classStatusAction = "active";
        StatusActionObj = obj;

    });
}

function changeClassStatus(obj, id, Status) {
    content = { "ClassName": id, "ClassStatus": Status };
    method = "PUT";
    //  Url = serverUrl+'/api/MongoDB/' + email;
     Url = serverUrl+'/api/Classes/ModifyClassStatus';

    $.ajax({

        url: Url,
        type: method,
        data: content,

    }).success(function (data) {
        if (data == true && classStatusAction == "inactive") {
            $(obj).parents("tr").find(".td-manage").prepend('<a onClick="class_start(this,id)" id=' + id + '  "href="javascript:;" title="Active" style="text-decoration:none"><i class="Hui-iconfont">&#xe615;</i></a>');
            $(obj).parents("tr").find(".td-status").html('<span class="label label-default radius">Inactive</span>');
            $(obj).remove();
            layer.msg('Already Inactived!', { icon: 5, time: 1000 });
            //updateUserList(id, Status);

        }
        else if (data == true && classStatusAction == "active") {
            $(obj).parents("tr").find(".td-manage").prepend('<a onClick="class_stop(this,id)" id=' + id + ' href="javascript:;" title="Inactive" style="text-decoration:none"><i class="Hui-iconfont">&#xe631;</i></a>');
            $(obj).parents("tr").find(".td-status").html('<span class="label label-success radius">Active</span>');
            $(obj).remove();
            layer.msg('Already Actived!', { icon: 6, time: 1000 });
            //updateUserList(id, Status);
        }
        else {
            layer.msg('Status Changing fail!', { icon: 6, time: 1000 });
        }


    });
}


function lessonAndStudent(title, url, id, w, h) {
    url = url + "?userEmail=" + id;
    $.each(allClassInformation, function (index, array) {
        if (array["ClassName"] === id){
            selectedClassInformation = array;
            return false;
        }
    }
    );
    layer_show(title, url, w, h);
}