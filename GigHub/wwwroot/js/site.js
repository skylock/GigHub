$(document).ready(function () {
    $.getJSON("/api/notifications", function (notifications) {
        $(".js-notifications-count")
            .text(notifications.length)
            .removeClass("hide")
            .addClass("animated bounceInDown");
    });

    $(".notifications").popover({
        html: true,
        title: "Notifications",
        content: function () {
            const compiled = _.template("Hello <%= name %>");
            const html = compiled({ name: "Mosh" });
            return html;
        },
        placement: "bottom"
    });
});
