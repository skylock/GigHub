var GigsController = function (attendanceService) {
    var button;

    var fail = function (jqXhr) {
        alert(jqXhr.responseJSON);
    };

    var done = function () {
        var buttonText = button.text() === "Going" ? "Going ?" : "Going";

        $(button)
            .toggleClass("btn-info")
            .toggleClass("btn-default")
            .text(buttonText);

    };

    var toggleAttendance = function (e) {
        button = $(e.target);

        var gigId = button.attr("data-gig-id");

        if (button.hasClass("btn-default"))
            attendanceService.createAttendance(gigId, done, fail);
        else
            attendanceService.deleteAttendance(gigId, done, fail);
    };

    var init = function (container) {
        $(container).on("click", ".js-toggle-attendance", toggleAttendance);
    };

    return {
        init: init
    };
}(AttendanceService);