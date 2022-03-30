$(document).ready(function () {
    $('.my-select').select2({
        placeholder: "انتخاب استاد"
    });
    $('.class-type-teacher').select2({
        placeholder: "انتخاب استاد"
    });
    $('.marker-select').select2({
        placeholder: "انتخاب مصحح"
    });
    debugger;
    var teacherSelected = $("#teacherSelected").val();
    if (teacherSelected !== undefined && teacherSelected !== "" && teacherSelected !== null) {
        var selected = teacherSelected.split(',');
        $('.class-type-teacher').val(selected);
        $('.class-type-teacher').trigger('change');
    }
});
