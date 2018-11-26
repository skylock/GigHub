$(document).ready(function () {
    $.getJSON("/api/notifications", function (notifications) {
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
            placement: "bottom"
        });
    });
});
