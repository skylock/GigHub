$(document).ready(function () {
    $.getJSON("/api/notifications/getNewNotifications", function (notifications) {
        if (notifications.length === 0)
            return; 

        $(".js-notifications-count")
            .text(notifications.length)
            .removeClass("hide")
            .addClass("animated bounceInDown");

        $(".notifications").popover({
            html: true,
            title: "Notifications",
            content: function () {
                const template = $("#notification-template").html();
                const compiled = _.template(template);
                return compiled({ notifications: notifications });
            },
            placement: "bottom",
            template: '<div class="popover popover-notifications" role="tooltip"><div class="arrow"></div><h3 class="popover-title"></h3><div class="popover-content"></div></div>'
        }).on("show.bs.popover", function () {
            $.post("/api/notifications/markAsRead")
                .done(function() {
                    $(".js-notifications-count")
                        .text("")
                        .addClass("hide");
                });
        });
    });
});

var GigsController = function() {
    var button;

    var message = function (jqXhr) {
        alert(jqXhr.responseJSON);
    };

    var done = function () {
        var buttonText = (button.text() === "Going") ? "Going ?" : "Going";

        $(button)
            .toggleClass("btn-info")
            .toggleClass("btn-default")
            .text(buttonText);

    };

    var createAttendance = function() {
        $.post("/api/attendances", { gigId: $(button).attr("data-gig-id") })
            .done(done)
            .fail(message);

    };

    var deleteAttendance = function() {
        $.ajax({
                url: "/api/attendances/" + button.attr("data-gig-id"),
                method: "DELETE"
            })
            .done(done)
            .fail(message);
    };

    var toggleAttendance = function (e) {
        button = $(e.target);
        if (button.hasClass("btn-default")) 
            createAttendance();
        else 
            deleteAttendance();
    };

    var init = function () {
        $(".js-toggle-attendance").click(toggleAttendance);
    };

    return {
        init: init
    };
}();
