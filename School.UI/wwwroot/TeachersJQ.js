
function Getteachers() {
    $.ajax({
        url: 'api/Teacher',
        type: 'GET',
        contentType: "application/json",
        success: function (teachers) {
            var rows = "";
            $.each(teachers, function (index, teacher) {
               
                rows += row(teacher);
            })
            $("table tbody").append(rows);
        }
    });
}
$(document).ready(function () {
    Getteachers();
    $('a[name=modal]').click(function (e) {
        
        e.preventDefault();

        var id = $(this).attr('href');

        var maskHeight = $(document).height();
        var maskWidth = $(window).width();

        $('#mask').css({ 'width': maskWidth, 'height': maskHeight });

        $('#mask').fadeIn(1000);
        $('#mask').fadeTo("slow", 0.8);

        var winH = $(window).height();
        var winW = $(window).width();

        $(id).css('top', winH / 2 - $(id).height() / 1);
        $(id).css('left', winW / 2 - $(id).width() / 1);

        $(id).fadeIn(2000);
    });

    $('.window .close').click(function (e) {

        e.preventDefault();
        $('#mask, .window').hide();
    });

    $('#mask').click(function () {
        $(this).hide();
        $('.window').hide();
    });
});





var row = function (teacher) {
    return "<tr data-rowid='" + teacher.id + "'><td>" + teacher.id + "</td>" +
        "<td>" + teacher.name + "</td> <td>" + teacher.middleName + "</td>" +
        "<td>" + teacher.surName + "</td> <td>" + teacher.position + "</td>" +
        "<td>" + teacher.studentCount + "</td>" +
        "<td><a href='#dialog'  name='modal' class='editLink open-modal-btn btn btn-success' data-id='" + teacher.id + "'>Изменить</a> | " +
        "<a class='removeLink btn btn-success' data-id='" + teacher.id + "'>Удалить</a></td></tr>";
}
function GetTeacher(id) {
    $.ajax({
        url: '/api/Teacher/' + id,
        type: 'GET',
        contentType: "application/json",
        success: function (teacher) {

            var form = document.forms["createTeacherForm"];
            form.elements["id"].value = teacher.id;
            form.elements["name"].value = teacher.name;
            form.elements["middleName"].value = teacher.middleName;
            form.elements["surName"].value = teacher.surName;
            form.elements["position"].value = teacher.position;
        

        }
    });
}
function Createteacher(teachername, teachermiddleName, teachersurName, teacherposition) {
    $.ajax({
        url: "api/Teacher",
        contentType: "application/json",
        method: "POST",
        data: JSON.stringify({
            name: teachername,
            middleName: teachermiddleName,
            surName: teachersurName,
            position: teacherposition
           
        }),
        success: function (teacher) {
            reset();
            Getteachers();
            
        }
    })
};
function Editteacher(teacherId, teacherName, teachermiddleName, teachersurName, teacherposition) {
    $.ajax({
        url: "api/Teacher",
        contentType: "application/json",
        method: "PUT",
        data: JSON.stringify({
            id: teacherId,
            name: teacherName,
            middleName: teachermiddleName,
            surName: teachersurName,
            position: teacherposition
          
        }),
        success: function (teacher) {

            reset();
            Getteachers();
        }
    })
};

function Deleteteacher(id) {
    $.ajax({
        url: "api/Teacher/" + id,
        contentType: "application/json",
        method: "DELETE",
        success: function (teacher) {
            $("tr[data-rowid='" + teacher.id + "']").remove();

        }
    })
};

$("body").on("click", ".teachersend", function () {

    var id = document.forms["createTeacherForm"].elements["id"].value;

    var name = document.forms["createTeacherForm"].elements["name"].value;

    var middleName = document.forms["createTeacherForm"].elements["middleName"].value;

    var surName = document.forms["createTeacherForm"].elements["surName"].value;
    var position = document.forms["createTeacherForm"].elements["position"].value;
   

    if (id == 0)
        Createteacher(name, middleName, surName,position);
    else
        Editteacher(id, name, middleName, surName, position);
});


$("body").on("click", ".editLink", function () {
    var id = $(this).data("id");
    
   
    GetTeacher(id);
    $('#mask, .window').show();


});


$("div").on("click", "#reset", function (e) {

    e.preventDefault();

    reset();
});

function reset() {

    var form = document.forms["createTeacherForm"];
    form.reset();

    form.elements["id"].value = 0;
}

$("body").on("click", ".removeLink", function () {
    var id = $(this).data("id");
    Deleteteacher(id);
});