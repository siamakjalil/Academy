function UpdateTeacherDays(id, isActive) {
    var obj = {
        id: id,
        isActive: isActive
    }
    $.ajax({
        type: "POST",
        url: '/TeacherDays/Update',
        data: obj
    }).done(function (res) {
        $("#teachersDay-table").html(res);
    });
}