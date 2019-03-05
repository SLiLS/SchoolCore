Getteachers();
// Получение всех учителей
function Getteachers() {
    $.ajax({
        url: 'api/Stat',
        type: 'GET',
        contentType: "application/json",
        success: function (teachers) {
            var rows = "";
            $.each(teachers, function (index, teacher) {
                // добавляем полученные элементы в таблицу
                rows += row(teacher);
            })
            $("table tbody").append(rows);
        }
    });
}


var row = function (teacher) {
    return "<tr data-rowid='" + teacher.id + "'><td>" + teacher.id + "</td>" +
        "<td>" + teacher.surName + "</td> <td>" + teacher.middleName + "</td>" +
        "<td>" + teacher.surName + "</td> <td>" + teacher.position + "</td>" +
        "<td>" + teacher.studentCount + "</td>" ;
}