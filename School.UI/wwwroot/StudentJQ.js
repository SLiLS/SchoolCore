
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

$('.modal').click(function () {
    wrap.on('click', function (event) {
        var select = $('.content');
        if ($(event.target).closest(select).length)
            return;
        modal.fadeOut();
        wrap.unbind('click');
    });
});


//Modal
var wrap = $('#wrapper'),
    btn = $('.open-modal-btn'),
    modal = $(".cover .modal .content");

btn.on('click', function () {
    modal.fadeIn();
});

// close modal
$('.modal').click(function () {
    wrap.on('click', function (event) {
        var select = $('.content');
        if ($(event.target).closest(select).length)
            return;
        modal.fadeOut();
        wrap.unbind('click');
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
}
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
}



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
}
// создание строки для таблицы
var row = function (student) {
    return "<tr data-rowid='" + student.id + "'><td>" + student.id + "</td>" +
        "<td>" + student.name + "</td> <td>" + student.middleName + "</td>" +
        "<td>" + student.surName + "</td> <td>" + student.sex + "</td>" +
        "<td>" + student.className + "</td>" +

        "<td><a class='editLink open-modal-btn' data-id='" + student.id + "'>Изменить</a> | " +
        "<a class='removeLink' data-id='" + student.id + "'>Удалить</a></td></tr>";
}

// отправка формы
$(".createStudentForm").submit(function (e) {
    e.preventDefault();
    var id = this.elements["id"].value;
    var name = this.elements["name"].value;
    var middleName = this.elements["middleName"].value;
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

    $(".cover, .modal, .content").fadeIn();
    Getstudent(id);
})

$("body").on("click", ".searchStudent", function () {
    var sex = $(this).data("sex");
    var schoolClass = $(this).data("schoolClass");

  GetStudentSearch(sex,schoolClass)
    
})
// нажимаем на ссылку Удалить
$("body").on("click", ".removeLink", function () {
    var id = $(this).data("id");
    Deletestudent(id);
})

    