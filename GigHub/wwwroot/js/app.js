var AttendanceService = function () {
    var createAttendance = function (gigId, done, fail) {
        $.post("/api/attendances", { gigId: gigId })
            .done(done)
            .fail(fail);

    };

    var deleteAttendance = function (gigId, done, fail) {
        $.ajax({
                url: "/api/attendances/" + gigId,
                method: "DELETE"
            })
            .done(done)
            .fail(fail);
    };

    return {
        createAttendance: createAttendance,
        deleteAttendance: deleteAttendance
    };
}();
var NotificationService = function() {
    var getNew = function (onDone) {
        $.getJSON("/api/notifications/getNewNotifications", onDone);
    };

    return {
        getNew     : getNew
    };
}();
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
var Notifications = function (notificationService) {

    var removeCountBadge = function() {
        $(".js-notifications-count")
            .text("")
            .addClass("hide");
    };

    var fail = function() {
        alert("Failed to mark as read!");
    };

    var showPopover = function(notifications) {
        if (notifications.length === 0)
            return;

        $(".js-notifications-count")
            .text(notifications.length)
            .removeClass("hide")
            .addClass("animated bounceInDown");

        $(".notifications").popover({
            html: true,
            title: "Notifications",
            content: function() {
                const template = $("#notification-template").html();
                const compiled = _.template(template);
                return compiled({ notifications: notifications });
            },
            placement: "bottom",
            template:
                '<div class="popover popover-notifications" role="tooltip"><div class="arrow"></div><h3 class="popover-title"></h3><div class="popover-content"></div></div>'
        }).on("show.bs.popover",
            function() {
                $.post("/api/notifications/markAsRead")
                    .done(removeCountBadge)
                    .fail(fail);
            });
    };

    var getNew = function () {
        notificationService.getNew(showPopover);
    };

    return {
        getNew: getNew
    };

}(NotificationService);