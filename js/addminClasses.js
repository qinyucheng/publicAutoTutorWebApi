var json = {};
var selectedID;
var lessonGroup;
var count = 0;
var classObj;
var classStatusAction;
var pagePath = "teacher_reviseClass.html";
var allClassInformation;
var selectedClassInformation;
var TeacherEmail;

function search() {
    var searchKey = $('#searchKey').val().trim();
    if (searchKey == null || searchKey == "") {
        content ="";

    }
    else {
        content = { "key": "advanceSearch", "searchKey": $('#searchKey').val().trim(), "TeacherEmail":""};
    }
    callAPI(content);

}

function callAPI(content) {
    method = "GET";
    //content = { "key": "advanceSearch", "searchKey": $('#searchKey').val().trim(), "TeacherEmail": "" };
    Url = '/api/Classes';
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
            allClassInformation = $.map(data, function (el) { return el });
            $("#ClassList").append(" <thead><tr class='text-c'><th width='5%'>NO.</th width='15%'><th>Teacher Email</th><th width='15%'>Class Name</th><th width='20%'>Study StartTime</th><th width='20%'>Class Status</th><th width='20%'>Operations</th></tr></thead>");

            var countNO = 0;
            $.each(data, function (index, array) { //loop  items for display  
                    countNO++;
                    var pageName = array['ClassName'];
                    pagePath = pagePath + "?ClassName=" + pageName;
                    var dt = new Date(array['StudyStartTime']);
                    var time = (dt.getMonth() + 1).toString() + "-" + dt.getDate() + "-" + dt.getFullYear();
                    if (array['ClassStatus'] == "active") {
                        $("#ClassList").append("<tr class='text-c'><td>" + countNO + "</td><td>" + array["TeacherEmail"] + "</td><td><u id=" + array['ClassName'] + " style='cursor:pointer' class='text-primary' onclick='lessonAndStudent(id,\"lessonStudentInfo.html\",id,\"900\",\"600\")'>  " + array['ClassName'] + "</u></td><td>" + time + "</td><td class='td-status'>" + '<span class="label label-success radius">Active</span>' + "</td>" + "</td><td class='td-manage'><a style='text-decoration:none' id=" + array['ClassName'] + " onclick='class_stop(this,id)' href='javascript:;' title='Inactive'><i class='Hui-iconfont'>&#xe631;</i></a></td></tr>");
                    }
                    else
                        $("#ClassList").append("<tr class='text-c'><td>" + countNO + "</td><td>" + array["TeacherEmail"] + "</td><td><u id=" + array['ClassName'] + " style='cursor:pointer' class='text-primary' onclick='lessonAndStudent(id,\"lessonStudentInfo.html\",id,\"900\",\"600\")'>  " + array['ClassName'] + "</u></td><td>" + time + "</td><td class='td-status'>" + '<span class="label  radius">Inactive</span>' + "</td>" + "</td><td class='td-manage'><a style='text-decoration:none' id=" + array['ClassName'] + " onclick='class_start(this,id)' href='javascript:;' title='Inactive'><i class='Hui-iconfont'>&#xe631;</i></a></td></tr>");
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

/*Lesson active*/
function openClassTab(obj, id) {
    $.each(allClassInformation, function (index, array) {
        if (array["ClassName"] === id) {
            selectedClassInformation = array;
            return false;
        }
    }
);

    localStorage.setItem('seletedClass', JSON.stringify(selectedClassInformation));
    Hui_admin_tab(obj);
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
function class_stop(obj, id) {
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
    // Url = '/api/MongoDB/' + email;
    Url = '/api/Classes/ModifyClassStatus';

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
        if (array["ClassName"] === id) {
            selectedClassInformation = array;
            return false;
        }
    }
    );
    layer_show(title, url, w, h);
}