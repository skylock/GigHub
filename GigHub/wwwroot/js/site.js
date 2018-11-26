$(document).ready(function () {
    $.getJSON("/api/notifications", function (notifications) {
        $(".js-notifications-count")
            .text(notifications.length)
            .removeClass("hide")
            .addClass("animated bounceInDown");
    });
});
