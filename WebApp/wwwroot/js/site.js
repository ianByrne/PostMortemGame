function SendMessageToChatScript(message) {
    var response = {
        Message: "Failed to complete Ajax"
    };

    $.ajax({
        type: "POST",
        url: '/Index?handler=SendMessageToChatScript',
        async: false,
        data: message,
        accept: "application/json",
        beforeSend: function (xhr) {
            xhr.setRequestHeader("XSRF-TOKEN",
                $('input:hidden[name="__RequestVerificationToken"]').val());
        },
        success: function (ajaxResponse) {
            response = ajaxResponse;
        },
        error: function (ajaxResponse) {
            console.log("Error");
            console.log(ajaxResponse);
            response = ajaxResponse;
        }
    });

    return response;
}

function EnsureUserCreated(user) {
    $.ajax({
        type: "POST",
        url: '/Index?handler=EnsureUserCreated',
        async: false,
        data: user,
        accept: "application/json",
        beforeSend: function (xhr) {
            xhr.setRequestHeader("XSRF-TOKEN",
                $('input:hidden[name="__RequestVerificationToken"]').val());
        },
        success: function (ajaxResponse) {
            //
        },
        error: function (ajaxResponse) {
            console.log("Error");
            console.log(ajaxResponse);
        }
    });

    SetCookieId(user.CookieId);

    return true;
}

function DeleteCookie() {
    Cookies.remove("id");

    return true;
}

function GetUserFromCookieId() {
    var id = Cookies.get("id");

    if (id == undefined) {
        return null;
    }

    var response = null;

    $.ajax({
        type: "POST",
        url: '/Index?handler=GetUserFromCookieId',
        async: false,
        data: {
            id: id
        },
        accept: "application/json",
        beforeSend: function (xhr) {
            xhr.setRequestHeader("XSRF-TOKEN",
                $('input:hidden[name="__RequestVerificationToken"]').val());
        },
        success: function (ajaxResponse) {
            response = ajaxResponse;
        },
        error: function (ajaxResponse) {
            DeleteCookie();
        }
    });

    return response;
}

function SetCookieId(id) {
    Cookies.set("id", id, { sameSite: 'strict' });

    return true;
}

function SaveUser(user) {
    $.ajax({
        type: "POST",
        url: '/Index?handler=SaveUser',
        async: false,
        data: user,
        accept: "application/json",
        beforeSend: function (xhr) {
            xhr.setRequestHeader("XSRF-TOKEN",
                $('input:hidden[name="__RequestVerificationToken"]').val());
        },
        success: function (ajaxResponse) {
            //
        },
        error: function (ajaxResponse) {
            console.log("Error");
            console.log(ajaxResponse);
        }
    });

    return true;
}

// https://softdevpractice.com/blog/razor-pages-ajax-modals-with-validation/
$(document).ready(function () {
    $('button[data-toggle="ajax-modal"]').click(function (event) {
        $.ajax({
            type: "GET",
            url: '/Index?handler=SurveyModalPartial',
            async: false,
            success: function (data) {
                $(document.body).append(data).find('.modal').modal('show');
            },
            error: function (data) {
                console.log("Error");
                console.log(data);
            }
        });
    });
});
