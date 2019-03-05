

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


function GetStudents() {
    $.ajax({
        url: '/api/values',
        type: 'GET',
        contentType: "application/json",
        success: function (students) {
           
            var rows = "";
            $.each(students, function (index, student) {
            
                rows += row(student);
            })
            $("table tbody").html(rows);
        }
    });
}

function GetStudentSearch(studentsex, schoolClass) {
    $.ajax({
        url: '/api/values/'+studentsex+'/'+schoolClass,      
        type: 'GET',
        contentType: "application/json",
        success: function (students) {
            
            var rows = "";
            $.each(students, function (index, student) {
               
                rows += row(student);
            })
            $("table tbody").html(rows);
        }
    });
}

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

var row = function (student) {
    return "<tr data-rowid='" + student.id + "'><td>" + student.id + "</td>" +
        "<td>" + student.name + "</td> <td>" + student.middleName + "</td>" +
        "<td>" + student.surName + "</td> <td>" + student.sex + "</td>" +
        "<td>" + student.className + "</td>" +

        "<td><a href='#dialog'  name='modal'  class='editLink open-modal-btn btn btn-success' data-id='" + student.id + "'>Изменить</a> | " +
        "<a class='removeLink btn btn-success' data-id='" + student.id + "'>Удалить</a></td></tr>";
};


$("body").on("click", ".studentsend", function () {   
    
    var id = document.forms["createStudentForm"].elements["id"].value;
    
    var name = document.forms["createStudentForm"].elements["name"].value;
    
    var middleName = document.forms["createStudentForm"].elements["middleName"].value;
   
    var surName = document.forms["createStudentForm"].elements["surName"].value;
    var sex = document.forms["createStudentForm"].elements["sex"].value;
    var classId = document.forms["createStudentForm"].elements["schoolClassId"].value;

    if (id == 0)
        Createstudent(name, middleName, surName, sex, classId);
    else
        Editstudent(id, name, middleName, surName, sex, classId);
});


$("body").on("click", ".editLink", function () {
    var id = $(this).data("id");
    
    GetStudent(id);
    $('#mask, .window').show();
 
   
});

$("div").on("click", ".searchStudent", function (event) {
    
    event.preventDefault();

    var sex = $('input[name=sex]:checked').val();
    var schoolClass = "1";
    schoolClass += document.forms["searchingStudentForm"].elements["schoolClass"].value;
  
    GetStudentSearch(sex, schoolClass)

});

$("div").on("click", "#reset", function (e) {
    
    e.preventDefault();
    
    reset();
});

function reset() {
    
    var form = document.forms["createStudentForm"];
    form.reset();
    
    form.elements["id"].value = 0;
}

$("body").on("click", ".removeLink", function () {
    var id = $(this).data("id");
    Deletestudent(id);
});

    