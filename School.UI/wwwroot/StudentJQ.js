

$(document).ready(function () {
    GetStudents();  
    $.ajax({
        type: "GET",
        url: "/api/SchoolClasses",
        data: "{}",
        success: function (data) {
            var s = '<option value="-1">Класс</option>';
            for (var i = 0; i < data.length; i++) {
                s += '<option value="' + data[i].id + '">' + data[i].name + '</option>';
            }
            $("#ClassesDropdown").html(s);
        }
    });
});

 $(document).ready(function() {
        //select all the a tag with name equal to modal
        $('a[name=modal]').click(function (e) {
            //Cancel the link behavior
            e.preventDefault();
            //Get the A tag
            var id = $(this).attr('href');
            //Get the screen height and width
            var maskHeight = $(document).height();
            var maskWidth = $(window).width();
            //Set heigth and width to mask to fill up the whole screen
            $('#mask').css({ 'width': maskWidth, 'height': maskHeight });
            //transition effect
            $('#mask').fadeIn(1000);
            $('#mask').fadeTo("slow", 0.8);
            //Get the window height and width
            var winH = $(window).height();
            var winW = $(window).width();
            //Set the popup window to center
            $(id).css('top', winH / 2 - $(id).height() / 1);
            $(id).css('left', winW / 2 - $(id).width() / 1);
            //transition effect
            $(id).fadeIn(2000);
        });
    //if close button is clicked
    $('.window .close').click(function (e) {
        //Cancel the link behavior
        e.preventDefault();
    $('#mask, .window').hide();
 });
 //if mask is clicked
 $('#mask').click(function () {
        $(this).hide();
    $('.window').hide();
 });
});

// Получение всех студентов
function GetStudents() {
    $.ajax({
        url: '/api/values',
        type: 'GET',
        contentType: "application/json",
        success: function (students) {
            
            var rows = "";
            $.each(students, function (index, student) {
                // добавляем полученные элементы в таблицу
                rows += row(student);
            })
            $("table tbody").append(rows);
        }
    });
}
//Поиск
function GetStudentSearch(studentsex, schoolClass) {
    $.ajax({
        url: '/api/values/'+studentsex+'/'+schoolClass,      
        type: 'GET',
        contentType: "application/json",
        success: function (students) {
            var rows = "";
            $.each(students, function (index, student) {
                // добавляем полученные элементы в таблицу
                rows += row(student);
            })
            $("table tbody").append(rows);
        }
    });
}
// Получение одного студента
function GetStudent(id) {
    $.ajax({
        url: '/api/values/' + id,
        type: 'GET',
        contentType: "application/json",
        success: function (student) {
          
            var form = document.forms["createStudentForm"];
            form.elements["id"].value = student.id;
            form.elements["name"].value = student.name;
            form.elements["middleName"].value = student.middleName;
            form.elements["surName"].value = student.surName;
            form.elements["sex"].value = student.sex;
            form.elements["schoolClassId"].value = student.schoolClassId;

        }
    });
}

// Добавление ученика
function Createstudent(studentname, studentmiddleName, studentsurName, studentsex, studentschoolClassId) {
    $.ajax({
        url: "api/values",
        contentType: "application/json",
        method: "POST",
        data: JSON.stringify({
            name: studentname,
            middleName: studentmiddleName,
            surName: studentsurName,
            sex: studentsex,
            schoolClassId: studentschoolClassId
        }),
        success: function (student) {      
            reset();
        
            $("table tbody").append(row(student));
        }
    })
};
// Изменение пользователя
function Editstudent(studentId, studentName, studentmiddleName, studentsurName, studentsex, schoolClassId) {
    $.ajax({
        url: "api/values",
        contentType: "application/json",
        method: "PUT",
        data: JSON.stringify({
            id: studentId,
            name: studentName,
            middleName: studentmiddleName,
            surName: studentsurName,
            sex: studentsex,
            schoolClassId: schoolClassId
        }),
        success: function (student) {
          
            reset();
            $("tr[data-rowid='" + student.id + "']").replaceWith(row(student));
        }
    })
};



// Удаление пользователя
function Deletestudent(id) {
    $.ajax({
        url: "api/values/" + id,
        contentType: "application/json",
        method: "DELETE",
        success: function (student) {
            $("tr[data-rowid='" + student.id + "']").remove();

        }
    })
};
// создание строки для таблицы
var row = function (student) {
    return "<tr data-rowid='" + student.id + "'><td>" + student.id + "</td>" +
        "<td>" + student.name + "</td> <td>" + student.middleName + "</td>" +
        "<td>" + student.surName + "</td> <td>" + student.sex + "</td>" +
        "<td>" + student.className + "</td>" +

        "<td><a href='#dialog'  name='modal'  class='editLink open-modal-btn btn btn-success' data-id='" + student.id + "'>Изменить</a> | " +
        "<a class='removeLink btn btn-success' data-id='" + student.id + "'>Удалить</a></td></tr>";
};

// отправка формы
$("form").submit(function (e) {
    alert("otpravka");
    e.preventDefault();
  
    var id = document.forms["createStudentForm"].elements["id"].value;
    var name = document.forms["createStudentForm"].elements["name"].value;
    var middleName = document.forms["createStudentForm"].elements["middleName"].value;
    var surName = this.elements["surName"].value;
    var sex = this.elements["sex"].value;
    var classId = this.elements["schoolClassId"].value;
    if (id == 0)
        Createstudent(name, middleName, surName, sex, classId);
    else
        Editstudent(id, name, middleName, surName, sex, classId);
});

// нажимаем на ссылку Изменить
$("body").on("click", ".editLink", function () {
    var id = $(this).data("id");
    
    GetStudent(id);
   
});

$("body").on("click", ".searchStudent", function () {
    var sex = $(this).data("sex");
    var schoolClass = $(this).data("schoolClass");

    GetStudentSearch(sex, schoolClass)

});
// сброс значений формы
$("#reset").click(function (e) {

    e.preventDefault();
    reset();
});
// сброс формы
function reset() {
    var form = document.forms["userForm"];
    form.reset();
    form.elements["id"].value = 0;
}
// нажимаем на ссылку Удалить
$("body").on("click", ".removeLink", function () {
    var id = $(this).data("id");
    Deletestudent(id);
});

    