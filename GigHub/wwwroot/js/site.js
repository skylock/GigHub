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
            return "Hello world !";
        },
        placement: bottom
    });
});
