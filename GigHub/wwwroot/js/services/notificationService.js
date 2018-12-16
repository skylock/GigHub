var NotificationService = function() {
    var getNew = function (onDone) {
        $.getJSON("/api/notifications/getNewNotifications", onDone);
    };

    return {
        getNew     : getNew
    };
}();