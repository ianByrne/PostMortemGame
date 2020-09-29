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

$(document).ready(function () {
    $('button[data-toggle="ajax-modal"]').click(function (event) {
        $.ajax({
            type: "GET",
            url: $(this).data('url'),
            async: false,
            success: function (data) {
                $("#modal-container").html(data);
                $("#modal-container").find('.modal').modal('show');
            },
            error: function (data) {
                console.log("Error");
                console.log(data);
            }
        });
    });

    $("#modal-container").on('click', '[data-save="modal"]', function (event) {
        event.preventDefault();

        var form = $(this).parents('.modal').find('form');
        var dataToSend = form.serialize();

        $.ajax({
            type: "POST",
            url: form.attr('action'),
            data: dataToSend,
            beforeSend: function (xhr) {
                xhr.setRequestHeader("XSRF-TOKEN",
                    $('input:hidden[name="__RequestVerificationToken"]').val());
            },
            success: function (data) {
                $("#modal-container").find('.modal-body').replaceWith($('.modal-body', data));

                var isValid = $("#modal-container").find('.modal-body').find('[name="IsValid"]').val() == 'True';

                if (isValid) {
                    $("#modal-container").find('.modal').modal('hide');
                }
            },
            error: function (data) {
                console.log("Error");
                console.log(ajaxResponse);
            }
        });
    });
});
