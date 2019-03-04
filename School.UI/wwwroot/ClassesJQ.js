GetClasses();
// Получение всех классы
function GetClasses() {
    $.ajax({
        url: 'api/SchoolClasses',
        type: 'GET',
        contentType: "application/json",
        success: function (schoolClasss) {
            var rows = "";
            $.each(schoolClasss, function (index, schoolClass) {
                // добавляем полученные элементы в таблицу
                rows += row(schoolClass);
            })
            $("table tbody").append(rows);
        }
    });
}


var row = function (schoolClass) {
    return "<tr data-rowid='" + schoolClass.id + "'><td>" + schoolClass.id + "</td>" +
        "<td>" + schoolClass.name + "</td> <td>" + schoolClass.studentCount + "</td>";   
}