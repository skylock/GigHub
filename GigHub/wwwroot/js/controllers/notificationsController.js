﻿var Notifications = function (notificationService) {

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