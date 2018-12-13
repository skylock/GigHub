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
    var showErrorMessage = function (jqXhr) {
        alert(jqXhr.responseJSON);
    };

    var toggleAttendance = function (e) {
        var button = $(e.target);
        if (button.hasClass("btn-default")) {
            $.post("/api/attendances", { gigId: $(button).attr("data-gig-id") })
                .done(function () {
                    $(button)
                        .removeClass("btn-default")
                        .addClass("btn-info")
                        .text("Going");
                })
                .fail(showErrorMessage);
        } else {
            $.ajax({
                    url: "/api/attendances/" + button.attr("data-gig-id"),
                    method: "DELETE"
                })
                .done(function () {
                    button
                        .removeClass("btn-info")
                        .addClass("btn-default")
                        .text("Going?");
                })
                .fail(showErrorMessage);
        }
    };

    var init = function () {
        $(".js-toggle-attendance").click(toggleAttendance);
    };

    return {
        init: init
    };
}();
